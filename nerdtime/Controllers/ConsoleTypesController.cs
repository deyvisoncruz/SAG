using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nerdtime.Models;

namespace nerdtime.Controllers
{
    public class ConsoleTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConsoleTypes
        public ActionResult Index()
        {
            return View(db.ConsoleTypes.ToList());
        }

        // GET: ConsoleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsoleType consoleType = db.ConsoleTypes.Find(id);
            if (consoleType == null)
            {
                return HttpNotFound();
            }

            var result = from g in db.Games
                         select new
                         {
                             g.Id,
                             g.Name,
                             Checked = ((from cg in db.GameConsoleTypes
                                         where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id)
                                         select cg).Count() > 0),
                             NumberCopys = ((from cg in db.GameConsoleTypes
                                             where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id)
                                             select cg.NumberCopys).SingleOrDefault()),
                             Viewer = ((from cg in db.GameConsoleTypes
                                             where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id)
                                             select cg.Viewer).Count() > 0)
                         };
            var MyViewModel = new ConsoleTypeViewModel();
            MyViewModel.Id = id.Value;
            MyViewModel.Name = consoleType.Name;

            var MyCheckBoxList = new List<CheckboxViewModel>();

            foreach (var item in result)
            {
                MyCheckBoxList.Add(new CheckboxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked, NumberCopys = item.NumberCopys, Viewer= item.Viewer });
            }

            MyViewModel.Games = MyCheckBoxList;

            return View(MyViewModel);
            
        }

        // GET: ConsoleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsoleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ConsoleType consoleType)
        {
            if (ModelState.IsValid)
            {
                db.ConsoleTypes.Add(consoleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consoleType);
        }

        // GET: ConsoleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsoleType consoleType = db.ConsoleTypes.Find(id);
            if (consoleType == null)
            {
                return HttpNotFound();
            }
            var result = from g in db.Games
                         select new
                         {
                             g.Id,
                             g.Name,
                             Checked = ((from cg in db.GameConsoleTypes
                                         where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id)
                                         select cg).Count() > 0),
                             NumberCopys = ((from cg in db.GameConsoleTypes
                                             where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id)
                                             select cg.NumberCopys).FirstOrDefault()),
                             Viewer = ((from cg in db.GameConsoleTypes
                                        where (cg.ConsoleTypesId == id) & (cg.GamesId == g.Id) & (cg.Viewer>0)
                                        select cg.Viewer).Count() > 0)
                         };
            var MyViewModel = new ConsoleTypeViewModel();
            MyViewModel.Id = id.Value;
            MyViewModel.Name = consoleType.Name;


            var MyCheckBoxList = new List<CheckboxViewModel>();

            foreach (var item in result)
            {
                MyCheckBoxList.Add(new CheckboxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked, NumberCopys = item.NumberCopys, Viewer = item.Viewer });
            }

            MyViewModel.Games = MyCheckBoxList;

            return View(MyViewModel);
        }

        // POST: ConsoleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name")] ConsoleType consoleType)
        public ActionResult Edit( ConsoleTypeViewModel consoleType)
        {
            if (ModelState.IsValid)
            {
                var MyConsoleType = db.ConsoleTypes.Find(consoleType.Id);
                MyConsoleType.Name = consoleType.Name;

                foreach(var item in db.GameConsoleTypes)
                {
                    if (item.ConsoleTypesId == consoleType.Id)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach(var item in consoleType.Games)
                {
                    if (item.Checked)
                    {
                        if(item.Viewer)
                        {
                            db.GameConsoleTypes.Add(new GameConsoleType() { ConsoleTypesId = consoleType.Id, GamesId = item.Id, NumberCopys = item.NumberCopys, Viewer = 1 });

                        }
                        else
                        {

                            db.GameConsoleTypes.Add(new GameConsoleType() { ConsoleTypesId = consoleType.Id, GamesId = item.Id, NumberCopys = item.NumberCopys, Viewer = 0 });
                        }
                   }
                }


               // db.Entry(consoleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consoleType);
        }

        // GET: ConsoleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsoleType consoleType = db.ConsoleTypes.Find(id);
            if (consoleType == null)
            {
                return HttpNotFound();
            }
            return View(consoleType);
        }

        // POST: ConsoleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsoleType consoleType = db.ConsoleTypes.Find(id);
            db.ConsoleTypes.Remove(consoleType);
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
