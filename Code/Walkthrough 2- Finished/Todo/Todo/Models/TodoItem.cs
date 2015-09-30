using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Todo
{
	public class TodoItem : INotifyPropertyChanged
	{
		private string name;

		[JsonProperty(PropertyName = "id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name 
		{ 
			get { return name; }
			set { name = value; NotifyPropertyChanged ("Name"); }
		}

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "done")]
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