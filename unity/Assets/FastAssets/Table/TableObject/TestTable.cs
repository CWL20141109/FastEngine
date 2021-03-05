// FastEngine
// excel2table auto generate

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastEngine;
using FastEngine.Core;
using FastEngine.Core.Excel2Table;

namespace Table
{
    public class TestTableData
    {
		//id
		public byte id;
		//名字
		public int name;
		//属性1
		public long property1;
		//属性2
		public float property2;
		//属性3
		public double property3;
		//属性4
		public string property4;
		//属性5
		public bool property5;
		//属性6
		public Vector2 property6;
		//属性7
		public Vector3 property7;
		//属性8
		private I18NObject _property8_I18N;
		public string property8 { get { return _property8_I18N.ToString(); } }
		//属性9
		public byte[] property9;
		//属性10
		public int[] property10;
		//属性11
		public long[] property11;
		//属性12
		public float[] property12;
		//属性13
		public double[] property13;
		//属性14
		public string[] property14;
		//属性15
		public bool[] property15;
		//属性16
		public Vector2[] property16;
		//属性17
		public Vector3[] property17;
		//属性18
		public I18NObject[] property18;

    }

    public class TestTable : Singleton<TestTable>, ITableObject
    {
        public string tableName { get { return "Test"; } }
        public int maxCount { get { return m_tableDatas.Length; } }
        
        public DataFormatOptions dataFormatOptions = DataFormatOptions.IntDictionary;
        private TestTableData[] m_tableDatas;
        private Dictionary<int, TestTableData> m_tableDataIntDictionary;
        private Dictionary<string, TestTableData> m_tableDataStringDictionary;
        private Dictionary<int, Dictionary<int, TestTableData>> m_tableDataInt2IntDictionary;

        public override void InitializeSingleton()
        {
            switch (dataFormatOptions)
            {
                case DataFormatOptions.Array:
                    m_tableDatas = new TableParseCSV<TestTableData>(tableName).ParseArray();
                    break;
                case DataFormatOptions.IntDictionary:
                    m_tableDataIntDictionary = new TableParseCSV<TestTableData>(tableName).ParseIntDictionary();
                    break;
                case DataFormatOptions.StringDictionary:
                    m_tableDataStringDictionary = new TableParseCSV<TestTableData>(tableName).ParseStringDictionary();
                    break;
                case DataFormatOptions.Int2IntDictionary:
                    m_tableDataInt2IntDictionary = new TableParseCSV<TestTableData>(tableName).ParseInt2IntDictionary();
                    break;
            }
        }

        private TestTableData _GetIndexData(int index)
        {
            if (dataFormatOptions == DataFormatOptions.Array)
            {
                if (index >= 0 && index < m_tableDatas.Length)
                {
                    return m_tableDatas[index];
                }
            }
            else Debug.LogError("[TestTable] DataFormatOptions: Array. Please use the GetKeyData(index)");
            return null;
        }

        private TestTableData _GetKeyData(int key)
        {
            if (dataFormatOptions == DataFormatOptions.IntDictionary)
            {
                TestTableData data = null;
                if (m_tableDataIntDictionary.TryGetValue(key, out data))
                {
                    return data;
                }
            }
            else Debug.LogError("[TestTable] DataFormatOptions: IntDictionary. Please use the GetKeyData(int-key)");
            return null;
        }

        private TestTableData _GetKeyData(string key)
        {
            if (dataFormatOptions == DataFormatOptions.StringDictionary)
            {
                TestTableData data = null;
                if (m_tableDataStringDictionary.TryGetValue(key, out data))
                {
                    return data;
                }
            }
            else Debug.LogError("[TestTable] DataFormatOptions: StringDictionary. Please use the GetKeyData(string-key)");
            return null;
        }

        private TestTableData _GetKeyData(int key1, int key2)
        {
            if (dataFormatOptions == DataFormatOptions.Int2IntDictionary)
            {
                Dictionary<int, TestTableData> dictionary = null;
                if (m_tableDataInt2IntDictionary.TryGetValue(key1, out dictionary))
                {
                    TestTableData data = null;
                    if (dictionary.TryGetValue(key2, out data))
                    {
                        return data;
                    }
                }
            }
            else Debug.LogError("[TestTable] DataFormatOptions: Int2IntDictionary. Please use the GetKeyData(int-key, int-key)");
            return null;
        }

        public static TestTableData GetIndexData(int index) { return instance._GetIndexData(index); }
        public static TestTableData GetKeyData(int key) { return instance._GetKeyData(key); }
        public static TestTableData GetKeyData(string key) { return instance._GetKeyData(key); }
        public static TestTableData GetKeyData(int key1, int key2) { return instance._GetKeyData(key1, key2); }
    }
}