# Build Your First Mobile App
Build your first mobile app for iOS, Android, and Windows Phone with Xamarin.Forms. 

### Setup
Before you being building your first mobile app, you need to make sure you have everything properly installed.

1. Install [Xamarin](https://xamarin.com/download). Xamarin allows you to build cross-platform native apps for iOS, Android, and Windows Phone using C#.
2. Install the [Xamarin Android Player](https://xamarin.com/android-player). The Xamarin Android Player uses hardware-accelerated virtualization to simulate, debug, demo, or run Android apps. Think Android emulators, but faster.
3. Download the demo. You can do so by clicking the "Download" button on the righthand side of this page.
4. Request a free Xamarin subscription. Xamarin is free for students! All you have to do is visit [xamarin.com/student#apply](xamarin.com/student#apply) to apply.

### Walkthrough #1a - Building a Todo App
Being in college is tough. When you aren't working on a group project you waited *way* too long to get started on, you are probably cramming for a test or skipping class. Naturally, it's easy to lose track of your assignments. Today, you are going to build your first mobile app for iOS, Android, and Windows Phone - a simple todo app - to solve your problem.

---

#### Project Setup and Architecture 

1. Open the solution (inside a folder called `Todo - Start Here`) using either Xamarin Studio or Visual Studio.
2. There are four projects inside the solution: `Todo`, `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone`. Remember, Xamarin.Forms allows you to build native UIs for iOS, Android, and Windows Phone from a single, shared codebase. The `Todo` project is a library called a PCL - or Portable Class Library. All the code we write here will be shared between iOS, Android, and Windows Phone.
3. Expand the `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone` projects. We won't have to work with these today, but if you wanted to add functionality to your app that requires APIs that are platform-specific, you would make those changes here.
4. Back to our `Todo` project. Open the `App.cs` file. This is the entry point for our Xamarin.Forms application. We can manage important app lifecycle events here, such as the app resuming, starting up, or sleeping. The most important thing is to define a `MainPage`, which will be the first page users see when they launch your app.
5. Notice that there are a few additional folders within the `Todo` project, called `Models`, `View Models`, and `Views`. These terms come from the [MVVM architectural pattern](https://en.wikipedia.org/wiki/Model_View_ViewModel). You don't need to know too much about MVVM, aside from the fact it helps to architect your applications in a scalable and decoupled manner. `Views` are simply the visual objects you see when you launch an app. `View Models` add behavior to `Views`. Finally, `Models` are just representations of something we are trying to abstract, like a todo item.
6. Go ahead and compile and run your app. We are going to transform this into a functional todo app to keep up with all the assignments we have to complete for class.

---

#### Create the Main Page

1. Back to Xamarin.Forms! We want the UI for the todo app to be simple - really simple. When users launch the app, there should be a list of all incomplete todos. You should be able to click each todo to get more information. Finally, we need an easy way to add new todos. Let's add a page to show a list of all outstanding todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoPage". This creates a new page that uses XAML markup to define UIs.
2. After the page is added, you will see that there are two files: a `TodoPage.xaml` file and a `Todo.xaml.cs` file. `TodoPage.xaml` is where we will describe our UI using XAML markup. `TodoPage.xaml.cs` is called a codebehind, and is just a place where we can add functionality to our views (if we don't do so in our view model).
3. Open up `TodoPage.xaml` and add a new ListView under `<ContentPage.Content>`. A list view is just a list of items, and is in almost every mobile app you've ever used, if you know where to look.
4. Remember, nobody will see this page if we don't let our application know that this is the main page. Jump back to `App.cs` and set the `MainPage` property to an instance of the page we just created, or `MainPage = new TodoPage ();`.
5. Compile and run the app, and you should now see a list view when the app launches.

---

#### Populate the ListView

1. Time to add some data to our list view! Remember, a page's behavior should come from a view model, not the page itself? For every page, we should create a new view model that adds behavior to the visual elements on the page. Right-click the `View Models` folder. Click Add->New File. Select the `General` category on the left-hand side. Create a new `Empty Class` and name it "TodoViewModel". Inherent from `BaseViewModel`, which will just give us some default view model functionality for use in `TodoViewModel`.
2. Before we configure our view model, we need to create a model to represent our todos. Right click the `Models` folder. Click Add->New File. Select the `General` category, and create a new `Empty Class` called "TodoItem".
3. Think about what properties a todo item needs. It should have a name, description, and something to let us know if the todo was completed. Add three properties, one for name, description, and done.

		public string Name { get; set; }
		public string Description { get; set; }
		public bool Done { get; set; }

4. Open back up `TodoViewModel.cs`. Our list view needs data to operate on; we can add this data by creating a list of todo items and connecting that to our list view. Add `using System.Collections.ObjectModel;` to the top of the file. This will allow us access to some extra classes that we will use for storing our data. Next, create a new `ObservableCollection<TodoItem>` property called `Todos`, which is basically just a List<T>, except with support for MVVM. 
5. In the constructor for the view model, let's create some dummy data to populate our app on launch. You can do this several ways, but one easy way is:

		Todos = new ObservableCollection<TodoItem> ();
		Todos.Add (new TodoItem { Name = "Reading assignment", Description = "Read chapters 29-34 and take notes." });
		Todos.Add (new TodoItem { Name = "Math homework", Description = "Complete problems 1-14 on worksheet." });
		Todos.Add (new TodoItem { Name = "Todo app", Description = "Build a todo app for my CS class" });

6. Remember how our view model is supposed to help out our view by supplying data and behavior? How do they share data? MVVM came up with a concept of data binding, which basically means that a view's property is "bound" to a property of our view model. Whenever the property changes (via view model), the view will update to reflect the changes. This is why we had to use an `ObservableCollection<T>`, rather than just a regular list. `ObservableCollection` is a special class made for data binding that will automatically alert our view that data has changed, and that the view needs to update. Now that we have bindings defined on the view model end, we need to update our view to handle this.
7. Time to give the list view the data it needs! Jump back over to `TodoPage` and open up the codebehind (`TodoPage.xaml.cs`). In the constructor, add `BindingContext = new TodoViewModel ()`. Why? We need to let our page know the source of all the data bindings we will create. While we are at it, set the title of the page to "Todos" by adding the following line of code to the constructor: `Title = "Todos";`.
8. In `TodoPage.xaml`, update the `ItemsSource` property to `"{Binding Todos}"`. This will mean that all the items for our list view will come from the `Todos` property of our binding context, which we just set to a new `TodoViewModel`. For clarity, this is what your XAML should look like right now.
		
		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage">
			<ContentPage.Content>
				<ListView ItemsSource="{Binding Todos}">
				</ListView>
			</ContentPage.Content>
		</ContentPage>
		
9. `ListViews` in Xamarin.Forms are made up of individual cells. Adding a cell is easy! There are many different types of cells, but what we will be using is called a `TextCell`. Also, remember how the list view is populated from the `Todos` binding, which is an `ObservableCollection<TodoItem>`? This means that each cell is representative of a single `TodoItem`. We should update our bindings to reflect that. When you are done, you should have something like this:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage">
			<ContentPage.Content>
				<ListView x:Name="todoListView"
					ItemsSource="{Binding Todos}">
					<ListView.ItemTemplate>
						<DataTemplate>
							<TextCell Text="{Binding Name}" />
						</DataTemplate>
					</ListView.ItemTemplate>
				</ListView>
			</ContentPage.Content>
		</ContentPage>

10. Compile and run your app. You should see your todo items in the list view!

---

#### Add a Navigation Bar and Setup Detail Page

1. Now it's time to take our todo app and make it a multi-page app. When a user taps on a todo item in the list, we want to open a new page that contains the item with the name, description, and if the task is done or not. This is called push-pop navigation, as a new page is pushed onto the screen, and then poped off. (Technically, it's pushing/popping off the navigation stack, but you get the point.)
2. Hop back to `App.cs`. Let's update our `MainPage` property so that we can handle this type of navigation. This will also add a navigation bar, so we will style that a bit as well.

		MainPage = new NavigationPage (new TodoPage ()) {
		  BarTextColor = Color.White,
		  BarBackgroundColor = Color.FromHex ("2C97DE")
		};

3. Compile and run your app. You should see a beautiful blue navigation bar with white text that says "Todos". Great, now that the framework is in place to handle navigation, let's create a new page for viewing and editing existing todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoDetailPage".
4. In `TodoDetailPage.xaml.xs` update the constructor to have the following signature `public TodoDetailPage (TodoItem item)`. Why? When a user selects a cell, we want to pass our selected item along to the page.
5. Remember how the binding context for `TodoPage` was a view model? It doesn't always have to be! In this case, we are binding to a single item, a `TodoItem`, so we can just set the `BindingContext = item` in the constructor of `TodoDetailPage`. We can use this binding in our actual page by adding a `Title` property to our `ContentPage` tag, and binding to the `TodoItem`'s name.

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
			</ContentPage.Content>
		</ContentPage>

6. Next, we want an easy way to navigate after a cell is tapped. Luckily, Xamarin.Forms list views expose an event called `ItemSelected` for this purpose. First, we are going to want to add a name to our list view by adding the `x:Name` property to `TodoPage.xaml` as follows: 

		<ListView x:Name="todoListView"
			ItemsSource="{Binding Todos}">
		</ListView>

7. Hop back over to `TodoPage.xaml.cs` and you should be able to add a lambda expression (fancy name for an anonymous method in C#) to the constructor that looks something like this:

		todoListView.ItemSelected += (sender, e) => {
			// Navigation logic here
		};

8. To navigate, all we have to do is use the `Navigation.PushAsync`method, coupled with an instance of the `TodoDetailPage`. When done, your `TodoPage.xaml.cs` should look something like this:

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
				}
			}
		}

9. Compile and run! Tap a cell in the list view... and boom! You should navigate to a new page with the title of the item that you just selected. Awesome! Now it's time to update our detail view so people can edit and view their todos in more detail.

---

#### Finish Detail Page

1. Jump back to `TodoDetailPage.xaml`. Let's add some visual elements. First, let's create a `StackLayout`, which is a managed layout. All we will have to do is define the order of controls, and the `StackLayout` will handle the rest for us.
2. Inside the `StackLayout`, let's add a few labels, two `Entry`s (for editing the title and description) and a `Switch` (for toggling the state of task). At this point, your XAML will look something like this:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
				<StackLayout Padding="15, 20, 15, 20">
					<Label Text="Title" />
					<Entry Text="Name" />
					<Label Text="Description" />
					<Entry Text="Description" />
					<Label Text="Done?" />
					<Switch IsToggled="false" />
				</StackLayout>
			</ContentPage.Content>
		</ContentPage>

3. Compile and run the app. Great! We have a nice structure, now let's actually hook this up to our selected `TodoItem` so we can actually make changes. Remember, to update our data, we need to use data binding. Because our `BindingContext` for `TodoDetailPage` is already set to the `TodoItem`, all we have to do is bind to specific properties of our `TodoItem`, like the name, description, and status. Give it a try yourself, but this is what `TodoDetailPage.xaml` should look like when you are done:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
				<StackLayout Padding="15, 20, 15, 20">
					<Label Text="Title" />
					<Entry Text="{Binding Name}" />
					<Label Text="Description" />
					<Entry Text="{Binding Description}" />
					<Label Text="Task Completed" />
					<Switch IsToggled="{Binding Done}" />
				</StackLayout>
			</ContentPage.Content>
		</ContentPage>


4. Compile and run the app again. Wow! Now we can make changes to the todo on the detail screen, and return to see our changes are still there. However, you may have noticed, if you update the title of the todo, it doesn't update on the list view. This is because nothing is notifying the list view that the `TodoItem` has changed. Fixing this is easy! All we have do is implement an interface called `INotifyPropertyChanged`, which will let our UI know about changes to the `TodoItem`. Copy the `TodoItem` implementation below back into your `TodoItem` class.

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


5. If you run the compile and app again, you should see that the title now updates when it is changed on the `TodoDetailPage`. 

---

#### Add functionality for creating items

1. One last step: we are missing a page to add new items. One easy way to do this is to add an "Add" button to our navigation bar, which will add a new todo to our list view that we can edit. Hop back over to `TodoPage.xaml.cs`. We can access what Xamarin.Forms calls the toolbar by using the `ToolbarItems` property on our `TodoPage`, and adding a new `ToolbarItem`. Add the following code to the constructor of the `TodoPage` class:

		ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
			// This is where we define actions that will happen when the "Add" button is tapped.
		}));

2. Now that we have a toolbar item, we need to make it actually do something! Let's add a new `TodoItem` to the list view. Remember how our list view uses the `ItemsSource` property, which references `BindingContext`of our page? Luckily for us, almost all the heavily lifting is done to do this. Because `ItemsSource` is binded to our `ObservableCollection<TodoItem>`, all updates to that collection will automatically update the UI. All we need to do then, is to add a new `TodoItem` to this collection. There are a few ways to acccomplish this, but this is the easiest:

		ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
			// Get a reference to our view model
			var viewModel = BindingContext as TodoViewModel;
			// Add our todo to the Todos collection
			viewModel.Todos.Add (new TodoItem { Name = "New Todo", Description = "Edit Me", Done = false });
		}));


3. Compile and run the app, and you have a working todo application! You will now be able to keep track of your assignments using this simple app that you built for iOS, Android, and Windows Phone. :) If you are up to the challenge, jump to Walkthrough #2, and learn how to add a cloud backend to your application (so you can view your todos anywhere), using [Microsoft Azure App Service](https://azure.microsoft.com/en-us/services/app-service/).

### Walkthrough #1b - Building a Todo App
Being in college is tough. When you aren't working on a group project you waited *way* too long to get started on, you are probably cramming for a test or skipping class. Naturally, it's easy to lose track of your assignments. Today, you are going to build your first mobile app for iOS, Android, and Windows Phone - a simple todo app - to solve your problem.

---

#### Project Setup and Architecture

1. Open the solution (inside a folder called `Todo - Start Here`) using either Xamarin Studio or Visual Studio.
2. There are four projects inside the solution: `Todo`, `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone`. Remember, Xamarin.Forms allows you to build native UIs for iOS, Android, and Windows Phone from a single, shared codebase. The `Todo` project is a library called a PCL - or Portable Class Library. All the code we write here will be shared between iOS, Android, and Windows Phone.
3. Expand the `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone` projects. We won't have to work with these today, but if you wanted to add functionality to your app that requires APIs that are platform-specific, you would make those changes here.
4. Back to our `Todo` project. Open the `App.cs` file. This is the entry point for our Xamarin.Forms application. We can manage important app lifecycle events here, such as the app resuming, starting up, or sleeping. The most important thing is to define a `MainPage`, which will be the first page users see when they launch your app.
5. Notice that there are a few additional folders within the `Todo` project, called `Models`, `View Models`, and `Views`. These terms come from the [MVVM architectural pattern](https://en.wikipedia.org/wiki/Model_View_ViewModel). You don't need to know too much about MVVM, aside from the fact it helps to architect your applications in a scalable and decoupled manner. `Views` are simply the visual objects you see when you launch an app. `View Models` add behavior to `Views`. Finally, `Models` are just representations of something we are trying to abstract, like a todo item.
6. Go ahead and compile and run your app. We are going to transform this into a functional todo app to keep up with all the assignments we have to complete for class.

---

#### Create the Main Page

1. Back to Xamarin.Forms! We want the UI for the todo app to be simple - really simple. When users launch the app, there should be a list of all incomplete todos. You should be able to click each todo to get more information. Finally, we need an easy way to add new todos. Let's add a page to show a list of all outstanding todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoPage". This creates a new page that uses XAML markup to define UIs.
2. After the page is added, you will see that there are two files: a `TodoPage.xaml` file and a `Todo.xaml.cs` file. `TodoPage.xaml` is where we will describe our UI using XAML markup. `TodoPage.xaml.cs` is called a codebehind, and is just a place where we can add functionality to our views (if we don't do so in our view model).
3. Open up `TodoPage.xaml` and add a new ListView under `<ContentPage.Content>`. A list view is just a list of items, and is in almost every mobile app you've ever used, if you know where to look. Here is what your `TodoPage.xaml` code should look like:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage">
			<ContentPage.Content>
				<ListView>
				</ListView>
			</ContentPage.Content>
		</ContentPage>

4. Remember, nobody will see this page if we don't let our application know that this is the main page. Jump back to `App.cs` and set the `MainPage` property to an instance of the page we just created, or `MainPage = new TodoPage ();`.
5. Compile and run the app, and you should now see a list view when the app launches.

---

#### Populate the ListView

1. Time to add some data to our list view! Remember, a page's behavior should come from a view model, not the page itself? For every page, we should create a new view model that adds behavior to the visual elements on the page. For your conveienence, you we have gone ahead and added the view model (`TodoViewModel`) for you. Also, if you go check out the `Models` folder, specifically `TodoItem.cs`, you will see that we have a "model" representation of a todo item, including a name, description, and something to let us know if the todo was completed.
2. Open back up `TodoViewModel.cs`. Our list view needs data to operate on; we can add this data by creating a list of todo items and connecting that to our list view. You can see that we created a new `ObservableCollection<TodoItem>` property called `Todos`, which is basically just a List<T>, except with support for MVVM. In the constructor, we went ahead and provided some dummy starting data to populate our app on launch.
3. Remember how our view model is supposed to help out our view by supplying data and behavior? How do they share data? MVVM came up with a concept of data binding, which basically means that a view's property is "bound" to a property of our view model. Whenever the property changes (via view model), the view will update to reflect the changes. This is why we had to use an `ObservableCollection<T>`, rather than just a regular list. `ObservableCollection` is a special class made for data binding that will automatically alert our view that data has changed, and that the view needs to update. Now that we have bindings defined on the view model end, we need to update our view to handle this.
4. Time to give the list view the data it needs! Jump back over to `TodoPage` and open up the codebehind (`TodoPage.xaml.cs`). In the constructor, add `BindingContext = new TodoViewModel ()`. Why? We need to let our page know the source of all the data bindings we will create.
5. In `TodoPage.xaml`, update the `ItemsSource` property to `"{Binding Todos}"`. This will mean that all the items for our list view will come from the `Todos` property of our binding context, which we just set to a new `TodoViewModel`. Also, add the `Title` property to your page in XAML and set the value to "Todos". For clarity, this is what your XAML should look like right now:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage"
			Title="Todos">
			<ContentPage.Content>
				<ListView ItemsSource="{Binding Todos}">
				</ListView>
			</ContentPage.Content>
		</ContentPage>

6. `ListViews` in Xamarin.Forms are made up of individual cells. Adding a cell is easy! There are many different types of cells, but what we will be using is called a `TextCell`. Also, remember how the list view is populated from the `Todos` binding, which is an `ObservableCollection<TodoItem>`? This means that each cell is representative of a single `TodoItem`. We should update our bindings to reflect that. When you are done, you should have something like this:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage"
			Title="Todos">
			<ContentPage.Content>
				<ListView x:Name="todoListView"
					ItemsSource="{Binding Todos}">
					<ListView.ItemTemplate>
						<DataTemplate>
							<TextCell Text="{Binding Name}" />
						</DataTemplate>
					</ListView.ItemTemplate>
				</ListView>
			</ContentPage.Content>
		</ContentPage>

7. Compile and run your app. You should see your todo items in the list view!

--- 

#### Add a Navigation Bar and Setup Detail Page

1. Now it's time to take our todo app and make it a multi-page app. When a user taps on a todo item in the list, we want to open a new page that contains the item with the name, description, and if the task is done or not. This is called push-pop navigation, as a new page is pushed onto the screen, and then poped off. (Technically, it's pushing/popping off the navigation stack, but you get the point.)
2. Hop back to `App.cs`. Let's update our `MainPage` property so that we can handle this type of navigation. This will also add a navigation bar, so we will style that a bit as well.

		MainPage = new NavigationPage (new TodoPage ()) {
		  BarTextColor = Color.White,
		  BarBackgroundColor = Color.FromHex ("2C97DE")
		};

3. Compile and run your app. You should see a beautiful blue navigation bar with white text that says "Todos". Great, now that the framework is in place to handle navigation, let's create a new page for viewing and editing existing todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoDetailPage".
4. In `TodoDetailPage.xaml.xs` update the constructor to have the following signature `public TodoDetailPage (TodoItem item)`. Why? When a user selects a cell, we want to pass our selected item along to the page.
5. Remember how the binding context for `TodoPage` was a view model? It doesn't always have to be! In this case, we are binding to a single item, a `TodoItem`, so we can just set the `BindingContext = item` in the constructor of `TodoDetailPage`. We can use this binding in our actual page by adding a `Title` property to our `ContentPage` tag, and binding to the `TodoItem`'s name.

		TodoDetailPage.xaml.cs

		using System;
		using System.Collections.Generic;

		using Xamarin.Forms;

		namespace Todo
		{
			public partial class TodoDetailPage : ContentPage
			{
				public TodoDetailPage (TodoItem item)
				{
					BindingContext = item;

					InitializeComponent ();
				}
			}
		}

		TodoDetailPage.xaml

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
			</ContentPage.Content>
		</ContentPage>

6. Next, we want an easy way to navigate after a cell is tapped. Luckily, Xamarin.Forms list views expose an event called `ItemSelected` for this purpose. First, we are going to want to add a name to our list view by adding the `x:Name` property to `TodoPage.xaml` as follows: 

		<ListView x:Name="todoListView"
			ItemsSource="{Binding Todos}">
			<ListView.ItemTemplate>
				<DataTemplate>
					<TextCell Text="{Binding Name}" />
				</DataTemplate>
			</ListView.ItemTemplate>
		</ListView>

7. Hop back over to `TodoPage.xaml.cs` and you should be able to add a lambda expression (fancy name for an anonymous method in C#) to the constructor that looks something like this:

		todoListView.ItemSelected += (sender, e) => {
			// Navigation logic here
		};

8. To navigate, all we have to do is use the `Navigation.PushAsync`method, coupled with an instance of the `TodoDetailPage`. When done, your `TodoPage.xaml.cs` should look something like this:

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
				}
			}
		}

9. Compile and run! Tap a cell in the list view... and boom! You should navigate to a new page with the title of the item that you just selected. Awesome! Now it's time to update our detail view so people can edit and view their todos in more detail.

---

#### Finish Detail Page

1. Jump back to `TodoDetailPage.xaml`. Let's add some visual elements. First, let's create a `StackLayout`, which is a managed layout. All we will have to do is define the order of controls, and the `StackLayout` will handle the rest for us.
2. Inside the `StackLayout`, let's add a few labels, two `Entry`s (for editing the title and description) and a `Switch` (for toggling the state of task). At this point, your XAML will look something like this:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
				<StackLayout Padding="15, 20, 15, 20">
					<Label Text="Title" />
					<Entry Text="Name" />
					<Label Text="Description" />
					<Entry Text="Description" />
					<Label Text="Done?" />
					<Switch IsToggled="false" />
				</StackLayout>
			</ContentPage.Content>
		</ContentPage>

3. Compile and run the app. Great! We have a nice structure, now let's actually hook this up to our selected `TodoItem` so we can actually make changes. Remember, to update our data, we need to use data binding. Because our `BindingContext` for `TodoDetailPage` is already set to the `TodoItem`, all we have to do is bind to specific properties of our `TodoItem`, like the name, description, and status. Give it a try yourself, but this is what `TodoDetailPage.xaml` should look like when you are done:

		<?xml version="1.0" encoding="UTF-8"?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoDetailPage"
			Title="{Binding Name}">
			<ContentPage.Content>
				<StackLayout Padding="15, 20, 15, 20">
					<Label Text="Title" />
					<Entry Text="{Binding Name}" />
					<Label Text="Description" />
					<Entry Text="{Binding Description}" />
					<Label Text="Task Completed" />
					<Switch IsToggled="{Binding Done}" />
				</StackLayout>
			</ContentPage.Content>
		</ContentPage>

4. Compile and run the app again. Wow! Now we can make changes to the todo on the detail screen, and return to see our changes are still there. You may have noticed, if you update the title of the todo, it will also update the cell in the list view.

---

#### Add functionality for creating items

1. One last step: we are missing a page to add new items. One easy way to do this is to add an "Add" button to our navigation bar, which will add a new todo to our list view that we can edit. Hop back over to `TodoPage.xaml.cs`. We can access what Xamarin.Forms calls the toolbar by using the `ToolbarItems` property on our `TodoPage`, and adding a new `ToolbarItem`. Add the following code to the constructor of the `TodoPage` class:

		ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
			// This is where we define actions that will happen when the "Add" button is tapped.
		}));

2. Now that we have a toolbar item, we need to make it actually do something! Let's add a new `TodoItem` to the list view. Remember how our list view uses the `ItemsSource` property, which references `BindingContext`of our page? Luckily for us, almost all the heavily lifting is done to do this. Because `ItemsSource` is binded to our `ObservableCollection<TodoItem>`, all updates to that collection will automatically update the UI. All we need to do then, is to add a new `TodoItem` to this collection. There are a few ways to acccomplish this, but this is the easiest:

		ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
			// Get a reference to our view model
			var viewModel = BindingContext as TodoViewModel;
			// Add our todo to the Todos collection
			viewModel.Todos.Add (new TodoItem { Name = "New Todo", Description = "Edit Me", Done = false });
		}));

3. Compile and run the app, and you have a working todo application! You will now be able to keep track of your assignments using this simple app that you built for iOS, Android, and Windows Phone. :) If you are up to the challenge, jump to Walkthrough #2, and learn how to add a cloud backend to your application (so you can view your todos anywhere), using [Microsoft Azure App Service](https://azure.microsoft.com/en-us/services/app-service/).

### Walkthrough #2 - Adding a Cloud Backend To The Todo App
It's 2015. Your app needs a cloud! Lucky for you, it's very easy to add "some cloud" to your todo app. Essentially, what we will be doing is adding a "backend" to our application. This will handle all the data storage behind the scenes and deliver the data whenever your "frontend" (the mobile app) requests it. Why is this awesome? Well, it means that you will be able to access your data from whatever device you pick! If you add todos on an Android or iOS device, you will be able view those todos on a Windows Phone device.

----
Mobile Services Setup
* How to get an account (malte)

----
Configuring Your Backend

1. Go to [Azure Portal](manage.windowsazure.com). Today, we are going to be creating what is called a "Mobile Service", which is just a fancy name for a backend service for your app hosted by Azure.
2. Click New->Compute->Mobile Service->Create. Give your app a descriptive URL (Note: These URLs must be unique.), such as "todoappwalkthrough". For Database, select "Create a new SQL database instance". Leave the rest of the features alone. Click the "Next" arrow.
3. Leave the default Name for the `Database settings` page and change the "Server" value to "New SQL database server". Enter a login name and password that you will remember later on. Click the "Next" arrow to create your mobile service. 
4. Give Azure a second to create your Mobile Service! Azure abstracts lots of complexity for us and makes it really easy to add a backend to our apps, so it's worth the wait.
5. When your application is created, click on the Mobile Service you just created, and then click the `Data` tab. At the bottom of your web browser, you should see a "Create" button. For "Table Name", enter "TodoItem". Leave the rest of the features with the default settings, and click the checkmark to create your table. This will be where all of our todos are stored behind the scenes.
6. Jump back to the `Dashboard` tab. Scroll down, and you should see a field on the right-hand side labeled "MOBILE SERVICE URL". Copy this URL into a document for the time being. We will need it to connect to our Mobile Service later.
7. Next, click the "Manage Keys" button on the bottom of the web browser, and copy your `Application Key` into a document to store until we need it. We will use it to authenticate our app with the mobile service (located at the URL from Step 6).
8. We are done configuring our Mobile Service! Azure makes it so easy to add a backend to your application, there's really no reason not to. Now that we've configured our "backend", let's update our "frontend" (the todo app) to interact with the Mobile Service.
---
Back to Application

1. The Microsoft Azure Team has made it really easy to interact with Mobile Services in C# by creating a client library. Instead of dealing with complex networking logic, all we have do is call a few methods - and boom! We have a backend for our application. This library is distributed via NuGet Package Manager, which we talked about a little earlier.
2. To add the library to your project, right click the `Packages` folder in your `Todo` project, and select `Add Packages`. Tick the box at the bottom that says `Show pre-release packages` and enter "Azure Mobile Services" in the search box. Select the `Windows Azure Mobile Services` package, and click `Add Package`. We also need to add the Azure Mobile Services package to each platform project. To do so, repeat the steps listed above for the `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone` projects.
3. Azure Mobile Services requires you to initialize their client library for each platform. To do so, visit `AppDelegate.cs`, `MainActivity.cs`, and `MainPage.xaml.cs` and add a call to `CurrentPlatform.Init` just above the Xamarin.Forms initialization logic. For example, your AppDelegate's `FinishedLaunching` method for iOS should look like this:

```
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
	Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();

	global::Xamarin.Forms.Forms.Init();
	LoadApplication(new App());

        return base.FinishedLaunching(app, options);
}
```

4. Great, now we are all configured, and ready to start interacting with data from our Azure Mobile Service. Right-click the `Todo` project and select Add->New File. Select the `General` category from the left-hand side, followed by `Empty Class`. Name your empty class "MobileService". This will be the place where all of our Mobile Services logic resides. To be able to access all the APIs available with Mobile Services, add the following using statement to the top of the file `using Microsoft.WindowsAzure.MobileServices;`.
5. Great! Everything in a mobile service revolves around a class called `MobileServiceClient`. This class handles all the complicated networking and behind-the-scenes logic for communicating with our backend, and gives us a nice, easy-to-use API. Let's initialize our `MobileServiceClient`. To do so, let's create a private field called "client" Remember that URL and application key from earlier? Well now is the time we are going to use them! To tell the `MobileServiceClient` class what Mobile Service to connect and authenticate with, let's pass in both the URL and the application key into the constructor of `MobileServiceClient`. It should look something like this:
```
using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
	public class MobileService
	{
		static MobileServiceClient client = new MobileServiceClient ("MOBILE_SERVICE_URL", "APPLICATION_KEY");
	}
}
```
6. Now that we have our `MobileServiceClient` initialized, it's time to interact with our data. First, we need the ability to retreive existing todos from the database. To do this, we are going to use the [async/await pattern](https://msdn.microsoft.com/en-us/library/hh191443.aspx) common in C#/.NET development. Why is it important that our app use asychronous patterns for fetching data from the Mobile Service. Well, remember from your classes that code executes sychronously, line-by-line. Whenever the runtime engine hits a line that takes longer, the entire program has to wait for that line to complete execution before moving along. This is especially bad for mobile apps, because your entire UI will freeze (while it waits for the sychronous call to complete), resutling in a horrible experience for users of your app. Asychronous programming allows your program to continue execution and return back to that particular statement (in our case, fetching data from our Mobile Service) when it completes. You may be familiar with threading or other techniques to accomplish asychronous operations in your apps. C# is an incredibly productive language, so it abstracts threads away from you. All you have to do is mark asychronous methods with the "async" modifier, and add the "await" keyword before asychronous calls.
7. Let's give it a try! To get started, import `System.Threading.Tasks`. A task is representative of a long-running "task" that needs to complete, and is used alongside the async and await keywords. Additionally, import `System.Collections.Generic`. We are going to use the `List<T>` class from this namespace. Next, add a new method with the following signature:
```
public static async Task<List<TodoItem>> GetTodosAsync ()
{

}
```

8. Notice a few things. First, we added the "async" modifier to this method. This is required for any methods you want to use the "await" keyword with. Next, we are returning a Task<T>, which is just our long-running task of whatever T is. In this case type T is a `List<TodoItem>`.
9. Next, let's actually grab data from our Mobile Service. To access a particular table, just use `client.GetTable<T> ()`. In our case, we are trying to access our `TodoItem` table, so to access our table the code would be `client.GetTable<TodoItem> ()`. Now that we have access to our table, we can call the `ToListAsync` method to grab all the data from the Mobile Service. Finally, because this is an "async" method, we need to add the "await" keyword before the method. Put it all together, and it should look like this:
```
public static async Task<List<TodoItem>> GetTodosAsync ()
{
	return await client.GetTable<TodoItem> ().ToListAsync ();
}
```
10. Now that we can grab our todos, it's time to save them! Before we save them, we need to update our `TodoItem` model. Why? We need to help Azure map our properties in `TodoItem` to columns in the `TodoItem` table within our Mobile Service. Go ahead and add a new property (of type string) called "ID". Finally, we need to add that "mapping" between our properties and the columns in our Table. Import `Newtonsoft.Json` and add the following attribute to the top of each of your properties (with the name obviously changed to the name of the property) `[JsonProperty(PropertyName = "id")]`. It should look something like this when you are done:

```
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
```

10. Now that we can grab our model is setup for saving, let's add a method to save them! Because we won't be returning any data, we don't need to worry about using `Task<T>`, just `Task`. Regular 'ole `Task` is basically the asychronous version of "void", so it's not really a big deal. We will want to add a parameter for our `TodoItem` we will be saving, and finally our "async" modifier. Your method should now look like this:
```
public static async Task SaveTodoAsync (TodoItem item)
{
}
```
11. You may be asking... why did we add that "ID" property? Well, when we add new items to our table in Mobile Services, it will automatically be given an ID. This is important because we can diversify between items that have already been saved in our Mobile Service (and just need to be updated), and brand-new items (that aren't saved in the Mobile Service yet). With that in mind, let's add a conditional statement to see if the ID property is null (or an empty string). If it is, then it's a new item and needs to be added to our Mobile Service. If the ID property has a value, then that means it already exists in our Mobile Service, and all we need to do is update it.
```
public static async Task SaveTodoAsync (TodoItem item)
{
	if (item.ID == null | item.ID == string.Empty) {
		// New item
	} else {
		// Update item
	}
}
```
12. Now all we need to do is call the Mobile Services methods for inserting and updating items. Remember, we need to fetch our table first, then we can access the `InsertAsync` and `UpdateAsync` methods. Because these are asychronous methods, we need to add the "await" keyword before the call. When you put it all together, your completed `MobileService` class should look like this:
```
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
	public class MobileService
	{
		static MobileServiceClient client;

		public MobileService ()
		{
			client = new MobileServiceClient ("https://tododemoapplication.azure-mobile.net/", "xRelREGxnCOaYeXKUpqxFDRZKADbaV37");
		}

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
```

13. Now that we can fetch our data, we need to update our `TodoPage` to be populated by the Mobile Service data instead of our dummy data we had there before. Remember, the behavior for our pages should come from our view model (this includes the data to populate our pages), so we need to update `TodoViewModel`.
14. Let's add a new method called `GetTodos` that will call the method we just created in our `MobileService` class and add the items to our `ObservableCollection<TodoItem>`. Remember, this collection is data-bound to our list view, so any changes to this collection will be reflected in the list view. :) Because we will have to use the "await" keyword for our `MobileService.GetTodos` method, we need to add the "async" modifier to our method (so we need to import `System.Threading.Tasks`). Once we call this method and get our todos, we need to loop through the collection returned by `MobileService.GetTodos` and add them to the `Todos` collection. When you put all the pieces together, you should have something like this:
```
async void GetTodos ()
{
	var todos = await MobileService.GetTodosAsync ();
	foreach (var todo in todos) {
		Todos.Add (todo);
	}
}
```
15. Finally, let's delete our "dummy data" from our constructor and call our new `GetTodos` method after we initialize the `Todos` property. Great, now our list view will be in sync with our Mobile Service!
16. Now that we are successfully syncing with our list view, it's time to sync new items when they are added to our list view. In `TodoPage.xaml.cs`, we already have all the code in place to do this locally. All we need to do is add one line of code to sync to our Mobile Service! In the lambda expression created by `ToolbarItems.Add` (around line 25), add a call to `MobileService.SaveTodoAsync (todo);` after adding the todo to the view model for the page. That's it! With that in place, your code should look like this (in `TodoPage.xaml.cs`):
```
ToolbarItems.Add (new ToolbarItem ("Add", null, async () => {
	var viewModel = BindingContext as TodoViewModel;
	var todo = new TodoItem { Name = "New Todo", Description = "Edit Me", Done = false };
	viewModel.Todos.Add (todo);

	MobileService.SaveTodoAsync (todo);
}));
```

17. Amazing! Now we are fetching todos and saving todos! What's left? Updating todos! Hop over to `TodoDetailPage.xaml.cs`. Making sure our items are up-to-date is very easy! Each page in Xamarin.Forms has it's own "lifecycle" events, like when the page comes onto the screen and when it disappears. We can hook into these to update our todo whenever a user navigates away from the `TodoDetailPage`. Override the `OnDisappearing` method, and add a call to `MobileService.SaveTodoAsync` after `base.OnDisappearing`. Remember, our `BindingContext` for this page is just the `TodoItem` that is being edited. So as the lone argument for `MobileService.SaveTodoAsync`, we can just pass in our `BindingContext`, and our items will be updated. When you're all done, it should look like this:
```
protected override void OnDisappearing ()
{
	base.OnDisappearing ();

	MobileService.SaveTodoAsync (BindingContext as TodoItem);
}
```

18. Run and compile your app (if you have issues, see the note below)! You have successfully added a cloud backend to your application! Add some items and edit them. Then compile and run your app for another platform, and you should see the items you created. You may even want to pull up the `Data` tab of your Azure Mobile Service to see the todo data in your database yourself! Azure Mobile Services makes it extremely easy to add a backend to your app. :) Now you can keep track of your todos across all of your devices, and hopefully won't lose track of any more assignments. ;)

NOTE: If you get an error saying that an assembly with namespace `NAMESPACE` has already been imported, this is due to a temporary bug in Azure Mobile Services. All you have to do to fix this is go to `Todo.iOS`->`References`->`From Packages`, and delete the namespaces that are repeated. In my case, it was `System.IO`, `System.Runtime`, and `System.Threading.Tasks`. You can delete these by right-clicking them and selecting "Delete".
