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
//
using System.Data;
using System.Data.SqlClient;

namespace CadCondôminos
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrmCondominio_Initialized(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }

        private void Senha(String Nome, String Senha)
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
                    FrmCadCond frmcadCond = new FrmCadCond();
                    frmcadCond.Show();
                    Close(); // Fecha o Form da Senha.
                }
                else
                {
                    MessageBox.Show("Dados não Conferem, Login e/ou Senha incorretos...", "ERRO!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtLogin.IsReadOnly = true;
                    txtSenha.IsEnabled  = false;
                }
                try
                {
                    dr.Close();
                    conn.Close();
                }
                catch (Exception )
                {
                   //
                }
                txtLogin.Text = string.Empty;
                txtSenha.Password  = string.Empty;
                txtLogin.IsReadOnly = false;
                txtSenha.IsEnabled = true;
                chkBoxMudaSenha.IsChecked  = false;
                txtLogin.Focus();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Detalhe: " + ex.Message + "Tente o Acesso assim que o Servidor e/ou a Reade forem restabelecidos.... O SISTEMA SERÁ ENCERRADO!!!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)

            /* Esse Método Abaixo, serve para deslocar com a tecla "ENTER" ao Ivés de "TAB";
             Só que em WPF Aplication, usa-se o Metodo abaixo no Formulário.cs, além de um comando: 
             (UIElement.PreviewKeyDown = "Grid_PreviewKeyDown")
             Dentro de Cada Controle onde haverá o Deslocamento no Código "xaml"
             Exemplo:  <TextBox x:Name="txtLogin" UIElement.PreviewKeyDown = "Grid_PreviewKeyDown".../>
            */
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
   
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            Senha(txtLogin.Text, txtSenha.Password);
        }

        private void FrmCondominio_Loaded(object sender, RoutedEventArgs e)
        {
            txtLogin.Focus();
            chkBoxMudaSenha.IsChecked = false;
            txtLogin.Text = string.Empty;
            txtSenha.Password = string.Empty;
        }

        private void chkBoxMudaSenha_Checked(object sender, RoutedEventArgs e)
        {
            FrmMudaSenha frmMudaSenha = new FrmMudaSenha();
            frmMudaSenha.ShowDialog();
        }
    }
}
