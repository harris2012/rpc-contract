namespace RpcContract
{
    public class AspNetCoreParam
    {
        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// SolutionGuid
        /// </summary>
        public string SolutionGuid { get; set; }

        /// <summary>
        /// 契约项目
        /// </summary>
        public AspNetCoreProject Contract { get; set; }

        /// <summary>
        /// 客户端项目
        /// </summary>
        public AspNetCoreProject Client { get; set; }
    }
}
