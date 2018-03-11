namespace Sudoku.Mvc.Common.Configuration.Options
{

    public class LogstashOptions
    {
        /// <summary>
        /// Не менять. Переменная окружения System_Env устанавливается при развертывании приложения
        /// </summary>
        public string System_Env { get; set; }
        public string Index => $"{System_Env}";
        public bool IsValidIndex => !string.IsNullOrWhiteSpace(System_Env);
    }
}
