using System;
using System.Collections.ObjectModel;

namespace Todo
{
	public class TodoViewModel : BaseViewModel
	{
		public ObservableCollection<TodoItem> Todos { get; set; }

		public TodoViewModel ()
		{
			Todos = new ObservableCollection<TodoItem> ();
			Todos.Add (new TodoItem { Name = "Reading assignment", Description = "Read chapters 29-34 and take notes." });
			Todos.Add (new TodoItem { Name = "Math homework", Description = "Complete problems 1-14 on worksheet." });
			Todos.Add (new TodoItem { Name = "Todo app", Description = "Build a todo app for my CS class" });
		}
	}
}