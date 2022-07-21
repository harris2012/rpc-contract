using Panosen.CodeDom;
using Panosen.CodeDom.Typescript;
using Panosen.CodeDom.Typescript.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RpcContract.Typescript.Contract
{
    public class TypescriptClientEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, TypescriptParam typescriptParam, InterfaceNode interfaceNode)
        {
            var codeFile = PrepareCodeFile(interfaceNode, assemblyName, typescriptParam);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, interfaceNode.FullName) + "Client" + ".ts"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, string assemblyName, TypescriptParam typescriptParam)
        {
            CodeFile codeFile = new CodeFile();

            List<Type> importTypes = new List<Type>();

            codeFile.AddProjectImport("LoadingResponse", "./loading-response", notDefault: true);
            codeFile.AddProjectImport(interfaceNode.Name, $"./{interfaceNode.Name}", notDefault: true);
            codeFile.AddProjectImport("BaseAxiosClient", "./base-axios-client", notDefault: true);

            var codeClass = codeFile.AddClass(interfaceNode.Name + "Client");
            codeClass.Summary = interfaceNode.Summary ?? interfaceNode.Name;
            codeClass.Export = true;

            codeClass.SetBaseClass("BaseAxiosClient");
            codeClass.AddInterface(interfaceNode.Name);

            foreach (var methodNode in interfaceNode.MethodNodeList)
            {
                var codeMethod = codeClass.AddMethod((methodNode.Name ?? string.Empty).ToLowerCamelCase());
                codeMethod.Summary = methodNode.Summary ?? methodNode.Name;

                var type = TypeHelper.ToPropertyType(interfaceNode.Namespace, methodNode.ReturnType, importTypes, assemblyName);
                codeMethod.Type = $"LoadingResponse<{type}>";

                if (methodNode.Parameters != null && methodNode.Parameters.Count > 0)
                {
                    foreach (var parameter in methodNode.Parameters)
                    {
                        codeMethod.AddParameter(TypeHelper.ToPropertyType(interfaceNode.Namespace, parameter.ParameterType, importTypes, assemblyName), parameter.Name);
                    }
                }

                codeMethod.StepStatement($"return this.common('api/{methodNode.Name.ToLowerCaseBreakLine()}', {string.Join(", ", methodNode.Parameters.Select(v => v.Name))});");
            }

            TypeHelper.Add(codeFile, interfaceNode.InterfaceType, importTypes);

            return codeFile;
        }
    }
}
