using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyFirstUwpApp.Actions
{
    internal class DataGridValidateEditingColumnAction : DataGridAction
    {
        private const int DefaultColumnIndex = 0;

        public static DependencyProperty MaxEditableColumnIndexProperty = DependencyProperty.Register(
            "MaxEditableColumnIndex",
            typeof(int),
            typeof(DataGridValidateEditingColumnAction),
            new PropertyMetadata(0));

        public int MaxEditableColumnIndex
        {
            get => (int)GetValue(MaxEditableColumnIndexProperty);
            set => SetValue(MaxEditableColumnIndexProperty, value);
        }

        public override object Execute(object sender, object parameter)
        {
            var selectedColumnIndex = DataGrid.Columns.IndexOf(DataGrid.CurrentColumn);
            var result = false;
            if (selectedColumnIndex > MaxEditableColumnIndex)
            {
                DataGrid.CurrentColumn = DataGrid.Columns[DefaultColumnIndex];
                result = DataGrid.BeginEdit();
            }

            return result;
        }
    }
}
