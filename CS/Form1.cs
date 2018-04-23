using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GridRecentItemsHelper helper = new GridRecentItemsHelper(gridView1);
            helper.RecentItems.Add(new GridRecentItem("WindowsFormsApplication1", WindowsApplication1.Properties.Resources.vs2010, false));
            helper.RecentItems.Add(new GridRecentItem("WindowsFormsApplication2", WindowsApplication1.Properties.Resources.vs2010, true));
            helper.RecentItems.Add(new GridRecentItem("WindowsFormsApplication3", WindowsApplication1.Properties.Resources.vs2010, false));
        }
    }
}