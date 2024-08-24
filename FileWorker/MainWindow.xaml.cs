using FileWorker.Core;
using FileWorker.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBContext context;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainVM();
            context = new DBContext();
            bool isCreatingDB = context.Database.EnsureCreated();
            if (isCreatingDB)
            {
                MessageBox.Show("База данных отсутствовала и была автоматически создана в процессе запуска программы.");
            }
        }
    }
}