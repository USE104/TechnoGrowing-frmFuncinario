using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using Google.Protobuf.WellKnownTypes;
using Correios.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.ConstrainedExecution;
using Org.BouncyCastle.Utilities.Collections;


namespace InterfaceONGs
{
    public partial class frmONG : Form
    {

        public frmONG()
        {
            InitializeComponent();
        }

       
       


        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);


        private void frmONG_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);

       
        }


        private void limparCampos()
        {
            txtNomeONG.Clear();
            txtEmailONG.Clear();
            txtEnderecoONG.Clear();
            txtComplementoONG.Clear();
            txtsobreONG.Clear();
            txtSiteONG.Clear();
            txtBairroONG.Clear();
            txtCidadeONG.Clear();
            txtUrl.Clear();
            mkdTelONG.Clear();
            mkdNumONG.Clear();
            mkdCNPJONG.Clear();
            mkdCEPONG.Clear();
            txtSenhaONG.Clear();
            cbbRedeSocial.ResetText();

        }

        private void btnCriar_Click(object sender, EventArgs e)
        {

            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbONG (Nome,Email,Senha,Tel,CNPJ,Endereco,Numero,Complemento,CEP,Bairro,Cidade,Categoria,Descricao,WebSite,RedeSocial,Url)" + "values (@Nome,@Email,SHA2(@senha,256),@Tel,@CNPJ,@Endereco,@Numero,@Complemento,@CEP,@Bairro,@Cidade,@Categoria,@Descricao,@WebSite,@RedeSocial,@Url)";
            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();

            comm.Parameters.Add("@Nome", MySqlDbType.VarChar, 45).Value = txtNomeONG.Text;
            comm.Parameters.Add("@Email", MySqlDbType.VarChar, 100).Value = txtEmailONG.Text;
            comm.Parameters.Add("@Senha", MySqlDbType.VarChar, 100).Value = txtSenhaONG.Text;
            comm.Parameters.Add("@Tel", MySqlDbType.VarChar, 18).Value = mkdTelONG.Text;
            comm.Parameters.Add("@CNPJ", MySqlDbType.VarChar, 25).Value = mkdCNPJONG.Text;
            comm.Parameters.Add("@Endereco", MySqlDbType.VarChar, 100).Value = txtEnderecoONG.Text;
            comm.Parameters.Add("@Numero", MySqlDbType.VarChar, 10).Value = mkdNumONG.Text;
            comm.Parameters.Add("@Complemento", MySqlDbType.VarChar, 40).Value = txtComplementoONG.Text;
            comm.Parameters.Add("@CEP", MySqlDbType.VarChar, 10).Value = mkdCEPONG.Text;
            comm.Parameters.Add("@Bairro", MySqlDbType.VarChar, 45).Value = txtBairroONG.Text;
            comm.Parameters.Add("@Cidade", MySqlDbType.VarChar, 45).Value = txtCidadeONG.Text;
            comm.Parameters.Add("@Categoria", MySqlDbType.VarChar, 15).Value = cboCategoriaONG.Text;
            comm.Parameters.Add("@Descricao", MySqlDbType.VarChar, 150).Value = txtsobreONG.Text;
            comm.Parameters.Add("@WebSite", MySqlDbType.VarChar, 75).Value = txtSiteONG.Text;
            comm.Parameters.Add("@RedeSocial", MySqlDbType.VarChar, 35).Value = cbbRedeSocial.Text;
            comm.Parameters.Add("@Url", MySqlDbType.VarChar, 100).Value = txtUrl.Text;


            comm.Connection = Conexao.obterConexao();
            try
            {

                comm.ExecuteNonQuery();
                if (MessageBox.Show("Confirmar o cadastramento da ONG?", "Cadastro de ONG", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    MessageBox.Show("ONG Cadastrado", "Cadastro de ONG", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    limparCampos();
                }
                else
                {
                    MessageBox.Show("ONG não Cadastrada", "Cadastro de ONG", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Campos obrigatórios para concluir o cadastrado da ONG estão incorretos", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }


            Conexao.fecharConexao();

        }

        public string Nome = null;

        public void alterarONG(string Nome)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "update tbONG set Email = @Email, Senha = SHA2(@senha,256) ,Tel = @Tel,CNPJ= @CNPJ,Endereco= @Endereco,Numero = @Numero,Complemento = @Complemento,CEP = @CEP ,Bairro = @Bairro,Cidade = @Cidade, Categoria = @Categoria,Descricao = @Descricao,WebSite =@WebSite, RedeSocial =@RedeSocial, Url = @Url where 'Nome' =";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            comm.Parameters.Clear();

            comm.Parameters.Add("@Email", MySqlDbType.VarChar, 100).Value = txtEmailONG.Text;
            comm.Parameters.Add("@Senha", MySqlDbType.VarChar, 100).Value = txtSenhaONG.Text;
            comm.Parameters.Add("@Tel", MySqlDbType.VarChar, 18).Value = mkdTelONG.Text;
            comm.Parameters.Add("@CNPJ", MySqlDbType.VarChar, 25).Value = mkdCNPJONG.Text;
            comm.Parameters.Add("@Endereco", MySqlDbType.VarChar, 100).Value = txtEnderecoONG.Text;
            comm.Parameters.Add("@Numero", MySqlDbType.VarChar, 10).Value = mkdNumONG.Text;
            comm.Parameters.Add("@Complemento", MySqlDbType.VarChar, 40).Value = txtComplementoONG.Text;
            comm.Parameters.Add("@CEP", MySqlDbType.VarChar, 10).Value = mkdCEPONG.Text;
            comm.Parameters.Add("@Bairro", MySqlDbType.VarChar, 45).Value = txtBairroONG.Text;
            comm.Parameters.Add("@Cidade", MySqlDbType.VarChar, 45).Value = txtCidadeONG.Text;
            comm.Parameters.Add("@Categoria", MySqlDbType.VarChar, 15).Value = cboCategoriaONG.Text;
            comm.Parameters.Add("@Descricao", MySqlDbType.VarChar, 150).Value = txtsobreONG.Text;
            comm.Parameters.Add("@WebSite", MySqlDbType.VarChar, 75).Value = txtSiteONG.Text;
            comm.Parameters.Add("@RedeSocial", MySqlDbType.VarChar, 35).Value = cbbRedeSocial.Text;
            comm.Parameters.Add("@Url", MySqlDbType.VarChar, 100).Value = txtUrl.Text;

            comm.ExecuteNonQuery();

            if (MessageBox.Show("Confirma a alteração dos dados da ONG", "Messangem do Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                MessageBox.Show("Alterado com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                limparCampos();
            }
            else
            {
                MessageBox.Show("A alteração foi cancelada", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            Conexao.fecharConexao();

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            alterarONG(Nome);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirmar a limpeza dos campos de cadastramento da ONG?", "Cadastro de ONG", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                limparCampos();

                MessageBox.Show("Limpeza Concluída", "Mensagem Sistema");
            }
            else
            {
                MessageBox.Show("Limpeza Cancelada", "Mensagem Sistema");
            }
        }

 


        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmailONG.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenhaONG.Focus();
            }
        }
        private void txtSenhaONG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mkdTelONG.Focus();
            }

        }



        private void mkdTelFixo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mkdCNPJONG.Focus();
            }
        }

        private void mkdCNPJ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEnderecoONG.Focus();
            }
        }

        private void txtEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mkdNumONG.Focus();
            }
        }

        private void txtNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtComplementoONG.Focus();
            }
        }

        private void txtComplemento_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                cboCategoriaONG.Focus();
            }

        }

        private void cboCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsobreONG.Focus();
            }
        }

        private void txtsobreONG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSiteONG.Focus();
            }

        }

        private void txtSite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUrl.Focus();
            }
        }

        

        public void carregaCEP(string cep)
        {
            WSCorreios.AtendeClienteClient ws = new WSCorreios.AtendeClienteClient();
            try
            {
                WSCorreios.enderecoERP endereco = ws.consultaCEP(cep);
                txtEnderecoONG.Text = endereco.end;
                txtBairroONG.Text = endereco.bairro;
                txtCidadeONG.Text = endereco.cidade;

                mkdNumONG.Focus();
            }

            catch (Exception)
            {

                MessageBox.Show("CEP não encontrado", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                mkdCEPONG.Text = "";
                mkdCEPONG.Focus();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmAdministrador Paginaadmin = new frmAdministrador();
            Paginaadmin.Show();
            this.Hide();
        }

        private void mkdCEP_Leave(object sender, EventArgs e)
        {
            carregaCEP(mkdCEPONG.Text);
        }

        private void mkdCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                carregaCEP(mkdCEPONG.Text);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "delete from tbONG where Email = @Email";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            comm.Parameters.Clear();
            comm.Parameters.Add("@Email", MySqlDbType.VarChar, 100).Value = txtEmailONG.Text;
            comm.ExecuteNonQuery();


            if (MessageBox.Show("Confirma a exclusão da ONG do Banco de dados ?", "Mensagem do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                MessageBox.Show("Dados da ONG Excluído com Sucesso !", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Dados da ONG para Exclusão Cancelada !", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisarONG abrirPesquisa = new frmPesquisarONG();  
            abrirPesquisa.Show();
        }


    }
}
