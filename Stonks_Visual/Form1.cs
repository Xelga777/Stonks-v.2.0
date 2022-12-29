using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Stonks_Visual
{
    public partial class Stonks_visual : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter sqlData = null;
        private DataSet dataSet = null;
        Timer timer1 = new Timer
        {
            //Interval = 30000

            // TEST INTERVAL:
            Interval = 10000
        };

        public Stonks_visual()
        {
            InitializeComponent();
        }

        private void LoadTableData()
        {
            sqlData = new SqlDataAdapter("SELECT CompanyName, StocksForSale, PriceForOneStock FROM Companies " +
            "SELECT CompanyName, StocksForSale AS [StocksPerson], PriceForOneStock FROM Stocks", sqlConnection);
            dataSet = new DataSet();
            sqlData.Fill(dataSet, "Companies");
            sqlData.Fill(dataSet, "Stocks");

            int minPrice, maxPrice, price, num;

            dataGridView1.Columns.Add("companyName", "Наименование предприятия");
            dataGridView1.Columns.Add("num", "Количество продаваемых акций");
            dataGridView1.Columns.Add("min_price", "Минимальная цена");
            dataGridView1.Columns.Add("max_price", "Максимальная цена");
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            for (int i = 0; i < dataSet.Tables["Companies"].Rows.Count; i++)
            {
                minPrice = 999999999;
                maxPrice = -1;
                num = 0;
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = dataSet.Tables["Companies"].Rows[i]["CompanyName"].ToString();

                num += (int)dataSet.Tables["Companies"].Rows[i]["StocksForSale"];
                price = (int)dataSet.Tables["Companies"].Rows[i]["PriceForOneStock"];

                if (price > maxPrice) maxPrice = price;
                if (price < minPrice) minPrice = price;

                for (int j = 0; j < dataSet.Tables["Stocks"].Rows.Count; j++)
                {
                    if (dataSet.Tables["Stocks"].Rows[j]["CompanyName"].ToString() == dataSet.Tables["Companies"].Rows[i]["CompanyName"].ToString())
                    {
                        num += (int)dataSet.Tables["Stocks"].Rows[j]["StocksForSale"];
                        price = (int)dataSet.Tables["Stocks"].Rows[i]["PriceForOneStock"];

                        if (price > maxPrice) maxPrice = price;
                        if (price < minPrice) minPrice = price;
                    }
                }

                string format = String.Format("{0} лабаксов", minPrice);

                dataGridView1.Rows[i].Cells[1].Value = num;
                dataGridView1.Rows[i].Cells[2].Value = format;

                format = String.Format("{0} лабаксов", maxPrice);
                dataGridView1.Rows[i].Cells[3].Value = format;
            }
        }

        private void Reload_Data()
        {
            MessageBox.Show("TimerIsOk");
            sqlData.Update(dataSet.Tables["Companies"]);
            sqlData.Update(dataSet.Tables["Stocks"]);

            int minPrice, maxPrice, price = 0, num;
            MessageBox.Show(dataGridView1.Rows.Count.ToString());
            MessageBox.Show(dataSet.Tables["Companies"].Rows.Count.ToString());
            if (dataGridView1.Rows.Count < dataSet.Tables["Companies"].Rows.Count)
            {
                MessageBox.Show("Row added");
                dataGridView1.Rows.Add();
            }
            for (int i = 0; i < dataSet.Tables["Companies"].Rows.Count; i++)
            {
                minPrice = 999999999;
                maxPrice = -1;
                num = 0;
                
                

                dataGridView1.Rows[i].Cells[0].Value = dataSet.Tables["Companies"].Rows[i]["CompanyName"].ToString();
                num += (int)dataSet.Tables["Companies"].Rows[i]["StocksForSale"];
                price = (int)dataSet.Tables["Companies"].Rows[i]["PriceForOneStock"];
                

                

                if (price > maxPrice) maxPrice = price;
                if (price < minPrice) minPrice = price;

                for (int j = 0; j < dataSet.Tables["Stocks"].Rows.Count; j++)
                {
                    if (dataSet.Tables["Stocks"].Rows[j]["CompanyName"].ToString() == dataSet.Tables["Companies"].Rows[i]["CompanyName"].ToString())
                    {
                        num += (int)dataSet.Tables["Stocks"].Rows[j]["StocksForSale"];
                        price = (int)dataSet.Tables["Stocks"].Rows[i]["PriceForOneStock"];

                        if (price > maxPrice) maxPrice = price;
                        if (price < minPrice) minPrice = price;
                    }
                }

                string format = String.Format("{0} лабаксов", minPrice);

                dataGridView1.Rows[i].Cells[1].Value = num;
                dataGridView1.Rows[i].Cells[2].Value = format;

                format = String.Format("{0} лабаксов", maxPrice);
                dataGridView1.Rows[i].Cells[3].Value = format;
            }
        }

        private void Stonks_visual_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Stonks"].ConnectionString);
            sqlConnection.Open();

            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Подключение с базой данных не установлено!");
            } else
            {
                LoadTableData();
                timer1.Enabled = true;
                timer1.Tick += new System.EventHandler(OnTimerEvent);
                
            }

            
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            Reload_Data();
        }
    }
}
