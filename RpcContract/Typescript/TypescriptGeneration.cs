using Panosen.Generation;
using Panosen.Resource.Npm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript
{
    public class TypescriptGeneration
    {
        public static void Generate(Package package, string prefix, string projectName, string version)
        {
            package.Add(Path.Combine(prefix, ".gitignore"), new NpmResource().GetResource(NpmResourceKeys.__gitignore));

            package.Add(Path.Combine(prefix, $"README.md"), ReadMeEngine.Generate());

            package.Add(Path.Combine(prefix, "package.json"), new PackageEngine().Generate(projectName, version));

            package.Add(Path.Combine(prefix, "tsconfig.json"), new TsConfigEngine().Generate());

            GenerateContract(package, prefix);
        }

        private static void GenerateContract(Package package, string prefix, string assemblyName, string version, AspNetCoreParam aspNetCoreParam, AssemblyModel assemblyModel)
        {
            var projectFolder = Path.Combine(prefix, "src");

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
    }
}
