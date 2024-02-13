using System;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;


namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public TaskWindow(int Id = 0)
    {
        InitializeComponent();
        try
        {
            CurrentTask = Id == 0 ? new()
            {
                Id = 0,
                Engineer = null,
                Description = "",
                Alias = "",
                Status = null,
                Deliverables = null,
                Remarks = null,
                CreateAtDate = null,
                StartDate = null,
                ScheduleDate = null,
                ForecastDate = null,
                DeadlineDate = null,
                ComplateDate = null,
                CompmlexityLevel = null,
            }
            : s_bl.Task.Read(Id)!;
        }
        catch (BO.BlDoesNotExistException message)
        { MessageBox.Show(message.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
    }

    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }
    public static readonly DependencyProperty CurrentTaskProperty =
       DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        string content = (sender as Button)!.Content.ToString()!;
        try
        {
            if (content == "Add")
                s_bl.Task.Create(CurrentTask);
            else
                s_bl.Task.Update(CurrentTask);
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlInvalidValueExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        MessageBox.Show("the transaction completed successfully");
        Close();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
