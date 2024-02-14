using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience LevelEngineer { get; set; } = BO.EngineerExperience.Novice;
    //Checking the integrity of the email
    public static bool RegexEmailCheck(string? input)
    {
        if (input == null)
            return true;
        // returns true if the input is a valid email
        return Regex.IsMatch(input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    public EngineerWindow(int Id = 0)
    {
        InitializeComponent();
        try
        {
            CurrentEngineer = Id == 0 ? new()
            {
                Id = 0,
                Cost = 0,
                Name = "",
                Email = "",
                Level = null
            }
            : s_bl.Engineer.Read(Id)!;
            TasksId = s_bl.Task.ReadAll().Select(task=>task!.Id);
        }
        catch (BO.BlDoesNotExistException message)
        { MessageBox.Show(message.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }
    public static readonly DependencyProperty CurrentEngineerProperty =
       DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    
    public IEnumerable<int> TasksId
    {
        get { return (IEnumerable<int>)GetValue(TasksIdProperty); }
        set { SetValue(TasksIdProperty, value); }
    }
    public static readonly DependencyProperty TasksIdProperty =
       DependencyProperty.Register("TasksId", typeof(IEnumerable<int>), typeof(EngineerWindow), new PropertyMetadata(null));

    //Update button content to add or update as requested
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        string content = (sender as Button)!.Content.ToString()!;
        try
        {
            if (CurrentEngineer.Id < 0 || CurrentEngineer.Name == "" || CurrentEngineer.Cost < 0 || !RegexEmailCheck(CurrentEngineer.Email))
            {
                MessageBox.Show("Invalid or missing data input", "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return;
            }
            if (content == "Add")
            {
                s_bl.Engineer.Create(CurrentEngineer);
                MessageBox.Show($"The engineer with a Id={CurrentEngineer.Id} was successfully added");
            }
            else
            {
                s_bl.Engineer.Update(CurrentEngineer);
                MessageBox.Show($"The engineer with a Id={CurrentEngineer.Id} was successfully updated");
            }
                
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.BlInvalidValueExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        Close();
    }

    private void Engineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }
}
