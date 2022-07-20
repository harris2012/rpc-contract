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
        public void Generate(Package package, string prefix, string assemblyName, ClassNode classNode)
        {
            var codeFile = PrepareCodeFile(classNode, assemblyName);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, classNode.FullName) + ".ts"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(ClassNode classNode, string assemblyName)
        {
            CodeFile codeFile = new CodeFile();

            var codeInterface = codeFile.AddInterface(classNode.Name);
            codeInterface.Summary = classNode.Summary ?? classNode.Name;
            codeInterface.Export = true;

            if (classNode.PropertyNodeList != null && classNode.PropertyNodeList.Count > 0)
            {
                foreach (var propertyNode in classNode.PropertyNodeList)
                {
                    var type = TypeHelper.ToPropertyType(classNode.Namespace, propertyNode.PropertyType, codeFile, assemblyName);

                    var codeProperty = codeInterface.AddField(type, propertyNode.Name.ToLowerCamelCase());

                    codeProperty.SetSummary(propertyNode.Summary ?? propertyNode.Name);
                }
            }

            return codeFile;
        }
    }
}
