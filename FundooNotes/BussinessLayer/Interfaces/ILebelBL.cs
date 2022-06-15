using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface ILebelBL
    {
        Task CreateLebel(int UserId, int NoteId, string LebelName);
    }
}
