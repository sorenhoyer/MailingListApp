using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MailingListApp.Models;

namespace MailingListApp.Controllers
{
    public class SubscribersController : ApiController
    {
        private MailingListAppContext db = new MailingListAppContext();

        // GET: api/Subscribers
        public IQueryable<Subscriber> GetSubscribers()
        {
            return db.Subscribers
                .Include(x => x.MailingLists);
        }

        // GET: api/Subscribers/5
        [ResponseType(typeof(Subscriber))]
        public async Task<IHttpActionResult> GetSubscriber(int id)
        {
            Subscriber subscriber = await db.Subscribers.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return Ok(subscriber);
        }

        // PUT: api/Subscribers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubscriber(int id, Subscriber subscriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subscriber.Id)
            {
                return BadRequest();
            }

            db.Entry(subscriber).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Subscribers
        [ResponseType(typeof(Subscriber))]
        public async Task<IHttpActionResult> PostSubscriber(SubscriberDTO subscriberDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<MailingList> mailingList = new List<MailingList>();
            foreach (var mailingListId in subscriberDTO.MailingLists)
            {
                foreach (var ml in db.MailingLists)
                {
                    if (ml.Id == mailingListId)
                    {
                        mailingList.Add(ml);
                        break;
                    }
                }
                //string name = "List" + m;
                //string description = "List " + m + "description";
                //mailingList.Add(new MailingList { Id = m, Name = name, Description = description });
            }

            var subscriber = new Subscriber { Id = subscriberDTO.Id, Email = subscriberDTO.Email, First = subscriberDTO.First, MailingLists = mailingList };

            db.Subscribers.Add(subscriber);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = subscriber.Id }, subscriber);
        }

        // DELETE: api/Subscribers/5
        [ResponseType(typeof(Subscriber))]
        public async Task<IHttpActionResult> DeleteSubscriber(int id)
        {
            Subscriber subscriber = await db.Subscribers.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            db.Subscribers.Remove(subscriber);
            await db.SaveChangesAsync();

            return Ok(subscriber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubscriberExists(int id)
        {
            return db.Subscribers.Count(e => e.Id == id) > 0;
        }
    }
}