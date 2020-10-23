namespace FastEngine.Core
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResState
    {
        // 未加载
        Waiting,
        // 正在加载
        Loading,
        // 加载失败
        Failed,
        // 已加载
        Ready,
    }
}