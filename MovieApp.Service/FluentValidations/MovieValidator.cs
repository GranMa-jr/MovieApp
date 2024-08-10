using FluentValidation;
using MovieApp.Entity.Entities;

namespace MovieApp.Service.FluentValidations
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithName("Film Adı");

            RuleFor(m => m.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(5000)
                .WithName("Film Açıklaması");

            RuleFor(m => m.Year)
                .NotEmpty()
                .NotNull()
                .WithName("Film Yılı");

            RuleFor(m => m.Time)
                .NotEmpty()
                .NotNull()
                .WithName("Film Süresi");

            RuleFor(m => m.Rate)
                .NotEmpty()
                .NotNull()
                .WithName("Film Puanı");
        }
    }

}