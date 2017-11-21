using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Marbid.Module.BusinessObjects;
using Marbid.Module.CustomCodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Marbid.Module.Win.CustomCodesWin
{
    public class ParameterForm
    {
        public ParameterForm()
        {
        }

        private List<ParameterDefinition> parameterDefinition = new List<ParameterDefinition>();
        private List<ParameterDictionary> parameterDictionary = new List<ParameterDictionary>();

        public List<ParameterDictionary> ParameterDictionary
        {
            get
            {
                return parameterDictionary;
            }
        }

        public List<ParameterDefinition> ParameterDefinition
        {
            get
            {
                return parameterDefinition;
            }
            set
            {
                parameterDefinition = value;
            }
        }

        public DialogResult ShowParameterDialog()
        {
            DataRetrieval retrieval = new DataRetrieval();
            XtraForm form = new XtraForm();
            form.Icon = Marbid.Module.Win.Properties.Resources.mareinico;
            form.Text = "Parameters";
            LayoutControl layout = new LayoutControl()
            {
                Name = "layout",
            };
            layout.Root.BeginUpdate();
            foreach (ParameterDefinition definition in parameterDefinition)
            {
                LayoutControlItem itemLayout;

                switch (definition.ParameterPropertyType)
                {
                    case ParameterPropertyType.DataSource:
                        string paramQuery = definition.ParameterQueryString;
                        string paramConnection = definition.ParameterConnection;
                        DataSet paramDataSet = new DataSet();
                        paramDataSet = retrieval.GetDataSet("ParamData", paramQuery, paramConnection, paramDataSet);
                        LookUpEdit lookUpEdit = new LookUpEdit();
                        lookUpEdit.Properties.DataSource = paramDataSet.Tables[0];
                        lookUpEdit.Properties.ValueMember = paramDataSet.Tables[0].Columns[0].ColumnName;
                        lookUpEdit.Properties.DisplayMember = paramDataSet.Tables[0].Columns[1].ColumnName;
                        lookUpEdit.EditValue = definition.ParameterDefaultValue;
                        itemLayout = layout.AddItem(definition.ParameterCaption, lookUpEdit);
                        itemLayout.OptionsTableLayoutItem.RowIndex = definition.ParameterIndex;
                        itemLayout.Name = definition.ParameterName;
                        lookUpEdit.EditValue = definition.ParameterDefaultValue;
                        break;

                    case BusinessObjects.ParameterPropertyType.DateTime:
                        DateEdit dateEdit = new DateEdit()
                        {
                            DateTime = DateTime.Now,
                            Name = definition.ParameterName,
                            EditValue = definition.ParameterDefaultValue
                        };
                        itemLayout = layout.AddItem(definition.ParameterCaption, dateEdit);
                        itemLayout.Name = definition.ParameterName;
                        itemLayout.OptionsTableLayoutItem.RowIndex = definition.ParameterIndex;
                        break;

                    case BusinessObjects.ParameterPropertyType.String:
                        TextEdit textEdit = new TextEdit()
                        {
                            Name = definition.ParameterName
                        };
                        itemLayout = layout.AddItem(definition.ParameterCaption, textEdit);
                        itemLayout.Name = definition.ParameterName;
                        itemLayout.OptionsTableLayoutItem.RowIndex = definition.ParameterIndex;
                        break;

                    case BusinessObjects.ParameterPropertyType.Integer:
                        SpinEdit spinEdit = new SpinEdit()
                        {
                            Name = definition.ParameterName
                        };
                        itemLayout = layout.AddItem(definition.ParameterCaption, spinEdit);
                        itemLayout.Name = definition.ParameterName;
                        itemLayout.OptionsTableLayoutItem.RowIndex = definition.ParameterIndex;
                        break;

                    case BusinessObjects.ParameterPropertyType.Numeric:
                        CalcEdit calcEdit = new CalcEdit()
                        {
                            Name = definition.ParameterName
                        };
                        itemLayout = layout.AddItem(definition.ParameterCaption, calcEdit);
                        itemLayout.Name = definition.ParameterName;
                        itemLayout.OptionsTableLayoutItem.RowIndex = definition.ParameterIndex;
                        break;
                }
            }
            SimpleButton buttonOK = new SimpleButton()
            {
                Name = "ButtonOK",
                DialogResult = DialogResult.OK,
                Text = "OK"
            };

            SimpleButton buttonCancel = new SimpleButton()
            {
                Name = "ButtonCancel",
                DialogResult = DialogResult.Cancel,
                Text = "Cancel"
            };
            LayoutControlItem btn = layout.AddItem("OK", buttonOK);
            btn.TextVisible = false;
            form.AcceptButton = buttonOK;
            btn = layout.AddItem("Cancel", buttonCancel);
            btn.TextVisible = false;
            form.CancelButton = buttonCancel;
            layout.Root.EndUpdate();
            layout.Dock = DockStyle.Fill;
            form.Controls.Add(layout);
            form.StartPosition = FormStartPosition.CenterScreen;

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (BaseLayoutItem item in layout.Items)
                {
                    if (item is LayoutControlItem)
                    {
                        LayoutControlItem controlItem = (LayoutControlItem)item;
                        if (((LayoutControlItem)item).Control.GetType() == typeof(SpinEdit))
                        {
                            var paramData = new ParameterDictionary() { ParameterName = item.Name, ParameterValue = ((SpinEdit)controlItem.Control).Value };
                            parameterDictionary.Add(paramData);
                        }

                        if (((LayoutControlItem)item).Control.GetType() == typeof(CalcEdit))
                        {
                            var paramData = new ParameterDictionary() { ParameterName = item.Name, ParameterValue = ((CalcEdit)controlItem.Control).Value };
                            parameterDictionary.Add(paramData);
                        }

                        if (((LayoutControlItem)item).Control.GetType() == typeof(DateEdit))
                        {
                            var paramData = new ParameterDictionary() { ParameterName = item.Name, ParameterValue = ((DateEdit)controlItem.Control).DateTime };
                            parameterDictionary.Add(paramData);
                        }

                        if (((LayoutControlItem)item).Control.GetType() == typeof(TextEdit))
                        {
                            var paramData = new ParameterDictionary() { ParameterName = item.Name, ParameterValue = ((TextEdit)controlItem.Control).Text };
                            parameterDictionary.Add(paramData);
                        }

                        if (((LayoutControlItem)item).Control.GetType() == typeof(LookUpEdit))
                        {
                            var paramData = new ParameterDictionary() { ParameterName = item.Name, ParameterValue = ((LookUpEdit)controlItem.Control).EditValue };
                            parameterDictionary.Add(paramData);
                        }
                    }
                }
            }
            return result;
        }
    }

    public class ParameterDefinition
    {
        public int ParameterIndex { get; set; }
        public string ParameterName { get; set; }
        public ParameterPropertyType ParameterPropertyType { get; set; }
        public string ParameterCaption { get; set; }
        public object ParameterDefaultValue { get; set; }
        public string ParameterConnection { get; set; }
        public string ParameterQueryString { get; set; }
    }
}