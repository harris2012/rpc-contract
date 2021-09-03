
using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;

namespace RpcContract.AspNetCore
{
    public class AspNetCoreProjectEngine
    {
        public string RootNamespace { get; set; }

        public string AssemblyName { get; set; }

        public string TransformText()
        {
            var project = new Project();

            project.Sdk = "Microsoft.NET.Sdk";

            project.AddTargetFramework("netstandard2.0");
            project.AssemblyName = this.AssemblyName;
            project.RootNamespace = this.RootNamespace;
            project.WithDocumentationFile = true;

            project.AddPackageReference("Newtonsoft.Json", "13.0.1");

            return project.TransformText();
        }
    }
}
