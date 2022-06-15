using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
