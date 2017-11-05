using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexao_MySQL
{
    class emprestimo //camada de negócios (Model)
    {
        //metodos privados para proteção e encapsulamento das informações
        private int _id;
        private int _id2;
        private string _nome;
        private Decimal _valor;
        private string _status;
        private DateTime _dia;
        private Decimal _juros;
        private int _atraso;
        private Decimal _pagamento;
       // private int txtID2;
        private string textNome2;
        private Decimal txtValor;
        private float txtDia;
        private Decimal txtJuros;
        private string txtStatus;
        private int _id3;
        private int v;


        //metodo construtor com sobrecarga
        public emprestimo() { }

        public emprestimo(string nome, Decimal valor, DateTime dia, Decimal juros, int atraso, String status) //para novos registros
        {
            //atribuindo os valores
            this.Nome = nome;
            this.Valor = valor;
            this.Dia = dia;
            this.Juros = juros;
            this.Atraso = atraso;
            this.Status = status;
        }
        public emprestimo(int id, string nome, Decimal valor, DateTime dia, Decimal juros, int atraso, String status) //para edicao de registros
        {
            //atribuindo os valores
            this.id = id;
            this.Nome = nome;
            this.Valor = valor;
            this.Dia = dia;
            this.Juros = juros;
            this.Atraso = atraso;
            this.Status = status;
        }

        public emprestimo(string textNome2, Decimal txtValor, Decimal txtJuros, DateTime dia, string txtStatus)
        {
            this.id2 = id2;
            this.Nome = textNome2;
            this.Valor = txtValor;
            this.Juros = txtJuros;
            this.Dia = dia;
            this.Status = txtStatus;
          
        }

        public emprestimo(Decimal pagamento, int id3, DateTime dia)
        {
            this.Pagamento = pagamento;
            this.id3 = id3;
            this.Dia = dia;
        }

        public emprestimo(int id3)
        {
            this.id3 = id3;
        }

        //encapsulando:
        public int id
        {
            get { return _id;}
            set {_id = value;}
        }
        public int id2
        {
            get { return _id2; }
            set { _id2 = value; }
        }
        public int id3
        {
            get { return _id3; }
            set { _id3 = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public Decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public DateTime Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }
        public Decimal Juros
        {
            get { return _juros; }
            set { _juros = value; }
        }
        public int Atraso
        {
            get { return _atraso; }
            set { _atraso = value; }
        }
        public Decimal Pagamento
        {
            get { return _pagamento; }
            set { _pagamento = value; }
        }
    }
    }

