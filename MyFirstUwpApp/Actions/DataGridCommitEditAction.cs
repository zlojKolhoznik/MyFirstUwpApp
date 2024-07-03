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
    internal class DataGridCommitEditAction : DataGridAction
    {
        public override object Execute(object sender, object parameter)
        {
            return DataGrid.CommitEdit();
        }
    }
}
