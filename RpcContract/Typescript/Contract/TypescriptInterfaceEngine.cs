using Panosen.CodeDom;
using Panosen.CodeDom.Typescript;
using Panosen.CodeDom.Typescript.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.Typescript.Contract
{
    public class TypescriptInterfaceEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, TypescriptParam typescriptParam, InterfaceNode interfaceNode)
        {
            var codeFile = PrepareCodeFile(interfaceNode, assemblyName, typescriptParam);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, interfaceNode.FullName) + ".ts"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, string assemblyName, TypescriptParam typescriptParam)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddProjectImport("LoadingResponse", "./loading-response", notDefault: true);

            var codeInterface = codeFile.AddInterface(interfaceNode.Name);
            codeInterface.Summary = interfaceNode.Summary ?? interfaceNode.Name;
            codeInterface.Export = true;

            foreach (var methodNode in interfaceNode.MethodNodeList)
            {

                var codeMethod = codeInterface.AddMethod((methodNode.Name ?? string.Empty).ToLowerCamelCase());
                codeMethod.Summary = methodNode.Summary ?? methodNode.Name;

                var type = TypeHelper.ToPropertyType(interfaceNode.Namespace, methodNode.ReturnType, codeFile, assemblyName);
                codeMethod.Type = $"LoadingResponse<{type}>";

                if (methodNode.Parameters != null && methodNode.Parameters.Count > 0)
                {
                    foreach (var parameter in methodNode.Parameters)
                    {
                        codeMethod.AddParameter(TypeHelper.ToPropertyType(interfaceNode.Namespace, parameter.ParameterType, codeFile, assemblyName), parameter.Name);
                    }
                }
            }

            return codeFile;
        }
    }
}
