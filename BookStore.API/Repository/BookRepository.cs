using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;
        private readonly IMapper mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await this.context.Books.Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Description = x.Description,
            //    Title = x.Title,
            //}).ToListAsync();

            //return records;

            var books = await context.Books.ToListAsync();
            return this.mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //var record = await this.context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Description = x.Description,
            //    Title = x.Title,
            //}).FirstOrDefaultAsync();

            //return record;

            var book = await this.context.Books.FindAsync(bookId);
            return this.mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description,

            };
            this.context.Books.Add(book);
            await this.context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            //var book = await this.context.Books.FindAsync(bookId);
            //if (book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;
            //    await this.context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };
            this.context.Books.Update(book);
            await this.context.SaveChangesAsync();

        }

        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await this.context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() { Id = bookId };
            this.context.Books.Remove(book);
            await this.context.SaveChangesAsync();

        }
    }
}
