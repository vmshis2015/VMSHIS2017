﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using SubSonic;

namespace VNS.UCs
{
    public partial class ucDynamicParam : UserControl
    {
        
        DataRow dr = null;
        public bool AllowSave = true;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved=false;
        public delegate void OnEnterKey(ucDynamicParam obj);
        public event OnEnterKey _OnEnterKey;
        bool useRtf = false;
        bool hasChanged = false;
        public bool onlyView = false;
        public bool hasAutoCorrect = false;
        public bool hasAutoCorrectOther = false;

        public bool AllowTxtChanged = true;
        public bool AllowRtbTextChanged = true;
        public ucDynamicParam()
        {
            InitializeComponent();
           
        }
        public ucDynamicParam(DataRow dr,bool AutoSaveWhenEnterKey)
        {
            InitializeComponent();
            this.dr = dr;
            this.AutoSaveWhenEnterKey = AutoSaveWhenEnterKey;
            txtValue._OnKeyDown += txtValue_KeyDown;
            txtValue._OnLostFocus += txtValue_LostFocus;
            txtValue._OnTextChanged += txtValue_TextChanged;
            
            
        }

        bool spacekey = false;

        void txtValue_LostFocus(object sender, EventArgs e)
        {
            if (!isSaved)
                Save();
            if (!hasAutoCorrect)
                AutoCorrectLastWord();

        }
        public bool _RichTextbox
        {
            get; set; 
        }
        public bool _AcceptTab
        {
            get { return txtValue._AcceptsTab; }
            set { txtValue._AcceptsTab = value; }
        }
        public bool _useRtf
        {
            get { return useRtf; }
            set
            {
                useRtf = value;
                txtValue.FindReplaceVisible = value;
                txtValue.ToolStripVisible = value;
            }
        }
        public bool _ReadOnly
        {
            get { return txtValue._Readonly ; }
            set { txtValue._Readonly = value; }
        }
        void txtValue_TextChanged(object sender, EventArgs e)
        {
            hasAutoCorrect = false;
            isSaved = false;
        }

        void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                if (AutoSaveWhenEnterKey)
                    Save();
                if (_OnEnterKey != null)
                {
                    if (!hasAutoCorrect)
                        AutoCorrectLastWord();
                    _OnEnterKey(this);
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                spacekey = true;
               
                AutoCorrectLastWord();
                spacekey = false;
               
                return;
            }
        }
       
        void AutoCorrectLastWord()
        {
            try
            {
                AllowRtbTextChanged = false;
                int SelectionStart = txtValue.rtbDocument.SelectionStart;
                if (spacekey) txtValue.Text.Insert(SelectionStart, " ");
                int start = txtValue.Text.Substring(0, SelectionStart).LastIndexOf(' ');
                if (start < 0) start = 0;
                int end = SelectionStart;
                string lastWord = Utility.DoTrim(txtValue.Text.Substring(0, SelectionStart)).Split(' ').Last().Split('\n').Last().Replace("\n", "").Replace("\t", "").Replace("\v", "").Replace("\r", "").Replace("\f", "").Replace("\b", "").Replace("\a", "").Replace("\0", "").Replace("\\", "").Replace("\"", "").Replace("\'", "");
                DmucChung objCorrectList = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("AUTOCORRECT")
                    .And(DmucChung.Columns.Ma).IsEqualTo(lastWord).ExecuteSingle<DmucChung>();
                if (objCorrectList != null)
                {

                    txtValue.rtbDocument.Find(lastWord, start, end, RichTextBoxFinds.None);
                    if (txtValue.rtbDocument.SelectionLength > 0)
                        txtValue.rtbDocument.SelectedText = Utility.DoTrim(objCorrectList.Ten);

                }
                AllowRtbTextChanged = true;
                hasAutoCorrect = true;
            }
            catch (Exception ex)
            {
            }
          
        }

        public void FocusMe()
        {
            txtValue.Select();
            txtValue.Focus();
        }
        public void Init()
        {
            try
            {
               if (dr != null )
               {
                   lblName.Dock = Utility.Byte2Bool(dr[DynamicField.Columns.TopLabel]) ? DockStyle.Top : DockStyle.Left;
                   _useRtf = Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]);
                   _AcceptTab = Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]);
                  
                   if (!useRtf)
                       txtValue.Text = Utility.sDbnull(dr[DynamicValue.Columns.Giatri], "");
                   else
                       txtValue.Rtf = Utility.sDbnull(dr[DynamicValue.Columns.Rtf], "");
                   if (onlyView)
                   {
                       _useRtf = false;
                       _AcceptTab = false;
                       _ReadOnly = false;
                   }
                   txtValue._Multiline = _useRtf;
                   lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "") + "(" + Utility.sDbnull(dr[DynamicField.Columns.Ma], "") + ")";
                   toolTip1.SetToolTip(lblName, Utility.sDbnull(dr[DynamicField.Columns.Ma], ""));
               }
               else
               {
                   lblName.Text = "UnKnown";
                   txtValue.Text = "";
               }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string _Mota
        {
            set { lblName.Text = value; }
            get { return lblName.Text; }
        }
        public string _Giatri
        {
            set { txtValue.Text = value; }
            get { return txtValue.Text; }
        }
        public void Save()
        {
            try
            {
                if (!AllowSave) return;
                List<DynamicValue> lstValues = new List<DynamicValue>();
                if (dr != null )
                {
                    DynamicValue objVal = null;


                    if (Utility.Int32Dbnull(dr["idValue"], -1) > 0)
                    {
                        objVal = DynamicValue.FetchByID(Utility.Int32Dbnull(dr["idValue"], -1));
                        objVal.IsNew = false;
                        objVal.MarkOld();
                    }
                    else
                    {
                        objVal = new DynamicValue();
                        objVal.IsNew = true;
                    }

                    objVal.Id = Utility.Int32Dbnull(dr["idValue"], -1);
                    objVal.Ma = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
                    objVal.Giatri = Utility.DoTrim(txtValue.Text);
                    objVal.Rtf = txtValue.Rtf;
                    objVal.ImageId = -1;
                    objVal.IdChidinhchitiet = Utility.Int32Dbnull(dr[DynamicValue.Columns.IdChidinhchitiet], -1);

                    lstValues.Add(objVal);
                    clsHinhanh.UpdateDynamicValues(lstValues);
                    dr["idValue"] = objVal.Id;
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                
            }  
        }
        
    }
}
