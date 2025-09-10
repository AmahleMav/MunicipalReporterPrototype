namespace MunicipalReporterPrototype.Models
{
    public class IssueNode
    {
        public ServiceIssue Data { get; set; }
        public IssueNode Next { get; set; }

        public IssueNode(ServiceIssue data)
        {
            Data = data;
            Next = null;
        }
    }
}
