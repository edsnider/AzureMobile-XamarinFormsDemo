using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zumoxamarindemo
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set {
				_isBusy = value;
				OnPropertyChanged ();
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}

		#endregion
	}
}

