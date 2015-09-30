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
			Title = "Todos";

			InitializeComponent ();

			todoListView.ItemSelected += (sender, e) => {
				if (todoListView.SelectedItem == null)
					return;

				var todoItem = todoListView.SelectedItem as TodoItem;
				var detailPage = new TodoDetailPage (todoItem);

				Navigation.PushAsync (detailPage);

				todoListView.SelectedItem = null;
			};

			ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
				var viewModel = BindingContext as TodoViewModel;
				viewModel.Todos.Add (new TodoItem { Name = "New Todo", Description = "Edit Me", Done = false });
			}));
		}
	}
}