using Panosen.Generation;
using Panosen.Reflection.Model;
using Panosen.Resource.Npm;
using RpcContract.Typescript.Contract;
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
        public static void Generate(Package package, string prefix, ProjectSettings projectSettings, AssemblyModel assemblyModel)
        {
            var assemblyName = projectSettings.CodeFirstAssemblyName;
            var version = projectSettings.Version;
            var typescriptParam = projectSettings.TypescriptParam;

            package.Add(Path.Combine(prefix, ".gitignore"), new NpmResource().GetResource(NpmResourceKeys.__gitignore));

            package.Add(Path.Combine(prefix, $"README.md"), ReadMeEngine.Generate());

            package.Add(Path.Combine(prefix, "package.json"), new PackageEngine().Generate(typescriptParam.ProjectName, version));

            package.Add(Path.Combine(prefix, "tsconfig.json"), new TsConfigEngine().Generate());

            //src
            {
                var srcFolder = Path.Combine(prefix, "src");

                package.Add(Path.Combine(srcFolder, "ref.ts"), new RefEngine().Generate());

                package.Add(Path.Combine(srcFolder, "loading-response.ts"), new LoadingResponseEngine().Generate());

                foreach (var interfaceNode in assemblyModel.InterfaceNodeList)
                {
                    new TypescriptInterfaceEngine().Generate(package, srcFolder, assemblyName, typescriptParam, interfaceNode);
                }

                foreach (var classNode in assemblyModel.ClassNodeList)
                {
                    new TypescriptClassEngine().Generate(package, srcFolder, assemblyName, classNode, "aaa123");
                }
            }
        }
    }
}
