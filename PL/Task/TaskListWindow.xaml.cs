﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Collections;
using BO;
using PL.Task;
using PL.Engineer;
using System.Linq;

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Status Task { get; set; } = BO.Status.None;
    public BO.EngineerExperience CompmlexityLevel { get; set; } = BO.EngineerExperience.None;
    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Task.ReadAll();
        TaskList = temp == null ? new() : new(s_bl!.TaskInList.ReadAll(temp!));
        //TasksId= new(TaskList.Select(task => task.Id));
    }

    public ObservableCollection<BO.TaskInList> TaskList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    //public ObservableCollection<int> TasksId
    //{
    //    get { return (ObservableCollection<int>)GetValue(TasksIdProperty); }
    //    set { SetValue(TasksIdProperty, value); }
    //}
    //public static readonly DependencyProperty TasksIdProperty =
    //   DependencyProperty.Register("TasksId", typeof(int), typeof(EngineerWindow), new PropertyMetadata(null));
    private void Task_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        var temp = Task == BO.Status.None ?
        s_bl?.Task.ReadAll() :
        s_bl?.Task.ReadAll(item => item.Status == Task);
        TaskList = temp == null ? new() : new(s_bl!.TaskInList.ReadAll(temp!));
    }

    private void btnAddTask(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
        IEnumerable<BO.Task> tasks = s_bl?.Task.ReadAll()!;
        TaskList = new(s_bl!.TaskInList.ReadAll(tasks));
    }

    private void ListView_MouseDoubleClick(object sender, RoutedEventArgs e)
    {
        BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;
        new TaskWindow(task!.Id).ShowDialog();
        IEnumerable<BO.Task> tasks = s_bl?.Task.ReadAll()!;
        TaskList = new(s_bl!.TaskInList.ReadAll(tasks));
    }
}