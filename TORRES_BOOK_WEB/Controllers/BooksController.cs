using Microsoft.AspNetCore.Mvc;
using BookstoreApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        // Static list of books
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m, PublicationYear = 1925 },
            new Book { Id = 2, Title = "1984", Author = "George Orwell", Price = 8.99m, PublicationYear = 1949 }
        };

        // List all books
        public IActionResult Index()
        {
            return View(books);
        }

        // Show the Create form
        public IActionResult Create()
        {
            return View();
        }

        // Handle Create form submission
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = books.Count + 1;
                books.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Edit form
        public IActionResult Edit(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Handle Edit form submission
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.PublicationYear = book.PublicationYear;
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Delete a book
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null) books.Remove(book);
            return RedirectToAction("Index");
        }
    }
}
