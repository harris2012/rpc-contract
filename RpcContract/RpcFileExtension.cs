using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract
{
    public static class RpcFileExtension
    {
        public static ProjectSettings ToProjectSettings(this RpcFile rpcFile)
        {
            ProjectSettings projectSettings = new ProjectSettings();
            projectSettings.CodeFirstSolutionName = rpcFile.ProjectName;
            projectSettings.Version = rpcFile.Version;

            projectSettings.CodeFirstProjectName = $"{rpcFile.ProjectName}.Contract";
            projectSettings.CodeFirstAssemblyName = $"{rpcFile.ProjectName}.Contract";
            projectSettings.CodeFirstSolutionGuid = rpcFile.CodeFirstSolutionGuid;
            projectSettings.CodeFirstProjectGuid = rpcFile.CodeFirstProjectGuid;

            if (rpcFile.AspNet != null)
            {
                projectSettings.AspnetCoreParam = new AspNetCoreParam();
                projectSettings.AspnetCoreParam.SolutionName = rpcFile.ProjectName;
                projectSettings.AspnetCoreParam.SolutionGuid = rpcFile.AspNet.SolutionGuid;

                projectSettings.AspnetCoreParam.Contract = new AspNetCoreProject();
                projectSettings.AspnetCoreParam.Contract.ProjectName = $"{rpcFile.ProjectName}.Contract";
                projectSettings.AspnetCoreParam.Contract.AssemblyName = $"{rpcFile.ProjectName}.Contract";
                projectSettings.AspnetCoreParam.Contract.RootNamespace = $"{rpcFile.ProjectName}.Contract";
                if (rpcFile.AspNet != null && rpcFile.AspNet.Contract != null)
                {
                    projectSettings.AspnetCoreParam.Contract.ProjectGuid = rpcFile.AspNet.Contract.ProjectGuid;
                }

                projectSettings.AspnetCoreParam.Client = new AspNetCoreProject();
                projectSettings.AspnetCoreParam.Client.ProjectName = $"{rpcFile.ProjectName}.Client";
                projectSettings.AspnetCoreParam.Client.AssemblyName = $"{rpcFile.ProjectName}.Client";
                projectSettings.AspnetCoreParam.Client.RootNamespace = $"{rpcFile.ProjectName}.Client";
                if (rpcFile.AspNet != null && rpcFile.AspNet.Client != null)
                {
                    projectSettings.AspnetCoreParam.Client.ProjectGuid = rpcFile.AspNet.Client.ProjectGuid;
                }
            }

            if (rpcFile.TsProject != null)
            {
                projectSettings.TypescriptParam = new TypescriptParam();
                projectSettings.TypescriptParam.ProjectName = $"{rpcFile.ProjectName.ToLowerCaseBreakLine()}-client";
                projectSettings.TypescriptParam.Namespace = $"{rpcFile.ProjectName}.Contract";
            }

            return projectSettings;
        }
    }
}
