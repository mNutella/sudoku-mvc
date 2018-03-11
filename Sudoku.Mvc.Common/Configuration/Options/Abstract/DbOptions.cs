namespace Sudoku.Mvc.Common.Configuration.Options.Abstract
{
    public abstract class DbOptions
    {
        public readonly string NameOfSection;

        protected DbOptions(string nameOfSection)
        {
            NameOfSection = nameOfSection;
        }
    }
}
