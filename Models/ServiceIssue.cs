namespace MunicipalReporterPrototype.Models
{
    public class ServiceIssue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }

        public ServiceIssue() { }

        public ServiceIssue(string location, string category, string description, string attachmentPath)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachmentPath = attachmentPath;
        }

        public override string ToString()
        {
            return $"{Category} issue at {Location}: {Description}";
        }
    }
}
