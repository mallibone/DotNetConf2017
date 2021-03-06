﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicNoteTaker.Core.Models;
using BasicNoteTaker.Core.Utils;

namespace BasicNoteTaker.Core.Services.Notes.Impl
{
    public class NotesServiceStub : INotesService
    {
        private readonly List<NoteItem> _notes;

        public NotesServiceStub()
        {
            _notes = new List<NoteItem>
            {
                new NoteItem {Id = 1, Title = "Local .Net Conf", Content = "Show how whats new in Xamarin.\n\n=>Don't forget to show the all new and cool Xamarin Live Player\n\nOh yeah and the .Net Standard stuff...", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new NoteItem {Id = 2, Title = "Plan Date Night", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new NoteItem {Id = 3, Title = "Prepare Answers for Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new NoteItem {Id = 4, Title = "Spacey Wacey Stuff", Content = "Dr. Who things and what not.", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };
        }

        async Task<bool> INotesService.Delete(NoteItem noteItem)
        {
            if (noteItem == null) throw new ArgumentNullException(nameof(noteItem));

            if(!_notes.Any(n => n.Equals(noteItem))) return false;


            await System.Threading.Tasks.Task.Delay(500);

            _notes.Remove(noteItem);

            return true;
        }

        async Task<IEnumerable<NoteItem>> INotesService.GetNotes(bool refresh)
        {
            await System.Threading.Tasks.Task.Delay(500);
            return _notes.Clone();
        }

        public async Task<bool> StoreNote(NoteItem note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            await System.Threading.Tasks.Task.Delay(500);

            NoteItem storedNote;

            if (note.Id != 0)
            {
                storedNote = _notes.FirstOrDefault(n => n.Id == note.Id);
                if (storedNote == null) return false;

                storedNote.Title = note.Title;
                storedNote.Content = note.Content;
                storedNote.LastEdited = DateTime.Now;
            }
            else
            {
                storedNote = note;
                storedNote.Id = (_notes.Last().Id + 1); // todo: replace with Snowflake ID...
                _notes.Add(storedNote);
            }

            return true;
        }
    }
}
