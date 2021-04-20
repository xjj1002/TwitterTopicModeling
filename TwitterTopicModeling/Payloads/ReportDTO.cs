
//this is the data transfer object for incoming data to generate reports
namespace TwitterTopicModeling.Payloads
{
    public class ReportDTO
    {
        public string username { get; set; }

        public int count { get; set; }
    }
}