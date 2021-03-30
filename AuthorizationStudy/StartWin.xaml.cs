using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AuthorizationStudy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UsersClass> UserList = new List<UsersClass>();
        int _CpActivate = 0;
        int _ErrorCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "Кристина",
                Login = "Kr1",
                Password = "Kr2"
            });

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "Илья",
                Login = "Il1",
                Password = "Il2"
            });

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "Никита",
                Login = "Ni1",
                Password = "Ni2"
            });

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "マシャイ",
                Login = "Ma1",
                Password = "Ma2"
            });

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "Юлия",
                Login = "Ul1",
                Password = "Ul2"
            });

            if (File.Exists("File.txt") == true)
            {
                if (FileSaveClass.FileRead("file.txt") != null)
                {
                    cbxRemind.IsChecked = true;
                    int SaveId = Convert.ToInt32(FileSaveClass.FileRead("file.txt"));
                    var user = UserList.Where(u => u.Id == SaveId).FirstOrDefault();
                    txtLogin.Text = user.Login;
                    pswPassword.Password = user.Password;
                }
            }
            else if (File.Exists("File.txt") == false)
            {
                File.Create("File.txt");
            }
        }

        private void CapchaGet()
        {
            string CpString = "";
            CpString = CapchaGenClass.CapchaGenerate();
            txbCapchaEnter.Text = CpString;
        }

        private void CapchaShow()//открывает капчу
        {
            btnClose1.Visibility = Visibility.Hidden;  
            btnLogin1.Visibility = Visibility.Hidden;
            btnClose2.Visibility = Visibility.Visible;
            btnLogin2.Visibility = Visibility.Visible;
            txtCapcha.Visibility = Visibility.Visible;
            txbCapcha.Visibility = Visibility.Visible;
            txbCapchaEnter.Visibility = Visibility.Visible;
            btnCapchaReboot.Visibility = Visibility.Visible;
            brdCapcha.Visibility = Visibility.Visible;
        }

        private void Login()
        {
            var user = UserList.Where(u => u.Login == txtLogin.Text && u.Password == pswPassword.Password).FirstOrDefault();

            if (user != null && txbCapchaEnter.Text.ToLower() == txtCapcha.Text.ToLower())
            {
                if (cbxRemind.IsChecked == true && FileSaveClass.FileRead("file.txt") == null)
                {
                    FileSaveClass.FileWrite(Convert.ToString(user.Id), "File.txt");
                }
                else if (cbxRemind.IsChecked == false)
                {
                    File.Delete("File.txt");
                }

                WorkWin workwin = new WorkWin(user);
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _ErrorCounter++;

                if (txbCapchaEnter.Text.ToLower() != txtCapcha.Text.ToLower() && _CpActivate == 1)
                {
                    MessageBox.Show("Неправильно введена капча!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (_ErrorCounter > 2)
                {
                    CapchaShow();
                    _CpActivate = 1;
                }
            }

            if (_CpActivate == 1)
            {
                CapchaGet();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCapchaReboot_Click(object sender, RoutedEventArgs e)
        {
            CapchaGet();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }

            if (e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}
