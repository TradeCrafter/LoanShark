using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Configuration; //para acessarmos arquivo xml de configuração, onde deixamos nossa string de conexao

//bibliotecas MySQL:
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Conexao_MySQL
{
    class emprestimoController //classe de camada de acesso aos dados (Controller)
    {
        string Conexao;

        //metodo construtor 
        public emprestimoController()
        {
            //obtendo a string de conexao do arquivo App.config
            Conexao = ConfigurationManager.AppSettings["ConexaoMySQL"];
        }

        //incluindo um novo registro fazendo conexao manual:
        public void IncluirNovoEmprestimo(emprestimo emprestimo)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "INSERT INTO emprestimo (id3, nome, valor, dia, juros, atraso, status) VALUES (?id3,?nome, ?valor, ?dia, ?juros, ?atraso, ?status)"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id3", emprestimo.id3);
            comando.Parameters.AddWithValue("?nome", emprestimo.Nome); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
            comando.Parameters.AddWithValue("?valor", emprestimo.Valor);
            comando.Parameters.AddWithValue("?dia", emprestimo.Dia);
            comando.Parameters.AddWithValue("?juros", emprestimo.Juros);
            comando.Parameters.AddWithValue("?atraso", emprestimo.Atraso);
            comando.Parameters.AddWithValue("?status", emprestimo.Status);

            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //excluindo registro
        public void ExcluirEmprestimo(int id3)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "DELETE FROM emprestimo WHERE id3=?id3"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id3", id3);
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //incluindo um novo registro fazendo conexao manual:
        public void AtualizarEmprestimo(emprestimo emprestimo)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "UPDATE emprestimo set pagamento = ?pagamento, dia = ?dia WHERE id=?id"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id", emprestimo.id3); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
          //comando.Parameters.AddWithValue("?pagamento", emprestimo.Pagamento);
            comando.Parameters.AddWithValue("?dia", emprestimo.Dia);
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
        //--========================================================================================================================
        public void PagarEmprestimo(emprestimo emprestimo)
        {
            
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "UPDATE emprestimo SET pagamento = pagamento + ?pagamento, dia = ?dia, status = 'ATIVO'   WHERE id3= ?id3"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id3", emprestimo.id3); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
            comando.Parameters.AddWithValue("?pagamento", emprestimo.Pagamento);
            comando.Parameters.AddWithValue("?dia", emprestimo.Dia);//  comando.Parameters.AddWithValue("?pagamento", emprestimo.Pagamento);

            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
        public void QuitarEmprestimo(emprestimo emprestimo)
        {

            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "UPDATE emprestimo SET Status = 'QUITADO', lucro = pagamento-valor  WHERE id3= ?id3"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id3", emprestimo.id3); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
           // comando.Parameters.AddWithValue("?pagamento", emprestimo.Pagamento); //  comando.Parameters.AddWithValue("?pagamento", emprestimo.Pagamento);

            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
        //
        public DataTable getSumario1()
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            comando.CommandText = "SELECT SUM(lucro)LUCRO FROM emprestimo WHERE status = 'QUITADO'"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtSumario = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtSumario); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtSumario;
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        public DataTable getGiro()
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            comando.CommandText = "SELECT SUM(valor)CAPITAL_GIRO FROM emprestimo WHERE status = 'QUITADO'"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtGiro = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtGiro); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtGiro;
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        public DataTable getSumario2()
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            comando.CommandText = "SELECT SUM(Valor)VALOR_INVESTIDO FROM emprestimo WHERE status = 'ATIVO'"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtSumario2 = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtSumario2); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtSumario2;
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //selecionando os registros da base de dados:
        public DataTable getEmprestimo()
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            comando.CommandText = "SELECT * FROM emprestimo"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtClientes = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtClientes); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtClientes;
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
        public class Devedor
        {
            public int dia { get; set; }
            public DateTime diaHj { get; set; }
        }
        public DataTable getDevedor()
        {
            System.DateTime diaHoje = DateTime.Today;
            float _hoje = diaHoje.Day;
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            //comando.CommandText = "SELECT dia FROM emprestimo";
            comando.CommandText = "select * from emprestimo where dia between date_add(current_date, interval -7 day) and current_date"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtDevedor = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtDevedor); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtDevedor;
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
    }
}
