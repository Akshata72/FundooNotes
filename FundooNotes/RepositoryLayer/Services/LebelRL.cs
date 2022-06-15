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
    public class LebelRL : ILebelRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public LebelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public async Task CreateLebel(int UserId, int NoteId, string LebelName)
        {
            try
            {
                Label label = new Label
                {
                    UserId = UserId,
                    NoteId = NoteId
                };
                label.LabelName = LebelName;
                fundooContext.Add(label);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        public async Task DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                var label = fundooContext.Label.FirstOrDefault(u=> u.UserId == UserId && u.NoteId == NoteId);
                fundooContext.Label.Remove(label);
                await fundooContext.SaveChangesAsync();
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
                return await fundooContext.Label.Where(u => u.UserId == UserId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Label> GetLabel(int UserId, int NoteId)
        {
            try
            {
                return await fundooContext.Label.Where(u => u.UserId == UserId && u.NoteId == NoteId).FirstOrDefaultAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateLabel(int UserId, int NoteId, string LebelName)
        {
            try
            {
               var label = fundooContext.Label.FirstOrDefault(u => u.UserId == UserId && u.NoteId == NoteId);
                if (label == null)
                {
                    throw new Exception("No Label Exist");
                }
                label.LabelName = LebelName;
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
