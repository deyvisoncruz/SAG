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
    public class GameConsoleTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GameConsoleTypes
        public ActionResult Index()
        {
            var gameConsoleTypes = db.GameConsoleTypes.Include(g => g.ConsoleTypes).Include(g => g.Games);
            return View(gameConsoleTypes.ToList());
        }

        // GET: GameConsoleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameConsoleType gameConsoleType = db.GameConsoleTypes.Find(id);
            if (gameConsoleType == null)
            {
                return HttpNotFound();
            }
            return View(gameConsoleType);
        }

        // GET: GameConsoleTypes/Create
        public ActionResult Create()
        {
            ViewBag.ConsoleTypesId = new SelectList(db.ConsoleTypes, "Id", "Name");
            ViewBag.GamesId = new SelectList(db.Games, "Id", "Name");
            return View();
        }

        // POST: GameConsoleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GamesId,ConsoleTypesId")] GameConsoleType gameConsoleType)
        {
            ViewBag.ConsoleTypesId = new SelectList(db.ConsoleTypes, "Id", "Name", gameConsoleType.ConsoleTypesId);
            ViewBag.GamesId = new SelectList(db.Games, "Id", "Name", gameConsoleType.GamesId);
            if (ModelState.IsValid)
            {
                try
                {
                    db.GameConsoleTypes.Add(gameConsoleType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception )
                {
                    ViewBag.MessageError = "Já existe cadastro deste Game cadastrado para o Console!";
                    return View(gameConsoleType);
                }
               
            }

            
            return View(gameConsoleType);
        }

        // GET: GameConsoleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameConsoleType gameConsoleType = db.GameConsoleTypes.Find(id);
            if (gameConsoleType == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsoleTypesId = new SelectList(db.ConsoleTypes, "Id", "Name", gameConsoleType.ConsoleTypesId);
            ViewBag.GamesId = new SelectList(db.Games, "Id", "Name", gameConsoleType.GamesId);
            return View(gameConsoleType);
        }

        // POST: GameConsoleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GamesId,ConsoleTypesId")] GameConsoleType gameConsoleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameConsoleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsoleTypesId = new SelectList(db.ConsoleTypes, "Id", "Name", gameConsoleType.ConsoleTypesId);
            ViewBag.GamesId = new SelectList(db.Games, "Id", "Name", gameConsoleType.GamesId);
            return View(gameConsoleType);
        }

        // GET: GameConsoleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameConsoleType gameConsoleType = db.GameConsoleTypes.Find(id);
            if (gameConsoleType == null)
            {
                return HttpNotFound();
            }
            return View(gameConsoleType);
        }

        // POST: GameConsoleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameConsoleType gameConsoleType = db.GameConsoleTypes.Find(id);
            db.GameConsoleTypes.Remove(gameConsoleType);
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
