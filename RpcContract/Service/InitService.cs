using Newtonsoft.Json;
using Panosen.Generation;
using RpcContract.AspNetCore;
using System;

namespace RpcContract.Service
{
    public class InitService
    {
        public void Generate(Package package)
        {
            package.Add("rpc.json", JsonConvert.SerializeObject(PrepareRpcFile(), Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            }));
        }

        private static RpcFile PrepareRpcFile()
        {
            RpcFile rpcFile = new RpcFile();
            rpcFile.ProjectName = "OnlineService";
            rpcFile.Version = "1.0.0";
            rpcFile.CodeFirstSolutionGuid = Guid.NewGuid().ToString("B").ToUpper();
            rpcFile.CodeFirstProjectGuid = Guid.NewGuid().ToString("B").ToUpper();

            {
                rpcFile.AspNet = new RpcAspNet();

                rpcFile.AspNet.SolutionGuid = Guid.NewGuid().ToString("B").ToUpper();

                rpcFile.AspNet.Contract = new RpcAspNetProject();
                rpcFile.AspNet.Contract.ProjectGuid = Guid.NewGuid().ToString("B").ToUpper();

                rpcFile.AspNet.Client = new RpcAspNetProject();
                rpcFile.AspNet.Client.ProjectGuid = Guid.NewGuid().ToString("B").ToUpper();
            }

            {
                rpcFile.TsProject = new TsProject();
            }

            return rpcFile;
        }
    }
}
