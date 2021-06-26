using ClientApp.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                ClientService.ServerName = textBox.Text;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                ClientService.Password = textBox.Text;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string ServerName = ClientService.ServerName;
            string Password = ClientService.Password;

            if (ClientService.BasicResourceUsageStatsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\BasicResourceUsageScript.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.BlockingAndDeadlocksEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\BlockingAndDeadlocks.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.CommonProblemsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\CommonProblems.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.CpuDetailsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\CpuDetailsScript.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.MemoryDetailsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\IODetailsScript.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.IODetailsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\MemoryDetailsScript.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.QueryStatsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\QueryStats.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.TempdbStatsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\TempdbStats.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }

            if (ClientService.WaitStatsEnabled)
            {
                var script = @"C:\SQLProjects\sql_monitoring\ClientApp\ClientApp\ClientApp\Scripts\WaitStats.ps1";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\" {ServerName}",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
            }
        }

        private void checkBox_Copy9_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.BackupStatsEnabled = checkBox.IsEnabled;
            }
        }

        private void CpuDetails_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.CpuDetailsEnabled= checkBox.IsEnabled;
            }
        }

        private void BasicInformation_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.BasicInformationEnabled = checkBox.IsEnabled;
            }
        }

        private void BasicResourceUsage_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.BasicResourceUsageStatsEnabled = checkBox.IsEnabled;
            }
        }

        private void MemoryDetails_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.MemoryDetailsEnabled = checkBox.IsEnabled;
            }
        }

        private void IODetails_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.IODetailsEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy1_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.CpuDetailsEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy6_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.TempdbStatsEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy7_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.QueryStatsEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy8_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.CommonProblemsEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy5_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.BlockingAndDeadlocksEnabled = checkBox.IsEnabled;
            }
        }

        private void checkBox_Copy4_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                ClientService.WaitStatsEnabled = checkBox.IsEnabled;
            }
        }
    }
}
