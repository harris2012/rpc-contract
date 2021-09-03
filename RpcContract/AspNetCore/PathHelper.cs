using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpcContract.AspNetCore
{
    internal class PathHelper
    {
        public static string MakeRelativePath(string assemblyName, string fullName)
        {
            return fullName.Replace(assemblyName, string.Empty).TrimStart('.').Replace(".", "\\");
        }
    }
}
