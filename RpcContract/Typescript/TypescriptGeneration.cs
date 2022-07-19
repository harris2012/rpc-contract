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

            new TypescriptClientEngine().Generate(package, prefix, "src");
        }
    }
}
