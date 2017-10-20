using BasicNoteTaker.Core.Models;
using BasicNoteTaker.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasicNoteTaker.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditNotePage : ContentPage
	{
		public EditNotePage (NoteItem noteItem)
		{
			InitializeComponent ();
	        Vm.Init(noteItem);
	        BindingContext = Vm;
		    Vm.GoBack = async () => await Navigation.PopAsync();
	        NavigationPage.SetBackButtonTitle(this, "Cancel");
	    }

	    private EditNoteViewModel Vm { get; } = new EditNoteViewModel(App.NoteService);
	}
}