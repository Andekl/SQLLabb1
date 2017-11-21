using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace SQLLabb1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
        Dictionary<string, string> column;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "SELECT * FROM Author";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Author a = new Author();
                            a.Id = reader.GetInt32(0);
                            a.Name = reader.GetString(1);
                            a.Nationality = reader.GetString(2);
                            
                            AuthorListBox.Items.Add(a.Name);

                            Book b = new Book();
                            b.BookId = reader.GetInt32(0);
                           // b.AuthorID = reader.GetInt32(1);
                            b.Title = reader.GetString(2);

                            BookListBox.Items.Add(b.BookId);
                        }
                    }
                }
            }


            //string connectionString1 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //using (SqlConnection con1 = new SqlConnection(connectionString1))
            //{
            //    con1.Open();
            //    var query = "SELECT Title FROM Book";
            //    using (SqlCommand cmd = new SqlCommand(query, con1))
            //    {
            //        using (SqlDataReader reader1 = cmd.ExecuteReader())
            //        {
            //            while (reader1.Read())
            //            {
            //                Book b = new Book();
            //                b.BookId = reader1.GetInt32(0);
            //                b.AuthorID = reader1.GetInt32(1);
            //                b.Title = reader1.GetString(2);

            //                BookListBox.Items.Add(b.Title);
            //            }
            //        }
            //    }
            //}
        }

        private void AuthorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Gör samma för BookListBox
        {
            var aa = sender as ListBox; // hur få tag på värden?
            string author = aa.SelectedItem.ToString();
            
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "SELECT * FROM Author where Name = '" + author + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                Author a = new Author(); //varför läses inte detta in?
                                a.Id = reader.GetInt32(0);
                                a.Name = reader.GetString(1);
                                a.Nationality = reader.GetString(2); //hur skriva ut hela objektet här?

                            AuthorTextBox.Text = + a.Id + ". " + a.Name + " is a " + a.Nationality + " author. ";
                        }
                    }
                }
            }
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bb = sender as ListBox; // hur få tag på värden?
            string book = bb.SelectedItem.ToString();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "SELECT * FROM Book where BookId = '" + book + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book b = new Book(); //varför läses inte detta in?
                            b.BookId = reader.GetInt32(0);
                            b.AuthorID = reader.GetInt32(1);
                            b.Title = reader.GetString(2); //hur skriva ut hela objektet här?

                            BookTextBox.Text = b.BookId + ". Author with ID no: " + b.AuthorID + " has written " + b.Title + ".";
                        }
                    }
                }
            }
        }
    }
}
