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
    /// Lógica interna para FrmMudaSenha2.xaml
    /// </summary>
    public partial class FrmMudaSenha2 : Window
    {
        // 2º Passo para Desabilitar o (X) de Fechar o Form em WPF.
        private const int GWL_STYLE = -16; private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public FrmMudaSenha2()
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
        private void SenhaNova()
        {
            try
            {
                {
                    // Instancio a Classe StrCon Onde esta a String de Conexão:
                    StrCon obj = new StrCon();
                    // Cria um Novo objeto SqlConnection object usando a string de conexão 
                    SqlConnection conn = new SqlConnection(obj.sql);
                    // abre Conexão
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tblSenhaChefe", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tblSenhaChefeDataGrid.DataContext  = (dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Detalhe -->> " + ex);
            }
        }

        private void SenhaNovaAltera()
        {
            try
            {
                {
                    // Instancio a Classe StrCon Onde esta a String de Conexão:
                    StrCon obj = new StrCon();
                    // Cria um Novo objeto SqlConnection object usando a string de conexão 
                    SqlConnection conn = new SqlConnection(obj.sql);
                    // abre Conexão
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE tblSenhaChefe SET Nome = '" + PTextUsuarioNovo.Password + "' WHERE Codigo = '" + TextGuardaCampo.Text + "'" + "UPDATE tblSenhaChefe SET Senha = '" + PTexeSenhaNova.Password  + "' WHERE Codigo = '" + TextGuardaCampo.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tblSenhaChefeDataGrid.DataContext = (dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Detalhe -->> " + ex);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PTextUsuarioNovo.Focus();
            SenhaNova();
            // 3º Passo para Desabilitar o (X) de Fechar o Form em WPF.
            var hwnd = new WindowInteropHelper(this).Handle; SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void btnConcluido_Click(object sender, RoutedEventArgs e)
        {
            SenhaNovaAltera();
            Close();
            MainWindow main = new MainWindow();
            main.Show();
            App.Current.MainWindow.Close();

        }

        private void btnCancela_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow main = new MainWindow();
            main.Show();
            App.Current.MainWindow.Close();
        }
    }
}
