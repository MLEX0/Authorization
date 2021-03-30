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
        List<UsersClass> UserList = new List<UsersClass>();// создание Листа по классу
        int cpActivate = 0;
        int errorCounter = 0;
        int errorOfRead = 0;

        public MainWindow()
        {
            InitializeComponent();

            UserList.Add(new UsersClass
            {
                Id = 0,
                Name = "Кристина",
                Login = "Kr1",
                Password = "Kr2"
            });// заполняем лист вместо базы

            UserList.Add(new UsersClass
            {
                Id = 1,
                Name = "Илья",
                Login = "Il1",
                Password = "Il2"
            });

            UserList.Add(new UsersClass
            {
                Id = 2,
                Name = "Никита",
                Login = "Ni1",
                Password = "Ni2"
            });

            UserList.Add(new UsersClass
            {
                Id = 3,
                Name = "マシャイ",
                Login = "Ma1",
                Password = "Ma2"
            });

            UserList.Add(new UsersClass
            {
                Id = 4,
                Name = "Юлия",
                Login = "Ul1",
                Password = "Ul2"
            });

            if (File.Exists("File.txt") == true)// защита от дауна
            {
                if (FileSaveClass.FileRead("file.txt") != null)
                {
                    try
                    {
                        Convert.ToInt32(FileSaveClass.FileRead("file.txt")); // проверка правильности данных в файле
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка сохранения пользователя, повторите вход!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        errorOfRead = 1;
                        File.Delete("File.txt");
                    }
                    if (errorOfRead == 0)
                    {
                        if (Convert.ToInt32(FileSaveClass.FileRead("file.txt")) > UserList.Count() || Convert.ToInt32(FileSaveClass.FileRead("file.txt")) < 0)// Проверка id 
                        {
                            MessageBox.Show("Сохранённый пользователь перестал существовать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            File.Delete("File.txt");
                        }
                        else
                        {
                            cbxRemind.IsChecked = true;
                            int saveId = Convert.ToInt32(FileSaveClass.FileRead("file.txt"));
                            var user = UserList.Where(u => u.Id == saveId).FirstOrDefault();
                            txtLogin.Text = user.Login;
                            pswPassword.Password = user.Password;
                        }
                    }
                }
            }
            else if (File.Exists("File.txt") == false)// Если файла не существует, создаёт файл
            {
                File.Create("File.txt");
            }
        }

        private void CapchaGet()//Получает новую капчу и присваивает её текстовому полю! 
        {
            string CpString = "";
            CpString = CapchaGenClass.CapchaGenerate();
            txbCapchaEnter.Text = CpString;
        }

        private void CapchaShow()//открывает капчу на окне
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

        private void Login()// Метод входа в приложение
        {
            var user = UserList.Where(u => u.Login == txtLogin.Text && u.Password == pswPassword.Password).FirstOrDefault();// Поиск по логину и паролю

            if (user != null && txbCapchaEnter.Text.ToLower() == txtCapcha.Text.ToLower())// проверка правильности ввода капчи и пароля
            {
                if (File.Exists("File.txt") == true)// Проверка существования файла!
                {
                    if (cbxRemind.IsChecked == true && FileSaveClass.FileRead("File.txt") == null && File.Exists("file.txt") == true)
                    {
                        FileSaveClass.FileWrite(Convert.ToString(user.Id), "File.txt");// записывает id пользователя в файл
                    }
                    else if (cbxRemind.IsChecked == false)// удаление файла 
                    {
                        File.Delete("File.txt");
                    }
                }
                else
                {
                    if (cbxRemind.IsChecked == true && File.Exists("file.txt") == false)// Полная шляпа, когда пользователь трогает сраный файл!!!
                    {
                        MessageBox.Show("Внимание! \nИсполняемый файл занят системным процессом! " +
                            "\nПри следующей авторизации вам придётся ещё раз ввести ваши данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                WorkWin workwin = new WorkWin(user); // Переход на рабочее окно
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else// при неправильном вводе пароля
            {
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                errorCounter++;// счёт ошибок

                if (txbCapchaEnter.Text.ToLower() != txtCapcha.Text.ToLower() && cpActivate == 1)// неправильно введена капча
                {
                    MessageBox.Show("Неправильно введена капча!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (errorCounter > 2)// Открытие капчи при трёх ошибках
                {
                    CapchaShow();
                    cpActivate = 1;
                }
            }

            if (cpActivate == 1)// Получение новой капчи при первом открытии
            {
                CapchaGet();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) 
        {
            Login();// Вход по кнопке
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие по кнопке
        }

        private void btnCapchaReboot_Click(object sender, RoutedEventArgs e)
        {
            CapchaGet(); // Обновление капчи по нажатию кнопки
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)// Выход при нажатии Esc
            {
                this.Close();
            }

            if (e.Key == Key.Enter)// Вход при нажатии Enter
            {
                Login();
            }
        }
    }
}
