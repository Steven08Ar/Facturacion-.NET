using InvoiceApp.WPF.ViewModels.Shell;
using System.Windows;

namespace InvoiceApp.WPF.Views.Shell;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
