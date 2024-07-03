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
    internal class DataGridBeginEditAction : DataGridAction
    {
        public override object Execute(object sender, object parameter)
        {
            DataGrid.SelectedItem = ((Button)sender).DataContext;
            return DataGrid.BeginEdit();
        }
    }
}
