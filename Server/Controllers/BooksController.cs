using FormationBlazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormationBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private static List<Book> books = new List<Book>()
        {
            new Book()
            {
                Id = 0,
                Name = "Clean Code",
                Author = "Robert Martin",
                Date = new DateTime(2008, 08, 01),
                link = "https://www.amazon.fr/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882/ref=asc_df_0132350882?FORM=SSAPC1"
            },
            new()
            {
                Id = 1,
                Name = "Blazor in Action",
                Author = "Chris Sainty",
                Date = new DateTime(2022, 05,01),
                link = "https://www.manning.com/books/blazor-in-action"
            }
        };




        [HttpGet]
        public IEnumerable<Book> Books() => books;

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            var last = books.OrderByDescending(p => p.Id).FirstOrDefault();
            book.Id = last.Id++;
            books.Add(book);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public Book GetBook(int id)
        {
            return books.FirstOrDefault(p => p.Id == id);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var currentIndex = books.FindIndex(p => p.Id == id);
            if (currentIndex != -1)
            {
                books[currentIndex] = book;
                return NoContent();
            }

            return NotFound();
            
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {

            var currentBook = books.FirstOrDefault(p => p.Id == id);
            
            if (currentBook == null)
                return NotFound();

            books.Remove(currentBook);
            return NoContent();
        }


    }
}
