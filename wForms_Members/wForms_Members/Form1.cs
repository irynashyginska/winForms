using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
namespace wForms_Members
{
    
    public partial class Form1 : Form
    {
        private List<Member> l;
        string filename;
        public Form1()
        {

            Thread thrd = new Thread(start_splash);
            thrd.Start();
            Thread.Sleep(5000);
            this.l = new List<Member>();
            InitializeComponent();
            thrd.Abort();
            table.Visible = false;
        }
        private void start_splash()
        {
            Application.Run(new SplashScreen());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupMenuStrip();
            SetupLayout();
        }
        private void SetupDataGridView()
        {

            this.Size = new Size(900, 500);
            table.ColumnCount = 4;
            table.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            table.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            table.ColumnHeadersDefaultCellStyle.Font =
                new Font(table.Font, FontStyle.Bold);
            table.Size = new Size(400, 250);
            table.Location = new Point(250, 100);
            table.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            table.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            table.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            table.GridColor = Color.Black;
            table.RowHeadersVisible = false;

            table.Columns[0].Name = "Surname";
            table.Columns[1].Name = "Number";
            table.Columns[2].Name = "Report";
            table.Columns[3].Name = "Time";
            table.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            table.MultiSelect = false;


        }
        
        private void create_list(StreamReader file)
        {
            int i = 0;
            string line = "";
            while ((line = file.ReadLine()) != null)
            {
                string[] a = line.Split(',');
                this.l.Add(new Member(a[0], a[1], a[2], a[3]));
                this.l[i].id = i;
                i++;
            }

        }
        public void update_row(int ind, Member m)
        {
            table.Rows[ind].SetValues((m.ToString()).Split(','));
            table.Rows[ind].Tag = m;
            int i = 0;
            foreach(Member mem in l)
            {
                if(mem.id == m.id)
                {
                    l[i] = m;
                    break;
                }
                i++;
            }
            
        }
        public void add_row(Member m)
        {
            string[] a = (m.ToString()).Split(',');
            int ind = table.Rows.Add(a);
            table.Rows[ind].Tag = m;
            l.Add(m);
            l[l.Count - 1].id = l.Count - 1;
        }
        private void SetupMenuStrip()
        {
            ToolStripMenuItem file = new ToolStripMenuItem("File");

            ToolStripMenuItem save = new ToolStripMenuItem("Save");
            save.Click += save_Click;
            ToolStripMenuItem create_new = new ToolStripMenuItem("Create new");
            create_new.Click += create_new_Click;
            ToolStripMenuItem saveAs = new ToolStripMenuItem("Save As") ;
            saveAs.Click += saveAs_Click;
            ToolStripMenuItem exit = new ToolStripMenuItem("Exit");
            exit.Click += exit_Click;
            save.ShortcutKeys = Keys.Control | Keys.S;

            ToolStripMenuItem open = new ToolStripMenuItem("Open");
            open.Click += open_Click;
            file.DropDownItems.Add(create_new);
            file.DropDownItems.Add(save);
            file.DropDownItems.Add(saveAs);
            file.DropDownItems.Add(open);
            file.DropDownItems.Add(exit);

            ToolStripMenuItem item = new ToolStripMenuItem("Item");
            ToolStripMenuItem add = new ToolStripMenuItem("Add new");
            add.Click += add_Click;
            ToolStripMenuItem update = new ToolStripMenuItem("Update");
            update.Click += update_Click;
            ToolStripMenuItem delete = new ToolStripMenuItem("Delete");
            delete.Click += delete_Click;

            
            item.DropDownItems.Add(add);
            item.DropDownItems.Add(update);
            item.DropDownItems.Add(delete);

            ToolStripMenuItem help = new ToolStripMenuItem("Help");
            ToolStripMenuItem view_help = new ToolStripMenuItem("View help");
            view_help.Click += view_help_Click;
            ToolStripMenuItem about_author = new ToolStripMenuItem("About author");
            about_author.Click += about_author_Click;
            help.DropDownItems.Add(view_help);
            help.DropDownItems.Add(about_author);
            menuStrip1.Items.Add(file);
            menuStrip1.Items.Add(item);
            menuStrip1.Items.Add(help);

        }
        private void create_new_Click(object sender, EventArgs e)
        {
            
            clear_table(table);
            clear_list();
            table.Visible = true;
        }
       
        private void clear_table(DataGridView d)
        {
            if (d.RowCount > 0)
            {
                for (int i = d.RowCount - 1; i >= 0; i--)
                {
                    d.Rows.RemoveAt(i);
                }
            }
        }
        private void clear_list()
        {
            if (l.Count > 0)
            {
                for (int i = l.Count - 1; i >= 0; i--)
                {
                    l.RemoveAt(i);
                }
            }
        }
        private void view_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The program is designed to work with a directory that stores data about conference participants.");
        }
        private void about_author_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
        "Do you want to visit author's instagram account?", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                 ) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://www.instagram.com/irynashyginska2000/");
            }
        }
        private void delete_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            var result = MessageBox.Show("Are you sure to delete?", " ", buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (this.table.SelectedRows.Count > 0)
                {
                    Member m = (Member)this.table.SelectedRows[0].Tag;
                    this.table.Rows.RemoveAt(
                        this.table.SelectedRows[0].Index);
                    int i = 0;
                    foreach (Member mem in l)
                    {
                        if (mem.id == m.id)
                        {
                            l.RemoveAt(i);
                            break;
                        }
                        i++;
                    }
                }
                
            }

        }
        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                ItemForm form2 = new ItemForm(this);
                form2.Show();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.ToString());
            }
        }
        private void update_Click(object sender, EventArgs e)
        {
            int ind = table.SelectedRows[0].Index;
            Member m = (Member)table.Rows[ind].Tag;
            ItemForm form2 = new ItemForm(m, ind, this);
            form2.Show();
        }
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "";

                for (int i = 0; i < table.RowCount; i++)
                {
                    Member m = (Member)table.Rows[i].Tag;
                    if (i != table.RowCount - 1)
                        text += m.ToString() + '\n';
                    else
                        text += m.ToString();

                }

                StreamWriter NewFile = File.CreateText(filename);
                NewFile.Write(text);
                NewFile.Close();
            }
            catch
            {
                MessageBox.Show("Try again");
            }
        }
        private void saveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                save.FilterIndex = 2;
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.Cancel)
                    return;
                string text = "";
                for (int i = 0; i < table.RowCount; i++)
                {
                    Member m = (Member)table.Rows[i].Tag;
                    if (i != table.RowCount - 1)
                        text += m.ToString() + '\n';
                    else
                        text += m.ToString();

                }
                System.IO.File.WriteAllText(save.FileName, text);
            }
            catch
            {
                MessageBox.Show("Try again");
            }
        }
            
        private void open_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            table.Visible = true;
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.InitialDirectory = @"C:\Users\Dell\source\repos\wForms_Members\wForms_Members\bin\Debug";
                open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                if (open.ShowDialog() == DialogResult.OK)
                {
                    filePath = open.FileName;
                    filename = open.FileName;
                    var fileStream = open.OpenFile();
                    using(StreamReader reader = new StreamReader(fileStream)){

                        clear_list();
                        create_list(reader);
                        table_fill();
                        buttonPanel.Visible = true;
                    }
                } 
            }
        }
        private void table_fill()
        {
            clear_table(table);
            int num = 0;
            foreach (Member m in l)
            {
                string[] a = (m.ToString()).Split(',');
                table.Rows.Add(a);
                table.Rows[num].Tag = m;
                num++;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var section = from m in l
                                 where m.Number == int.Parse(textBox1.Text)
                                 select m;
                clear_table(table);
                int num = 0;
                foreach (var item in section)
                {
                    string[] a = (item.ToString()).Split(',');
                    table.Rows.Add(a);
                    table.Rows[num].Tag = item;
                    num++;

                }
                textBox2.Text = "";
            }

        }
        private void SetupLayout()
        {
            buttonPanel.Height = 100;
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Visible = true;
            this.Controls.Add(this.buttonPanel);
            groupBox1.Location = new Point(10,10);
          
            groupBox2.Location = new Point(230, 10);
           
            groupBox3.Location = new Point(490, 10);

            button1.Location = new Point(750, 30);
            buttonPanel.Visible = false;
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var section = from m in l
                                  where m.Number == int.Parse(textBox2.Text)
                                  select m;
                    List<Member> list = l;
                    foreach (var item in section.ToList())
                    {
                        item.Number = int.Parse(textBox3.Text);
                        for(int i = 0;i<list.Count;i++)
                        {
                            if (list[i].id == item.id)
                            {
                                list[i] = item;
                                
                            }
                            
                        }
                    }
                    l = list;
                    table_fill();
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Try again");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var reports = from m in l
                          group m by m.Surname into g
                          orderby g.Key descending
                          select new
                          {
                              Surname = g.Key,
                              Count = g.Count(),

                          };
            clear_table(table);
            int num = 0;
            foreach (var gr in reports)
            {

                string s = gr.Surname + ", ," + gr.Count + ", ";
                string[] a = (s.ToString()).Split(',');
                table.Rows.Add(a);
                table.Rows[num].Tag = gr;
                num++;
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            table_fill();
        }
    }
    
}
