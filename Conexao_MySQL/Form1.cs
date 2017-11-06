using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conexao_MySQL
{
    public partial class Form1 : Form
    {
        private void modoNavegacao() // desabilitando os itens de edicao
        {
            txtNome.Enabled = false;
            //  textNome2.Enabled = false;
            txtEndereco.Enabled = false;
            txtNome3.Enabled = false;
            txtQuita.Enabled = false;
            textJuro.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            maskCEP.Enabled = false;
            btnLimpar.Enabled = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnCancel.Enabled = false;
            btnSair.Enabled = true;
            dateCadastro.Enabled = false;
            DGClientes.Enabled = true;
            DGEmprestimo.Enabled = true;
            DGDevedor.Enabled = true;
        }
        private void modoEdicao() // habilitando os itens de edicao
        {
            txtNome.Enabled = true;
            //  textNome2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            txtNome3.Enabled = true;
            txtQuita.Enabled = true;
            textJuro.Enabled = true;
            txtEndereco.Enabled = true;
            maskCEP.Enabled = true;
            btnLimpar.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnCancel.Enabled = true;
            btnSair.Enabled = false;
            dateCadastro.Enabled = true;
            DGClientes.Enabled = false;
            DGEmprestimo.Enabled = false;
            DGDevedor.Enabled = false;
        }

        public void limparCampos()
        {
            clientesController clienteController = new clientesController();
            emprestimoController emprestimoController = new emprestimoController();
            //limpando campos
            txtID.Clear();
            txtNome.Clear();
            textNome2.Clear();
            txtValor.Clear();
            txtJuros.Clear();
            txtDia.Clear();
            txtStatus.Clear();
            txtEndereco.Clear();
            maskCEP.Clear();
            txtNome.Focus(); //cursor volta ao campo nome
            DGClientes.DataSource = clienteController.getClientes(); //preenchendo o DataGrid
            DGEmprestimo.DataSource = emprestimoController.getEmprestimo();
            DGDevedor.DataSource = emprestimoController.getDevedor();
            DGSumario1.DataSource = emprestimoController.getSumario1();
            DGSumario2.DataSource = emprestimoController.getSumario2();
        }

        public Form1()
        {
            InitializeComponent();
            modoNavegacao(); //iniciando o form com os campos bloqueados
            //preenchendo o DataGrid:
            clientesController clienteController = new clientesController();
            DGClientes.DataSource = clienteController.getClientes();
            emprestimoController emprestimoController = new emprestimoController();
            DGEmprestimo.DataSource = emprestimoController.getEmprestimo();
            emprestimoController emprestimoController1 = new emprestimoController();
            DGDevedor.DataSource = emprestimoController1.getDevedor();
            emprestimoController emprestimoController2 = new emprestimoController();
            DGSumario1.DataSource = emprestimoController2.getSumario1();
            emprestimoController emprestimoController3 = new emprestimoController();
            DGSumario2.DataSource = emprestimoController3.getSumario2();
            emprestimoController emprestimoController4 = new emprestimoController();
            DGGiro.DataSource = emprestimoController3.getGiro();


        }

        private void btnSalvar_Click(object sender, EventArgs e) //salvando novo registro
        {
            if (txtNome.Text.Equals(""))
            {
                MessageBox.Show("Nome não pode ser vazio", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF se o campo nome estiver vazio (poderia implementar mais validaçoes)
            }
            try
            {
                clientesController clienteController = new clientesController(); //objeto para acessar as funcoes de manuseio do BD

                if (txtID.Text == "") //se o campo ID estiver vazio, é novo registro
                {
                    clientes novoCliente = new clientes(txtNome.Text, txtEndereco.Text, maskCEP.Text, dateCadastro.Value.Date);
                    clienteController.IncluirNovoCliente(novoCliente); //cadastrando novo cliente
                }
                else //aí é salvar alguma edição
                {
                    clientes editarCliente = new clientes(Convert.ToInt32(txtID.Text), txtNome.Text, txtEndereco.Text, maskCEP.Text);
                    clienteController.AtualizarCliente(editarCliente); //efetuando a atualização do registro
                }
                MessageBox.Show("Dados registrados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpar.PerformClick(); //chamando o evento click do botao limpar
                modoNavegacao(); //desabilitando os campos apos salvar
            }
            catch (Exception erro)
            {//se der erro
                MessageBox.Show("Houve o seguinte erro: " + erro.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e) //limpando os campos e o dando um refresh nos dados de memória
        {
            limparCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //habilitando campos e botoes para cadastro
            modoEdicao();
            txtNome.Focus(); //cursor no txtNome
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //desabilitando campos
            modoNavegacao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close(); //encerrando a aplicação
        }

        private void DGClientes_Click(object sender, EventArgs e)
        {
            if (DGClientes.CurrentRow.Cells[0].Value.ToString() != null)
            {
                txtID.Text = DGClientes.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = DGClientes.CurrentRow.Cells[1].Value.ToString();
                textNome2.Text = DGClientes.CurrentRow.Cells[1].Value.ToString();
                txtEndereco.Text = DGClientes.CurrentRow.Cells[2].Value.ToString();
                maskCEP.Text = DGClientes.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") //testando se há registro selecionado
            {
                MessageBox.Show("Selecione um registro na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF
            }
            modoEdicao(); //habilita a edicao dos campos
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") //testando se há registro selecionado
            {
                MessageBox.Show("Selecione um registro na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF
            }
            clientesController clienteController = new clientesController(); //criando e instanciando o objeto para fazermos a exclusao
            clienteController.ExcluirCliente(Convert.ToInt32(txtID.Text)); //excluindo
            limparCampos();
            MessageBox.Show("Registro excluido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void DGClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            emprestimoController emprestimoController = new emprestimoController();
            emprestimo novoEmprestimo = new emprestimo(textNome2.Text, Convert.ToDecimal(txtValor.Text), Convert.ToDecimal(txtJuros.Text), Convert.ToDateTime(txtDia.Text), txtStatus.Text);
            emprestimoController.IncluirNovoEmprestimo(novoEmprestimo); //cadastrando novo cliente

            MessageBox.Show("Dados registrados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLimpar.PerformClick(); //chamando o evento click do botao limpar
            modoNavegacao(); //desabilitando os campos apos salvar
            DGEmprestimo.DataSource = emprestimoController.getEmprestimo();
        }

        private void DGEmprestimo_Click(object sender, EventArgs e)
        {

            Decimal resultado;
            decimal cell1 = Convert.ToDecimal(DGEmprestimo.CurrentRow.Cells[4].Value);
            decimal cell2 = Convert.ToDecimal(DGEmprestimo.CurrentRow.Cells[2].Value);
            resultado = cell1 + cell2;

            if (DGEmprestimo.CurrentRow.Cells[0].Value.ToString() != null)
            {
                textBox1.Text = DGEmprestimo.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = DGEmprestimo.CurrentRow.Cells[1].Value.ToString();
                txtQuita.Text = Convert.ToString(resultado);
                textJuro.Text = DGEmprestimo.CurrentRow.Cells[4].Value.ToString();
                textBox2.Text = DGEmprestimo.CurrentRow.Cells[8].Value.ToString();
                // textNome2.Text = DGClientes.CurrentRow.Cells[1].Value.ToString();
                txtNome3.Text = DGEmprestimo.CurrentRow.Cells[1].Value.ToString();


            }
        }

        private void btnPG_Click(object sender, EventArgs e)
        {
            modoEdicao();
            emprestimoController emprestimoController = new emprestimoController();
            emprestimo emprestimo = new emprestimo(Convert.ToDecimal(txtPG.Text), Convert.ToInt32(textBox1.Text), Convert.ToDateTime(txtNovaData.Text));
            emprestimoController.PagarEmprestimo(emprestimo); //cadastrando novo cliente
            MessageBox.Show("Dados registrados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLimpar.PerformClick(); //chamando o evento click do botao limpar
            modoNavegacao(); //desabilitando os campos apos salvar
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            modoEdicao();
            emprestimoController emprestimoController = new emprestimoController();
            emprestimo emprestimo = new emprestimo(Convert.ToInt32(textBox1.Text));
            emprestimoController.QuitarEmprestimo(emprestimo); //cadastrando novo cliente

            MessageBox.Show("Dados registrados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLimpar.PerformClick(); //chamando o evento click do botao limpar
            modoNavegacao(); //desabilitando os campos apos salvar
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void DGDevedor_Click(object sender, EventArgs e)
        {
            if (DGDevedor.CurrentRow.Cells[0].Value.ToString() != null)
            {
                DGDevedor.CurrentRow.Cells[0].Value.ToString();
                DGDevedor.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void DGSumario2_Click(object sender, EventArgs e)
        {
            if (DGSumario1.CurrentRow.Cells[0].Value.ToString() != null)
            {
                DGSumario1.CurrentRow.Cells[0].Value.ToString();
                DGSumario1.CurrentRow.Cells[1].Value.ToString();
            }
        }
        private void DGGiro_Click(object sender, EventArgs e)
        {
            if (DGGiro.CurrentRow.Cells[0].Value.ToString() != null)
            {
                DGGiro.CurrentRow.Cells[0].Value.ToString();
                DGGiro.CurrentRow.Cells[1].Value.ToString();
            }
        }

    }
}
    
    

