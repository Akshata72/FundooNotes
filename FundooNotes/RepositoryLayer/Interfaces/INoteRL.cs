using DataBaseLayer.Notes;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote(int UserId,NotePostModel notePostModel);
        Task DeleteNote(int UserId,int NoteId);
        Task UpdateNote(int UserId,int NoteId, UpdateModel updateModel);
        Task IsTrash(int UserId,int NoteId);
        Task Remainder(int UserId,int NoteId, DateTimeModel dateTimeModel);
        Task IsArchive(int UserId,int NoteId);
        Task IsPin(int UserId,int NoteId);
        Task ChangeColour(int UserId,int NoteId,string Colour);
        Task<Note> GetNote(int UserId,int NoteId);
        Task<List<Note>> GetAllNotes(int UserId);
    }
}

