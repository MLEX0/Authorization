using System;
using System.Collections.Generic;
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

namespace AuthorizationStudy
{
    /// <summary>
    /// Логика взаимодействия для WorkWin.xaml
    /// </summary>
    public partial class WorkWin : Window
    {
        public WorkWin()
        {
            InitializeComponent();
        }

        public WorkWin(UsersClass user)
        {
            InitializeComponent();

            txtId.Text = Convert.ToString(user.Id);
            txtName.Text = user.Name;
            txtLogin.Text = user.Login;
            txtPassword.Text = user.Password;
        }
    }
}
