using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class TodoPage : ContentPage
	{
		public TodoPage ()
		{
			BindingContext = new TodoViewModel ();

			InitializeComponent ();

			todoListView.ItemSelected += (sender, e) => {
				if (todoListView.SelectedItem == null)
					return;

				Navigation.PushAsync (new TodoDetailPage (todoListView.SelectedItem as TodoItem));

				todoListView.SelectedItem = null;
			};

			ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
				var viewModel = BindingContext as TodoViewModel;
				var todo = new TodoItem { Name = "New Todo", Description = "Edit Me", Done = false };
				viewModel.Todos.Add (todo);

				MobileService.SaveTodoAsync (todo);
			}));
		}
	}
}

