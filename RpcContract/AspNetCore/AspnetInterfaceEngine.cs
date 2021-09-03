using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.AspNetCore
{
    public class AspnetInterfaceEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, AspNetCoreParam context, InterfaceNode interfaceNode)
        {
            var codeFile = PrepareCodeFile(interfaceNode, context);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, interfaceNode.FullName) + ".cs"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, AspNetCoreParam context)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");

            var codeNamespace = codeFile.AddNamespace(context.AssemblyName);

            var codeInterface = codeNamespace.AddInterface(interfaceNode.Name);
            codeInterface.AccessModifiers = AccessModifiers.Public;
            codeInterface.Summary = interfaceNode.Summary ?? interfaceNode.Name;

            foreach (var methodNode in interfaceNode.MethodNodeList)
            {
                var codeMethod = codeInterface.AddMethod(methodNode.Name);
                codeMethod.Summary = methodNode.Summary ?? methodNode.Name;

                codeMethod.Type = TypeHelper.ToPropertyType(methodNode.ReturnType, codeFile);

                if (methodNode.Parameters != null && methodNode.Parameters.Count > 0)
                {
                    foreach (var parameter in methodNode.Parameters)
                    {
                        codeMethod.AddParameter(TypeHelper.ToPropertyType(parameter.ParameterType, codeFile), parameter.Name);
                    }
                }
            }

            return codeFile;
        }
    }
}
