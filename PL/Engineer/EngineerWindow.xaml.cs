using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience LevelOfEngineer { get; set; } = BO.EngineerExperience.None;
        public EngineerWindow(int Id=0)
        {
            InitializeComponent();
            try
            {
                Engineer = Id == 0 ? new() : s_bl.Engineer.Read(Id)!;
            }
            catch (BO.BlDoesNotExistException message) 
            {
                //לזכור לבדוק
              Console.WriteLine(message);
            }
        }
        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineer); }
            set { SetValue(CurrentEngineer, value); }
        }
        public static readonly DependencyProperty CurrentEngineer =
           DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
