using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RpcContract.AspNetCore
{
    public class AspnetCoreClassEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, AspNetCoreParam aspNetCoreParam, ClassNode classNode)
        {
            var codeFile = PrepareCodeFile(classNode, assemblyName, aspNetCoreParam);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, classNode.FullName) + ".cs"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(ClassNode classNode, string assemblyName, AspNetCoreParam aspNetCoreParam)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");

            codeFile.AddNugetUsing("Newtonsoft.Json");

            var codeNamespace = codeFile.AddNamespace(classNode.Namespace.Replace(assemblyName, aspNetCoreParam.AssemblyName));

            var codeClass = codeNamespace.AddClass(classNode.Name);
            codeClass.AccessModifiers = AccessModifiers.Public;
            codeClass.Summary = classNode.Summary ?? classNode.Name;

            if (classNode.BaseTypeName != null)
            {
                codeClass.SetBaseClass(classNode.BaseTypeName);
            }

            if (classNode.PropertyNodeList != null && classNode.PropertyNodeList.Count > 0)
            {
                foreach (var propertyNode in classNode.PropertyNodeList)
                {
                    var codeProperty = codeClass.AddProperty(TypeHelper.ToPropertyType(classNode.Namespace, propertyNode.PropertyType, codeFile, assemblyName, aspNetCoreParam), propertyNode.Name);
                    codeProperty.AddAttribute("JsonProperty").AddStringParam(propertyNode.Name.ToLowerCamelCase());
                    codeProperty.SetSummary(propertyNode.Summary ?? propertyNode.Name);
                }
            }

            return codeFile;
        }
    }
}
