using System;
using BasicNoteTaker.Core.Models;
using BasicNoteTaker.Core.Services.Notes;
using Xamarin.Forms;

namespace BasicNoteTaker.Core.ViewModels
{

    public class EditNoteViewModel : ViewModelBase
    {
        NoteItem _currentNoteItem;
        readonly INotesService _notesService;
        private bool _isBusy;

        public EditNoteViewModel(INotesService notesService)
        {
            if (notesService == null) throw new ArgumentNullException(nameof(notesService));

            this._notesService = notesService;
            GoBack = () => { };

            SaveNote = new Command(SaveChangesToNote);
            DeleteNote = new Command(DeleteNoteFromStorage);
        }

        public Action GoBack { get; set; }

        internal void Init(NoteItem noteItem)
        {
            this._currentNoteItem = noteItem;
            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Content));
        }

        public string Title {
            get
            {
                return _currentNoteItem.Title;
            }
            set
            {
                if (value == null || value == _currentNoteItem.Title) return;
                _currentNoteItem.Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get
            {
                return _currentNoteItem.Content;
            }
            set
            {
                if (value == null || value == _currentNoteItem.Content) return;
                _currentNoteItem.Content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public Command DeleteNote { get; private set; }

        public Command SaveNote { get; private set; }

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

        async void SaveChangesToNote()
        {
            IsBusy = true;
            await _notesService.StoreNote(this._currentNoteItem);
            IsBusy = false;
            GoBack();
        }

        async void DeleteNoteFromStorage()
        {
            IsBusy = true;
            await _notesService.Delete(_currentNoteItem);
            IsBusy = false;
            GoBack();
        }
    }
}
