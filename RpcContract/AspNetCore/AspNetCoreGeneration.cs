using Panosen.Generation;
using Panosen.Reflection.Model;
using System.IO;

namespace RpcContract.AspNetCore
{
    class AspNetCoreGeneration
    {
        public void Generate(Package package, string prefix, string assemblyName, AspNetCoreParam aspNetCoreParam, AssemblyModel assemblyModel)
        {
            package.Add(Path.Combine(prefix, $"{aspNetCoreParam.SolutionName}.sln"), new AspNetCoreSolutionEngine
            {
                ProjectName = aspNetCoreParam.ProjectName,
                SolutionGuid = aspNetCoreParam.SolutionGuid,
                ProjectGuid = aspNetCoreParam.ProjectGuid
            }.TransformText());

            {
                var projectFolder = Path.Combine(prefix, aspNetCoreParam.ProjectName);

                package.Add(Path.Combine(projectFolder, $"{aspNetCoreParam.ProjectName}.csproj"), new AspNetCoreProjectEngine
                {
                    RootNamespace = aspNetCoreParam.AssemblyName,
                    AssemblyName = aspNetCoreParam.AssemblyName
                }.TransformText());

                foreach (var interfaceNode in assemblyModel.InterfaceNodeList)
                {
                    new AspnetInterfaceEngine().Generate(package, projectFolder, assemblyName, aspNetCoreParam, interfaceNode);
                }

                foreach (var classNode in assemblyModel.ClassNodeList)
                {
                    new AspnetCoreClassEngine().Generate(package, projectFolder, assemblyName, aspNetCoreParam, classNode);
                }
            }
        }
    }
}
