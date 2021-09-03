using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;

namespace RpcContract.AspNetCore
{
    public class AspNetCoreSolutionEngine
    {
        public string ProjectName { get; set; }

        public string ProjectGuid { get; set; }

        public string SolutionGuid { get; set; }

        public string TransformText()
        {
            var solution = new Solution();
            solution.SolutionGuid = this.SolutionGuid;

            var project = solution.AddProject();
            project.ProjectName = this.ProjectName;
            project.ProjectGuid = this.ProjectGuid;
            project.ProjectTypeGuid = ProjectTypeGuids.CSharpLibrarySDK;
            project.ProjectPath = $"{this.ProjectName}/{this.ProjectName}.csproj";

            return solution.TransformText();
        }
    }
}
