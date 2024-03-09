using System.ComponentModel.DataAnnotations;

namespace GameStore.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute{

private readonly int _maxFileSizeAttribute;

    public MaxFileSizeAttribute(int maxFileSizeAttribute)
    {
        _maxFileSizeAttribute = maxFileSizeAttribute;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if(file is not null)
        {
            if(file.Length > _maxFileSizeAttribute)
            {
                return new ValidationResult($"Max File Size Allowed is {_maxFileSizeAttribute} MB");
            }
        }

        return ValidationResult.Success;
    }


}