using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BasicNoteTaker.Core.Models;
using BasicNoteTaker.Core.Services.Notes;
using Xamarin.Forms;

namespace BasicNoteTaker.Core.ViewModels
{
	public class NoteTakerViewModel : ViewModelBase
	{
        readonly INotesService _notesService;
	    private bool _isBusy;

	    public NoteTakerViewModel(INotesService notesService)
		{
		    RefreshCommand = new Command(async () => await RefreshNotes(), () => !_isBusy);
		    NoteSelectedNavigation = (i) => { };

            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
        }

	    public ObservableCollection<NoteViewItem> Notes { get; private set; }
        public ICommand RefreshCommand { get; }
	    public Action<NoteItem> NoteSelectedNavigation { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        internal async System.Threading.Tasks.Task Init()
        {
            IsBusy = true;
            var notes = await _notesService.GetNotes();
            var deleteNote = new Func<NoteViewItem, System.Threading.Tasks.Task>(RemoveNote);

            Notes = new ObservableCollection<NoteViewItem>(
                notes.Select(n => new NoteViewItem(n) { DeleteNote = deleteNote }).ToList()
                );
            RaisePropertyChanged(nameof(Notes));
            IsBusy = false;
        }

	    private async System.Threading.Tasks.Task RemoveNote(NoteViewItem noteViewItem)
	    {
	        IsBusy = true;
	        Notes.Remove(noteViewItem);
	        await _notesService.Delete(noteViewItem.NoteItem);
	        IsBusy = false;
	    }

	    internal void NoteSelected(NoteViewItem selectedNote)
	    {
	        NoteSelectedNavigation(selectedNote.NoteItem);
		}

		internal void NewNote()
		{
	        NoteSelectedNavigation(new NoteItem());
		}

	    private async System.Threading.Tasks.Task RefreshNotes()
	    {
	        await Init();
	    }
	}
}
