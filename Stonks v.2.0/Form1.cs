using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Stonks_v._2._0
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter sqlData_Accounts = null;
        private SqlDataAdapter sqlData_Stocks = null;
        private SqlDataAdapter sqlData_Companies = null;

        private SqlCommandBuilder builder_Accounts = null;
        private SqlCommandBuilder builder_Stocks = null;
        private SqlCommandBuilder builder_Companies = null;

        private DataSet dataSet_Accounts = null;
        private DataSet dataSet_Stocks = null;
        private DataSet dataSet_Companies = null;

        private bool newRowAdding = false;
        private bool newRowAdding2 = false;
        private bool newRowAdding3 = false;

        private readonly string OwnersName = "Имя владельца счета";
        private readonly string ID = "Номер счета";
        private readonly string Value = "Количество средств на счету";
        private readonly string Comment = "Комментарий";
        private readonly string Command = "Команда";
        private readonly string companyName = "Наименование предприятия";
        private readonly string StocksNum = "Количество имеющихся акций";
        private readonly string StocksForSale = "Количество акций, выставленных на продажу";
        private readonly string PriceForOneStock = "Цена за одну акцию";
        private readonly string CompanysIncomePerCycle = "Доход предприятия за этот цикл";

        public Form1()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            try
            {
                // Accounts table
                //sqlData_Accounts = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Accounts", sqlConnection);
                sqlData_Accounts = new SqlDataAdapter("SELECT ID AS [Номер счета], OwnersName AS [Имя владельца счета]," +
                    "Value AS [Количество средств на счету], Comment AS [Комментарий], 'Delete' AS [Команда] FROM Accounts", sqlConnection);
                builder_Accounts = new SqlCommandBuilder(sqlData_Accounts);

                builder_Accounts.GetInsertCommand();
                builder_Accounts.GetUpdateCommand();
                builder_Accounts.GetDeleteCommand();

                dataSet_Accounts = new DataSet();
                sqlData_Accounts.Fill(dataSet_Accounts, "Accounts");
                dataGridView1.DataSource = dataSet_Accounts.Tables["Accounts"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[dataGridView1.ColumnCount - 1, i] = linkCell;
                }
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


                // Stocks table
                string format = String.Format("SELECT ID, CompanyName AS [{0}], OwnersName AS [{1}], AccountID AS [{2}], StocksNum AS [{3}], " +
                    "StocksForSale AS [{4}], PriceForOneStock AS [{5}], Comment AS [{6}], 'Delete' AS [{7}] FROM Stocks", companyName, OwnersName,
                    ID, StocksNum, StocksForSale, PriceForOneStock, Comment, Command);
                sqlData_Stocks = new SqlDataAdapter(format, sqlConnection);
                builder_Stocks = new SqlCommandBuilder(sqlData_Stocks);
                builder_Stocks.GetInsertCommand();
                builder_Stocks.GetUpdateCommand();
                builder_Stocks.GetDeleteCommand();

                dataSet_Stocks = new DataSet();
                sqlData_Stocks.Fill(dataSet_Stocks, "Stocks");
                dataGridView2.DataSource = dataSet_Stocks.Tables["Stocks"];

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell1 = new DataGridViewLinkCell();
                    dataGridView2[dataGridView2.ColumnCount - 1, i] = linkCell1;
                }
                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


                // Company table
                sqlData_Companies = new SqlDataAdapter("SELECT CompanyName AS [Наименование предприятия], StocksForSale AS [Количество акций, выставленных на продажу]," +
                    "PriceForOneStock AS [Цена за одну акцию], CompanysIncomePerCycle AS [Доход предприятия за этот цикл], 'Delete' AS [Команда] FROM Companies", sqlConnection);
                builder_Companies = new SqlCommandBuilder(sqlData_Companies);

                builder_Companies.GetInsertCommand();
                builder_Companies.GetUpdateCommand();
                builder_Companies.GetDeleteCommand();

                dataSet_Companies = new DataSet();
                sqlData_Companies.Fill(dataSet_Companies, "Companies");
                dataGridView3.DataSource = dataSet_Companies.Tables["Companies"];

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView3[dataGridView3.ColumnCount - 1, i] = linkCell;
                }
                dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                //Information table
                Information_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка изначальной загрузки данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reload_Accounts_Data()
        {
            try
            {
                dataSet_Accounts.Tables["Accounts"].Clear();
                sqlData_Accounts.Fill(dataSet_Accounts, "Accounts");
                dataGridView1.DataSource = dataSet_Accounts.Tables["Accounts"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[dataGridView1.ColumnCount - 1, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reload_Stocks_Data()
        {
            try
            {
                dataSet_Stocks.Tables["Stocks"].Clear();
                sqlData_Stocks.Fill(dataSet_Stocks, "Stocks");
                dataGridView2.DataSource = dataSet_Stocks.Tables["Stocks"];

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView2[dataGridView2.ColumnCount - 1, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reload_Companies_Data()
        {
            try
            {
                dataSet_Companies.Tables["Companies"].Clear();
                sqlData_Companies.Fill(dataSet_Companies, "Companies");
                dataGridView3.DataSource = dataSet_Companies.Tables["Companies"];

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView3[dataGridView3.ColumnCount - 1, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Stonks"].ConnectionString);
            sqlConnection.Open();

            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Подключение с базой данных не установлено!");
            }

            Load_Data();

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Reload_Accounts_Data();
        }

        private void UpdateStocksButton_Click(object sender, EventArgs e)
        {
            Reload_Stocks_Data();
        }

        private void UpdateCompaniesButton_Click(object sender, EventArgs e)
        {
            Reload_Companies_Data();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string task = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                if (task == "Delete")
                {
                    if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        int rowIndex = e.RowIndex;
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        dataSet_Accounts.Tables["Accounts"].Rows[rowIndex].Delete();
                        sqlData_Accounts.Update(dataSet_Accounts, "Accounts");
                    }
                } else if (task == "Update")
                {
                    int r = e.RowIndex;

                    dataSet_Accounts.Tables["Accounts"].Rows[r][OwnersName] = dataGridView1.Rows[r].Cells[OwnersName].Value;
                    dataSet_Accounts.Tables["Accounts"].Rows[r][Value] = dataGridView1.Rows[r].Cells[Value].Value;
                    dataSet_Accounts.Tables["Accounts"].Rows[r][Comment] = dataGridView1.Rows[r].Cells[Comment].Value;

                    sqlData_Accounts.Update(dataSet_Accounts, "Accounts");
                    dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns.Count - 1].Value = "Delete";

                } else if (task == "Insert")
                {
                    int rowIndex = dataGridView1.Rows.Count - 2;
                    DataRow row = dataSet_Accounts.Tables["Accounts"].NewRow();

                    row[OwnersName] = dataGridView1.Rows[rowIndex].Cells[OwnersName].Value;
                    row[Value] = dataGridView1.Rows[rowIndex].Cells[Value].Value;
                    row[Comment] = dataGridView1.Rows[rowIndex].Cells[Comment].Value;

                    dataSet_Accounts.Tables["Accounts"].Rows.Add(row);
                    dataSet_Accounts.Tables["Accounts"].Rows.RemoveAt(dataSet_Accounts.Tables["Accounts"].Rows.Count - 1);
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                    dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns.Count - 1].Value = "Delete";

                    sqlData_Accounts.Update(dataSet_Accounts, "Accounts");
                    newRowAdding = false;

                } else
                {
                    MessageBox.Show("Неизвестная команда", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Reload_Accounts_Data();
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;

                    int last_row = dataGridView1.RowCount - 2;
                    DataGridViewRow row = dataGridView1.Rows[last_row];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[dataGridView1.ColumnCount - 1, last_row] = linkCell;
                    row.Cells[Command].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[dataGridView1.ColumnCount - 1, rowIndex] = linkCell;
                    editingRow.Cells[Command].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                string task = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();

                if (task == "Delete")
                {
                    if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        int rowIndex = e.RowIndex;
                        dataGridView2.Rows.RemoveAt(rowIndex);
                        dataSet_Stocks.Tables["Stocks"].Rows[rowIndex].Delete();
                        sqlData_Stocks.Update(dataSet_Stocks, "Stocks");
                    }
                }
                else if (task == "Update")
                {
                    int r = e.RowIndex;

                    dataSet_Stocks.Tables["Stocks"].Rows[r][companyName] = dataGridView2.Rows[r].Cells[companyName].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][OwnersName] = dataGridView2.Rows[r].Cells[OwnersName].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][ID] = dataGridView2.Rows[r].Cells[ID].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][StocksNum] = dataGridView2.Rows[r].Cells[StocksNum].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][StocksForSale] = dataGridView2.Rows[r].Cells[StocksForSale].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][PriceForOneStock] = dataGridView2.Rows[r].Cells[PriceForOneStock].Value;
                    dataSet_Stocks.Tables["Stocks"].Rows[r][Comment] = dataGridView2.Rows[r].Cells[Comment].Value;

                    sqlData_Stocks.Update(dataSet_Stocks, "Stocks");
                    dataGridView2.Rows[e.RowIndex].Cells[dataGridView2.Columns.Count - 1].Value = "Delete";

                }
                else if (task == "Insert")
                {
                    int rowIndex = dataGridView2.Rows.Count - 2;
                    DataRow row = dataSet_Stocks.Tables["Stocks"].NewRow();

                    row[companyName] = dataGridView2.Rows[rowIndex].Cells[companyName].Value;
                    row[OwnersName] = dataGridView2.Rows[rowIndex].Cells[OwnersName].Value;
                    row[ID] = dataGridView2.Rows[rowIndex].Cells[ID].Value;
                    row[StocksNum] = dataGridView2.Rows[rowIndex].Cells[StocksNum].Value;
                    row[StocksForSale] = dataGridView2.Rows[rowIndex].Cells[StocksForSale].Value;
                    row[PriceForOneStock] = dataGridView2.Rows[rowIndex].Cells[PriceForOneStock].Value;
                    row[Comment] = dataGridView2.Rows[rowIndex].Cells[Comment].Value;

                    dataSet_Stocks.Tables["Stocks"].Rows.Add(row);
                    dataSet_Stocks.Tables["Stocks"].Rows.RemoveAt(dataSet_Stocks.Tables["Stocks"].Rows.Count - 1);
                    dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 2);
                    dataGridView2.Rows[e.RowIndex].Cells[dataGridView2.Columns.Count - 1].Value = "Delete";

                    sqlData_Stocks.Update(dataSet_Stocks, "Stocks");
                    newRowAdding2 = false;

                }
                else
                {
                    MessageBox.Show("Неизвестная команда", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Reload_Stocks_Data();
            }
        }

        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding2 == false)
                {
                    newRowAdding2 = true;

                    int last_row = dataGridView2.RowCount - 2;
                    DataGridViewRow row = dataGridView2.Rows[last_row];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView2[dataGridView2.ColumnCount - 1, last_row] = linkCell;
                    row.Cells[Command].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding2 == false)
                {
                    int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow editingRow = dataGridView2.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView2[dataGridView2.ColumnCount - 1, rowIndex] = linkCell;
                    editingRow.Cells[Command].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string task = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();

                if (task == "Delete")
                {
                    if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        int rowIndex = e.RowIndex;
                        dataGridView3.Rows.RemoveAt(rowIndex);
                        dataSet_Companies.Tables["Companies"].Rows[rowIndex].Delete();
                        sqlData_Companies.Update(dataSet_Companies, "Companies");
                    }
                }
                else if (task == "Update")
                {
                    bool hasIncome = false;
                    int r = e.RowIndex;

                    if (dataSet_Companies.Tables["Companies"].Rows[r][CompanysIncomePerCycle] != dataGridView3.Rows[r].Cells[CompanysIncomePerCycle].Value)
                    {
                        hasIncome = true;
                    }

                    dataSet_Companies.Tables["Companies"].Rows[r][companyName] = dataGridView3.Rows[r].Cells[companyName].Value;
                    dataSet_Companies.Tables["Companies"].Rows[r][StocksForSale] = dataGridView3.Rows[r].Cells[StocksForSale].Value;
                    dataSet_Companies.Tables["Companies"].Rows[r][PriceForOneStock] = dataGridView3.Rows[r].Cells[PriceForOneStock].Value;
                    dataSet_Companies.Tables["Companies"].Rows[r][CompanysIncomePerCycle] = dataGridView3.Rows[r].Cells[CompanysIncomePerCycle].Value;


                    sqlData_Companies.Update(dataSet_Companies, "Companies");
                    dataGridView3.Rows[e.RowIndex].Cells[dataGridView3.Columns.Count - 1].Value = "Delete";

                    if (hasIncome && (MessageBox.Show("Произвести начисление депозитов?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        DepositAccrual(dataSet_Companies.Tables["Companies"].Rows[r][companyName].ToString());
                    }
                }
                else if (task == "Insert")
                {
                    int rowIndex = dataGridView3.Rows.Count - 2;
                    DataRow row = dataSet_Companies.Tables["Companies"].NewRow();

                    row[companyName] = dataGridView3.Rows[rowIndex].Cells[companyName].Value;
                    row[StocksForSale] = dataGridView3.Rows[rowIndex].Cells[StocksForSale].Value;
                    row[PriceForOneStock] = dataGridView3.Rows[rowIndex].Cells[PriceForOneStock].Value;
                    row[CompanysIncomePerCycle] = dataGridView3.Rows[rowIndex].Cells[CompanysIncomePerCycle].Value;

                    dataSet_Companies.Tables["Companies"].Rows.Add(row);
                    dataSet_Companies.Tables["Companies"].Rows.RemoveAt(dataSet_Companies.Tables["Companies"].Rows.Count - 1);
                    dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 2);
                    dataGridView3.Rows[e.RowIndex].Cells[dataGridView3.Columns.Count - 1].Value = "Delete";

                    sqlData_Companies.Update(dataSet_Companies, "Companies");
                    newRowAdding3 = false;

                }
                else
                {
                    MessageBox.Show("Неизвестная команда", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Reload_Companies_Data();
            }
        }

        private void dataGridView3_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding3 == false)
                {
                    newRowAdding3 = true;

                    int last_row = dataGridView3.RowCount - 2;
                    DataGridViewRow row = dataGridView3.Rows[last_row];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView3[dataGridView3.ColumnCount - 1, last_row] = linkCell;
                    row.Cells[Command].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding3 == false)
                {
                    int rowIndex = dataGridView3.SelectedCells[0].RowIndex;
                    DataGridViewRow editingRow = dataGridView3.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView3[dataGridView3.ColumnCount - 1, rowIndex] = linkCell;
                    editingRow.Cells[Command].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void DepositAccrual(string _company_name)
        {
            int row = 0;
            int value = 0;
            int account_id = 0;
            int account_value = 0;
            int stocks_num = 0;

            for (int i = 0; i < dataSet_Companies.Tables["Companies"].Rows.Count; i++)
            {
                if (dataSet_Companies.Tables["Companies"].Rows[i][companyName].ToString() == _company_name)
                {
                    row = i;
                    break;
                }
            }

            value = (int)dataSet_Companies.Tables["Companies"].Rows[row][CompanysIncomePerCycle];

            for (int i = 0; i < dataSet_Stocks.Tables["Stocks"].Rows.Count; i++)
            {
                if (dataSet_Stocks.Tables["Stocks"].Rows[i][companyName].ToString() == _company_name)
                {
                    account_id = (int)dataSet_Stocks.Tables["Stocks"].Rows[i][ID];
                    stocks_num = (int)dataSet_Stocks.Tables["Stocks"].Rows[i][StocksNum];

                    for (int j = 0; j < dataSet_Accounts.Tables["Accounts"].Rows.Count; j++)
                    {
                        if ((int)dataSet_Accounts.Tables["Accounts"].Rows[j][ID] == account_id)
                        {
                            account_value = (int)dataSet_Accounts.Tables["Accounts"].Rows[j][Value];
                            account_value += (int)((float)stocks_num / 100 * value);

                            dataSet_Accounts.Tables["Accounts"].Rows[j][Value] = account_value;
                        }
                    }
                }
            }

            sqlData_Accounts.Update(dataSet_Accounts, "Accounts");
            Reload_Accounts_Data();
            Reload_Stocks_Data();
            Reload_Companies_Data();

            MessageBox.Show(String.Format("Произведено начисление депозитов для всех владельцев акций компании {0}", _company_name));
        }

        private void Update_all_Click(object sender, EventArgs e)
        {
            Reload_Accounts_Data();
            Reload_Stocks_Data();
            Reload_Companies_Data();

        }

        private void Information_Load()
        {
            string _companyName;
            string format;

            dataGridView4.Columns.Add("CompanyName", companyName);
            dataGridView4.Columns.Add("Offer_0", "Предложения от компании");

            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                int _columns_num = 2;

                _companyName = dataGridView3.Rows[i].Cells[0].Value.ToString();

                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = dataGridView3.Rows[i].Cells[0].Value;

                int stocks_num = (int)dataGridView3.Rows[i].Cells[StocksForSale].Value;
                int price = (int)dataSet_Companies.Tables["Companies"].Rows[i][PriceForOneStock];

                if (stocks_num != 0)
                {
                    format = String.Format("{0} шт. - за {1} лабаксов", stocks_num, price);
                    dataGridView4.Rows[i].Cells[1].Value = format;
                }
                else
                {
                    dataGridView4.Rows[i].Cells[1].Value = "Владелец компании больше не продает акции";
                }

                for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                {
                    if (dataGridView2.Rows[j].Cells[companyName].Value.ToString() == dataGridView4.Rows[i].Cells[0].Value.ToString())
                    {
                        stocks_num = (int)dataGridView2.Rows[j].Cells[StocksForSale].Value;
                        price = (int)dataGridView2.Rows[j].Cells[PriceForOneStock].Value;

                        if (stocks_num != 0)
                        {
                            _columns_num += 1;
                            format = String.Format("{0} шт. - за {1} лабаксов", stocks_num, price);
                            if (_columns_num > dataGridView4.Columns.Count)
                            {
                                dataGridView4.Columns.Add("Offer_" + (_columns_num - 2).ToString(), "Предложение " + (_columns_num - 2).ToString());

                            }
                            dataGridView4.Rows[i].Cells[_columns_num - 1].Value = format;
                        }
                    }
                }
            }

            dataGridView4.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
