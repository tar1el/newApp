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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TelecomNevaApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string code;
        public MainWindow()
        {
            InitializeComponent();

            inputPassword.IsEnabled = false;
            inputCode.IsEnabled = false;
            buttonLogin.IsEnabled = false;
            buttonReload.IsEnabled = false;
        }
        DispatcherTimer dt;
        
        private void GetNewCode() //Метод получающий код
        {
            code = null; // онулирование
            Random random = new Random();

            dt.Interval = TimeSpan.FromSeconds(10);
            dt.Tick += dTick;

            string[] abc = new string[] // Массив символов
            {
                "Q","W","E","R","T","Y","U","I","O","P",
                "A","S","D","F","G","H","J","K","L","Z",
                "X","C","V","B","N","M",
                "0","1","2","3","4","5","6","7","8","9",
                "@","#","$","%"
            };

            for(int i = 0; i < 2; i++)
            {
                code += abc[random.Next(0, abc.Length)];    
            }
            // Вывод случайного кода
            if (MessageBox.Show(code, "Код", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                dt.Start();
                buttonLogin.IsEnabled = true;
                buttonReload.IsEnabled = true;
                inputCode.Focus();

            }
        }

        private void dTick(object sender, EventArgs e)
        {
            dt.Stop();
            MessageBox.Show("Время вышло");
        }

        // Отмена всех действий
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            inputNumber.Focus();
            inputPassword.IsEnabled = false;
            inputCode.IsEnabled = false;
            buttonLogin.IsEnabled = false;
            buttonReload.IsEnabled = false;
        }
        // Кнопка авторизации
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (code == inputCode.Text)
            {
                using (var db = new SuperMegaExamDBEntities())
                {
                    var role1 = db.Users.FirstOrDefault(f => f.RoleID.ToString() == "1");
                    if (role1 == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать {role1.FIO}, Вы Специалист ТП (выездной инженер)");
                        dt.Stop();
                    }

                }

                using (var db = new SuperMegaExamDBEntities())
                {
                    var role2 = db.Users.FirstOrDefault(f => f.RoleID.ToString() == "2");
                    if (role2 == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать {role2.FIO}, Вы Директор по развитию");
                        dt.Stop();
                    }

                }

                using (var db = new SuperMegaExamDBEntities())
                {
                    var role3 = db.Users.FirstOrDefault(f => f.RoleID.ToString() == "3");
                    if (role3 == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать {role3.FIO}, Вы Технический департамент");
                        dt.Stop();
                    }

                }

                using (var db = new SuperMegaExamDBEntities())
                {
                    var role4 = db.Users.FirstOrDefault(f => f.RoleID.ToString() == "4");
                    if (role4 == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать {role4.FIO}, Вы Бухгалтер");
                        dt.Stop();
                    }

                }

                using (var db = new SuperMegaExamDBEntities())
                {
                    var role5 = db.Users.FirstOrDefault(f => f.RoleID.ToString() == "5");
                    if (role5 == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать {role5.FIO}, Вы Менеджер по работе с клиентами");
                        dt.Stop();
                    }

                }
            }
            
        }

        private void buttonReload_Click(object sender, RoutedEventArgs e)
        {
            GetNewCode();
        }

        private void inputNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                using (var db = new SuperMegaExamDBEntities())
                {
                    var numberLog = db.Users.FirstOrDefault(f => f.Number.ToString() == inputNumber.Text);
                    if (numberLog == null)
                    {
                        MessageBox.Show("Такого номера не существует");
                    }
                    else
                    {
                        inputPassword.IsEnabled = true;
                        inputNumber.IsEnabled = false;
                        inputPassword.Focus();
                    }
                }
            }
        }

        private void inputPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                using (var db = new SuperMegaExamDBEntities())
                {
                    var pass = db.Users.FirstOrDefault(f => f.Number.ToString() == inputNumber.Text && f.Password == inputPassword.Password);
                    if (pass == null)
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                    else
                    {
                        inputPassword.IsEnabled = false;
                        inputCode.IsEnabled = true;
                        inputCode.Focus();
                        GetNewCode();
                    }
                }
            }
        }
    }
}
