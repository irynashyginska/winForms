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
    public partial class Section : Form
    {
        List<Member> l;
        public Section(List<Member> list)
        {
            InitializeComponent();
            this.l = list;
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Chart_Load(object sender, EventArgs e)
        {
            foreach(Member m in l)
            {
                chart1.Series["Section"].Points.AddXY(m.Surname, m.Number.ToString());
            }
            chart1.Titles.Add("Section Chart");
        }
    }
}
