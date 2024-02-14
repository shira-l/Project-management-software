using PL.Engineer;
using System;
using System.Collections.ObjectModel;
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
                Engineer = new(),
                Description = "",
                Alias = "",
                IsActive = true,
                Status = null,
                Deliverables = null,
                Remarks = null,
                CreateAtDate = DateTime.Now,
                StartDate =null ,
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
            if (CurrentTask.Alias == "")
            {
                MessageBox.Show("Invalid or missing data input", "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return;
            }
            if (content == "Add")
            {
               int id= s_bl.Task.Create(CurrentTask);
                MessageBox.Show($"The task with a Id={id} was successfully added");
            }
            else
            {
                s_bl.Task.Update(CurrentTask);
                MessageBox.Show($"The task with a Id={CurrentTask.Id} was successfully updated");
            }
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error);}
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error);  }
        catch (BO.BlInvalidValueExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.BlIncorrectDateOrderExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        Close();
    }

}
