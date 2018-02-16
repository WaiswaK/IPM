using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class Pest_SymptomController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Pest_Symptom
        public ActionResult Index()
        {
            var pest_Symptoms = db.Pest_Symptoms.Include(p => p.Part).Include(p => p.Pest).Include(p => p.Symptom);
            return View(pest_Symptoms.ToList());
        }

        // GET: Pest_Symptom/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Symptom pest_Symptom = db.Pest_Symptoms.Find(id);
            if (pest_Symptom == null)
            {
                return HttpNotFound();
            }
            return View(pest_Symptom);
        }

        // GET: Pest_Symptom/Create
        public ActionResult Create()
        {
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name");
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name");
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description");
            return View();
        }

        // POST: Pest_Symptom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PS_ID,P_ID,S_ID,Part_ID")] Pest_Symptom pest_Symptom)
        {
            var query = db.Pest_Symptoms.Count() + 1;
            string temp = "PSY-" + query;
            bool exist = false;
            try
            {
                var search = db.Pest_Symptoms.Where(c => c.PS_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Pest_Symptoms.ToList();
                var dis = all.Last();
                pest_Symptom.PS_ID = "PSY-" + DataModel.Constants.NextNumber(dis.PS_ID);
            }
            else
            {
                pest_Symptom.PS_ID = temp;
            }
            if (ModelState.IsValid)
            {
                db.Pest_Symptoms.Add(pest_Symptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", pest_Symptom.Part_ID);
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Symptom.P_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", pest_Symptom.S_ID);
            return View(pest_Symptom);
        }

        // GET: Pest_Symptom/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Symptom pest_Symptom = db.Pest_Symptoms.Find(id);
            if (pest_Symptom == null)
            {
                return HttpNotFound();
            }
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", pest_Symptom.Part_ID);
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Symptom.P_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", pest_Symptom.S_ID);
            return View(pest_Symptom);
        }

        // POST: Pest_Symptom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PS_ID,P_ID,S_ID,Part_ID")] Pest_Symptom pest_Symptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pest_Symptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", pest_Symptom.Part_ID);
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Symptom.P_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", pest_Symptom.S_ID);
            return View(pest_Symptom);
        }

        // GET: Pest_Symptom/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Symptom pest_Symptom = db.Pest_Symptoms.Find(id);
            if (pest_Symptom == null)
            {
                return HttpNotFound();
            }
            return View(pest_Symptom);
        }

        // POST: Pest_Symptom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pest_Symptom pest_Symptom = db.Pest_Symptoms.Find(id);
            db.Pest_Symptoms.Remove(pest_Symptom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
