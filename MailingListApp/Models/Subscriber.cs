using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailingListApp.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string First { get; set; }
        public ICollection<MailingList> MailingLists { get; set; }
    }
}