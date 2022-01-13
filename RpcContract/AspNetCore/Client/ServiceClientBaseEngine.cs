using Panosen.CodeDom;
using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Language.CSharp;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.AspNetCore.Client
{
    public static class ServiceClientBaseEngine
    {
        public static string Generate(string rootNamespace)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Net.Http");
            codeFile.AddSystemUsing("System.Text");
            codeFile.AddSystemUsing("System.Threading.Tasks");

            codeFile.AddNugetUsing("Newtonsoft.Json");

            codeFile.AddProjectUsing($"Wutip.Contract");
            codeFile.AddProjectUsing($"Wutip.Contract.Search");

            var codeNamespace = codeFile.AddNamespace(rootNamespace);

            var codeClass = codeNamespace.AddClass("ServiceClientBase");
            codeClass.AccessModifiers = AccessModifiers.Public;
            codeClass.Summary = "ServiceClientBase";
            codeClass.IsAbstract = true;

            codeClass.AddField("IHttpClientFactory", "httpClientFactory", isReadOnly: true);

            //contractor
            {
                var codeMethod = codeClass.AddConstructor();
                codeMethod.Name = "ServiceClientBase";
                codeMethod.AccessModifiers = AccessModifiers.Public;
                codeMethod.Summary = "ServiceClientBase";
                codeMethod.AddParameter("IHttpClientFactory", "httpClientFactory");

                codeMethod.StepStatement("this.httpClientFactory = httpClientFactory;");
            }

            //CallRpcAsync
            {
                var codeMethod = codeClass.AddMethod($"CallRpcAsync<TRequest, TResponse>");
                codeMethod.Summary = "CallRpcAsync";
                codeMethod.AccessModifiers = AccessModifiers.Protected;
                codeMethod.IsAsync = true;

                codeMethod.Type = $"Task<TResponse>";

                codeMethod.AddParameter(CSharpTypeConstant.STRING, "path");
                codeMethod.AddParameter("TRequest", "request");

                {
                    codeMethod.StepStatement("var requestContent = JsonConvert.SerializeObject(request);");

                    codeMethod.StepEmpty();
                    codeMethod.StepStatement("var httpClient = httpClientFactory.CreateClient(\"api\");");

                    codeMethod.StepEmpty();
                    codeMethod.StepStatement("var rpcResponse = await httpClient.PostAsync(path, new StringContent(requestContent, Encoding.UTF8, \"application/json\"));");

                    codeMethod.StepEmpty();
                    codeMethod.StepStatement("var responseContent = await rpcResponse.Content.ReadAsStringAsync();");

                    codeMethod.StepEmpty();
                    codeMethod.StepStatement("var response = JsonConvert.DeserializeObject<TResponse>(responseContent);");

                    codeMethod.StepEmpty();
                    codeMethod.StepStatement("return response;");

                }
            }

            return codeFile.TransformText();
        }
    }
}
