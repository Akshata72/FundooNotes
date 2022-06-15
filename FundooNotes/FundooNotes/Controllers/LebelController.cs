﻿using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
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
        public async Task<ActionResult> AddCollab(int NoteId, string LebelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                if(LebelName == null)
                {
                    await this.lebelBL.CreateLebel(userId, NoteId, LebelName);
                    return this.Ok(new { success = true, message = $"Lebel Added Successful" });
                }
                return this.BadRequest(new { success = false, message = $"Already LebelName Added.." });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLabel")]
        public async Task<ActionResult> RemoveCollab(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var lebel = fundooContext.Note.FirstOrDefault(u => u.UserId == userId && u.NoteId == NoteId);
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
    }
}