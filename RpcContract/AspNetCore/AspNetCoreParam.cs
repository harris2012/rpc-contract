using System;
using System.Collections.Generic;
using System.Text;

namespace RpcContract.AspNetCore
{
    public class AspNetCoreParam
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 程序集名称 和 根命名空间
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// SolutionGuid
        /// </summary>
        public string SolutionGuid { get; set; }

        /// <summary>
        /// ProjectGuid
        /// </summary>
        public string ProjectGuid { get; set; }
    }
}
