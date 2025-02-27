using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Summary { get; set; } = string.Empty;

    [StringLength(1000)]
    public string PersonalReview { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime PublicationDate { get; set; }

    [StringLength(50)]
    public string Genre { get; set; } = string.Empty;

    [Range(0, 1000)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}
