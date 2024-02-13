using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public BO.Level LevelEngineer { get; set; } = BO.Level.Novice;

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

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        string content = (sender as Button)!.Content.ToString()!;
        try
        {
            if (CurrentEngineer.Id < 0 || CurrentEngineer.Name == "" || CurrentEngineer.Cost < 0 || !RegexEmailCheck(CurrentEngineer.Email))
            {
                throw new Exception("Incorrect data");
            }
            if (content == "Add")
                s_bl.Engineer.Create(CurrentEngineer);
            else
                s_bl.Engineer.Update(CurrentEngineer);
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlInvalidValueExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        MessageBox.Show("the transaction completed successfully");
        Close();
    }

    private void Engineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }
}
