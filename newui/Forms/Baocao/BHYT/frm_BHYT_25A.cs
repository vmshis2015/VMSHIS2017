﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT
{
    public partial class frm_BHYT_25A : Form
    {
        
        public frm_BHYT_25A()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_BHYT_25A_KeyDown);
            
           
            bhyT_25A1.Init();
           
        }

        void frm_BHYT_25A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}
