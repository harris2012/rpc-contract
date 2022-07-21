using Newtonsoft.Json;
using Panosen.Generation;
using Panosen.Reflection;
using Panosen.Reflection.Model;
using Panosen.Toolkit;
using RpcContract.AspNetCore;
using RpcContract.Service;
using RpcContract.Typescript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RpcContract
{
    class Program
    {
        private const string ConfigFileName = "rpc.json";

        private static readonly Encoding DefaultEncoding = new UTF8Encoding();

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                ShowMenu();
                return;
            }

            Package package = new Package();

            if ("init".Equals(args[0], StringComparison.OrdinalIgnoreCase))
            {
                new InitService().Generate(package);
                WriteToFile(Environment.CurrentDirectory, package);
                return;
            }

            if (!File.Exists(ConfigFileName))
            {
                Console.WriteLine($"{ConfigFileName} is required.");
                return;
            }

            var content = File.ReadAllText(ConfigFileName);
            var rpcFile = JsonConvert.DeserializeObject<RpcFile>(content);

            ProjectSettings projectSettings = rpcFile.ToProjectSettings();

            switch (args[0])
            {
                case "setup":
                    {
                        new SetupService().Process(package, projectSettings);
                    }
                    break;
                case "update":
                    {
                        AssemblyModel assemblyModel = LoadAssemblyModel(projectSettings);

                        if (projectSettings.AspnetCoreParam != null)
                        {
                            AspNetCoreGeneration.Generate(package, "AspNetCore", projectSettings, assemblyModel);
                        }

                        if (projectSettings.TypescriptParam != null)
                        {
                            TypescriptGeneration.Generate(package, "Typescript", projectSettings, assemblyModel);
                        }
                    }
                    break;
                default:
                    break;
            }

            WriteToFile(Environment.CurrentDirectory, package);
        }

        private static AssemblyModel LoadAssemblyModel(ProjectSettings projectSettings)
        {
            var dllPath = Path.Combine("CodeFirst", $"{projectSettings.CodeFirstProjectName}", "bin", "Debug", "netstandard2.0", $"{projectSettings.CodeFirstAssemblyName}.dll");
            var xmlPath = Path.Combine("CodeFirst", $"{projectSettings.CodeFirstProjectName}", "bin", "Debug", "netstandard2.0", $"{projectSettings.CodeFirstAssemblyName}.xml");

            Assembly assembly = Assembly.LoadFrom(dllPath);

            XmlDoc xmlDoc = XmlLoader.LoadXml(xmlPath);

            return AssemblyLoader.LoadAssembly(assembly, xmlDoc);
        }

        private static void WriteToFile(string output, Package package)
        {
            foreach (var item in package.Files)
            {
                var path = Path.Combine(output, item.FilePath);

                var fileDirectory = Path.GetDirectoryName(path);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                if (item is Panosen.Generation.PlainFile)
                {
                    if (File.Exists(path) && Hash.SHA256HEX(File.ReadAllBytes(path)) == Hash.SHA256HEX(DefaultEncoding.GetBytes(((Panosen.Generation.PlainFile)item).Content)))
                    {
                        continue;
                    }
                    File.WriteAllText(path, ((Panosen.Generation.PlainFile)item).Content, DefaultEncoding);
                }

                if (item is Panosen.Generation.BytesFile)
                {
                    if (File.Exists(path) && Hash.SHA256HEX(File.ReadAllBytes(path)) == Hash.SHA256HEX(((Panosen.Generation.BytesFile)item).Bytes))
                    {
                        continue;
                    }
                    File.WriteAllBytes(path, ((Panosen.Generation.BytesFile)item).Bytes);
                }
            }
        }

        static void ShowMenu()
        {
            var menu = @"1. rpc init 创建项目，生成soa.json模版文件
2. rpc setup 初始化项目
3. rpc update 生成契约
";

            Console.WriteLine(menu);
        }
    }
}
