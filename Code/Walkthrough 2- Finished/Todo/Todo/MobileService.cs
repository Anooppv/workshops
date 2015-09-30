using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
	public class MobileService
	{
		public static MobileServiceClient Client { get; set; }

		public static async Task<List<TodoItem>> GetTodosAsync ()
		{
			return await Client.GetTable<TodoItem> ().ToListAsync ();
		}

		public static async Task SaveTodoAsync (TodoItem item)
		{
			System.Diagnostics.Debug.WriteLine (item.Name);
			if (item.ID == null || item.ID == string.Empty) {
				await Client.GetTable<TodoItem> ().InsertAsync (item);
				System.Diagnostics.Debug.WriteLine ("Insert");
			} else {
				await Client.GetTable<TodoItem> ().UpdateAsync (item);
				System.Diagnostics.Debug.WriteLine ("Update");
			}
		}
	}
}