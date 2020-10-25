namespace FastEngine.Core
{
    /// <summary>
    /// 创建资源
    /// </summary>
    public class ResFactory
    {
        public static Res Create(ResData data)
        {
            switch (data.type)
            {
                case ResType.Bundle:
                    return null;
                default:
                    return null;
            }
        }

    }
}