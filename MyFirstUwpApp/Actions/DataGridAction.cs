using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyFirstUwpApp.Actions
{
    internal abstract class DataGridAction : DependencyObject, IAction
    {
        public static DependencyProperty DataGridProperty = DependencyProperty.Register(
            "DataGrid",
            typeof(DataGrid),
            typeof(DataGridAction),
            new PropertyMetadata(null));

        public DataGrid DataGrid
        {
            get => (DataGrid)GetValue(DataGridProperty);
            set => SetValue(DataGridProperty, value);
        }

        public abstract object Execute(object sender, object parameter);
    }
}
