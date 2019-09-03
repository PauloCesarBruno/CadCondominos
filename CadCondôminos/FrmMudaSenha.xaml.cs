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
//
using System.Data;
using System.Data.SqlClient;
//
using System.Runtime.InteropServices; // 1º Passo para Desabilitar o (X) de Fechar o Form em WPF.
using System.Windows.Interop; // 1º Passo para Desabilitar o (X) de Fechar o Form em WPF.
//

namespace CadCondôminos
{
    /// <summary>
    /// Lógica interna para FrmMudaSenha.xaml
    /// </summary>
    public partial class FrmMudaSenha : Window 
    {
        // 2º Passo para Desabilitar o (X) de Fechar o Form em WPF.
        private const int GWL_STYLE = -16; private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public FrmMudaSenha()
        {
            InitializeComponent();
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
                      
        {
            var uie = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(
                new TraversalRequest(
                FocusNavigationDirection.Next));
            }
        }

        private void MudaSenha(String Nome, String Senha)
        {
            try
            {

                SqlDataReader dr = null;
                StrCon obj = new StrCon();
                SqlConnection conn = new SqlConnection(obj.sql);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from tblSenhaChefe where Nome =  '" + Nome + "' and senha = '" + Senha + "'", conn);
                dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    dr.Read();
                    FrmMudaSenha2 muda = new FrmMudaSenha2();
                    muda.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Dados não Conferem, Login e/ou Senha incorretos...", "ERRO!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    PTextLogin.IsEnabled = false;
                    PtextSenha.IsEnabled = false;
                }
                try
                {
                    dr.Close();
                    conn.Close();
                }
                catch (Exception)
                {
                    //
                }
                PTextLogin.Password  = string.Empty;
                PtextSenha.Password = string.Empty;
                PTextLogin.IsEnabled = true;
                PtextSenha.IsEnabled = true;
                PTextLogin.Focus();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Detalhe: " + ex.Message + "Tente o Acesso assim que o Servidor e/ou a Reade forem restabelecidos.... O SISTEMA SERÁ ENCERRADO!!!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FrmMudaSenha1_Loaded(object sender, RoutedEventArgs e)
        {
            PTextLogin.Focus();
            // 3º Passo para Desabilitar o (X) de Fechar o Form em WPF.
            var hwnd = new WindowInteropHelper(this).Handle; SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void btnCancela_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow main = new MainWindow();
            main.Show();
            App.Current.MainWindow.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MudaSenha(PTextLogin.Password , PtextSenha.Password);
        }

        private void btnInforma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Por Gentileza Entre em Contato com o Desenvolvedor...!!!", "AVISO", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
                App.Current.MainWindow.Close();
            }
            catch (Exception)
            {
                //
            }
        }   
    }
}