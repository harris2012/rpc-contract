using Panosen.CodeDom.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RpcContract.AspNetCore
{
    public class TypeHelper
    {
        public static string ToPropertyType(string xNamespace, Type propertyType, CodeFile codeFile, string assemblyName, string targetAssemblyName)
        {
            switch (propertyType.Namespace + "." + propertyType.Name)
            {
                case "System.Collections.Generic.List`1":
                    {
                        codeFile.AddSystemUsing("System.Collections.Generic");

                        return string.Format("List<{0}>",
                            string.Join(", ", propertyType.GetGenericArguments().Select(v => ToPropertyType(xNamespace, v, codeFile, assemblyName, targetAssemblyName))));
                    }

                default:
                    break;
            }

            switch (propertyType.FullName)
            {
                case "System.Byte":
                    return "byte";

                case "System.Int16":
                    return "short";

                case "System.Int32":
                    return "int";

                case "System.Int64":
                    return "long";

                case "System.String":
                    return "string";

                case "System.Boolean":
                    return "bool";

                case "System.DateTime":
                    return "DateTime";

                case "System.Decimal":
                    return "decimal";

                case "System.Single":
                    return "float";

                case "System.Double":
                    return "double";

                default:
                    break;
            }

            if (xNamespace != propertyType.Namespace)
            {
                codeFile.AddProjectUsing(propertyType.Namespace.Replace(assemblyName, targetAssemblyName));
            }

            return propertyType.Name;
        }
    }
}
