using System;
using Xamarin.Forms;

namespace zumoxamarindemo
{
	public class MainView : ContentPage
	{
		private MainViewModel _viewModel
		{
			get { return BindingContext as MainViewModel; }
		}

		public MainView ()
		{
			BindingContext = new MainViewModel ();

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 10)
			};

			var addStack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (0, 10)
			};

			var nameEntry = new Entry { Placeholder = "Name" };
			nameEntry.SetBinding (Entry.TextProperty, "NewContactName", BindingMode.TwoWay);
			var phoneEntry = new Entry { Placeholder = "Phone number" };
			phoneEntry.SetBinding (Entry.TextProperty, "NewContactPhone", BindingMode.TwoWay);
			var addButton = new Button { Text = "Add", Command = _viewModel.AddCommand };

			addStack.Children.Add (nameEntry);
			addStack.Children.Add (phoneEntry);
			addStack.Children.Add (addButton);

			stack.Children.Add (addStack);

			var progress = new ActivityIndicator
			{
				IsEnabled = true,
				Color = Color.White
			};

			progress.SetBinding(IsVisibleProperty, "IsBusy");
			progress.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			stack.Children.Add(progress);

			var listView = new ListView { ItemsSource = _viewModel.Contacts };

			var itemTemplate = new DataTemplate(typeof (TextCell));
			itemTemplate.SetBinding(TextCell.TextProperty, "Name");
			itemTemplate.SetBinding(TextCell.DetailProperty, "Phone");
			listView.ItemTemplate = itemTemplate;

			stack.Children.Add(listView);

			Content = stack;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing ();

			if (_viewModel == null || _viewModel.IsBusy || _viewModel.Contacts.Count > 0)
				return;

			_viewModel.RefreshCommand.Execute (null);
		}
	}
}

