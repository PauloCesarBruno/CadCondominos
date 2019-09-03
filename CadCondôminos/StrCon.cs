using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;
//
using CadCondôminos.Properties;

namespace CadCondôminos
{

    class StrCon
    {
        // Define a string de conexão com Banco de Dados pegando lá de Propertiers/Settings/-->Default e o Nome ...
        public string sql = (Settings.Default.StringConexao);

        public StrCon() // Construtor da Classe
        {
            // Nada Necessário.
        }
    }
}