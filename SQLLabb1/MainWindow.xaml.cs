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
        //Method to update Author listbox info.
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

                        AuthorListBox.Items.Add(a);
                    }
                }
            }
        }
        //Method to update Book listbox info
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
                        b.AuthorID = reader.GetInt32(1);
                        b.Title = reader.GetString(2);

                        BookListBox.Items.Add(b);
                    }
                }
            }
        }

        private void AuthorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteAuthortButton.IsEnabled = true;
            SaveChangesButton.IsEnabled = true;

            var aa = sender as ListBox;
            if (aa.SelectedItem != null)
            {
                var author = aa.SelectedItem as Author;

                IdTextBox.Text = author.Id.ToString();
                AuthorNameTextBox.Text = author.Name;
                NationalityTextBox.Text = author.Nationality;
            }
            Author a = new Author();
            Book b = new Book();
            if (a.Id != b.AuthorID)
                DeleteBookButton.IsEnabled = false;
        }
        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteBookButton.IsEnabled = true;
            SaveChangesBookButton.IsEnabled = true;

            var bb = sender as ListBox;
            if (bb.SelectedItem != null)
            {
                string book = bb.SelectedItem.ToString();

                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = "SELECT * FROM Book where Title = '" + book + "'";
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

                    string query = "UPDATE Author SET " + " Name = '" + AuthorNameTextBox.Text + "', Nationality = '" + NationalityTextBox.Text + "' WHERE AuthorId = '" + IdTextBox.Text + "' ";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Author has been updated!");

                    GetAuthorData();

                    for (int i = 0; i < AuthorListBox.Items.Count; i++)
                    {
                        AuthorListBox.Items.RemoveAt(0);
                    }
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
        private void SaveChangesBookButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Book SET BookId = '" + BookIdTextBox.Text + "' ," + " AuthorID = '" + AuthorIdTextBox.Text + "', Title = '" + TitleTextBox.Text + "' WHERE BookId = '" + BookIdTextBox.Text + "' ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Book has been updated!");

                    GetBookData();

                    for (int i = 0; i < BookListBox.Items.Count; i++)
                    {
                        BookListBox.Items.RemoveAt(0);
                    }
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
                    string query = "INSERT INTO Author (Name, Nationality) VALUES (@Name, @Nationality)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Name", AuthorNameTextBox.Text);
                    command.Parameters.AddWithValue("Nationality", NationalityTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new Author has been created!");

                    AuthorListBox.Items.Clear();
                    GetAuthorData();
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Book (AuthorID, Title) VALUES (@AuthorID, @Title)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("AuthorID", AuthorIdTextBox.Text);
                    command.Parameters.AddWithValue("Title", TitleTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new Book has been created!");

                    BookListBox.Items.Clear();
                    GetBookData();
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

        private void DeleteAuthortButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Author a = (Author)AuthorListBox.SelectedItem;
                    string query = "DELETE FROM Author WHERE AuthorId = " + a.Id + " ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("The Author has been deleted!");

                    GetAuthorData();
                    for (int i = 0; i < AuthorListBox.Items.Count; i++)
                    {
                        AuthorListBox.Items.RemoveAt(0);
                    }

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Book WHERE Title = '" + BookListBox.SelectedItem + "' ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("The Book has been deleted!");

                    GetAuthorData();
                    for (int i = 0; i < BookListBox.Items.Count; i++)
                    {
                        BookListBox.Items.RemoveAt(0);
                    }

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

        private void AuthorNameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddAuthorButton.IsEnabled = true;
            AuthorNameTextBox.Text = string.Empty;
            NationalityTextBox.Text = string.Empty;
        }

        private void NationalityTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddAuthorButton.IsEnabled = true;
            AuthorNameTextBox.Text = string.Empty;
            NationalityTextBox.Text = string.Empty;
        }

        private void TitleTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddBookButton.IsEnabled = true;
            TitleTextBox.Text = string.Empty;
        }
    }
}
