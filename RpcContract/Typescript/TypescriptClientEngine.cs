using Panosen.CodeDom;
using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.Typescript
{
    public class TypescriptClientEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, AspNetCoreParam aspNetCoreParam, InterfaceNode interfaceNode)
        {
            var codeFile = PrepareCodeFile(interfaceNode, assemblyName, aspNetCoreParam);

            package.Add(Path.Combine(prefix, "src", "index.ts"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, string assemblyName, AspNetCoreParam aspNetCoreParam)
        {
         

            return codeFile;
        }
    }
}
