using DataBaseLayer.Collab;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public CollabRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public async Task AddCollab(int UserId, int NoteId,CollabPostModel collabPostModel)
        {
            try
            {
                Collabrator collab = new Collabrator
                {
                    UserId = UserId,
                    NoteId = NoteId
                };
                collab.CollabEmail = collabPostModel.CollabEmail;
                fundooContext.Add(collab);
                await fundooContext.SaveChangesAsync();

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
                return await fundooContext.Collabrator.Where(u => u.UserId == UserId).ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task RemoveCollab(int UserId, int NoteId)
        {
            try
            {
                Collabrator collab = new Collabrator
                {
                    UserId = UserId,
                    NoteId = NoteId
                };
                fundooContext.Collabrator.Remove(collab);
                await fundooContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
