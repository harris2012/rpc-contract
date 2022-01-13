using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.AspNetCore
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
            builder.AppendLine(@"<Project>

  <ItemGroup>
    <PackageReference Include=""Microsoft.Extensions.DependencyInjection"" Version =""5.0.2"" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\Wutip.Client\Wutip.Client.csproj"" />
  </ItemGroup>

</Project>");
            builder.AppendLine("```");

            builder.AppendLine("## Call Rpc");
            builder.AppendLine();

            builder.AppendLine("```csharp");
            builder.AppendLine(@"using Microsoft.Extensions.DependencyInjection;
using Wutip.Contract;
using Wutip.Contract.Search;
using System;
using System.Threading.Tasks;

namespace Savory.SecurityService.Sample
{
    class Program
    {
        async static Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddWutipClient(new Uri(""http://localhost:42745""));

            var serviceProvider = services.BuildServiceProvider();

            var securityService = serviceProvider.GetRequiredService<ISearchService>();

            var response = await securityService.SearchAsync(new SearchRequest{});
        }
    }
}");
            builder.AppendLine("```");

            return builder.ToString();
        }
    }
}
