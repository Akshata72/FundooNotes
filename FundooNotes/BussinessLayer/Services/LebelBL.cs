using BussinessLayer.Interfaces;
using RepositoryLayer.Entities;
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

        public async Task<List<Label>> GetAllLabel(int UserId)
        {
            try
            {
               return await lebelRL.GetAllLabel(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateLabel(int UserId, int NoteId, string LebelName)
        {
            try
            {
                await lebelRL.UpdateLabel(UserId, NoteId, LebelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
