using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_9
{
    [DataContract]
    internal class BaseDeDados
    {
        //Atributo
        [DataMember]
        private List<CadastroPessoa> listaDePessoas;//toda essa lista ira ser convertida em XML
        private string caminhoBaseDeDados;

        //Métodos
        public void AdicionarPessoa(CadastroPessoa pPessoa)
        {
            listaDePessoas.Add(pPessoa);
            Serializador.Serializa(caminhoBaseDeDados, this);
        }
        public List<CadastroPessoa> PesquisaPessoaPorDoc(string pNumeroDeDocumento)
        {
            List<CadastroPessoa> listaDePessoasTemp = listaDePessoas.Where(x => x.NumeroDoDocumento == pNumeroDeDocumento).ToList();
            if (listaDePessoasTemp.Count > 0)
                return listaDePessoasTemp;
            else
                return null;
        }
        public List<CadastroPessoa> RemoverPessoaPorDoc(string pNumeroDoDocumento)
        {
            List<CadastroPessoa> listaDePessoasTemp = listaDePessoas.Where(x => x.NumeroDoDocumento == pNumeroDoDocumento).ToList();
            if (listaDePessoasTemp.Count > 0)
            {
                foreach (CadastroPessoa pessoa in listaDePessoasTemp)
                {
                    listaDePessoas.Remove(pessoa);
                }
                return listaDePessoasTemp;
            }
            else
                return null;
        }

        //Construtor
        public BaseDeDados(string pCaminhoBaseDeDados)
        {
            caminhoBaseDeDados = pCaminhoBaseDeDados;
            BaseDeDados baseDeDadosTemp = Serializador.Desserializa(caminhoBaseDeDados);
            if (baseDeDadosTemp != null)//retorne diferente de nullo existe o arquivo então
                listaDePessoas = baseDeDadosTemp.listaDePessoas;
            //caso não exista o arquivo , então sera criado um novo
            else
                listaDePessoas = new List<CadastroPessoa>();
        }
    }
}
