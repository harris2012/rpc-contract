using System.IO;

using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;
using Panosen.Generation;
using Panosen.Reflection.Model;
using Panosen.Resource.CSharp;
using RpcContract.AspNetCore.Contract;

namespace RpcContract.AspNetCore.Client
{
    public static class ClientProjectEngine
    {
        public static string PrepareClientProject(DotNetProject clientDotNetProject, string version, DotNetProject contractDotNetProject)
        {
            var project = new Project();

            project.Sdk = "Microsoft.NET.Sdk";

            project.AddTargetFramework("netstandard2.0");
            project.AssemblyName = clientDotNetProject.AssemblyName;
            project.RootNamespace = clientDotNetProject.RootNamespace;
            project.Version = version;
            project.WithDocumentationFile = true;
            project.GeneratePackageOnBuild = true;

            project.AddPackageReference("Microsoft.AspNet.WebApi.Client", "5.2.7");
            project.AddPackageReference("Microsoft.Extensions.DependencyInjection", "5.0.2");
            project.AddPackageReference("Microsoft.Extensions.Http", "5.0.0");

            project.AddPackageReference("Newtonsoft.Json", "13.0.1");

            if (contractDotNetProject != null)
            {
                project.AddProjectReference($"..\\{contractDotNetProject.ProjectName}\\{contractDotNetProject.ProjectName}.csproj");
            }

            return project.TransformText();
        }
    }
}
