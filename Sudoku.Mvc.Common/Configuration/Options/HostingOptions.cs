using Sudoku.Mvc.Common.Configuration.Options.Abstract;

namespace Sudoku.Mvc.Common.Configuration.Options
{
    public class HostingOptions : DbOptions
    {
        public const string SectionName = "HostingOptions";

        public string HostUrl { get; set; }

        public HostingOptions()
            : base(nameOfSection: SectionName)
        {
            HostUrl = "localhost";
        }
    }
}
