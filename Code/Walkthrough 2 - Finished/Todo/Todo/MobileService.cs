using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
	public class MobileService
	{
		static MobileServiceClient client = new MobileServiceClient 
			("https://tododemoapplication.azure-mobile.net/", "xRelREGxnCOaYeXKUpqxFDRZKADbaV37");

		public static async Task<List<TodoItem>> GetTodosAsync ()
		{
			return await client.GetTable<TodoItem> ().ToListAsync ();
		}

		public static async Task SaveTodoAsync (TodoItem item)
		{
			if (item.ID == null | item.ID == string.Empty) {
				await client.GetTable<TodoItem> ().InsertAsync (item);
			} else {
				await client.GetTable<TodoItem> ().UpdateAsync (item);
			}
		}
	}
}