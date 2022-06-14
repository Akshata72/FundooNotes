using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    [Keyless]
    public class Collabrator
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        public string CollabEmail { get; set; }
    }
}
