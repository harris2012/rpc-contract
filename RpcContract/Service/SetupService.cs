using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;
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

            package.Add(Path.Combine(folder, "update.bat"), new UpdateBatEngine().TransformText(), encoding: Encoding.ASCII);

            var codeFirstFolder = Path.Combine(folder, "CodeFirst");

            CSharpResource cSharpResource = new CSharpResource();

            package.Add(Path.Combine(codeFirstFolder, ".gitignore"), cSharpResource.GetResource(CSharpResourceKeys.__gitignore));

            package.Add(Path.Combine(codeFirstFolder, $"{projectSettings.CodeFirstProjectName}.sln"), PrepareSolution(projectSettings));

            {
                var projectFolder = Path.Combine(codeFirstFolder, projectSettings.CodeFirstProjectName);

                package.Add(Path.Combine(projectFolder, $"{projectSettings.CodeFirstProjectName}.csproj"), PrepareProject(projectSettings));

                package.Add(Path.Combine(projectFolder, "ISampleService.cs"), new CodeFirstSampleServiceEngine
                {
                    RootNamespace = projectSettings.CodeFirstAssemblyName
                }.TransformText());
            }

        }

        public string PrepareSolution(ProjectSettings projectSettings)
        {
            var solution = new Solution();
            solution.SolutionGuid = projectSettings.CodeFirstSolutionGuid;

            var project = solution.AddProject();
            project.ProjectName = projectSettings.CodeFirstProjectName;
            project.ProjectGuid = projectSettings.CodeFirstProjectGuid;
            project.ProjectTypeGuid = ProjectTypeGuids.CSharpLibrarySDK;
            project.ProjectPath = $"{projectSettings.ProjectName}\\{projectSettings.ProjectName}.csproj";

            return solution.TransformText();
        }

        private string PrepareProject(ProjectSettings projectSettings)
        {
            var project = new Project();

            project.Sdk = "Microsoft.NET.Sdk";

            project.AddTargetFramework("netstandard2.0");
            project.AssemblyName = projectSettings.CodeFirstAssemblyName;
            project.RootNamespace = projectSettings.CodeFirstRootNamespace;
            project.WithDocumentationFile = true;

            return project.TransformText();
        }
    }
}
