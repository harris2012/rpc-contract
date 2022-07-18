using Newtonsoft.Json;
using Panosen.Generation;
using Panosen.Reflection;
using Panosen.Reflection.Model;
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
                            AspNetCoreGeneration.Generate(package, "AspNetCore", projectSettings.CodeFirstAssemblyName, projectSettings.Version, projectSettings.AspnetCoreParam, assemblyModel);
                        }

                        if (projectSettings.TypescriptParam != null)
                        {
                            TypescriptGeneration.Generate(package, "Typescript");
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
                byte[] bytes = null;
                switch (item.ContentType)
                {
                    case ContentType.String:
                        {
                            var plainFile = item as PlainFile;
                            var encoding = plainFile.Encoding ?? Encoding.UTF8;
                            bytes = encoding.GetBytes(plainFile.Content);
                        }
                        break;
                    case ContentType.Bytes:
                        {
                            var bytesFile = item as BytesFile;
                            bytes = bytesFile.Bytes;
                        }
                        break;
                    default:
                        break;
                }

                if (bytes == null)
                {
                    continue;
                }

                var path = Path.Combine(output, item.FilePath);
                var directry = Path.GetDirectoryName(path);
                if (!Directory.Exists(directry))
                {
                    Directory.CreateDirectory(directry);
                }

                if (File.Exists(path) && HashHelper.ComputeHash(File.ReadAllBytes(path)) == HashHelper.ComputeHash(bytes))
                {
                    continue;
                }

                File.WriteAllBytes(path, bytes);
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
