using System.ComponentModel.DataAnnotations;

namespace GameStore.Models;

public class Device : BaseEntity{
    [MaxLength(length:500)]
    public string Icon { get; set; } = string.Empty;
}