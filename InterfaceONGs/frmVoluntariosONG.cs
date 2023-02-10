using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace InterfaceONGs
{
    public partial class frmVoluntariosONG : Form
    {
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmVoluntariosONG()
        {
            InitializeComponent();
        }


        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmAdministrador abrirAdmin = new frmAdministrador();
            abrirAdmin.Show();
            //esconder a página anterior da página de login
            this.Hide();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);

            buscainfs();

        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbVoluntario (Nome,dataNascimento,Email,Senha,CPF,Tel,Experiencia,RedeSocial,Url)" + "values (@Nome,@dataNascimento,@Email,SHA2(@Senha,256),@CPF,@Tel,@Experiencia,@RedeSocial,@Url)";
            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();


            comm.Parameters.Add("@Nome", MySqlDbType.VarChar, 45).Value = txtNome.Text;
            comm.Parameters.Add("@dataNascimento", MySqlDbType.Date).Value = dtpNasc.Value;
            comm.Parameters.Add("@Email", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            comm.Parameters.Add("@Senha", MySqlDbType.VarChar, 45).Value = txtSenha.Text;
            comm.Parameters.Add("@CPF", MySqlDbType.VarChar, 45).Value = mkdCPF.Text;
            comm.Parameters.Add("@Tel", MySqlDbType.VarChar, 45).Value = mkdCelular.Text;
            comm.Parameters.Add("@Experiencia", MySqlDbType.VarChar, 12).Value = cboEXP.Text;
            comm.Parameters.Add("@RedeSocial", MySqlDbType.VarChar, 20).Value = cboSocias.Text;
            comm.Parameters.Add("@Url", MySqlDbType.VarChar, 100).Value = txtURL.Text;


            comm.Connection = Conexao.obterConexao();

            comm.ExecuteNonQuery();


            if (MessageBox.Show("Confirmar o cadastramento do voluntario?", "Mensagem do Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {

                MessageBox.Show("Voluntário Cadastrado", "Mensagem do Sistema");
                LimparCampos();

            }
            else
            {
                MessageBox.Show("Voluntário  não Cadastrado", "Mensagem do Sistema");
            }


            Conexao.fecharConexao();
        }


        private void buscainfs()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbVoluntario  where  CPF like '%" + mkdCPF.Text + "%'";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader dr;

            txtNome.Clear();
            mkdCPF.ResetText();
            dtpNasc.ResetText();
            txtEmail.Clear();
            txtSenha.Clear();
            mkdCPF.ResetText();
            mkdCelular.ResetText();
            cboEXP.Text = "";
            cboSocias.Text = "";
            txtURL.Clear();
            cboSocias.Text = "";

            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                txtNome.Text = dr.GetString(1);
                dtpNasc.Text = dr.GetString(2);
                txtEmail.Text= dr.GetString(3);
                txtSenha.Text = dr.GetString(4);
                mkdCPF.Text = dr.GetString(5);
                mkdCelular.Text = dr.GetString(6);
                cboEXP.Text = dr.GetString(7);
                cboSocias.Text = dr.GetString(8);
                txtURL.Text = dr.GetString(9);
            }
            Conexao.fecharConexao();

        }


        public string CPF = null;

        public void alterarUsuario(string CPF)
        {

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "UPDATE `tbVoluntario` SET Nome= @Nome,Email = @Email,Senha = SHA2(@Senha, 256),CPF =@CPF,Tel = @tel,Experiencia = @Experiencia,Url = @Url WHERE CPF = " + mkdCPF.Text + ";";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            comm.Parameters.Clear();
            comm.Parameters.Add("@Nome", MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@Email", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            comm.Parameters.Add("@Senha", MySqlDbType.VarChar, 45).Value = txtSenha.Text;
            comm.Parameters.Add("@CPF",MySqlDbType.VarChar,45).Value = mkdCPF.Text;
            comm.Parameters.Add("@Tel", MySqlDbType.VarChar, 45).Value = mkdCelular.Text;
            comm.Parameters.Add("@Experiencia", MySqlDbType.VarChar, 12).Value = cboEXP.Text;
            comm.Parameters.Add("@RedeSocial", MySqlDbType.VarChar, 20).Value = cboSocias.Text;
            comm.Parameters.Add("@Url", MySqlDbType.VarChar, 100).Value = txtURL.Text;

            comm.ExecuteNonQuery();


            if (MessageBox.Show("Confirma as alterações de Informações do Voluntário ? ", "Mensagem do sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                MessageBox.Show("Informações do Voluntário Alterado ", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                LimparCampos();
            }

            Conexao.fecharConexao();
        }
        string pegainfs;
        string pegaVoluntario;


        private void btnAlterar_Click(object sender, EventArgs e)
        {
            alterarUsuario(CPF);
        }


        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpNasc.Focus();
            }
        }

        private void dtpNasc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mkdCPF.Focus();
            }
        }

        private void mkdCPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mkdCelular.Focus();
            }
        }

        private void mkdCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboEXP.Focus();
            }

        }

        private void cboEXP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboSocias.Focus();
            }
        }

        private void cboSocias_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtURL.Focus();
        }



        private void btnDeletar_Click(object sender, EventArgs e)
        {
            DialogResult vresp;

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "delete from tbVoluntario where CPF = @CPF";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            comm.Parameters.Clear();
            comm.Parameters.Add("@CPF", MySqlDbType.VarChar, 45).Value = mkdCPF.Text;
            comm.ExecuteNonQuery();
            vresp = MessageBox.Show("Deseja apagar o Cadastro do Usuário ?", "Mensagem do sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (vresp == DialogResult.Yes)
            {

                MessageBox.Show("Voluntário excluído com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                LimparCampos();

            }
            else
            {
                MessageBox.Show("Impossível excluir Voluntário. Pois, ele possui vínculo de dados com outros dados no Banco de Dados!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            Conexao.fecharConexao();

        }

        private void LimparCampos() {
            txtNome.Clear();
            mkdCPF.ResetText();
            dtpNasc.ResetText();
            txtEmail.Clear();
            txtSenha.Clear();
            mkdCPF.ResetText();
            mkdCelular.ResetText();
            cboEXP.Text = "";
            cboSocias.Text = "";
            txtURL.Clear();
            cboSocias.Text = "";
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisarVoluntarios PesquisarVoluntario = new frmPesquisarVoluntarios();
            PesquisarVoluntario.Show();
        }

        private void cboVoluntarios_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
