/*
* @Author: cwl
* @Description:
* @Date: 2021-03-03 14:16:47
*/

using System.Text;
namespace FastEngine.Core.Excel2Table
{
    public class Excel2TableObject : Excel2Any
    {
        private string _template = @"// FastEngine
// excel2table auto generate

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastEngine;
using FastEngine.Core;
using FastEngine.Core.Excel2Table;

namespace $namespace$
{
    public class $tableName$TableData
    {
$variable$
    }

    public class $tableName$Table : Singleton<$tableName$Table>, ITableObject
    {
        public string TableName { get { return $tableNameStr$; } }
        public int maxCount { get { return m_tableDatas.Length; } }
        
        public DataFormatOptions dataFormatOptions = DataFormatOptions.$dataFormat$;
        private $tableName$TableData[] m_tableDatas;
        private Dictionary<int, $tableName$TableData> m_tableDataIntDictionary;
        private Dictionary<string, $tableName$TableData> m_tableDataStringDictionary;
        private Dictionary<int, Dictionary<int, $tableName$TableData>> m_tableDataInt2IntDictionary;

        public override void InitializeSingleton()
        {
            switch (dataFormatOptions)
            {
                case DataFormatOptions.Array:
                   // m_tableDatas = new TableParseCSV(TableName).ParseArray<$tableName$TableData>();
                    break;
                case DataFormatOptions.IntDictionary:
                   // m_tableDataIntDictionary = new TableParseCSV(TableName).ParseIntDictionary<$tableName$TableData>();
                    break;
                case DataFormatOptions.StringDictionary:
                   // m_tableDataStringDictionary = new TableParseCSV(TableName).ParseStringDictionary<$tableName$TableData>();
                    break;
                case DataFormatOptions.Int2IntDictionary:
                   // m_tableDataInt2IntDictionary = new TableParseCSV(TableName).ParseInt2IntDictionary<$tableName$TableData>();
                    break;
            }
        }

        private $tableName$TableData _GetIndexData(int index)
        {
            if (dataFormatOptions == DataFormatOptions.Array)
            {
                if (index >= 0 && index < m_tableDatas.Length)
                {
                    return m_tableDatas[index];
                }
            }
            else $GetIndexDataError$
            return null;
        }

        private $tableName$TableData _GetKeyData(int key)
        {
            if (dataFormatOptions == DataFormatOptions.IntDictionary)
            {
                $tableName$TableData data = null;
                if (m_tableDataIntDictionary.TryGetValue(key, out data))
                {
                    return data;
                }
            }
            else $GetIntKeyDataError$
            return null;
        }

        private $tableName$TableData _GetKeyData(string key)
        {
            if (dataFormatOptions == DataFormatOptions.StringDictionary)
            {
                $tableName$TableData data = null;
                if (m_tableDataStringDictionary.TryGetValue(key, out data))
                {
                    return data;
                }
            }
            else $GetStringKeyDataError$
            return null;
        }

        private $tableName$TableData _GetKeyData(int key1, int key2)
        {
            if (dataFormatOptions == DataFormatOptions.Int2IntDictionary)
            {
                Dictionary<int, $tableName$TableData> dictionary = null;
                if (m_tableDataInt2IntDictionary.TryGetValue(key1, out dictionary))
                {
                    $tableName$TableData data = null;
                    if (dictionary.TryGetValue(key2, out data))
                    {
                        return data;
                    }
                }
            }
            else $GetInt2IntKeyDataError$
            return null;
        }

        public static $tableName$TableData GetIndexData(int index) { return Instance._GetIndexData(index); }
        public static $tableName$TableData GetKeyData(int key) { return Instance._GetKeyData(key); }
        public static $tableName$TableData GetKeyData(string key) { return Instance._GetKeyData(key); }
        public static $tableName$TableData GetKeyData(int key1, int key2) { return Instance._GetKeyData(key1, key2); }
    }
}";

        private StringBuilder _mStringBuilder = new StringBuilder();
        public Excel2TableObject(ExcelReader reader) : base(reader)
        {
            if (string.IsNullOrEmpty(reader.Options.TableModelNamespace))
            {
                reader.Options.TableName = "Table";
            }

            _template = _template.Replace("$namespace$", reader.Options.TableModelNamespace);
            _template = _template.Replace("$tableName$", reader.Options.TableName);
            _template = _template.Replace("$tableNameStr$", $"\"{reader.Options.TableName}\"");
            _template = _template.Replace("$dataFormat$", reader.Options.DataFormatOptions.ToString());
            _template = _template.Replace("$GetIndexDataError$", $"Debug.LogError(\"[{reader.Options.TableName}Table] DataFormatOptions: {DataFormatOptions.Array.ToString()}. Please use the GetKeyData(index)\");");
            _template = _template.Replace("$GetIntKeyDataError$", $"Debug.LogError(\"[{reader.Options.TableName}Table] DataFormatOptions: {DataFormatOptions.IntDictionary.ToString()}. Please use the GetKeyData(int-key)\");");
            _template = _template.Replace("$GetStringKeyDataError$", $"Debug.LogError(\"[{reader.Options.TableName}Table] DataFormatOptions: {DataFormatOptions.StringDictionary.ToString()}. Please use the GetKeyData(string-key)\");");
            _template = _template.Replace("$GetInt2IntKeyDataError$", $"Debug.LogError(\"[{reader.Options.TableName}Table] DataFormatOptions: {DataFormatOptions.Int2IntDictionary.ToString()}. Please use the GetKeyData(int-key, int-key)\");");

            _mStringBuilder.Clear();
            for (int i = 0; i < reader.Fields.Count; i++)
            {
                _mStringBuilder.AppendLine($"\t\t//{reader.Descriptions[i]}");

                if (reader.Types[i] == FieldType.I18N)
                {
                    _mStringBuilder.AppendLine($"\t\tprivate {TypeUtils.FieldTypeToTypeContent(reader.Types[i])} _{reader.Fields[i]};");
                    _mStringBuilder.AppendLine($"\t\tpublic string {reader.Fields[i]} {{ get {{ return _{reader.Fields[i]}.ToString(); }} }}");
                }
                else
                {
                    _mStringBuilder.AppendLine($"\t\tpublic {TypeUtils.FieldTypeToTypeContent(reader.Types[i])} {reader.Fields[i]} {{ get; }}");
                }
            }
            _template = _template.Replace("$variable$", _mStringBuilder.ToString());
            FilePathUtils.FileWriteAllText(reader.Options.TableModelOutFilePath, _template);
        }
    }
}