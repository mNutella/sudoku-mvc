using Sudoku.Mvc.Common.Configuration.Options.Abstract;

namespace Sudoku.Mvc.Common.Configuration.Options
{
    public class DadataOptions : DbOptions
    {
        public const string SectionName = "Dadata";

        public string ApiKey { get; set; }

        public string Url { get; set; }

        public DadataOptions()
            : base(nameOfSection: SectionName)
        {
            ApiKey = "03155a0886d3bc58743e688e6a1830119bf274e4";
            Url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address";
        }
    }
}
