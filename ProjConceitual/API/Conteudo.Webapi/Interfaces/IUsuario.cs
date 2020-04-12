using Conteudo.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conteudo.Webapi.Interfaces
{
	interface IUsuario
	{
		List<Usuario> Get();

		void Post(Usuario usuario);

	}
}
