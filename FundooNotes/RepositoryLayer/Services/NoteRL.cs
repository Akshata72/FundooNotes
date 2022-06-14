using DataBaseLayer.Notes;
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
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try
            {
                Note note = new Note();
                note.UserId = UserId;
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.Colour = notePostModel.Colour;
                note.Ispin = false;
                note.IsArchieve = false;
                note.IsRemainder = false;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                fundooContext.Add(note);
                await fundooContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteNote(int UserId,int NoteId)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u=>u.NoteId == NoteId && u.UserId == UserId);
                if (note == null)
                {
                    throw new Exception("No Note Exist");
                }
                fundooContext.Note.Remove(note);
                await fundooContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task Remainder(int UserId, int NoteId, DateTimeModel dateTimeModel)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == UserId);
                if (note != null)
                {
                   if(note.IsTrash == false)
                    {
                        note.IsRemainder = true;
                        note.Remainder = dateTimeModel.Remainder;
                    }
                   await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task IsTrash(int UserId,int NoteId)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u=> u.NoteId == NoteId && u.UserId == UserId);
                if (note != null)
                {
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;    
                    }
                    else
                    {
                        note.IsTrash = false;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateNote(int UserId,int NoteId, UpdateModel updateModel)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == UserId);
                if (note == null)
                {
                    throw new Exception("No Note Exist");
                }
                note.Title = updateModel.Title;
                note.ModifiedDate =DateTime.Now;
                note.Description = updateModel.Description;
                note.Colour = updateModel.Colour;
                note.IsTrash = updateModel.IsTrash;
                note.Ispin = updateModel.Ispin;
                note.IsArchieve = updateModel.IsArchieve;
                await fundooContext.SaveChangesAsync();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task IsArchive(int UserId, int NoteId)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == UserId);
                if (note != null)
                {
                    if (note.IsArchieve == false)
                    {
                        note.IsArchieve = true;
                    }
                    else
                    {
                        note.IsArchieve = false;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task IsPin(int UserId, int NoteId)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u => u.NoteId == NoteId && u.UserId == UserId);
                if (note != null)
                {
                    if (note.Ispin == false)
                    {
                        note.Ispin = true;
                    }
                    else
                    {
                        note.Ispin = false;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ChangeColour(int UserId, int NoteId, string Colour)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(u => u.UserId == UserId && u.NoteId == NoteId);
                if(note != null)
                {
                    note.Colour = Colour;
                }
                await fundooContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Note> GetNote(int UserId, int NoteId)
        {
            try
            {
               return await fundooContext.Note.Where(u => u.UserId == UserId && u.NoteId == NoteId).FirstOrDefaultAsync();

            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<Note>> GetAllNotes(int UserId)
        {
            try
            {
                return await fundooContext.Note.Where(u => u.UserId == UserId).ToListAsync();
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
