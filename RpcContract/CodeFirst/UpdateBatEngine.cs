using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.CodeFirst
{
    public class UpdateBatEngine
    {
        public string TransformText()
        {
            return @"pushd CodeFirst

dotnet restore

dotnet build

popd

rmdir /S /Q AspNetCore

rpc update

pause
";
        }
    }
}
