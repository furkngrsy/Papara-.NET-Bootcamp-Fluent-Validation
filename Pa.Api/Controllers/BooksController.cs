using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using FluentValidation;

namespace Pa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private List<Book> list;
        private readonly IValidator<Book> _validator;

        public BooksController(IValidator<Book> validator)
        {
            _validator = validator;

            list = new List<Book>();
            list.Add(new Book() { Id = 1, Name = "Test1", Author = "Author1", PageCount = 993 });
            list.Add(new Book() { Id = 2, Name = "Test2", Author = "Author2", PageCount = 234 });
        }

        [HttpGet]
        public ApiResponse<List<Book>> Get()
        {
            return new ApiResponse<List<Book>>(list);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = new Book { Id = id };
            ValidationResult idValidationResult = _validator.Validate(book, options => options.IncludeProperties(b => b.Id));
            if (!idValidationResult.IsValid)
            {
                return BadRequest(idValidationResult.Errors);
            }

            var item = list?.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return NotFound(new ApiResponse<Book>("Item not found in system."));
            }

            return Ok(new ApiResponse<Book>(item));
        }


        [HttpPost]
        public IActionResult Post([FromBody] Book value)
        {
            ValidationResult result = _validator.Validate(value);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            list.Add(value);
            return Ok(new ApiResponse<List<Book>>(list));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book value)
        {
            ValidationResult result = _validator.Validate(value);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var item = list.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return NotFound(new ApiResponse<List<Book>>("Item not found in system."));
            }

            list.Remove(item);
            list.Add(value);
            return Ok(new ApiResponse<List<Book>>(list));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = new Book { Id = id };
            ValidationResult idValidationResult = _validator.Validate(book, options => options.IncludeProperties(b => b.Id));
            if (!idValidationResult.IsValid)
            {
                return BadRequest(idValidationResult.Errors);
            }

            var item = list.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return NotFound(new ApiResponse<List<Book>>("Item not found in system."));
            }

            list.Remove(item);
            return Ok(new ApiResponse<List<Book>>(list));
        }
    }
}
