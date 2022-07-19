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
    public class PackageEngine
    {
        public string Generate(string projectName, string version)
        {
            DataObject package = BuildPackage(projectName, version);

            var builder = new StringBuilder();

            new JsCodeEngine().GenerateDataObject(package, new CodeWriter(new StringWriter(builder)), new GenerateOptions
            {
                DataArrayItemBreakLine = true,
                DataObjectKeyQuotation = "\""
            });

            return builder.ToString();
        }

        private static DataObject BuildPackage(string projectName, string version)
        {
            var package = new DataObject();

            package.AddDataValue("name", DataValue.DoubleQuotationString(projectName));
            package.AddDataValue("version", DataValue.DoubleQuotationString(version));
            package.AddDataValue("description", DataValue.DoubleQuotationString(string.Empty));
            package.AddDataValue("main", DataValue.DoubleQuotationString("./dist/index.js"));
            package.AddDataValue("types", DataValue.DoubleQuotationString("./dist/index.d.ts"));

            var scripts = package.AddDataObject("scripts");
            {
                scripts.AddDataValue("build", DataValue.DoubleQuotationString("tsc"));
            }

            var files = package.AddDataArray("files");
            {
                files.AddDataValue(DataValue.DoubleQuotationString("dist/**/*"));
            }

            package.AddDataValue("author", DataValue.DoubleQuotationString(string.Empty));
            package.AddDataValue("license", DataValue.DoubleQuotationString("MIT"));

            var devDependencies = package.AddDataObject("devDependencies");
            {
                devDependencies.AddDataValue("typescript", DataValue.DoubleQuotationString("^4.7.4"));
            }

            var dependencies = package.AddDataObject("dependencies");
            {
                dependencies.AddDataValue("axios", DataValue.DoubleQuotationString("^0.27.2"));
            }

            return package;
        }
    }
}
