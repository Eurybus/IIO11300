using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H10_Bookstore
{
    [Serializable]
    public class Book
    {
        #region PROPERTIES
        private int id;

        public int Id
        {
            get { return id; }
        }


        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string maa;

        public string Maa
        {
            get { return maa; }
            set { maa = value; }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Book(int id)
        {
            this.id = id;
        }
        public Book(int id, string title, string author, string country, int year)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.country = country;
            this.year = year;
        }

        public override string ToString()
        {
            return title + " written by " + author;
        }
        #endregion
    }
    public class BookShop
    {
        private static string cs = H10_Bookstore.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            //metodi palauttaa kokoelman book-olioita
            List < Book > temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(2, "Anne Karenina", "Leo Tolstoi", "Venäjä", 1869));

            return temp;
        }
        public static List<Book> GetBooks(Boolean useDB)
        {
            //Haetaan kirjoja, db kerroksen palauttama datatable mapataan olioiksi
            //eli tehdään ORM
            DataTable dt;
            List<Book> temp = new List<Book>();
            try
            {
                if (useDB)
                {
                    //MessageBox.Show("Ebin");
                    dt = DBbooks.GetBooks(cs);
                }
                else
                {
                    dt = DBbooks.GetTestData();
                }
                //Tehdään ORM eli datatablen rivi muutetaan olioiksi
                Book book;
                foreach (DataRow dr in dt.Rows)
                {
                    book = new Book((int)dr[0]);
                    book.Author = dr["author"].ToString();
                    book.Title = dr["name"].ToString();
                    book.Country = dr["country"].ToString();
                    book.Year = (int)dr["year"];
                    temp.Add(book);
                }

                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void UpdateBook(Book book)
        {
            try
            {
                int lkm = DBbooks.UpdateBook(cs, book.Id, book.Title, book.Author, book.Author, book.Year);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
