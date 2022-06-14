using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Notes
{
    public class DateTimeModel
    {
        [Required(ErrorMessage = "Select Date Of Birth")]
   
       // [RegularExpression(@"^(?:19\d{2}|20[01][0-9]|2020)[-/.](?:0[1-9]|1[012])[-/.](?:0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Enter YYYY-MM-DD format only")]
    // [RegularExpression(@"^(19|20)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Enter YYYY-MM-DD format only")]
        [RegularExpression("^(3[01]|[12][0-9]|0?[1-9])/(1[0-2]|0?[1-9])/(?:[0-9]{2})?[0-9]{2}$", ErrorMessage = "Enter YYYY-MM-DD format only")]
        public DateTime Remainder { get; set; }
    }
}
