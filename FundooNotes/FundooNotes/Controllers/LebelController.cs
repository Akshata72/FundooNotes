using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LebelController : ControllerBase
    {
        FundooContext fundooContext;
        ILebelBL lebelBL;
        public LebelController(FundooContext fundooContext, ILebelBL lebelBL)
        {
            this.fundooContext = fundooContext;
            this.lebelBL = lebelBL;
        }
        [Authorize]
        [HttpPost("CreateLebel")]
        public async Task<ActionResult> AddLabel(int NoteId, string LebelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                if(NoteId == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note Does not Exist" });
                }
                await this.lebelBL.CreateLebel(userId, NoteId, LebelName);
                return this.Ok(new { success = true, message = $"Lebel Added Successful" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLabel")]
        public async Task<ActionResult> RemoveLabel(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var lebel = fundooContext.Label.FirstOrDefault(u => u.UserId == userId && u.NoteId == NoteId);
                if (lebel == null)
                {
                    return this.BadRequest(new { success = false, message = "Unable to retrieve Delete lebel." });
                }
                await this.lebelBL.DeleteLabel(userId, NoteId);
                return this.Ok(new { success = true, message = $"lebel detete Successfully" });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("UpdateLabel/{NoteId}")]
        public async Task<ActionResult> UpdateLabel( int NoteId, string LabelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var lebel = fundooContext.Label.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
                if (lebel == null)
                {
                    return this.BadRequest(new { success = false, message = "Note does not exist." });
                }
                await this.lebelBL.UpdateLabel(userId,NoteId, LabelName);
                return this.Ok(new { success = true, message = $"Label updated Successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("GetAllLabel")]
        public async Task<ActionResult> GetAllLabel()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Label> list = new List<Label>();

                list = await this.lebelBL.GetAllLabel(userId);
                return this.Ok(new { success = true, message = $"Get Label Successful", data = list });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetLabel/{NoteId}")]
        public async Task<ActionResult> GetLabel(int NoteId)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int userId = Int32.Parse(userid.Value);
            var label = fundooContext.Label.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == userId);
            if (label == null)
            {
                return this.BadRequest(new { success = false, message = $"Label Does not Found." });
            }
            await this.lebelBL.GetLabel(userId, NoteId);
            return this.Ok(new { success = true, message = $"Required label is:", data = label });
        }
    }
}
