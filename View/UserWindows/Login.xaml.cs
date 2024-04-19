using FoodApp.Model;
using FoodApp.ModuleDB;
using System.Windows;

namespace FoodApp.View.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ApplicationContext context = new();

        public Login()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != String.Empty)
            {
                string login = LoginBox.Text;
                foreach (var c in login)
                {
                    if (c == ' ')
                    {
                        ErrorLog.Text = "Логин не должен содержать пробелы!";
                        return;
                    }
                }
                if (context.Users.Any(e => e.Login.ToLower() == login.ToLower()))
                {
                    ErrorLog.Text = "Пользователь с таким логином существует!";
                    return;
                }

                if (OriginalPassword.Password != String.Empty)
                {
                    string passwrd = OriginalPassword.Password;
                    foreach (var c in passwrd)
                    {
                        if (c == ' ')
                        {
                            ErrorLog.Text = "Пароль не должен содержать пробелы!";
                            return;
                        }
                    }

                    if (RepeatedPassword.Password != String.Empty)
                    {
                        string passwrdRpt = RepeatedPassword.Password;
                        if (passwrdRpt != passwrd)
                        {
                            ErrorLog.Text = "Пароли должны совпадать!";
                            return;
                        }
                    }
                    else
                    {
                        ErrorLog.Text = "Повторите пароль!";
                        return;
                    }
                }
                else
                {
                    ErrorLog.Text = "Поле \"Пароль\" обязательно для заполнения!";
                    return;
                }
            }
            else
            {
                ErrorLog.Text = "Поле \"Логин\" обязательно для заполнения!";
                return;
            }

            User newUser = new()
            {
                Login = LoginBox.Text,
                Password = Encrypting.HashPassword(OriginalPassword.Password)
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            MainWindow.CurrentUser = newUser;

            DialogResult = true;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != String.Empty)
            {
                string login = LoginBox.Text;
                foreach (var c in login)
                {
                    if (c == ' ')
                    {
                        ErrorLog.Text = "Логин не должен содержать пробелы!";
                        return;
                    }
                }
                if (!context.Users.Any(e => e.Login.ToLower() == login.ToLower()))
                {
                    ErrorLog.Text = "Пользователя не существует!";
                    return;
                }
                else
                {

                    if (OriginalPassword.Password != String.Empty)
                    {
                        string passwordInput = OriginalPassword.Password;
                        string passwordStored_MD5 = context.Users.Where(e => e.Login == login).FirstOrDefault().Password;

                        string passwordInput_MD5 = Encrypting.HashPassword(passwordInput);

                        if (passwordInput_MD5 == passwordStored_MD5)
                        {
                            MainWindow.CurrentUser = context.Users.Where(e => e.Login == login).FirstOrDefault();
                            DialogResult = true;
                        }
                        else
                        {
                            ErrorLog.Text = "Пароль неверный!";
                            return;
                        }
                    }
                    else
                    {
                        ErrorLog.Text = "Поле \"Пароль\" обязательно для заполнения!";
                        return;
                    }
                }
            }
            else
            {
                ErrorLog.Text = "Поле \"Логин\" обязательно для заполнения!";
                return;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            Title = "Регистрация";
            WinTitle.Text = "Зарегистрируйтесь в системе";
            RepeatPasswordPanel.Visibility = Visibility.Visible;
            SignIn.Visibility = Visibility.Collapsed;
            Registration.Visibility = Visibility.Visible;
            Link.Visibility = Visibility.Collapsed;
        }
    }
}
