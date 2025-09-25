using System.ComponentModel.DataAnnotations;
using Florin_Back.Enums;

namespace Florin_Back.Models.DTOs.Transaction;

public class CreateTransactionDTO
{
    [Required]
    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
    public long CategoryId { get; set; }
}
