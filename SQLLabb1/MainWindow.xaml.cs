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
            GetAuthorData();
            GetBookData();
        }

        public void GetAuthorData()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "SELECT * FROM Author";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author a = new Author();
                        a.Id = reader.GetInt32(0);
                        a.Name = reader.GetString(1);
                        a.Nationality = reader.GetString(2);

                        AuthorListBox.Items.Add(a.Name);
                    }
                }
            }
        }
        public void GetBookData()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "SELECT * FROM Book";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book b = new Book();
                        b.BookId = reader.GetInt32(0);
                        //b.AuthorID = reader.GetInt32(1);
                        b.Title = reader.GetString(2);

                        BookListBox.Items.Add(b.BookId);
                    }
                }
            }
        }

        private void AuthorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aa = sender as ListBox;
            if (aa.SelectedItem != null)
            {
                string author = aa.SelectedItem.ToString();
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = "SELECT * FROM Author where Name = '" + author + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Author a = new Author();
                            a.Id = reader.GetInt32(0);
                            a.Name = reader.GetString(1);
                            a.Nationality = reader.GetString(2);

                            IdTextBox.Text = a.Id.ToString();
                            AuthorNameTextBox.Text = a.Name;
                            NationalityTextBox.Text = a.Nationality;
                        }
                    }
                }
            }
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bb = sender as ListBox;
            if (bb.SelectedItem != null)
            {
                string book = bb.SelectedItem.ToString();

                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = "SELECT * FROM Book where BookId = '" + book + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book b = new Book();
                            b.BookId = reader.GetInt32(0);
                            b.AuthorID = reader.GetInt32(1);
                            b.Title = reader.GetString(2);

                            BookIdTextBox.Text = b.BookId.ToString();
                            AuthorIdTextBox.Text = b.AuthorID.ToString();
                            TitleTextBox.Text = b.Title;
                        }
                    }
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Author SET AuthorId = '" + IdTextBox.Text + "' ," +
                         " Name = '" + AuthorNameTextBox.Text + "', Nationality = '" + NationalityTextBox.Text +
                         "' WHERE AuthorId = '" + IdTextBox.Text + "' ";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Author has been updated!");

                    GetAuthorData();

                    AuthorListBox.Items.RemoveAt(0);
                    AuthorListBox.Items.RemoveAt(0);
                    AuthorListBox.Items.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            #region
            //string cmdString = "UPDATE Author SET Name = @val2, Nationality = @val3, AuthorId = @val1 WHERE AuhorId=" + IdTextBox.Text;
            //string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlCommand comm = new SqlCommand(cmdString, conn))
            //    {
            //        comm.Connection = conn;
            //        comm.CommandText = cmdString;
            //        // Dispatcher.Invoke(() => AuthorIdTextBox);
            //        comm.Parameters.AddWithValue("@val1", IdTextBox.Text);
            //        comm.Parameters.AddWithValue("@val2", AuthorNameTextBox.Text);
            //        comm.Parameters.AddWithValue("@val3", NationalityTextBox.Text);

            //        conn.Open();
            //        comm.ExecuteNonQuery();
            //    }
            //}
            #endregion
        }

        private void SaveChangesBookButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Book SET BookId = '" + BookIdTextBox.Text + "' ," +
                         " AuthorID = '" + AuthorIdTextBox.Text + "', Title = '" + TitleTextBox.Text +
                         "' WHERE BookId = '" + BookIdTextBox.Text + "' ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Book has been updated!");

                    GetBookData();

                    BookListBox.Items.RemoveAt(0);
                    BookListBox.Items.RemoveAt(0);
                    BookListBox.Items.RemoveAt(0);
                    BookListBox.Items.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Author VALUES AuthorId = '" + IdTextBox.Text + "' ," + //hur skriva denna query?
                        " Name = '" + AuthorNameTextBox.Text + "', Nationality = '" + NationalityTextBox.Text +
                        "' WHERE AuthorId = '" + IdTextBox.Text + "' ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new Author has been created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "INSERT INTO Book VALUES BookId = '" + BookIdTextBox.Text + "' ," +
                        " AuthorID = '" + AuthorIdTextBox.Text + "', Title = '" + TitleTextBox.Text +
                        "' WHERE BookId = '" + BookIdTextBox.Text + "' ";
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

                            BookIdTextBox.Text = b.BookId.ToString();
                            AuthorIdTextBox.Text = b.AuthorID.ToString();
                            TitleTextBox.Text = b.Title;
                        }
                    }
                }
            }
        }

        private void DeleteAuthortButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE Author FROM AuthorId = '" + IdTextBox.Text + "' ," + //hur skriva denna query?
                        " Name = '" + AuthorNameTextBox.Text + "', Nationality = '" + NationalityTextBox.Text +
                        "' WHERE AuthorId = '" + IdTextBox.Text + "' ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new Author has need created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = "DELETE Book FROM BookId = '" + BookIdTextBox.Text + "' ," +
                        " AuthorID = '" + AuthorIdTextBox.Text + "', Title = '" + TitleTextBox.Text +
                        "' WHERE BookId = '" + BookIdTextBox.Text + "' ";
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

                            BookIdTextBox.Text = b.BookId.ToString();
                            AuthorIdTextBox.Text = b.AuthorID.ToString();
                            TitleTextBox.Text = b.Title;
                        }
                    }
                }
            }
        }
    }
}
