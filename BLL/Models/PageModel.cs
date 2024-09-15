#nullable disable

using System.ComponentModel;

namespace BLL.Models
{
    public class PageModel
    {
        [DisplayName("Page Number")]
        public int PageNumber { get; set; }

        [DisplayName("Records per Page")]
        public string RecordsPerPageCount { get; set; }

        public int TotalRecordsCount { get; set; }
        public List<string> RecordsPerPageCounts { get; private set; }

        public List<int> PageNumbers
        {
            get
            {
                var pageNumbers = new List<int>();
                int recordsPerPageCount;
                if (TotalRecordsCount > 0 && int.TryParse(RecordsPerPageCount, out recordsPerPageCount))
                {
                    int numberOfPages = Convert.ToInt32(Math.Ceiling(TotalRecordsCount / Convert.ToDecimal(recordsPerPageCount)));
                    for (int page = 1; page <= numberOfPages; page++)
                    {
                        pageNumbers.Add(page);
                    }
                }
                else
                {
                    pageNumbers.Add(1);
                }
                return pageNumbers;
            }
        }

        public PageModel()
        {
            PageNumber = 1;
            RecordsPerPageCount = "10";
            RecordsPerPageCounts = new List<string>() { "5", "10", "25", "50", "100", "All" };
        }
    }
}
