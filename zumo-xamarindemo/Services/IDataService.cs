using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace zumoxamarindemo
{
	public interface IDataService
	{
		bool IsAuthenticated { get; }

		Task AuthenticateAsync ();
		Task<IList<Contact>> GetContactsAsync ();
		Task AddContactAsync(Contact contact);
	}
}

