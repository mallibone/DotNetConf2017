using System.Collections.Generic;
using System.Threading.Tasks;
using BasicNoteTaker.Core.Models;

namespace BasicNoteTaker.Core.Services.Notes
{
    public interface INotesService
    {
        Task<bool> Delete(NoteItem noteItem);
        Task<IEnumerable<NoteItem>> GetNotes(bool refresh = false);
        Task<bool> StoreNote(NoteItem noteItem);
    }
}