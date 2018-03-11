namespace Sudoku.Mvc.Common.Configuration.Options
{
    public class DataConnectionsOptions
    {
        public const string SectionName = "DataConnections";

        public DataConnectionsOptions()
        {
            Login = "login";
            Password = "password";
            SqlContactPoints = "127.0.0.1";
        }

        public string SqlContactPoints { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}