using BussinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class LebelBL : ILebelBL
    {
        ILebelRL lebelRL;
        public LebelBL(ILebelRL lebelRL)
        {
            this.lebelRL = lebelRL;
        }

        public async Task CreateLebel(int UserId, int NoteId, string LebelName)
        {
            try
            {
                await lebelRL.CreateLebel(UserId, NoteId,LebelName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        public async Task DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                await lebelRL.DeleteLabel(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
