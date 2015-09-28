# Build Your First Mobile App
Build your first mobile app for iOS, Android, and Windows Phone with Xamarin.Forms. 

### Setup
Before you being building your first mobile app, you need to make sure you have everything properly installed.

1. Install [Xamarin](https://xamarin.com/download). Xamarin allows you to build cross-platform native apps for iOS, Android, and Windows Phone using C#.
2. Install the [Xamarin Android Player](https://xamarin.com/android-player). The Xamarin Android Player uses hardware-accelerated virtualization to simulate, debug, demo, or run Android apps. Think Android emulators, but faster.
3. Download the demo. You can do so by clicking the "Download" button on the righthand side of this page.
4. Request a free Xamarin subscription. Xamarin is free for students! All you have to do is visit [xamarin.com/student#apply](xamarin.com/student#apply) to apply.

### Walkthrough
Being in college is tough. When you aren't working on a group project you waited *way* too long to get started on, you are probably cramming for a test or skipping class. Naturally, it's easy to lose track of your assignments. Today, you are going to build your first mobile app for iOS, Android, and Windows Phone - a simple todo app - to solve your problem.

1. Open the solution (inside a folder called `Todo - Start Here`) using either Xamarin Studio or Visual Studio.
2. There are four projects inside the solution: `Todo`, `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone`. Remember, Xamarin.Forms allows you to build native UIs for iOS, Android, and Windows Phone from a single, shared codebase. The `Todo` project is a library called a PCL - or Portable Class Library. All the code we write here will be shared between iOS, Android, and Windows Phone.
3. Expand the `Todo.iOS`, `Todo.Droid`, and `Todo.WinPhone` projects. We won't have to work with these today, but if you wanted to add functionality to your app that requires APIs that are platform-specific, you would make those changes here.
4. Back to our `Todo` project. Open the `App.cs` file. This is the entry point for our Xamarin.Forms application. We can manage important app lifecycle events here, such as the app resuming, starting up, or sleeping. The most important thing is to define a `MainPage`, which will be the first page users see when they launch your app.
5. Notice that there are a few additional folders within the `Todo` project, called `Models`, `View Models`, and `Views`. These terms come from the [MVVM architectural pattern](https://en.wikipedia.org/wiki/Model_View_ViewModel). You don't need to know too much about MVVM, aside from the fact it helps to architect your applications in a scalable and decoupled manner. `Views` are simply the visual objects you see when you launch an app. `View Models` add behavior to `Views`. Finally, `Models` are just representations of something we are trying to abstract, like a todo item.
6. Go ahead and compile and run your app. We are going to transform this into a functional todo app to keep up with all the assignments we have to complete for class.
6. Back to Xamarin.Forms! We want the UI for the todo app to be simple - really simple. When users launch the app, there should be a list of all incomplete todos. You should be able to click each todo to get more information. Finally, we need an easy way to add new todos. Let's add a page to show a list of all outstanding todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoPage". This creates a new page that uses XAML markup to define UIs.
7. After the page is added, you will see that there are two files: a `TodoPage.xaml` file and a `Todo.xaml.cs` file. `TodoPage.xaml` is where we will describe our UI using XAML markup. `TodoPage.xaml.cs` is called a codebehind, and is just a place where we can add functionality to our views (if we don't do so in our view model).
8. Open up `TodoPage.xaml` and add a new ListView under `<ContentPage.Content>`. A list view is just a list of items, and is in almost every mobile app you've ever used, if you know where to look.
9. Remember, nobody will see this page if we don't let our application know that this is the main page. Jump back to `App.cs` and set the `MainPage` property to an instance of the page we just created, or `MainPage = new TodoPage ();`.
10. Compile and run the app, and you should now see a list view when the app launches.
11. Time to add some data to our list view! Remember, a page's behavior should come from a view model, not the page itself? For every page, we should create a new view model that adds behavior to the visual elements on the page. Right-click the `View Models` folder. Click Add->New File. Select the `General` category on the left-hand side. Create a new `Empty Class` and name it "TodoViewModel". Inherent from `BaseViewModel`, which will just give us some default view model functionality for use in `TodoViewModel`.
12. Before we configure our view model, we need to create a model to represent our todos. Right click the `Models` folder. Click Add->New File. Select the `General` category, and create a new `Empty Class` called "TodoItem".
13. Think about what properties a todo item needs. It should have a name, description, and something to let us know if the todo was completed. Add three properties, one for name, description, and done.

```
public string Name { get; set; }
public string Description { get; set; }
public bool Done { get; set; }
```

14. We also want to override the `ToString` method in our model, so that whenever TodoItem.ToString is called, it spits out the name of the todo item.

```
public override string ToString ()
{
  return Name;
}
```

15. Open back up `TodoViewModel.cs`. Our list view needs data to operate on; we can add this data by creating a list of todo items and connecting that to our list view. Add `using System.Collections.ObjectModel;` to the top of the file. This will allow us access to some extra classes that we will use for storing our data. Next, create a new `ObservableCollection<TodoItem>` property called `Todos`, which is basically just a List<T>, except with support for MVVM. 
15. In the constructor for the view model, let's create some dummy data to populate our app on launch. You can do this several ways, but one easy way is:

```
Todos = new ObservableCollection<TodoItem> ();
Todos.Add (new TodoItem { Name = "Reading assignment", Description = "Read chapters 29-34 and take notes." });
Todos.Add (new TodoItem { Name = "Math homework", Description = "Complete problems 1-14 on worksheet." });
Todos.Add (new TodoItem { Name = "Todo app", Description = "Build a todo app for my CS class" });
```

16. Remember how our view model is supposed to help out our view by supplying data and behavior? How do they share data? MVVM came up with a concept of data binding, which basically means that a view's property is "bound" to a property of our view model. Whenever the property changes (via view model), the view will update to reflect the changes. This is why we had to use an `ObservableCollection<T>`, rather than just a regular list. `ObservableCollection` is a special class made for data binding that will automatically alert our view that data has changed, and that the view needs to update. Now that we have bindings defined on the view model end, we need to update our view to handle this.
17. Time to give the list view the data it needs! Jump back over to `TodoPage` and open up the codebehind (`TodoPage.xaml.cs`). In the constructor, add `BindingContext = new TodoViewModel ()`. Why? We need to let our page know the source of all the data bindings we will create. While we are at it, set the title of the page to "Todos" by adding the following line of code to the constructor: `Title = "Todos";`.
18. In `TodoPage.xaml`, update the `ItemsSource` property to `"{Binding Todos}"`. This will mean that all the items for our list view will come from the `Todos` property of our binding context, which we just set to a new `TodoViewModel`. For clarity, this is what your XAML should look like right now.

```
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Todo.TodoPage">
	<ContentPage.Content>
		<ListView ItemsSource="{Binding Todos}">
		</ListView>
	</ContentPage.Content>
</ContentPage>
```

19. Compile and run your app. You should see your todo items in the list view!
20. Now it's time to take our todo app and make it a multi-page app. When a user taps on a todo item in the list, we want to open a new page that contains the item with the name, description, and if the task is done or not. This is called push-pop navigation, as a new page is pushed onto the screen, and then poped off. (Technically, it's pushing/popping off the navigation stack, but you get the point.)
21. Hop back to `App.cs`. Let's update our `MainPage` property so that we can handle this type of navigation. This will also add a navigation bar, so we will style that a bit as well.

```
MainPage = new NavigationPage (new TodoPage ()) {
  BarTextColor = Color.White,
  BarBackgroundColor = Color.FromHex ("2C97DE")
};
```

22. Compile and run your app. You should see a beautiful blue navigation bar with white text that says "Todos". Great, now that the framework is in place to handle navigation, let's create a new page for viewing and editing existing todos. Right-click the `Views` folder. Click Add->New File. Select the `Forms` category on the left-hand side. Create a new `Forms ContentPage XAML` and name it "TodoDetailPage".
