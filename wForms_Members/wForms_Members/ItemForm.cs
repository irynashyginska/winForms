using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wForms_Members
{
    public partial class ItemForm : Form
    {
        private Member member;
        private int index;
        private bool check = false;
        private Form1 form;

        public ItemForm(Member m,int i,Form1 f)
        {
            InitializeComponent();
            this.member = m;
            this.index = i;
            this.form = f;
        }
        public ItemForm(Form1 f)
        {
            this.form = f;
            this.check = true;
            InitializeComponent();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            if (!this.check)
            {
                for_editing();
            }
            else
                button1.Visible = false;
        }
        private void for_editing()
        {
            
            button2.Visible = false;
            textBox1.Text = member.Surname;
            textBox2.Text = member.Number.ToString();
            textBox3.Text = member.Report;
            textBox4.Text = member.Time;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.member.Surname = textBox1.Text;
                this.member.Number = int.Parse(textBox2.Text);
                this.member.Report = textBox3.Text;
                this.member.Time = textBox4.Text;
                this.form.update_row(this.index, this.member);
                this.form.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Check your incoming data");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.member = new Member();
                this.member.Surname = textBox1.Text;
                this.member.Number = int.Parse(textBox2.Text);
                this.member.Report = textBox3.Text;
                this.member.Time = textBox4.Text;
                this.form.add_row(this.member);
                this.form.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Check your incoming data");
            }
        }
    }
}
