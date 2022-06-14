using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Notes
{
    public class NoteTrash
    {
        [DefaultValue(false)]
        public bool IsTrash { get; set; }
    }
}
