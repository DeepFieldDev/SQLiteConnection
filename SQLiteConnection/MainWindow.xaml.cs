using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace SQLiteExampleConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public IList<string> produceList;

        public void FetchProduce(string producetype)
        {
            DataTable dt = new DataTable();
            string datasource = "Data Source='C:\\Users\\Robert\\Documents\\Visual Studio 2015\\Projects\\SQLiteConnection\\SQLiteConnection\\ProjectDB.db';";

            using (SQLiteConnection conn = new SQLiteConnection(datasource))
            {
                string sql = $"SELECT * From Produce{producetype};";
                Console.WriteLine(sql);
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                da.Fill(dt);
                conn.Close();
            }

            produceList = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                produceList.Add(row[1].ToString());
            }

            populateList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string producetype = comboBox.Text;
            if (comboBox.SelectedIndex != -1)
            {
                //If the combobox selectedIndex is anything but -1, return only the produce type selected
                FetchProduce($" WHERE ProduceType='{producetype}'");
                
            }else
            {
                //else, return all produce in the table
                FetchProduce(string.Empty);
            };
        }

        void populateList()
        {
            listView.Items.Clear();
            foreach (string str in produceList)
            {
                listView.Items.Add(str);
            }
        }

        private void clearComboBox_Click(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            listView.Items.Clear();
        }
    }
}
