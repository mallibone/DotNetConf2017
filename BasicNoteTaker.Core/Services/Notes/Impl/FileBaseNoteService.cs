using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicNoteTaker.Core.Models;
using BasicNoteTaker.Core.Utils;
using Newtonsoft.Json;

namespace BasicNoteTaker.Core.Services.Notes.Impl
{
    class FileBaseNoteService : INotesService
    {
        private readonly string _notesFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "notes.json");
        private readonly List<NoteItem> _notes;

        public FileBaseNoteService()
        {
            _notes = new List<NoteItem>();
        }
        #region Implementation of INotesService

        public async Task<bool> Delete(NoteItem noteItem)
        {
            var noteToDelete = _notes.FirstOrDefault(n => n.Id == noteItem.Id);
            if (noteToDelete == null) return false;
            _notes.Remove(noteToDelete);
            await Task.Run(() =>
            {
                var notesString = JsonConvert.SerializeObject(_notes);
                File.WriteAllText(_notesFilename, notesString, Encoding.UTF8);
            });
            return true;
        }

        public async Task<IEnumerable<NoteItem>> GetNotes(bool refresh = false)
        {
            if (_notes.Any()) return _notes.Clone();

            if (!File.Exists(_notesFilename))
            {
                Init();
                return _notes;
            }

            var notesString = File.ReadAllText(_notesFilename);
            _notes.Clear();
            _notes.AddRange(JsonConvert.DeserializeObject<List<NoteItem>>(notesString));

            return _notes.Clone();
        }

        public async Task<bool> StoreNote(NoteItem noteItem)
        {
            var existingNote =_notes.FirstOrDefault(n => n.Id == noteItem.Id);
            if (existingNote != null)
            {
                existingNote.Content = noteItem.Content;
                existingNote.Created = noteItem.Created;
                existingNote.LastEdited = noteItem.LastEdited;
                existingNote.Title = noteItem.Title;

                await Task.Run(() =>
                {
                    var notesString = JsonConvert.SerializeObject(_notes);
                    File.WriteAllText(_notesFilename, notesString, Encoding.UTF8);
                });

                return true;
            }

            noteItem.Id = _notes.Last().Id + 1;
            _notes.Add(noteItem);

            await Task.Run(() =>
            {
                var notesString = JsonConvert.SerializeObject(_notes);
                File.WriteAllText(_notesFilename, notesString, Encoding.UTF8);
            });

            return true;
        }

        #endregion

        private void Init()
        {
            _notes.Clear();
            _notes.AddRange(GenerateNotes());

            var notesString = JsonConvert.SerializeObject(_notes);
            var gnabber = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
            File.WriteAllText(_notesFilename, notesString, Encoding.UTF8);
        }

        private List<NoteItem> GenerateNotes()
        {
            var notes = new List<NoteItem>
            {
                new NoteItem {Id = 1, Title = "Local .Net Conf", Content = "Show how whats new in Xamarin.\n\n=>Don't forget to show the all new and cool Xamarin Live Player\n\nOh yeah and the .Net Standard stuff...", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new NoteItem {Id = 2, Title = "Plan Date Night", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new NoteItem {Id = 3, Title = "Prepare Answers for Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new NoteItem {Id = 4, Title = "Spacey Wacey Stuff", Content = "Dr. Who things and what not.", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };

            return notes;
        }
    }
}
