using Illumina.CotentRater.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Illumina.CotentRater.Dal
{
    public class BookRepository : IRepository<Book> 
    {
        //create real DB (SQL, MongoDB, etc) to connect tos
        private static volatile List<Book> data = null;
        private static object syncRoot = new Object();

        private static void PopulateRepository()
        {
            if (data == null)
            {
                lock(syncRoot)
                {
                    if (data == null)
                    {
                        //add books
                    }
                }
            }
        }

        public BookRepository()
        {
            PopulateRepository();
        }

        public IEnumerable<Book> Get()
        {
            return data.ToList();
        }

        public Book Save(Book book)
        {
            if (book.Id != Guid.Empty)
                throw new ArgumentException("Cannot save an existing book.  Use Update method");
            book.Id = Guid.NewGuid();
            data.Add(book);
            return book;
        }

        public void Update(Book book)
        {
            if (book.Id == Guid.Empty)
                throw new ArgumentException("Cannot update an new book.  Use Save method");
            var existing = data.FirstOrDefault(b => b.Id == book.Id);
            data.Remove(existing);
            data.Add(book);
        }
    }
}
