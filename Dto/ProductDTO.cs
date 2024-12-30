using FluentValidation;
using Simple_Product_Management_System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Simple_Product_Management_System.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
      
        public string Name { get; set; }

       
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
   

public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be a positive number.");
        }
    }
}
