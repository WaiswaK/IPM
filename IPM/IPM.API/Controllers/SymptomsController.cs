using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;
using System.Web;
using System.IO;

namespace IPM.API.Controllers
{
    public class SymptomsController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Symptoms
        public ActionResult Index()
        {
            return View(db.Symptoms.ToList());
        }

        // GET: Symptoms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        // GET: Symptoms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_ID,Description,ImagePath,Keywords")] Symptom symptom, HttpPostedFileBase upload)
        {
            var query = db.Symptoms.Count() + 1;
            string temp = "SY-" + query;
            bool exist = false;
            try
            {
                var search = db.Symptoms.Where(c => c.S_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;
            }
            if (exist)
            {
                var all = db.Symptoms.ToList();
                var dis = all.Last();
                symptom.S_ID = "SY-" + DataModel.Constants.NextNumber(dis.S_ID);
            }
            else
            {
                symptom.S_ID = temp;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {                      
                        string ext = Path.GetExtension(upload.FileName).ToLower();
                        if (ext == ".jpg" || ext==".png" || ext ==".jpeg")
                        {
                            var uploadpath = Path.Combine(Server.MapPath("~/Images/Symptoms"),
                               symptom.S_ID + ".jpg"); 
                            var path = @"~/Images/Symptoms" + @"/" + symptom.S_ID + ".jpg";
                            symptom.ImagePath = path;
                            upload.SaveAs(uploadpath);
                        }
                    }
                    db.Symptoms.Add(symptom);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            return View(symptom);
        }

        // GET: Symptoms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_ID,Description,ImagePath,Keywords")] Symptom symptom, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        string ext = Path.GetExtension(upload.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                        {
                            var uploadpath = Path.Combine(Server.MapPath("~/Images/Symptoms"),
                               symptom.S_ID + ".jpg");
                            var path = @"~/Images/Symptoms" + @"/" + symptom.S_ID + ".jpg";
                            symptom.ImagePath = path;
                            upload.SaveAs(uploadpath);
                        }
                    }
                    db.Entry(symptom).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch
            {

            }
            return View(symptom);
        }

        // GET: Symptoms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Symptom symptom = db.Symptoms.Find(id);
            db.Symptoms.Remove(symptom);
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
