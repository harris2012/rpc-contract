using Newtonsoft.Json;
using Panosen.Generation;
using RpcContract.AspNetCore;
using System;

namespace RpcContract.Service
{
    public class InitService
    {
        public void Generate(Package package)
        {
            package.Add("rpc.json", JsonConvert.SerializeObject(PrepareTemplate(), Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            }));
        }

        private static ProjectSettings PrepareTemplate()
        {
            ProjectSettings projectSettings = new ProjectSettings();
            projectSettings.CodeFirstSolutionName = "CodeFirstSolution";
            projectSettings.CodeFirstProjectName = "CodeFirstProject";
            projectSettings.CodeFirstAssemblyName = "CodeFirstAssembly";
            projectSettings.CodeFirstSolutionGuid = Guid.NewGuid().ToString("B").ToUpper();
            projectSettings.CodeFirstProjectGuid = Guid.NewGuid().ToString("B").ToUpper();
            projectSettings.Version = "1.0.0";

            projectSettings.AspnetCoreParam = new AspNetCoreParam();
            projectSettings.AspnetCoreParam.Active = true;
            projectSettings.AspnetCoreParam.SolutionName = "SampleTargetSolution";
            projectSettings.AspnetCoreParam.ProjectName = "SampleTargetProject";
            projectSettings.AspnetCoreParam.AssemblyName = "SampleTargetAssembly";
            projectSettings.AspnetCoreParam.SolutionGuid = Guid.NewGuid().ToString("B").ToUpper();
            projectSettings.AspnetCoreParam.ProjectGuid = Guid.NewGuid().ToString("B").ToUpper();

            return projectSettings;
        }
    }
}
