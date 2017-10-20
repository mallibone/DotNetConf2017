using BasicNoteTaker.Core.Services.Notes;
using BasicNoteTaker.Core.Services.Notes.Impl;
using BasicNoteTaker.Core.Views;
using Xamarin.Forms;

namespace BasicNoteTaker.Core
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    MainPage = new NavigationPage(new NoteTakerPage());
		}

	    public static INotesService NoteService { get; } = new NotesServiceStub();

	    protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
