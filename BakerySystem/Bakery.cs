using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakerySystem
{
    public partial class Bakery : Form
    {
        public Bakery()
        {
            InitializeComponent();
            DisplayElements("ProductTbl",ProductsDGV);
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lahiru sujith\OneDrive\Documents\BakeryDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(1);
            DisplayElements("CustomerTbl",CustomersDGV);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
            DisplayElements("ProductTbl",ProductsDGV);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(2);
            DisplayElements("CategoryTbl", CategoryDGV);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(3);
            DisplayElements("ProductTbl", BProductDGV);
            DisplayElements("SalesTbl", BillingListDGV);
            GetCustomer();

        }

        private void label12_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(4);

        }

        private void label11_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(5);
            DisplayElements("EmployeeTbl", EmployeesDGV);


        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddressTb.Text == "" || CPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CustomerTbl(CustName,CustPhone,CustAddress) values(@CN,@CP,@CA)", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully...!!");
                    Con.Close();
                    DisplayElements("CustomerTbl",CustomersDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DisplayElements(string TName, Bunifu.UI.WinForms.BunifuDataGridView DGV)
        {
            Con.Open();
            string Query = "select * from " + TName + "";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(ProdNameTb.Text == "" || QuantityTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ProductTbl(ProdName,ProdCat,ProdPrice,ProdQty) values(@PN,@PC,@PP,@PQ)", Con);
                    cmd.Parameters.AddWithValue("@PN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", QuantityTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added...!!");
                    Con.Close();
                    DisplayElements("ProductTbl", ProductsDGV);

                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        int key = 0;
        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            QuantityTb.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(ProdNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ProdNameTb.Text == "" || QuantityTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set ProdName=@PN,ProdCat=@PC,ProdPrice=@PP,ProdQty=@PQ where ProdId=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@PN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", QuantityTb.Text);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated...!!");
                    Con.Close();
                    DisplayElements("ProductTbl",ProductsDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Ckey = 0;
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CAddressTb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (CNameTb.Text == "")
            {
                Ckey = 0;
            }
            else
            {
                Ckey = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Informations...!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete ProductTbl where ProdId=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted...!!");
                    Con.Close();
                    DisplayElements("ProductTbl", ProductsDGV);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            
        }

        private void DelCustBtn_Click(object sender, EventArgs e)
        {
            if (Ckey == 0)
            {
                MessageBox.Show("Missing Informations...!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete CustomerTbl where CustId=@Ckey", Con);
                    cmd.Parameters.AddWithValue("@Ckey", Ckey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted...!!");
                    Con.Close();
                    DisplayElements("CustomerTbl", CustomersDGV);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void EditCustBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CPhoneTb.Text == "" || CAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName=@CN,CustPhone=@CP,CustAddress=@CA where CustId=@Ckey", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                    cmd.Parameters.AddWithValue("@Ckey", Ckey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated...!!");
                    Con.Close();
                    DisplayElements("CustomerTbl", CustomersDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EAddressTb.Text == "" || EPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into EmployeeTbl(EmpName,EmpPhone,EmpAddress) values(@EN,@EP,@EA)", Con);
                    cmd.Parameters.AddWithValue("@EN", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EAddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added Successfully...!!");
                    Con.Close();
                    DisplayElements("EmployeeTbl", EmployeesDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Ekey = 0;
        private void DeleteEmpBtn_Click(object sender, EventArgs e)
        {
            if (Ekey == 0)
            {
                MessageBox.Show("Missing Informations...!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete EmployeeTbl where EmpId=@Ekey", Con);
                    cmd.Parameters.AddWithValue("@Ekey", Ekey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted...!!");
                    Con.Close();
                    DisplayElements("EmployeeTbl", EmployeesDGV);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ENameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            EAddressTb.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ENameTb.Text == "")
            {
                Ekey = 0;
            }
            else
            {
                Ekey = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditEmBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EPhoneTb.Text == "" || EAddressTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EmpName=@EN,EmpPhone=@EP,EmpAddress=@EA where EmpId=@Ekey", Con);
                    cmd.Parameters.AddWithValue("@EN", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EAddressTb.Text);
                    cmd.Parameters.AddWithValue("@Ekey", Ekey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated...!!");
                    Con.Close();
                    DisplayElements("EmployeeTbl", EmployeesDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void AddCatBtn_Click(object sender, EventArgs e)
        {
            if (CatNameTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CategoryTbl(CatName) values(@CATN)", Con);
                    cmd.Parameters.AddWithValue("@CatN", CatNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added Successfully...!!");
                    Con.Close();
                    DisplayElements("CategoryTbl", CategoryDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Catkey = 0;
        private void EditCatBtn_Click(object sender, EventArgs e)
        {
            if (CatNameTb.Text == "")
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CategoryTbl set CatName=@CatN where CatId=@Catkey", Con);
                    cmd.Parameters.AddWithValue("@CatN", CatNameTb.Text);
                    cmd.Parameters.AddWithValue("@Catkey", Catkey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Updated...!!");
                    Con.Close();
                    DisplayElements("CategoryTbl", CategoryDGV);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CategoryDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatNameTb.Text = CategoryDGV.SelectedRows[0].Cells[1].Value.ToString();
            if (CatNameTb.Text == "")
            {
                Catkey = 0;
            }
            else
            {
                Catkey = Convert.ToInt32(CategoryDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteCatBtn_Click(object sender, EventArgs e)
        {
            if (Catkey == 0)
            {
                MessageBox.Show("Missing Informations...!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete CategoryTbl where CatId=@Catkey", Con);
                    cmd.Parameters.AddWithValue("@Catkey", Catkey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted...!!");
                    Con.Close();
                    DisplayElements("CategoryTbl", CategoryDGV);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void BillingDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int BPkey = 0;
        int Stock = 0;
        private void BProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BProdNameTb.Text = BProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            BPriceTb.Text = BProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (BProdNameTb.Text == "")
            {
                BPkey = 0;
                Stock = 0;
            }
            else
            {
                BPkey = Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[0].Value.ToString());
                Stock = Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[2].Value.ToString());

            }
        }

        int n = 0;
        int GrdTotal = 0;
        private void AddBillBtn_Click(object sender, EventArgs e)
        {
            if (BQtyTb.Text == "")
            {
                MessageBox.Show("Enter the Quantity");
            }
            else if (Convert.ToInt32(BQtyTb.Text) > Stock)
            {
                MessageBox.Show("No Enough Stock");

            }
            else
            {
                int Total = Convert.ToInt32(BQtyTb.Text) * Convert.ToInt32(BPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(YourBillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BProdNameTb.Text;
                newRow.Cells[2].Value = BQtyTb.Text;
                newRow.Cells[3].Value = BPriceTb.Text;
                newRow.Cells[4].Value = Total;
                YourBillDGV.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + Total;
                GrdTotalLbl.Text = "Rs " + GrdTotal+" /=";
            }
        }

        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            CustomerCb.ValueMember = "CustId";
            CustomerCb.DataSource = dt;
            Con.Close();
        }
        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            BProdNameTb.Text = "";
            BPriceTb.Text = "";
            BQtyTb.Text = "";
        }

        private void SaveBillTb_Click(object sender, EventArgs e)
        {
            if (CustomerCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information..!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into SalesTbl(Customer,SAmount,SDate) values(@CN,@SA,@SD)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustomerCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SA", GrdTotal);
                    cmd.Parameters.AddWithValue("@SD", DateTime.Today.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sales Added Successfully...!!");
                    Con.Close();
                    DisplayElements("SalesTbl", BillingListDGV);
                    BProdNameTb.Text = "";
                    BPriceTb.Text = "";
                    BQtyTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
