using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILebelRL
    {
        Task CreateLebel(int UserId, int NoteId, string LebelName);
        Task DeleteLabel(int UserId, int NoteId);
        Task UpdateLabel(int UserId, int NoteId, string LebelName);
        Task<List<Label>> GetAllLabel(int UserId);
        Task<Label> GetLabel(int UserId, int NoteId);
    }
}
