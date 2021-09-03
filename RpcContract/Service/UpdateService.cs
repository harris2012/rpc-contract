using Panosen.Resource.CSharp;
using RpcContract.CodeFirst;
using System;
using System.IO;
using System.Text;

namespace RpcContract.Service
{
    public class UpdateService
    {
        public void Process(Panosen.Generation.Package package, ProjectSettings projectSettings)
        {
            var folder = Environment.CurrentDirectory;

            var aspNetCoreFolder = Path.Combine(folder, "AspNetCore");

            CSharpResource cSharpResource = new CSharpResource();

            package.Add(Path.Combine(aspNetCoreFolder, ".gitignore"), cSharpResource.GetResource(CSharpResourceKeys.__gitignore));

            package.Add(Path.Combine(aspNetCoreFolder, $"{projectSettings.CodeFirstProjectName}.sln"), new CodeFirstSolutionEngine
            {
                ProjectName = projectSettings.CodeFirstProjectName,
                SolutionGuid = projectSettings.CodeFirstSolutionGuid,
                ProjectGuid = projectSettings.CodeFirstProjectGuid
            }.TransformText());

            package.Add(Path.Combine(folder, "update.bat"), new UpdateBatEngine().TransformText(), encoding: Encoding.ASCII);

            {
                var projectFolder = Path.Combine(aspNetCoreFolder, projectSettings.CodeFirstProjectName);

                package.Add(Path.Combine(projectFolder, $"{projectSettings.CodeFirstProjectName}.csproj"), new CodeFirstProjectEngine
                {
                    RootNamespace = projectSettings.CodeFirstAssemblyName,
                    AssemblyName = projectSettings.CodeFirstAssemblyName
                }.TransformText());

                package.Add(Path.Combine(projectFolder, "ISampleService.cs"), new CodeFirstSampleServiceEngine
                {
                    RootNamespace = projectSettings.CodeFirstAssemblyName
                }.TransformText());
            }

        }
    }
}
