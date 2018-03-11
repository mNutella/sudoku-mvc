using Sudoku.Mvc.Common.Configuration.Options;

namespace Sudoku.Mvc.Common.Extensions
{
    public static class HostOptionsExtensions
    {
        public static string GetFullUrlFromSubdomain(this HostingOptions options, string subdomainUrl)
        {
            return $"{subdomainUrl}.{options.HostUrl}";
        }
    }
}
