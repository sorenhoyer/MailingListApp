using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailingListApp.Models
{
    public class SubscriberDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string First { get; set; }
        public int[] MailingLists { get; set; }
    }
}