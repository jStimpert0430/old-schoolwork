using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jStimpert_Week13lab
{
    public partial class Form1 : Form
    {
        string prefix = "Total Records: ";
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'storeDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.storeDataSet.Products);
            label1.Text = prefix + bindingNavigator1.CountItem.Text.Substring(3);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                productsBindingSource.EndEdit();
                productsTableAdapter.Update(storeDataSet.Products);
                MessageBox.Show("Saved");
                this.productsTableAdapter.Fill(this.storeDataSet.Products);
                label1.Text = bindingNavigator1.CountItem.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void bindingNavigatorCountItem_Click(object sender, EventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bindingNavigatorMovePreviousItem.PerformClick();
            label1.Text = prefix + bindingNavigator1.CountItem.Text.Substring(3);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindingNavigatorMoveNextItem.PerformClick();
            label1.Text = prefix + bindingNavigator1.CountItem.Text.Substring(3);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bindingNavigatorAddNewItem.PerformClick();
            label1.Text = prefix + bindingNavigator1.CountItem.Text.Substring(3);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.PerformClick();
            label1.Text = prefix + bindingNavigator1.CountItem.Text.Substring(3);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            toolStripButton1.PerformClick();
        }
    }
}
