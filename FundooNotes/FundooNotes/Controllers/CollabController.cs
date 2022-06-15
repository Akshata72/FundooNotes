using BussinessLayer.Interfaces;
using DataBaseLayer.Collab;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooContext fundooContext;
        ICollabBL collabBL;
        public CollabController(FundooContext fundooContext, ICollabBL collabBL,IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.fundooContext = fundooContext;
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
        }
        [Authorize]
        [HttpPost("AddCollab")]
        public async Task<ActionResult> AddCollab(int NoteId,CollabPostModel collabPostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                if(collabPostModel == null)
                {
                    await this.collabBL.AddCollab(userId, NoteId, collabPostModel);
                    return this.Ok(new { success = true, message = $"Collab Added Successful" });
                }
                return this.BadRequest(new { success = false, message = $"Already CollabEmail is Added.." });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllCollab")]
        public async Task<ActionResult> GetAllCollab()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Collabrator> list = new List<Collabrator>();
                
                list = await this.collabBL.GetAllCollab(userId);
                return this.Ok(new { success = true, message = $"Get Collab Successful", data = list });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("RemoveCollab")]
        public async Task<ActionResult> RemoveCollab(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var collab = fundooContext.Note.FirstOrDefault(u => u.UserId == userId && u.NoteId == NoteId);
                if (collab == null)
                {
                    return this.BadRequest(new { success = false, message = "Unable to retrieve Delete collab." });
                }
                await this.collabBL.RemoveCollab(userId,NoteId);
                return this.Ok(new { success = true, message = $"collab detete Successfully" });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
