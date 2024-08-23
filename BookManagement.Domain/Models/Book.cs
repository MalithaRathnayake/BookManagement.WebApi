using BookManagement.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author can't be longer than 100 characters.")]
        public string Author { get; set; } = null!;

        [Required(ErrorMessage = "Published Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Published Date must be a valid date.")]
        [DateInPast(ErrorMessage = "Published Date cannot be in the future.")]
        public DateTime PublishedDate { get; set; }
    }
}
