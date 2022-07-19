using Panosen.CodeDom;
using Panosen.CodeDom.JavaScript.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcContract.Typescript
{
    public class TsConfigEngine
    {
        public string Generate()
        {
            DataObject package = BuildPackage();

            var builder = new StringBuilder();

            new JsCodeEngine().GenerateDataObject(package, new CodeWriter(new StringWriter(builder)), new GenerateOptions
            {
                DataArrayItemBreakLine = true,
                DataObjectKeyQuotation = "\""
            });

            return builder.ToString();
        }

        private static DataObject BuildPackage()
        {
            var tscofig = new DataObject();

            var compilerOptions = tscofig.AddDataObject("compilerOptions");
            {
                compilerOptions.AddDataValue("target:", DataValue.DoubleQuotationString("ES2018,"));
                compilerOptions.AddDataValue("module:", DataValue.DoubleQuotationString("es2020,"));
                compilerOptions.AddDataValue("declaration:", true);
                compilerOptions.AddDataValue("outDir:", DataValue.DoubleQuotationString("./dist,"));
                compilerOptions.AddDataValue("strict:", true);
                compilerOptions.AddDataValue("sourceMap:", false);
            }

            var include = tscofig.AddDataArray("include");
            {
                include.AddDataValue(DataValue.DoubleQuotationString("src/**/*.ts"));
            }

            var exclude = tscofig.AddDataArray("exclude");
            {
                exclude.AddDataValue(DataValue.DoubleQuotationString("node_modules"));
                exclude.AddDataValue(DataValue.DoubleQuotationString("**/*.d.ts"));
            }

            return tscofig;
        }
    }
}
