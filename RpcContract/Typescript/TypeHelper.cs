using Panosen.CodeDom.Typescript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RpcContract.Typescript
{
    public class TypeHelper
    {
        public static string ToPropertyType(string xNamespace, Type propertyType, CodeFile codeFile, string assemblyName)
        {
            switch (propertyType.Namespace + "." + propertyType.Name)
            {
                case "System.Collections.Generic.List`1":
                    {
                        //codeFile.AddSystemUsing("System.Collections.Generic");

                        return string.Format("List<{0}>",
                            string.Join(", ", propertyType.GetGenericArguments().Select(v => ToPropertyType(xNamespace, v, codeFile, assemblyName))));
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
                    return "bool";

                case "System.DateTime":
                    return "DateTime";

                default:
                    break;
            }

            {
                var source = "." + propertyType.FullName.Replace(assemblyName, string.Empty).Replace(".", "/");
                codeFile.AddProjectImport(propertyType.Name, source, notDefault: true);
            }

            return propertyType.Name;
        }
    }
}
