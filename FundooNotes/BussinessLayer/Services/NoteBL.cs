using BussinessLayer.Interfaces;
using DataBaseLayer.Notes;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try
            {
               await noteRL.AddNote(UserId, notePostModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteNote(int UserId, int NoteId)
        {
            try
            {
                await noteRL.DeleteNote(UserId,NoteId);
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
                await noteRL.Remainder(UserId, NoteId, dateTimeModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task IsTrash(int UserId, int NoteId)
        {
            try
            {
                await noteRL.IsTrash(UserId, NoteId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateNote(int UserId, int NoteId, UpdateModel updateModel)
        {
            try
            {
                await noteRL.UpdateNote(UserId, NoteId, updateModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task IsArchive(int UserId, int NoteId)
        {
            try
            {
                await noteRL.IsArchive(UserId, NoteId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task IsPin(int UserId, int NoteId)
        {
            try
            {
                await noteRL.IsPin(UserId, NoteId);
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
                await noteRL.ChangeColour(UserId, NoteId, Colour);
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
                return await noteRL.GetNote(UserId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Note>> GetAllNotes(int UserId)
        {
            try
            {
                return await noteRL.GetAllNotes(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
