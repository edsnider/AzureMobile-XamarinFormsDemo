/* 
 * Azure Mobile Services + Xamarin.Forms Demo
 * 
 * FOR DEMO PURPOSES ONLY
 * This code was developed for demo purposes only - it is not complete or error-proof.
 * 
 * Ed Snider
 * http://www.edsnider.net
 * http://www.github.com/edsnider
 * http://www.twitter.com/edsnider
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using zumoxamarindemo.Android;

[assembly: Xamarin.Forms.Dependency(typeof(DataService))]
namespace zumoxamarindemo.Android
{
	public class DataService : IDataService
	{
		private MobileServiceClient _client;

		public bool IsAuthenticated 
		{
			get { return _client != null && _client.CurrentUser != null; }
		}

		public DataService ()
		{ }

		private void InitializeMobileServicesClient()
		{
			_client = new MobileServiceClient (Constants.ZumoAppUrl, Constants.ZumoAppKey);
		}

		public async Task AuthenticateAsync()
		{
			try
			{
				if (_client == null)
					InitializeMobileServicesClient();
					
				await _client.LoginAsync(Xamarin.Forms.Forms.Context, MobileServiceAuthenticationProvider.Facebook);
			}
			catch (Exception e ) {
				throw;
			}
		}

		// TODO: Don't return model directly - should be wrapped in a response object so any 
		//   errors and/or status codes from the API can be included in the response back to the 
		//	 caller (e.g., ViewModel) - today is a rainy day, but I'll wait for the next one.
		public async Task<IList<Contact>> GetContactsAsync()
		{
			try
			{
				if (_client == null)
					InitializeMobileServicesClient();

				var table = _client.GetTable<Contact> ();
				var items = await table.ReadAsync ();
				var contacts = new List<Contact> ();

				foreach (var i in items) {
					contacts.Add (i);
				}

				return contacts;
			}
			catch (Exception e ) {
				throw;
			}
		}

		public async Task AddContactAsync(Contact contact)
		{
			try
			{
				if (_client == null)
					InitializeMobileServicesClient();

				var table = _client.GetTable<Contact> ();
				await table.InsertAsync(contact);
			}
			catch (Exception e ) {
				throw;
			}
		}
	}
}