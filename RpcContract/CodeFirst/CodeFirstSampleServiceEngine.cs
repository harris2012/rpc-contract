using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Language.CSharp;

namespace RpcContract.CodeFirst
{
    public class CodeFirstSampleServiceEngine
    {
        public string RootNamespace { get; set; }

        public string TransformText()
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");

            var codeNamespace = codeFile.AddNamespace(this.RootNamespace);

            codeNamespace.AddInterface(PrepareISampleService());
            codeNamespace.AddClass(PrepareSayHiRequest());
            codeNamespace.AddClass(PrepareSayHiResponse());
            codeNamespace.AddClass(PrepareBaseResponse());

            return codeFile.TransformText();
        }

        public CodeInterface PrepareISampleService()
        {
            CodeInterface codeInterface = new CodeInterface();
            codeInterface.AccessModifiers = AccessModifiers.Public;
            codeInterface.Name = "ISampleService";

            var codeMethod = codeInterface.AddMethod("SayHi");
            codeMethod.Type = "SayHiResponse";
            codeMethod.AddParameter("SayHiRequest", "requets");

            return codeInterface;
        }

        public CodeClass PrepareSayHiRequest()
        {
            CodeClass codeClass = new CodeClass();

            codeClass.Name = "SayHiRequest";
            codeClass.AccessModifiers = AccessModifiers.Public;

            codeClass.AddProperty(CSharpTypeConstant.STRING, "Name");

            return codeClass;
        }

        public CodeClass PrepareSayHiResponse()
        {
            CodeClass codeClass = new CodeClass();

            codeClass.Name = "SayHiResponse";
            codeClass.AccessModifiers = AccessModifiers.Public;

            codeClass.AddProperty(CSharpTypeConstant.STRING, "Content");

            return codeClass;
        }

        public CodeClass PrepareBaseResponse()
        {
            CodeClass codeClass = new CodeClass();

            codeClass.Name = "BaseResponse";
            codeClass.AccessModifiers = AccessModifiers.Public;
            codeClass.IsAbstract = true;

            codeClass.AddProperty(CSharpTypeConstant.STRING, "Name");

            return codeClass;
        }
    }
}
