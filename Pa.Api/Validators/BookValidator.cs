using FluentValidation;

namespace Pa.Api.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Id)
                .NotEmpty()
                .WithMessage("Book id is required.")
                .InclusiveBetween(1, 10000)
                .WithMessage("Book id must be between 1 and 10000.");

            RuleFor(book => book.Name)
                .NotEmpty()
                .WithMessage("Book name is required.")
                .Length(5, 50)
                .WithMessage("Book name must be between 5 and 50 characters.");

            RuleFor(book => book.Author)
                .NotEmpty()
                .WithMessage("Book author info is required.")
                .Length(5, 50)
                .WithMessage("Book author info must be between 5 and 50 characters.");

            RuleFor(book => book.PageCount)
                .NotEmpty()
                .WithMessage("Book page count is required.")
                .InclusiveBetween(50, 400)
                .WithMessage("Book page count must be between 50 and 400.")
                .Must((book, pageCount) => IsValidPageCount(book))
                .WithMessage(book => $"Invalid page count for Year {book.Year}.");

            RuleFor(book => book.Year)
                .NotEmpty()
                .WithMessage("Book year is required.")
                .InclusiveBetween(1900, 2024)
                .WithMessage("Book year must be between 1900 and 2024.");
        }

        private bool IsValidPageCount(Book book)
        {
            if (book.Year >= 1900 && book.Year <= 1950)
            {
                return book.PageCount <= 100;
            }
            if (book.Year >= 1951 && book.Year <= 1999)
            {
                return book.PageCount <= 200;
            }
            return true;
        }
    }
}

