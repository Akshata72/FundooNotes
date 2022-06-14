using DataBaseLayer.Collab;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface ICollabBL
    {
        Task AddCollab(int UserId, int NoteId, CollabPostModel collabPostModel);
        Task<List<Collabrator>> GetAllCollab(int UserId);
        Task RemoveCollab(int UserId, int NoteId);
    }
}
