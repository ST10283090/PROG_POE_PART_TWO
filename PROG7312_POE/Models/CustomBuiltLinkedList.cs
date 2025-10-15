namespace PROG7312_POE.Models
{
    public class ReportNode
    {
        public Report Data { get; set; }
        public ReportNode? Next { get; set; }

        public ReportNode(Report data)
        {
            Data = data;
            Next = null;
        }
    }

    //prt one feedback implemetedd a custom linked list
    public class CustomBuiltLinkedList
    {
        private ReportNode? head;

        public void Add(Report report)
        {
            ReportNode newNode = new ReportNode(report);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                ReportNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        // conv to list so dont needa chnage the viewReport.cshtml
        public List<Report> ToList()
        {
            List<Report> list = new List<Report>();
            ReportNode? current = head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }
    }
}