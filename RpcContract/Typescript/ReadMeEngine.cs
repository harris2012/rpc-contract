using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript
{
    public static class ReadMeEngine
    {
        public static string Generate()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("#Sample");
            builder.AppendLine();

            builder.AppendLine("## Add Dependency");
            builder.AppendLine();

            builder.AppendLine("```xml");
            builder.AppendLine(@"yarn add ...");
            builder.AppendLine("```");

            builder.AppendLine("## Call Rpc");
            builder.AppendLine();

            builder.AppendLine("```csharp");
            builder.AppendLine(@"console.log(123)");
            builder.AppendLine("```");

            return builder.ToString();
        }
    }
}
