using FluentValidation;

namespace Application.QCards
{
    public class BuyValidator : AbstractValidator<Buy.Command>
    {
        public BuyValidator()
        {
            RuleFor(x => x.Id)
            .Matches(@"^(\d{2}-\d{4}-\d{4}|\d{4}-\d{4}-\d{4})$")
            .WithMessage("Invalid ID format. It should be ##-####-#### or ####-####-####");
        }
    }
}