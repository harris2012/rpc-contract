using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract
{
    public class RpcFile
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// CodeFirstSolutionGuid
        /// </summary>
        public string CodeFirstSolutionGuid { get; set; }

        /// <summary>
        /// CodeFirstProjectGuid
        /// </summary>
        public string CodeFirstProjectGuid { get; set; }

        /// <summary>
        /// AspNet
        /// </summary>
        public RpcAspNet AspNet { get; set; }

        /// <summary>
        /// Typescript Project
        /// </summary>
        public TsProject TsProject { get; set; }
    }

    public class RpcAspNet
    {
        /// <summary>
        /// 解决方案GUID
        /// </summary>
        public string SolutionGuid { get; set; }

        /// <summary>
        /// Contract
        /// </summary>
        public RpcAspNetProject Contract { get; set; }

        /// <summary>
        /// Client
        /// </summary>
        public RpcAspNetProject Client { get; set; }
    }

    public class RpcAspNetProject
    {
        /// <summary>
        /// 项目GUID
        /// </summary>
        public string ProjectGuid { get; set; }
    }

    public class TsProject
    {
    }
}
