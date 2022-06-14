using BussinessLayer.Interfaces;
using DataBaseLayer.Collab;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL noteRL)
        {
            this.collabRL = noteRL;
        }

        public async Task AddCollab(int UserId, int NoteId, CollabPostModel collabPostModel)
        {
            try
            {
                await collabRL.AddCollab(UserId, NoteId, collabPostModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        public async Task<List<Collabrator>> GetAllCollab(int UserId)
        {
            try
            {
               return await collabRL.GetAllCollab(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  async Task RemoveCollab(int UserId, int NoteId)
        {
            try
            {
                await collabRL.RemoveCollab(UserId,NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
