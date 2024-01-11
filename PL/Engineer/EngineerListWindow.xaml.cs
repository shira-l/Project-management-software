

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience Engineer { get; set; } = BO.EngineerExperience.None;
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp!);
        }

        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        private void Engineer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = Engineer == BO.EngineerExperience.None ?
            s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => item.Level == Engineer);
            EngineerList = temp == null ? new() : new(temp!);
        }

        private void btnAddEngineer(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            EngineerList = new(s_bl?.Engineer.ReadAll()!);
        }

        private void ListView_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            BO.Engineer? EngineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(EngineerInList!.Id).ShowDialog();
            EngineerList = new(s_bl?.Engineer.ReadAll()!);
        }
    }
}
