using System.ComponentModel.DataAnnotations;

namespace Sudoku.Mvc.Common.Exceptions
{
    public enum CustomErrorCode
    {
        // Set a default value
        UnknownError = 0,

        // Provide a value for successful rather than the default "UnknownError"
        [Display(Description = "Success")]
        Success = 999,

        // Defaults
        [Display(Description = "Internal server error")]
        InternalServerErrorDefault = 1000,
        [Display(Description = "Bad user input")]
        BadUserInputDefault = 1010,
        [Display(Description = "Not found")]
        NotFoundDefault = 1020,
        [Display(Description = "Source IP address not found")]
        GeoLocationForbiddenDefault = 1030,
        [Display(Description = "Forbidden")]
        ForbiddenDefault = 1040,
        [Display(Description = "Item already exists")]
        ItemAlreadyExistsDefault = 1050,
        [Display(Description = "Unauthorized")]
        UnauthorizedDefault = 1060
    }
}
