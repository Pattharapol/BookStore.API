using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;
        public BookRepository(BookStoreContext context)
        {
            this.context = context;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await this.context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
            }).ToListAsync();

            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var records = await this.context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
            }).FirstOrDefaultAsync();

            return records;
        }
    }
}
