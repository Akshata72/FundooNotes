using BussinessLayer.Interfaces;
using DataBaseLayer.Notes;
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
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooContext fundooContext;
        INoteBL noteBL;
        public NoteController(FundooContext fundooContext, INoteBL noteBL,IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.fundooContext = fundooContext;
            this.noteBL = noteBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("AddNote")]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.noteBL.AddNote(userId, notePostModel);
                return this.Ok(new { success = true, message = $"Note Added Successful" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public async Task<ActionResult> DeleteNote(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Unable to retrieve Delete notes." });
                }
                await this.noteBL.DeleteNote(userId, NoteId);
                return this.Ok(new { success = true, message = $"Notes detete Successfully" });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public async Task<ActionResult> UpdateNote(UpdateModel updateModel, int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note Id does not match." });
                }
                await this.noteBL.UpdateNote(userId, NoteId, updateModel);
                return this.Ok(new { success = true, message = $"Note updated Successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("IsTrash/{NoteId}")]
        public async Task<ActionResult> IsTrash(int NoteId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (note == null)
            {
                return this.BadRequest(new { success = false, message = $"Unable to  Trash note." });
            }
            await this.noteBL.IsTrash(userId, NoteId);
            return this.Ok(new { success = true, message = $"Updated The Trash" });
        }
        [Authorize]
        [HttpPut("RemainderNote/{NoteId}")]
        public async Task<ActionResult> Remainder(int NoteId, DateTimeModel dateTimeModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note does not exist." });
                }
                await this.noteBL.Remainder(userId, NoteId, dateTimeModel);
                return this.Ok(new { success = true, message = $"Remainder set succsefully." });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("IsArchieve/{NoteId}")]
        public async Task<ActionResult> IsArchieve(int NoteId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (note == null)
            {
                return this.BadRequest(new { success = false, message = $"Unable to  Archieve note." });
            }
            await this.noteBL.IsArchive(userId, NoteId);
            return this.Ok(new { success = true, message = $"Updated The Archieve" });
        }
        [Authorize]
        [HttpPut("IsPin/{NoteId}")]
        public async Task<ActionResult> IsPin(int NoteId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (note == null)
            {
                return this.BadRequest(new { success = false, message = $"Unable to  Pin note." });
            }
            await this.noteBL.IsPin(userId, NoteId);
            return this.Ok(new { success = true, message = $"Updated The Pin" });
        }
        [Authorize]
        [HttpPut("ChangeColour/{NoteId}/{Colour}")]
        public async Task<ActionResult> ChangeColour(int NoteId, string Colour)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (note == null)
            {
                return this.BadRequest(new { success = false, message = $"Note Does not Exist." });
            }
            await this.noteBL.ChangeColour(userId, NoteId, Colour);
            return this.Ok(new { success = true, message = $"Changed Colour Successfully" });
        }
        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public async Task<ActionResult> GetNote(int NoteId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (note == null)
            {
                return this.BadRequest(new { success = false, message = $"Note Does not Found." });
            }
            await this.noteBL.GetNote(userId, NoteId);
            return this.Ok(new { success = true, message = $"Required note is:", data = note });
        }
        [Authorize]
        [HttpGet("GetAllNote")]
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> list = new List<Note>();

                list = await this.noteBL.GetAllNotes(userId);
                return this.Ok(new { success = true, message = $"Get Note Successful", data = list });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNoteRedisCache")]
        public async Task<ActionResult> GetAllNoteRedisCache()
        {
            try
            {
                var CacheKey = "NoteList";
                string SerializeNoteList;
                var notelist = new List<Note>();
                var redisnotelist = await distributedCache.GetAsync(CacheKey);
                if(redisnotelist != null)
                {
                    SerializeNoteList = Encoding.UTF8.GetString(redisnotelist);
                    notelist = JsonConvert.DeserializeObject<List<Note>>(SerializeNoteList);    
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                    int userId = Int32.Parse(userid.Value);
                    notelist = await this.noteBL.GetAllNotes(userId);
                    SerializeNoteList = JsonConvert.SerializeObject(notelist);
                    redisnotelist = Encoding.UTF8.GetBytes(SerializeNoteList);

                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(CacheKey, redisnotelist, option);
                }
                return this.Ok(new { success = true, message = $"Get Note Successful", data = notelist });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
