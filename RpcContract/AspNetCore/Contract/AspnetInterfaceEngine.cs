using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.AspNetCore.Contract
{
    public class AspnetInterfaceEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, InterfaceNode interfaceNode, string targetAssemblyName)
        {
            var codeFile = PrepareCodeFile(interfaceNode, assemblyName, targetAssemblyName);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, interfaceNode.FullName) + ".cs"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, string assemblyName, string targetAssemblyName)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Threading.Tasks");

            var codeNamespace = codeFile.AddNamespace(interfaceNode.Namespace.Replace(assemblyName, targetAssemblyName));

            var codeInterface = codeNamespace.AddInterface(interfaceNode.Name);
            codeInterface.AccessModifiers = AccessModifiers.Public;
            codeInterface.Summary = interfaceNode.Summary ?? interfaceNode.Name;

            foreach (var methodNode in interfaceNode.MethodNodeList)
            {
                var codeMethod = codeInterface.AddMethod($"{methodNode.Name}Async");
                codeMethod.Summary = methodNode.Summary ?? methodNode.Name;

                var type = TypeHelper.ToPropertyType(interfaceNode.Namespace, methodNode.ReturnType, codeFile, assemblyName, targetAssemblyName);
                codeMethod.Type = $"Task<{type}>";

                if (methodNode.Parameters != null && methodNode.Parameters.Count > 0)
                {
                    foreach (var parameter in methodNode.Parameters)
                    {
                        codeMethod.AddParameter(TypeHelper.ToPropertyType(interfaceNode.Namespace, parameter.ParameterType, codeFile, assemblyName, targetAssemblyName), parameter.Name);
                    }
                }
            }

            return codeFile;
        }
    }
}
