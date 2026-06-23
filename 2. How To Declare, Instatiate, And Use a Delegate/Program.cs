// A set of classes for handling a bookstore
namespace Bookstore;

// Describes a book in the book list
public record struct Book(string Title, string Author, decimal Price, bool Paperback);

// Declare a delegate type for processing a book
public delegate void ProcessBookCallback(Book book);

// Maintains a book database
public class BookDB
{
    // List of all books in the database
    readonly List<Book> list = [];

    // Add a book to the database
    public void AddBook(string title, string author, decimal price, bool paperback) => list.Add(new Book(title, author, price, paperback));
    
    // Call a passed in delegate on each paperback book to process it
    public void ProcessPaperbackBooks(ProcessBookCallback processBook)
    {
        foreach (Book b in list)
        {
            if (b.Paperback)
            {
                // Calling the delegate to process the book
                processBook(b);
            }
        }
    }

    // Using the Bookstore classes

    // Class to total and average prices of books
    class PriceTotaller
    {
        private int countBooks = 0;
        private decimal priceBooks = 0.0m;

        internal void AddBookToTotal(Book book)
        {
            countBooks++; 
            priceBooks += book.Price;            
        }

        internal decimal AveragePrice() => priceBooks / countBooks;
    }

    // Class to test the book database and delegate processing
    class Test
    {
        // Method to print the title of a book
        static void PrintTitle(Book b) => Console.WriteLine($"   {b.Title}");

        // Main method to run the test
        static void Main()
        {
            // Create a new book database
            BookDB bookDB = new();

            // Add some books to the database
            AddBooks(bookDB);

            Console.WriteLine("Paperback Book Titles:");

            // Process each paperback book and print its title
            bookDB.ProcessPaperbackBooks(PrintTitle);

            // Create a new PriceTotaller to calculate average price
            PriceTotaller totaller = new();

            // Process each paperback book and add its price to the total
            bookDB.ProcessPaperbackBooks(totaller.AddBookToTotal);

            Console.WriteLine($"Average Paperback Book Price: {totaller.AveragePrice():#.##}");
        }
    }

    static void AddBooks(BookDB bookDB)
    {
        bookDB.AddBook("Professional C# 7 and .NET Core 2.0", "Christian Nagel", 50.00m, true);
        bookDB.AddBook("Professional C# 8 and .NET Core 3.0", "Christian Nagel", 60.00m, true);
        bookDB.AddBook("Professional C# 9 and .NET 5", "Christian Nagel", 70.00m, true);
        bookDB.AddBook("Professional C# 10 and .NET 6", "Christian Nagel", 80.00m, true);
        bookDB.AddBook("Professional C# 11 and .NET 7", "Christian Nagel", 90.00m, true);
    }
}




