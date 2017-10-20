using BasicNoteTaker.Core.ViewModels;
using Xamarin.Forms;

namespace BasicNoteTaker.Core.Views
{
    public partial class NoteTakerPage : ContentPage
    {
        public NoteTakerPage()
        {
            InitializeComponent();

            BindingContext = Vm;
            Vm.NoteSelectedNavigation = async (noteItem) =>
            {
                await Navigation.PushAsync(new EditNotePage(noteItem));
            };

            ToolbarItems.Add(new ToolbarItem("add", "add", () =>
            {
                Vm.NewNote();
            }));
        }

        public NoteTakerViewModel Vm { get; } = new NoteTakerViewModel(App.NoteService);

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            var selectedNote = ((NoteViewItem)e.SelectedItem);
            //Navigation.PushAsync(new EditNoteView(selectedNote.Id));
            Vm.NoteSelected(selectedNote);

            NoteListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.Init();
        }
    }
}
