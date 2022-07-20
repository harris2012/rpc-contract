using System.IO;

using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using Panosen.Resource.CSharp;
using RpcContract.AspNetCore.Contract;

namespace RpcContract.AspNetCore.Contract
{
    public static class ContractEngine
    {

        public static string PrepareContractProject(AspNetCoreProject dotNetProject, string version)
        {
            var project = new Project();

            project.Sdk = "Microsoft.NET.Sdk";

            project.AddTargetFramework("netstandard2.0");
            project.AssemblyName = dotNetProject.AssemblyName;
            project.RootNamespace = dotNetProject.RootNamespace;
            project.Version = version;
            project.WithDocumentationFile = true;
            project.GeneratePackageOnBuild = true;

            project.AddPackageReference("Newtonsoft.Json", "13.0.1");

            return project.TransformText();
        }
    }
}
