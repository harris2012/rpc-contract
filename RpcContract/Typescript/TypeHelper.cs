using Panosen.CodeDom.Typescript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RpcContract.Typescript
{
    public class TypeHelper
    {
        public static string ToPropertyType(string xNamespace, Type propertyType, List<Type> importTypes, string assemblyName)
        {
            switch (propertyType.Namespace + "." + propertyType.Name)
            {
                case "System.Collections.Generic.List`1":
                    {
                        //codeFile.AddSystemUsing("System.Collections.Generic");

                        return string.Format("Array<{0}>",
                            string.Join(", ", propertyType.GetGenericArguments().Select(v => ToPropertyType(xNamespace, v, importTypes, assemblyName))));
                    }

                default:
                    break;
            }

            switch (propertyType.FullName)
            {
                case "System.Byte":
                    return "byte";

                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Single":
                case "System.Double":
                    return "number";

                case "System.String":
                    return "string";

                case "System.Boolean":
                    return "boolean";

                case "System.DateTime":
                    return "DateTime";

                default:
                    break;
            }

            importTypes.Add(propertyType);

            return propertyType.Name;
        }

        public static void Add(CodeFile codeFile, List<Type> importTypes)
        {
            if (importTypes == null || importTypes.Count == 0)
            {
                return;
            }

            foreach (var importType in importTypes)
            {
                //TODO
                var source = "." + importType.FullName.Replace(".", "/");
                codeFile.AddProjectImport(importType.Name, source, notDefault: true);
            }
        }
    }
}
