using RpcContract.AspNetCore;

namespace RpcContract
{
    public class ProjectSettings
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// CodeFirst 解决方案名称
        /// </summary>
        public string CodeFirstSolutionName { get; set; }

        /// <summary>
        /// CodeFirst 项目名称
        /// </summary>
        public string CodeFirstProjectName { get; set; }

        /// <summary>
        /// CodeFirst 程序集名称
        /// </summary>
        public string CodeFirstAssemblyName { get; set; }

        /// <summary>
        /// CodeFirst 根命名空间
        /// </summary>
        public string CodeFirstRootNamespace { get; set; }

        /// <summary>
        /// CodeFirstSolutionGuid
        /// </summary>
        public string CodeFirstSolutionGuid { get; set; }

        /// <summary>
        /// CodeFirstProjectGuid
        /// </summary>
        public string CodeFirstProjectGuid { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 用于 AspNetCore 项目
        /// </summary>
        public AspNetCoreParam AspnetCoreParam { get; set; }
    }

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
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 契约项目
        /// </summary>
        public DotNetProject Contract { get; set; }

        /// <summary>
        /// 客户端项目
        /// </summary>
        public DotNetProject Client { get; set; }
    }

    public class DotNetProject
    {

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// ProjectGuid
        /// </summary>
        public string ProjectGuid { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 根命名空间
        /// </summary>
        public string RootNamespace { get; set; }
    }
}
