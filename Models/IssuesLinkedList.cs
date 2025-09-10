using System.Collections.Generic;

namespace MunicipalReporterPrototype.Models
{
    public class IssueLinkedList
    {
        private IssueNode head;

        public void Add(ServiceIssue issue)
        {
            IssueNode newNode = new IssueNode(issue);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                IssueNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public List<ServiceIssue> ToList()
        {
            List<ServiceIssue> list = new List<ServiceIssue>();
            IssueNode current = head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }
    }
}
