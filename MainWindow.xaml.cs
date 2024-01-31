using System;
using System.Collections.Generic;
using System.Data;
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


namespace ASP.NET_Task_1
{


    public class NewBookData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public int YearPages { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
    }
    public partial class MainWindow : Window
    {
        NewBookData newBookData = new NewBookData();
        public MainWindow()
        {
            InitializeComponent();
        }


        public void ShowAllBoks()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library1;Integrated Security=True;";


            using (var conn = new SqlConnection())
            {
                List<NewBookData> booksAll = new List<NewBookData>();
                conn.ConnectionString = connectionString;
                conn.Open();


                SqlDataReader reader = null;


                string query = "SELECT * FROM Books";


                using (var command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newBookData = new NewBookData();
                        newBookData.Id = int.Parse(reader[0].ToString());
                        newBookData.Name = reader[1].ToString();
                        newBookData.Pages = int.Parse(reader[2].ToString());
                        newBookData.YearPages = int.Parse(reader[3].ToString());
                        newBookData.Comment = reader[4].ToString();
                        newBookData.Quantity = int.Parse(reader[5].ToString());


                        booksAll.Add(newBookData);

                    }
                }


                booksList.ItemsSource = booksAll;
            }


        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library1;Integrated Security=True;"))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO Books(Id, Name, Pages, YearPress, Comment, Quantity) " +
                                   "VALUES(@id, @name, @pages, @yearpress, @comment, @quantity)";

                    using (var command = new SqlCommand(query, conn))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(idTxb.Text);
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTxb.Text;
                        command.Parameters.Add("@pages", SqlDbType.Int).Value = int.Parse(pagesTxb.Text);
                        command.Parameters.Add("@yearpress", SqlDbType.Int).Value = int.Parse(yearPressTxb.Text);
                        command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = commentTxb.Text;
                        command.Parameters.Add("@quantity", SqlDbType.Int).Value = int.Parse(quantityTxb.Text);

                        var result = command.ExecuteNonQuery();
                        Console.WriteLine($"{result} row affected");
                        MessageBox.Show("New Book Added", "BOOK", MessageBoxButton.OK);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }







        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowAllBoks();
        }
    }



}