using Panosen.Resource.CSharp;
using RpcContract.CodeFirst;
using System;
using System.IO;
using System.Text;

namespace RpcContract.Service
{
    public class SetupService
    {
        public void Process(Panosen.Generation.Package package, ProjectSettings projectSettings)
        {
            var folder = Environment.CurrentDirectory;

            var codeFirstFolder = Path.Combine(folder, "CodeFirst");

            CSharpResource cSharpResource = new CSharpResource();

            package.Add(Path.Combine(codeFirstFolder, ".gitignore"), cSharpResource.GetResource(CSharpResourceKeys.__gitignore));

            package.Add(Path.Combine(folder, "update.bat"), new UpdateBatEngine().TransformText(), encoding: Encoding.ASCII);

            package.Add(Path.Combine(codeFirstFolder, $"{projectSettings.CodeFirstProjectName}.sln"), new CodeFirstSolutionEngine
            {
                ProjectName = projectSettings.CodeFirstProjectName,
                SolutionGuid = projectSettings.CodeFirstSolutionGuid,
                ProjectGuid = projectSettings.CodeFirstProjectGuid
            }.TransformText());

            {
                var projectFolder = Path.Combine(codeFirstFolder, projectSettings.CodeFirstProjectName);

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
