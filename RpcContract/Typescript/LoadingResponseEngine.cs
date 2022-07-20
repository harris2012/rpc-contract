using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript
{
    public class LoadingResponseEngine
    {
        public string Generate()
        {
            return @"import { Ref } from './ref';

export interface LoadingResponse<TResponse> {

    loading: Ref<boolean>;
    
    response: Ref<TResponse>
}";
        }
    }
}
