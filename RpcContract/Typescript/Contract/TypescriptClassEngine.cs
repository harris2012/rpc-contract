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
    public class TypescriptClassEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, ClassNode classNode, string targetAssemblyName)
        {
            var codeFile = PrepareCodeFile(classNode, assemblyName, targetAssemblyName);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, classNode.FullName) + ".cs"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(ClassNode classNode, string assemblyName, string targetAssemblyName)
        {
            CodeFile codeFile = new CodeFile();

            var codeNamespace = codeFile.AddNamespace(classNode.Namespace.Replace(assemblyName, targetAssemblyName));

            var codeInterface = codeNamespace.AddInterface(classNode.Name);
            codeInterface.Summary = classNode.Summary ?? classNode.Name;

            if (classNode.PropertyNodeList != null && classNode.PropertyNodeList.Count > 0)
            {
                foreach (var propertyNode in classNode.PropertyNodeList)
                {
                    var type = TypeHelper.ToPropertyType(classNode.Namespace, propertyNode.PropertyType, codeFile, assemblyName, targetAssemblyName);

                    var codeProperty = codeInterface.AddField(type, propertyNode.Name.ToLowerCamelCase());

                    codeProperty.SetSummary(propertyNode.Summary ?? propertyNode.Name);
                }
            }

            return codeFile;
        }
    }
}
