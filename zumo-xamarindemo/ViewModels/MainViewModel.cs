using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace zumoxamarindemo
{
	public class MainViewModel : BaseViewModel
	{
		private readonly IDataService _dataService;

		private ObservableCollection<Contact> _contacts;
		public ObservableCollection<Contact> Contacts
		{
			get { return _contacts; }
			set 
			{ 
				_contacts = value; 
				OnPropertyChanged ();
			}
		}

		private string _newContactName;
		public string NewContactName
		{
			get { return _newContactName; }
			set 
			{
				_newContactName = value;
				OnPropertyChanged ();
			}
		}

		private string _newContactPhone;
		public string NewContactPhone
		{
			get { return _newContactPhone; }
			set 
			{
				_newContactPhone = value;
				OnPropertyChanged ();
			}
		}

		private Command _refreshCommand;
		public Command RefreshCommand 
		{
			get { return _refreshCommand ?? (_refreshCommand = new Command (async () => await ExecuteRefreshCommand ())); }
		}

		private Command _addCommand;
		public Command AddCommand
		{
			get { return _addCommand ?? (_addCommand = new Command (async () => await ExecuteAddCommand ())); }
		}

		public MainViewModel ()
		{
			_dataService = Xamarin.Forms.DependencyService.Get<IDataService> ();

			Contacts = new ObservableCollection<Contact> ();
		}

		private async Task ExecuteRefreshCommand()
		{
			NewContactName = string.Empty;
			NewContactPhone = string.Empty;

			// First make sure the user has authenticated with the Azure mobile service
			if (!_dataService.IsAuthenticated)
				await _dataService.AuthenticateAsync ();

			if (_dataService.IsAuthenticated)
				await LoadContacts ();
		}

		private async Task LoadContacts()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			Contacts.Clear ();

			try
			{
				var response = await _dataService.GetContactsAsync();

				foreach (var c in response)
					Contacts.Add(c);
			}
			catch (Exception e) {
				throw;
			}
			finally {
				IsBusy = false;
			}
		}

		private async Task ExecuteAddCommand()
		{
			if (string.IsNullOrEmpty(NewContactName) || 
				string.IsNullOrEmpty(NewContactPhone))
				return;

			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var contact = new Contact {Name = NewContactName, Phone = NewContactPhone};
				await _dataService.AddContactAsync(contact);
			}
			catch (Exception e) {
				throw;
			}
			finally {
				IsBusy = false;
				await ExecuteRefreshCommand();
			}
		}
	}
}

