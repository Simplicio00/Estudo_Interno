using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Conteudo.Webapi.DataContent
{
	 public class DefaultValues
	{
		public void PostDefaultUser(int numbuser)
		{
			numbuser++;
			SqlConnection connect = new SqlConnection("Data Source=LUCASSOLIVEIRA\\SQLEXPRESS; " +
						"initial catalog=BancoUConteudo; integrated security=true;");
			var post = $"insert into usuario(nome,email,senha)values('Usuario Padrão','padrao{numbuser}@email.com','padrao@12345')";
			SqlCommand cmd = new SqlCommand(post, connect);
			connect.Open();
			cmd.ExecuteNonQuery();
			connect.Close();
		}

	}
}
