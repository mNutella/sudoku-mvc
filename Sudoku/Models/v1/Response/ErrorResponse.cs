using System.Collections.Generic;
using Sudoku.Mvc.Common.Exceptions;

namespace Sudoku.Mvc.Api.Models.v1.Response
{
    public class ErrorResponse
    {
        public CustomErrorCode Code { get; set; }

        public string Message { get; set; }

        public string LogstashLink { get; set; }

        public IDictionary<string, object> InputErrors { get; set; }
    }
}