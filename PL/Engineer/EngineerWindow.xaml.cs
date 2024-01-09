using System;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;


namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerWindow(int Id=0)
        {
            InitializeComponent();
            try
            {
                CurrentEngineer = Id == 0 ? new() { Id = 0, Cost = 0 , Name ="", Email ="", Level =null} : s_bl.Engineer.Read(Id)!;
            }
            catch (BO.BlDoesNotExistException message) 
            {
                //לזכור לבדוק
              Console.WriteLine(message);
            }
        }
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
        public static readonly DependencyProperty CurrentEngineerProperty =
           DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            try
            {
                if (btn.Content == "Add")
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                }
            }
            catch (BO.BlAlreadyExistsException ex)
            {
                Console.WriteLine(ex);
            }
            catch (BO.BlDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            MessageBox.Show("succeeded!!");
            this.Close();
            
        }
    }
}
