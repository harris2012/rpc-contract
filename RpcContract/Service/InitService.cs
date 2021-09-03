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
            projectSettings.CodeFirstSolutionGuid = Guid.NewGuid().ToString("D");
            projectSettings.CodeFirstProjectGuid = Guid.NewGuid().ToString("D");

            projectSettings.AspnetCoreParam = new AspNetCoreParam();
            projectSettings.AspnetCoreParam.Active = true;
            projectSettings.AspnetCoreParam.SolutionName = "SampleTargetSolution";
            projectSettings.AspnetCoreParam.ProjectName = "SampleTargetProject";
            projectSettings.AspnetCoreParam.AssemblyName = "SampleTargetAssembly";
            projectSettings.AspnetCoreParam.SolutionGuid = Guid.NewGuid().ToString("D");
            projectSettings.AspnetCoreParam.ProjectGuid = Guid.NewGuid().ToString("D");

            return projectSettings;
        }
    }
}
