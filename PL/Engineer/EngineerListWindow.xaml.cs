

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Collections;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience Engineer { get; set; } = BO.EngineerExperience.None;
        //View the list of engineers
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(s_bl!.EngineerInList.ReadAll(temp!));
        }

        public ObservableCollection<BO.EngineerInList> EngineerList
        {
            get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));
        //View the list of engineers by level
        private void Engineer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = Engineer == BO.EngineerExperience.None ?
            s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => item.Level == Engineer);
            EngineerList = temp == null ? new() : new(s_bl!.EngineerInList.ReadAll(temp!));
        }
        //Adding an engineer
        private void btnAddEngineer(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            IEnumerable<BO.Engineer> engineers = s_bl?.Engineer.ReadAll()!;
            s_bl!.EngineerInList.ReadAll(engineers);
            EngineerList = new(s_bl!.EngineerInList.ReadAll(engineers));
        }
        //Update the engineer details
        private void ListView_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            BO.EngineerInList ?engineer = (sender as ListView)?.SelectedItem as BO.EngineerInList;
            new EngineerWindow(engineer!.Id).ShowDialog();
            IEnumerable<BO.Engineer> engineers = s_bl?.Engineer.ReadAll()!;
            EngineerList = new(s_bl!.EngineerInList.ReadAll(engineers));
        }
    }
}
