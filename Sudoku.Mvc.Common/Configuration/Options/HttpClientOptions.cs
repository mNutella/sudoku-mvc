using Sudoku.Mvc.Common.Configuration.Options.Abstract;

namespace Sudoku.Mvc.Common.Configuration.Options
{
    public class HttpClientOptions : DbOptions
    {
        public const string SectionName = "HttpClient";

        public int DefaultHttpRequestTimeoutMilliseconds { get; set; }

        public HttpClientOptions()
            : base (nameOfSection: SectionName)
        {
            DefaultHttpRequestTimeoutMilliseconds = 10000;
        }
    }
}
