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
                var query = "SELECT Author FROM Author";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuthorListBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }


            //string connectionString1 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //using (SqlConnection con1 = new SqlConnection(connectionString1))
            //{
            //    con1.Open();
            //    var query = "SELECT Book title FROM Book";
            //    using (SqlCommand cmd = new SqlCommand(query, con1))
            //    {
            //        using (SqlDataReader reader1 = cmd.ExecuteReader())  // MÅSTE DÖPA OM COLUMNER SÅ DE EJ HAR MELLANRUM
            //        {
            //            while (reader1.Read())
            //            {
            //                BookListBox.Items.Add(reader1.GetString(0));
            //            }
            //        }
            //    }
            //}
        }

        private void AuthorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Gör samma för BookListBox
        {
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
                            column = new Dictionary<string, string>();
                            column["Birthday"] = reader["Birthday"].ToString();
                            column["Id"] = reader["Id"].ToString();
                            column["Author"] = reader["Author"].ToString();

                            Dispatcher.Invoke(() =>
                            {
                                AuthorTextBox.Text = reader.GetString(0);
                            });

                        }
                    }
                }
            }
        }
    }
}
