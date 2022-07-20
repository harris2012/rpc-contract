using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript
{
    public class RefEngine
    {
        public string Generate()
        {
            return @"export class Ref<T> {
    _value?: T;

    constructor(val?: T) {
        this._value = val;
    }

    get value(): T | undefined {
        return this._value;
    }

    set value(val: T | undefined) {
        this._value = val;
    }
}
";
        }
    }
}
