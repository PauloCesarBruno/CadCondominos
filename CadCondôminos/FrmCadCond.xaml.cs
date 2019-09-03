using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CadCondôminos
{
    /// <summary>
    /// Lógica interna para FrmCadCond.xaml
    /// </summary>
    public partial class FrmCadCond : Window
    {

        public FrmCadCond()
        {
            InitializeComponent();
        }

        private void CarregaDados()
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
                    SqlCommand cmd = new SqlCommand("SELECT tblNome.IdNome as Código ,tblNome.Nome, tblNome.Imagem, tblEntradaApto.IdEntr_Apto as ID, tblEntradaApto.Entrada_Apto as Entrada FROM tblNome  INNER JOIN tblEntradaApto   ON tblNome.IdEntr_Apto = tblEntradaApto.IdEntr_Apto", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;
                    
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Detalhe -->> " + ex);
            }
        }

        private void CarregaDadosNoList()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tblEntradaApto ", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ListUnidades.SelectedValuePath = "IdEntr_Apto";
                    ListUnidades.DisplayMemberPath = "Entrada_Apto";
                    ListUnidades.ItemsSource = dt.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Detalhe -->> " + ex);
            }
        }

        private void ListBoxCondominos()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT tblNome.IdNome as Código ,tblNome.Nome, tblNome.Imagem, tblEntradaApto.IdEntr_Apto as ID, tblEntradaApto.Entrada_Apto as Entrada FROM tblNome  INNER JOIN tblEntradaApto  ON tblNome.IdEntr_Apto = tblEntradaApto.IdEntr_Apto WHERE tblNome.IdEntr_Apto = " + ListUnidades.SelectedValue, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception)
            {
                //
            }
        }


        private void Novo_Morador()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    string Query = "SELECT * FROM tblEntradaApto WHERE Entrada_Apto= '" + ListUnidades.Text + "' ";
                    SqlCommand createCommand = new SqlCommand(Query, conn);
                    SqlDataReader dr = createCommand.ExecuteReader();
                    while (dr.Read())
                    {

                        string IdEntr_Apto = dr.GetInt32(0).ToString();
                        string Entrada_Apto = dr.GetString(1);

                        txtCod_EntradaApto.Text = IdEntr_Apto;
                        txtEntrada_Apto.Text = Entrada_Apto;
                        txtCod_EntradaApto.IsReadOnly = true;
                        txtEntrada_Apto.IsReadOnly = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void InseriDados()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    SqlCommand cmd = new SqlCommand("insert into tblNome (Nome, IdEntr_Apto,Imagem) Values  ('" + txtNome.Text + "','" + txtCod_EntradaApto.Text + "','" + Textnome.Text + "')  Select  Nome FROM tblNome", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception)
            {
                // 
            }
        }

        private void DeletaDados()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tblNome WHERE IdNome = '" + txtCod_nome.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Detalhe -->> " + ex);
            }
        }

        private void AlteraDados()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE tblNome SET Nome = '" + txtNome.Text + "' WHERE IdNome = '" + txtCod_nome.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;
                    conn.Close();
                }
            }
            catch (Exception)
            {
                //  
            }
        }
     
        private void BuscaporNome()
        {
            try
            {
                {
                    StrCon obj = new StrCon();
                    SqlConnection conn = new SqlConnection(obj.sql);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT tblNome.IdNome as Código ,tblNome.Nome, tblNome.Imagem, tblEntradaApto.IdEntr_Apto as ID,tblEntradaApto.Entrada_Apto as Entrada FROM tblNome  INNER JOIN tblEntradaApto   ON tblNome.IdEntr_Apto = tblEntradaApto.IdEntr_Apto WHERE Nome LIKE '%" + txtBuscar.Text + "%'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDados.DataContext = dt.DefaultView;

                    conn.Close();

                }
            }
            catch (Exception)
            {
                // 
            }
        }

        private void itemSair_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult per = MessageBox.Show("Deseja Realmente Sair Sim ou Não ???", "SAIDA:", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (per != MessageBoxResult.Yes)
            {
                MessageBox.Show("Ok, Mantendo...!!!", "AVISO:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Até a Próxima !!!", "AVISO:", MessageBoxButton.OK, MessageBoxImage.Question);
            this.Close();
        }


        private void FrmCadConôminos_Loaded(object sender, RoutedEventArgs e)
        {
            Textnome.IsReadOnly  = true;
            txtBuscar.Focus();
            //CarregaDados();
            CarregaDadosNoList();
            txtCod_nome.IsReadOnly = true;
            txtNome.IsReadOnly = true;
            txtCod_EntradaApto.IsReadOnly = true;
            txtEntrada_Apto.IsReadOnly = true;
            Textnome.Text = string.Empty;
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            CarregaDados();
            txtBuscar.Text = string.Empty;
            Textnome.Clear();
            btnInserir.IsEnabled = false;
            btnAlterar.IsEnabled = true;
            btnExcluir.IsEnabled = true;
            Textnome.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void ListaUnidades_DropDownClosed(object sender, EventArgs e) // Evento
        {
            ListBoxCondominos();
            Novo_Morador();
            txtNome.Focus();
            btnImagem.Focus();
            txtCod_nome.Text = string.Empty;
            txtNome.Text = string.Empty;
            Textnome.Text = string.Empty;
           // PicFoto.Source = null;
        }

            private void Grava_Imagem()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                byte[] dadosImagem = new byte[fs.Length];
                fs.Read(dadosImagem, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                PicFoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));

                Textnome.Text = dlg.FileName.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :: " + ex.Message);
            }
        }

        private void Le_Imagem()
        {
            // ====================================================================
            // Ler Imadem A Partir do Caminho Gravado no "Textnome.Text"...
            // ====================================================================

            try
            {

                FileStream fs = File.Open(Textnome.Text, FileMode.Open);
                System.Drawing.Bitmap dImg = new System.Drawing.Bitmap(fs);
                MemoryStream ms = new MemoryStream();
                dImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                System.Windows.Media.Imaging.BitmapImage bImg = new System.Windows.Media.Imaging.BitmapImage();
                bImg.BeginInit();
                bImg.StreamSource = new MemoryStream(ms.ToArray());
                bImg.EndInit();
                fs.Close();
                fs.Dispose(); // Dispensa da Memória para mostrar outra imagem, para não dar Erro.
               
                PicFoto.Source = bImg;
            }
            catch (Exception)
            {
                MessageBox.Show("Se a nova Imagem do novo regidtro não Carregou, clique em outra qualquer e de pois volte a clicar nela, faça isso a té que a mesma aparecça normalmente. Isso não é um 'Bug', trata-se de um procedimento interno de sua Máquina para Alocar o novo registro de Imagem em Memóeria, uma vez carregada, não haverá mais Problema algum..." , "INFORMAÇÃO IMPORTANTE !!!", MessageBoxButton .OK ,MessageBoxImage.Information);
            }

        }


        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            PicFoto.Source = null;
            ListUnidades.Focus();
            txtNome.IsEnabled = false;
            txtCod_nome.Text = string.Empty;
            txtNome.Text = string.Empty;
            MessageBox.Show("ATENÇÃO: Escolha a Entrada e o Apartamento na Caixa de Istagem ('Entradas / Apto´s') no canto Superior-Direito do Formulário, feito isso clique no Botão ('Escolher Imagem'), Que Sera habilitado no canto Inferior-Esquerdo do Formulário, escolha Uma Imagem (FOTO), clicando no botão. Enfim, dentro do campo ('Manipulação) serão habilitados os botões 'GRAVAR' & 'CANCELAR', Clique no botão 'GRAVAR' para gravar o registro, ou botão 'CANCELAR', caso desista de inserir o novo Registro.", "INSTRUÇÕES:", MessageBoxButton.OK, MessageBoxImage.Information);
            txtNome.IsReadOnly = false;
            btnInserir.IsEnabled = true;
            btnImagem.IsEnabled = true;
            btnExcluir.IsEnabled = false;
            btnAlterar.IsEnabled = false;
            btnListar.IsEnabled = false;
            btnCancelar.IsEnabled = true;
            txtBuscar.IsEnabled = false;
        }

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            txtNome.IsReadOnly = true;
            if (txtNome.Text == string.Empty)
            {
                MessageBox.Show("Digite o Nome do Novo Morador...", "Aviso:", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNome.IsReadOnly = false;
                txtNome.Focus();
                return;
            }
            else
            {
                InseriDados();
                CarregaDados();
                btnInserir.IsEnabled = false;
                btnExcluir.IsEnabled = true;
                btnAlterar.IsEnabled = true;
                btnListar.IsEnabled = true;
                btnCancelar.IsEnabled = false;
                txtBuscar.IsEnabled = true;
                PicFoto.Source = null;
            }

            if (txtCod_EntradaApto.Text == string.Empty || txtEntrada_Apto.Text == string.Empty)
            {
                MessageBox.Show("Clique em (Entradas / Apto´s) Para Escolher Entrada e Apartamento...", "Aviso:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ListUnidades.Text = string.Empty;
            txtCod_nome.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCod_EntradaApto.Text = string.Empty;
            txtEntrada_Apto.Text = string.Empty;
            Textnome.Text = string.Empty;
            CarregaDados();
        }

        private void txtBuscar_TextChanged_(object sender, TextChangedEventArgs e)
        {
            txtNome.IsReadOnly = false;
            BuscaporNome();
        }

        public void dgvDados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Rotina para preencher TextBox´s Clicando na Linha do DGV
            // Codificar como abaixo, no EVENTO "_SelectionChanged"
            DataGrid gd = (DataGrid)sender;
            DataRowView linha_selecionada = gd.SelectedItem as DataRowView;
            if (linha_selecionada != null)
            {
                txtCod_nome.Text = linha_selecionada["Código"].ToString();
                txtNome.Text = linha_selecionada["Nome"].ToString();
                Textnome.Text = linha_selecionada["Imagem"].ToString();
                txtCod_EntradaApto.Text = linha_selecionada["ID"].ToString();
                txtEntrada_Apto.Text = linha_selecionada["Entrada"].ToString();
                txtNome.IsReadOnly = false;
                Le_Imagem(); // Método onde va ler a Imagem

            }
        }

        private void btnExcluir_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBoxResult per = MessageBox.Show("Deseja Realmente Excluir Este Registro", "EXCLUSÃO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (per != MessageBoxResult.Yes)
            {
                MessageBox.Show("Ok, Registro Mantido !!!", "EXCLUSÃO", MessageBoxButton.OK, MessageBoxImage.Information);
                CarregaDados();
                txtBuscar.Text = string.Empty;
                return;
            }
            MessageBox.Show("Registro Excluido com Sucesso !!!", "EXCLUSÃO", MessageBoxButton.OK, MessageBoxImage.Question);
            DeletaDados();
            CarregaDados();
            txtBuscar.Text = string.Empty;
            Textnome.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            AlteraDados();
            CarregaDados();
            MessageBoxResult pergunta = MessageBox.Show("Deseja Alterar a Foto Tambem ??? (Sim ou Não ???)", "SAIDA:", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (pergunta != MessageBoxResult.Yes)
            {
                MessageBox.Show("Ok, Mantendo a FOTO Atual...!!!", "AVISO:", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBuscar.Text = string.Empty;
                return;
            }
            else
            {
                MessageBox.Show("Neste Caso Será Necessário a Exclusão do Registro para ser RE-INSERIDO Com outros dados (Nome e/ou Foto); Basta Proceder da seguinte Maneira: Apague (Botão Excluir) o Registro Cujo a foto sera Alterada e Grave um novo com uma nova Foto e/ou Nome...", "INSTRUÇÕES:", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBuscar.Text = string.Empty;
                txtBuscar.Focus();
            }
            ListUnidades.Text = string.Empty;
            txtCod_nome.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCod_EntradaApto.Text = string.Empty;
            txtEntrada_Apto.Text = string.Empty;
            txtBuscar.Text = string.Empty;
            Textnome.Text = string.Empty;
        }


        private void btnSair_Click_(object sender, RoutedEventArgs e)
        {
            MessageBoxResult per = MessageBox.Show("Deseja Realmente Sair??? (Sim ou Não ???)", "SAIDA:", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (per != MessageBoxResult.Yes)
            {
                MessageBox.Show("Ok, Mantendo...!!!", "AVISO:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Até a Próxima !!!", "AVISO:", MessageBoxButton.OK, MessageBoxImage.Question);
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            frmSobre sobre = new frmSobre();
            sobre.ShowDialog();
        }

        private void btnImagem_Click(object sender, RoutedEventArgs e)
        {
            Grava_Imagem(); // Chama o Método que Grava a Imagem.
            txtNome.IsEnabled = true;
            txtNome.Focus();
            btnImagem.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnImagem.IsEnabled = false;
            btnInserir.IsEnabled = false;
            btnCancelar .IsEnabled =false ;
            btnAlterar.IsEnabled = true;
            btnExcluir.IsEnabled = true;
            btnListar.IsEnabled = true;
            txtNome.IsEnabled = true;
            txtBuscar.IsEnabled = true;
            txtBuscar.Focus();
            CarregaDados();
        }
    }
}