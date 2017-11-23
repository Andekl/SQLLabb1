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
using System.Data.SqlTypes;
using System.Data;
using System.Threading;

namespace SQLLabb1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                            //b.AuthorID = reader.GetInt32(1);
                            b.Title = reader.GetString(2);

                            BookListBox.Items.Add(b.BookId);
                        }
                    }
                }
            }
        }

        private void AuthorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aa = sender as ListBox;
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
                            Author a = new Author();
                            a.Id = reader.GetInt32(0);
                            a.Name = reader.GetString(1);
                            a.Nationality = reader.GetString(2);

                            IdTextBox.Text = a.Id.ToString();
                            AuthorNameTextBox.Text = "Authors name is " + a.Name;
                            NationalityTextBox.Text = "Author is " + a.Nationality;
                        }
                    }
                }
            }
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bb = sender as ListBox;
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
                            Book b = new Book();
                            b.BookId = reader.GetInt32(0);
                            b.AuthorID = reader.GetInt32(1);
                            b.Title = reader.GetString(2);

                            BookIdTextBox.Text = "Book has ID number " + b.BookId;
                            AuthorIdTextBox.Text = "Author has ID number " + b.AuthorID;
                            TitleTextBox.Text = "The title is " + "''" + b.Title +"''";
                        }
                    }
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            #region Gammal kod
            //int index = AuthorListBox.SelectedIndex;
            //AuthorListBox.Items.RemoveAt(AuthorListBox.SelectedIndex);
            //AuthorListBox.Items.Insert(index, (new Author { author = AuthorTextBox.Text }));

            //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    con.Open();
            //    var query = "UPDATE * SET Book where BookId = '" +  + "'";
            //    using (SqlCommand cmd = new SqlCommand(query, con))
            //    {
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {

            //            }
            //        }
            //    }
            //}
            #endregion

            string cmdString = "UPDATE Author SET Name = @val2, Nationality = @val3, AuthorId = @val1 WHERE AuhorId=" + IdTextBox.Text;
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand(cmdString, conn))
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    // Dispatcher.Invoke(() => AuthorIdTextBox);
                    comm.Parameters.AddWithValue("@val1", IdTextBox.Text);
                    comm.Parameters.AddWithValue("@val2", AuthorNameTextBox.Text);
                    comm.Parameters.AddWithValue("@val3", NationalityTextBox.Text);

                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        private void IdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorIdTextBox.Text = string.Empty;
        }
    }
}
