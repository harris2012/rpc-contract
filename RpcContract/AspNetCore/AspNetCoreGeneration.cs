using System.IO;

using Panosen.Generation;
using Panosen.Reflection.Model;
using Panosen.Resource.CSharp;
using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;

using RpcContract.AspNetCore.Client;
using RpcContract.AspNetCore.Contract;

namespace RpcContract.AspNetCore
{
    public static class AspNetCoreGeneration
    {
        public static void Generate(Package package, string prefix, string assemblyName, string version, AspNetCoreParam aspNetCoreParam, AssemblyModel assemblyModel)
        {
            package.Add(Path.Combine(prefix, ".gitignore"), new CSharpResource().GetResource(CSharpResourceKeys.__gitignore));

            package.Add(Path.Combine(prefix, $"README.md"), ReadMeEngine.Generate());

            package.Add(Path.Combine(prefix, $"{aspNetCoreParam.SolutionName}.sln"), PrepareSolution(aspNetCoreParam));

            GenerateClient(package, prefix, assemblyName, version, aspNetCoreParam, assemblyModel);

            GenerateContract(package, prefix, assemblyName, version, aspNetCoreParam, assemblyModel);
        }

        private static void GenerateContract(Package package, string prefix, string assemblyName, string version, AspNetCoreParam aspNetCoreParam, AssemblyModel assemblyModel)
        {
            var projectFolder = Path.Combine(prefix, aspNetCoreParam.Contract.ProjectName);

            package.Add(Path.Combine(projectFolder, $"{aspNetCoreParam.Contract.ProjectName}.csproj"), ContractEngine.PrepareContractProject(aspNetCoreParam.Contract, version));

            foreach (var interfaceNode in assemblyModel.InterfaceNodeList)
            {
                new AspnetInterfaceEngine().Generate(package, projectFolder, assemblyName, interfaceNode, aspNetCoreParam.Contract.AssemblyName);
            }

            foreach (var classNode in assemblyModel.ClassNodeList)
            {
                new AspnetCoreClassEngine().Generate(package, projectFolder, assemblyName, classNode, aspNetCoreParam.Contract.AssemblyName);
            }
        }

        private static void GenerateClient(Package package, string prefix, string assemblyName, string version, AspNetCoreParam aspNetCoreParam, AssemblyModel assemblyModel)
        {
            var projectFolder = Path.Combine(prefix, aspNetCoreParam.Client.ProjectName);

            package.Add(Path.Combine(projectFolder, $"{aspNetCoreParam.Client.ProjectName}.csproj"), ClientProjectEngine.PrepareClientProject(aspNetCoreParam.Client, version, aspNetCoreParam.Contract));

            package.Add(Path.Combine(projectFolder, $"ServiceClientBase.cs"), ServiceClientBaseEngine.Generate(aspNetCoreParam.Client.RootNamespace));

            package.Add(Path.Combine(projectFolder, $"IServiceCollectionExtension.cs"), IServiceCollectionExtensionEngine.Generate(aspNetCoreParam.Client.RootNamespace, assemblyModel.InterfaceNodeList));

            foreach (var interfaceNode in assemblyModel.InterfaceNodeList)
            {
                new AspnetClientEngine().Generate(package, projectFolder, assemblyName, aspNetCoreParam, interfaceNode);
            }
        }

        private static string PrepareSolution(AspNetCoreParam aspNetCoreParam)
        {
            var solution = new Solution();
            solution.SolutionGuid = aspNetCoreParam.SolutionGuid;

            if (aspNetCoreParam.Contract != null)
            {
                var project = solution.AddProject();
                project.ProjectName = aspNetCoreParam.Contract.ProjectName;
                project.ProjectGuid = aspNetCoreParam.Contract.ProjectGuid;
                project.ProjectTypeGuid = ProjectTypeGuids.CSharpLibrarySDK;
                project.ProjectPath = $"{aspNetCoreParam.Contract.ProjectName}\\{aspNetCoreParam.Contract.ProjectName}.csproj";
            }

            if (aspNetCoreParam.Client != null)
            {
                var project = solution.AddProject();
                project.ProjectName = aspNetCoreParam.Client.ProjectName;
                project.ProjectGuid = aspNetCoreParam.Client.ProjectGuid;
                project.ProjectTypeGuid = ProjectTypeGuids.CSharpLibrarySDK;
                project.ProjectPath = $"{aspNetCoreParam.Client.ProjectName}\\{aspNetCoreParam.Client.ProjectName}.csproj";
            }

            return solution.TransformText();
        }
    }
}
