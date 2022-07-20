using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript.Client
{
    public class BaseAxiosClientEngine
    {
        public string Generate()
        {
return @"
import { Ref } from ""./ref"";

import { LoadingResponse } from ""./loading-response"";

import axios from '../node_modules/axios/index';

export class BaseAxiosClient {

    common(path: string, request: any): LoadingResponse<any> {
        const loading = new Ref(true);
        const response = new Ref<any>();

        axios.post<any>(path, request).then(res => {
            response.value = res.data;
        }).catch(err => {
            console.log(err);
        }).finally(() => {
            loading.value = false;
        });

        return { loading, response };
    }
}
";
        }
    }
}
