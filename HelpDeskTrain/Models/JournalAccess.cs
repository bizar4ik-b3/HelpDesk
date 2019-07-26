using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTrain.Models
{
    public class JournalAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JournalId { get; set; }
    }
}