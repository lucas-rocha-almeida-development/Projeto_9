using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projeto_9
{
    internal static class Serializador
    {
        static private DataContractSerializer serializador = new DataContractSerializer(typeof(BaseDeDados));

        //metodo que sera responsavel pela Serialização
        public static void Serializa(string pCaminhoArquivoXml, BaseDeDados pBaseDeDados)
        {
            XmlWriterSettings xmlConfig = new XmlWriterSettings { Indent = true };//responsavel pela identação
            StringBuilder construtorDeStrings = new StringBuilder();//
            XmlWriter escritorDeXml = XmlWriter.Create(construtorDeStrings, xmlConfig);
            serializador.WriteObject(escritorDeXml, pBaseDeDados);
            escritorDeXml.Flush();
            string objetoSerializadoStr = construtorDeStrings.ToString();//contem todos os dados serializados

            //salvar aquivo 
            FileStream meuArquivoXml = File.Create(pCaminhoArquivoXml);
            meuArquivoXml.Close();//fechar
            File.WriteAllText(pCaminhoArquivoXml, objetoSerializadoStr);
            escritorDeXml.Close();
        }

        //retornar o arquivoxml para string 
        public static BaseDeDados Desserializa(string pCaminhoArquivoXml)
        {
            try
            {
                //tratamento de excessão para verificar existencia do arquivo
                if (File.Exists(pCaminhoArquivoXml))
                {
                    string conteudoDoObjetoSerializado = File.ReadAllText(pCaminhoArquivoXml);
                    StringReader leitorDeString = new StringReader(conteudoDoObjetoSerializado);
                    XmlReader leitorDeXml = XmlReader.Create(leitorDeString);
                    BaseDeDados baseDeDadosTemp = (BaseDeDados)serializador.ReadObject(leitorDeXml); //obj basedados
                    return baseDeDadosTemp;
                }
                //caso o arquivo não exista, retorna null
                else
                    return null;
            }
            //caso ocorra qualquer excessão, retornar nulo
            catch
            {
                return null;
            }
        }
    }
}
