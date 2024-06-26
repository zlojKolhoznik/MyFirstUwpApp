using Microsoft.Toolkit.Uwp.UI.Controls;
using MyFirstUwpApp.Models;
using MyFirstUwpApp.Services.MessageService;
using MyFirstUwpApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyFirstUwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int FirstNameColumnIndex = 0;
        private const int LastNameColumnIndex = 1;
        private const int EditColumnIndex = 2;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel(new ContentDialogMessageService());
            Application.Current.Suspending += OnApplicationSuspending;
        }

        public MainViewModel ViewModel { get; private set; }

        private void OnApplicationSuspending(object sender, SuspendingEventArgs e)
        {
            ViewModel.SaveCustomers();
        }

        private void OnDeleteCustomerButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && ViewModel.RemoveCustomerCommand.CanExecute(button.DataContext))
            {
                ViewModel.RemoveCustomerCommand.Execute(button.DataContext);
            }
        }

        private void OnEditCustomerButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Customer customer)
            {
                ViewModel.SaveCustomerState(customer);
                dgCustomers.SelectedItem = customer;
                dgCustomers.CurrentColumn = dgCustomers.Columns[FirstNameColumnIndex];
                dgCustomers.BeginEdit();
            }
        }

        private void OnDataGridBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column != dgCustomers.Columns[FirstNameColumnIndex] && e.Column != dgCustomers.Columns[LastNameColumnIndex])
            {
                dgCustomers.CurrentColumn = dgCustomers.Columns[FirstNameColumnIndex];
                dgCustomers.BeginEdit();
            }
            else
            {
                var selectedRowEditColumnContent = dgCustomers.Columns[EditColumnIndex].GetCellContent(e.Row.DataContext);
                ShowEditButtons(selectedRowEditColumnContent as StackPanel);
            }
        }

        private void OnCancelEditingButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var hideButtonsManually = !dgCustomers.CancelEdit();
            var selectedRowEditColumnContent = dgCustomers.Columns[EditColumnIndex].GetCellContent(btn.DataContext);
            if (hideButtonsManually)
            {
                HideEditButtons(selectedRowEditColumnContent as StackPanel);
            }
            ViewModel.RestoreSavedCustomerState();
        }

        private void OnSaveEditingButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var hideButtonsManually = !dgCustomers.CommitEdit();
            var selectedRowEditColumnContent = dgCustomers.Columns[EditColumnIndex].GetCellContent(btn.DataContext);
            if (hideButtonsManually)
            {
                HideEditButtons(selectedRowEditColumnContent as StackPanel);
            }
            ViewModel.ReleaseSavedCustomerState();
        }

        private void OnDataGridRowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            var selectedRowEditColumnContent = dgCustomers.Columns[EditColumnIndex].GetCellContent(e.Row.DataContext);
            HideEditButtons(selectedRowEditColumnContent as StackPanel);
        }

        private void ShowEditButtons(StackPanel stackPanel)
        {
            var buttons = stackPanel.Children.OfType<Button>();
            buttons.First(b => b.Name == "btnEdit").Visibility = Visibility.Collapsed;
            buttons.First(b => b.Name == "btnSave").Visibility = Visibility.Visible;
            buttons.First(b => b.Name == "btnCancel").Visibility = Visibility.Visible;
        }

        private void HideEditButtons(StackPanel stackPanel)
        {
            var buttons = stackPanel.Children.OfType<Button>();
            buttons.First(b => b.Name == "btnEdit").Visibility = Visibility.Visible;
            buttons.First(b => b.Name == "btnSave").Visibility = Visibility.Collapsed;
            buttons.First(b => b.Name == "btnCancel").Visibility = Visibility.Collapsed;
        }
    }
}
