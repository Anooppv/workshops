using System;
using System.ComponentModel;

namespace Todo
{
	public class TodoItem : INotifyPropertyChanged
	{
		private string name;

		public string Name 
		{ 
			get { return name; }
			set { name = value; NotifyPropertyChanged ("Name"); }
		}
		public string Description { get; set; }
		public bool Done { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}