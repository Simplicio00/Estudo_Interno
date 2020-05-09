using Conteudo.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conteudo.Webapi.Interfaces
{
	public interface IUsuario
	{
		List<Usuario> Get();

		void Post(Usuario usuario);

		void Update(int id, Usuario usuario);

		Usuario GetById(int id);

		void StatusOff(int id);

	}
}
