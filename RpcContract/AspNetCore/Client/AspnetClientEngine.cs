using Panosen.CodeDom;
using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RpcContract.AspNetCore.Client
{
    public class AspnetClientEngine
    {
        public void Generate(Package package, string prefix, string assemblyName, AspNetCoreParam aspNetCoreParam, InterfaceNode interfaceNode)
        {
            var codeFile = PrepareCodeFile(interfaceNode, assemblyName, aspNetCoreParam);

            package.Add(Path.Combine(prefix, PathHelper.MakeRelativePath(assemblyName, interfaceNode.FullName) + "Client" + ".cs"), codeFile.TransformText());
        }

        private CodeFile PrepareCodeFile(InterfaceNode interfaceNode, string assemblyName, AspNetCoreParam aspNetCoreParam)
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Net.Http");
            codeFile.AddSystemUsing("System.Text");
            codeFile.AddSystemUsing("System.Threading.Tasks");

            codeFile.AddNugetUsing("Newtonsoft.Json");

            codeFile.AddProjectUsing("Wutip.Contract.Search");

            var codeNamespace = codeFile.AddNamespace(interfaceNode.Namespace.Replace(assemblyName, aspNetCoreParam.Client.AssemblyName));

            var codeClass = codeNamespace.AddClass(interfaceNode.Name + "Client", summary: interfaceNode.Name + "Client");
            codeClass.AccessModifiers = AccessModifiers.Public;
            codeClass.Summary = interfaceNode.Summary ?? interfaceNode.Name;

            codeClass.SetBaseClass("ServiceClientBase");
            codeClass.AddInterface(interfaceNode.Name);

            //contractor
            {
                var codeMethod = codeClass.AddConstructor();
                codeMethod.Name = interfaceNode.Name + "Client";
                codeMethod.Summary = interfaceNode.Name + "Client";
                codeMethod.AccessModifiers = AccessModifiers.Public;
                codeMethod.AddParameter("IHttpClientFactory", "httpClientFactory");
                codeMethod.BaseConstructor = "base(httpClientFactory)";
                codeMethod.StepBuilders = new List<StepBuilderOrCollection>();
            }

            foreach (var methodNode in interfaceNode.MethodNodeList)
            {
                var codeMethod = codeClass.AddMethod($"{methodNode.Name}Async");
                codeMethod.Summary = methodNode.Summary ?? methodNode.Name;
                codeMethod.AccessModifiers = AccessModifiers.Public;
                codeMethod.IsAsync = true;

                var responseType = TypeHelper.ToPropertyType(interfaceNode.Namespace, methodNode.ReturnType, codeFile, assemblyName, aspNetCoreParam.Client.AssemblyName);
                codeMethod.Type = $"Task<{responseType}>";

                List<string> genericypes = new List<string>();
                if (methodNode.Parameters != null && methodNode.Parameters.Count > 0)
                {
                    foreach (var parameter in methodNode.Parameters)
                    {
                        var requestType = TypeHelper.ToPropertyType(interfaceNode.Namespace, parameter.ParameterType, codeFile, assemblyName, aspNetCoreParam.Client.AssemblyName);

                        codeMethod.AddParameter(requestType, parameter.Name);
                        genericypes.Add(requestType);
                    }
                }
                genericypes.Add(responseType);

                {
                    var temp = codeMethod.StepStatementChain()
                        .AddCallMethodExpression($"return await CallRpcAsync<{string.Join(", ", genericypes)}>");
                    temp.AddParameter(DataValue.DoubleQuotationString($"api/{methodNode.Name.ToLowerCaseBreakLine()}"));
                    temp.AddParameter("request");
                }
            }

            return codeFile;
        }
    }
}
