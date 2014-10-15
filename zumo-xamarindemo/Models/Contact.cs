using System;
using Microsoft.WindowsAzure.MobileServices;

namespace zumoxamarindemo
{
	[DataTable("Contacts")]
	public class Contact
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
	}
}

