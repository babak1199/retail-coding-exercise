using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RetailCodingExercise.Models
{
    public class Search
    {
        [FromQuery(Name = "query")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Search string cannot be empty")]
        [MinLength(2, ErrorMessage = "Query string must be at least 2 characters long")]
        public string Query { get; set; }
    }
}
