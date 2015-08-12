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
    public class MailingListsController : ApiController
    {
        private MailingListAppContext db = new MailingListAppContext();

        // GET: api/MailingLists
        public IQueryable<MailingList> GetMailingLists()
        {
            return db.MailingLists;
        }

        // GET: api/MailingLists/5
        [ResponseType(typeof(MailingList))]
        public async Task<IHttpActionResult> GetMailingList(int id)
        {
            MailingList mailingList = await db.MailingLists.FindAsync(id);
            if (mailingList == null)
            {
                return NotFound();
            }

            return Ok(mailingList);
        }

        // PUT: api/MailingLists/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMailingList(int id, MailingList mailingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mailingList.Id)
            {
                return BadRequest();
            }

            db.Entry(mailingList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailingListExists(id))
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

        // POST: api/MailingLists
        [ResponseType(typeof(MailingList))]
        public async Task<IHttpActionResult> PostMailingList(MailingList mailingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MailingLists.Add(mailingList);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mailingList.Id }, mailingList);
        }

        // DELETE: api/MailingLists/5
        [ResponseType(typeof(MailingList))]
        public async Task<IHttpActionResult> DeleteMailingList(int id)
        {
            MailingList mailingList = await db.MailingLists.FindAsync(id);
            if (mailingList == null)
            {
                return NotFound();
            }

            db.MailingLists.Remove(mailingList);
            await db.SaveChangesAsync();

            return Ok(mailingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MailingListExists(int id)
        {
            return db.MailingLists.Count(e => e.Id == id) > 0;
        }
    }
}