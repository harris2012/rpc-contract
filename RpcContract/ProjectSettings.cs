using RpcContract.AspNetCore;

namespace RpcContract
{
    public class ProjectSettings
    {
        /// <summary>
        /// CodeFirst 解决方案名称
        /// </summary>
        public string CodeFirstSolutionName { get; set; }

        /// <summary>
        /// CodeFirst 项目名称
        /// </summary>
        public string CodeFirstProjectName { get; set; }

        /// <summary>
        /// CodeFirst 程序集名称 和 根命名空间
        /// </summary>
        public string CodeFirstAssemblyName { get; set; }

        /// <summary>
        /// CodeFirstSolutionGuid
        /// </summary>
        public string CodeFirstSolutionGuid { get; set; }

        /// <summary>
        /// CodeFirstProjectGuid
        /// </summary>
        public string CodeFirstProjectGuid { get; set; }

        /// <summary>
        /// 用于 AspNetCore 项目
        /// </summary>
        public AspNetCoreParam AspnetCoreParam { get; set; }
    }
}
