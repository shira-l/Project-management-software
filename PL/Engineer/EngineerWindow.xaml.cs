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
                if (content == "Add")
                    s_bl.Engineer.Create(CurrentEngineer);
                else
                    s_bl.Engineer.Update(CurrentEngineer);
            }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlInvalidValueExeption ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }

            MessageBox.Show("the transaction completed successfully");
            Close();
        }
    }
}
