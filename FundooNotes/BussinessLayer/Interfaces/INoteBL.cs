using DataBaseLayer.Notes;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface INoteBL
    {
        Task AddNote(int UserId, NotePostModel notePostModel);
        Task DeleteNote(int UserId, int NoteId);
        Task UpdateNote(int UserId, int NoteId, UpdateModel updateModel);
    }  
}
