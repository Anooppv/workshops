using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Todo
{
	public class TodoViewModel : BaseViewModel
	{
		public ObservableCollection<TodoItem> Todos { get; set; }

		public TodoViewModel ()
		{
			Todos = new ObservableCollection<TodoItem> ();
			GetTodos ();
		}

		async void GetTodos ()
		{
			var todos = await MobileService.GetTodosAsync ();
			foreach (var todo in todos) {
				Todos.Add (todo);
			}
		}
	}
}