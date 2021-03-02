namespace FastEngine.Core
{
    /// <summary>
    /// 创建资源
    /// </summary>
    public class ResFactory
    {
        public static Res Create(ResData data)
        {
            switch (data.Type)
            {
                case ResType.Bundle:
                    return BundleRes.Allocate(data);
                case ResType.Asset:
                    return AssetRes.Allocate(data);
                default:
                    return null;
            }
        }

    }
}