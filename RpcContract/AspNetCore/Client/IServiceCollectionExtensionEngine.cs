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
    public static class IServiceCollectionExtensionEngine
    {
        public static string Generate(string rootNamespace, List<InterfaceNode> interfaceNodeList)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Net.Http");
            codeFile.AddSystemUsing("System.Text");
            codeFile.AddSystemUsing("System.Threading.Tasks");

            codeFile.AddNugetUsing("Newtonsoft.Json");

            codeFile.AddProjectUsing($"Wutip.Client.Search");
            codeFile.AddProjectUsing($"Wutip.Contract.Search");

            var codeNamespace = codeFile.AddNamespace("Microsoft.Extensions.DependencyInjection");

            var codeClass = codeNamespace.AddClass("IServiceCollectionExtension");
            codeClass.AccessModifiers = AccessModifiers.Public;
            codeClass.Summary = "ServiceClientBase";
            codeClass.IsStatic = true;
            codeClass.IsPartial = true;

            {
                var codeMethod = codeClass.AddMethod($"AddWutipClient");
                codeMethod.Summary = "AddWutipClient";
                codeMethod.AccessModifiers = AccessModifiers.Public;
                codeMethod.IsStatic = true;
                codeMethod.Type = "void";

                codeMethod.AddParameter("IServiceCollection", "services", withThis: true);
                codeMethod.AddParameter("Uri", "baseAddress");

                {
                    {
                        var chain = codeMethod.StepStatementChain("services");
                        var addHttpClientExpesssion = chain.AddCallMethodExpression("AddHttpClient");
                        addHttpClientExpesssion.AddParameter(DataValue.DoubleQuotationString("api"));
                        addHttpClientExpesssion.AddParameterOfLamdaStepBuilderCollection()
                            .SetParameter("httpClient")
                            .StepStatement("httpClient.BaseAddress = baseAddress;");
                    }

                    foreach (var interfaceNode in interfaceNodeList)
                    {
                        codeMethod.StepEmpty();
                        codeMethod.StepStatement($"services.AddSingleton<{interfaceNode.Name}, {interfaceNode.Name}Client>();");
                    }
                }
            }

            return codeFile.TransformText();
        }
    }
}
