using PL.Engineer;
using PL.Task;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnHandleEngineers(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    private void btnHandleTasks(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
    private void btnEngineerInitialization(object sender, RoutedEventArgs e)
    {
        string message = "Do you want to initialize the data?";
        string title = "Initialize Window";
        MessageBoxButton buttons = MessageBoxButton.YesNo;
        MessageBoxResult result = MessageBox.Show(message, title, buttons);
        if (result == MessageBoxResult.Yes)
            DalTest.Initialization.Do();
    }
}
