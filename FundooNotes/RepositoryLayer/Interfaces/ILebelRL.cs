using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILebelRL
    {
        Task CreateLebel(int UserId, int NoteId, string LebelName);
    }
}
