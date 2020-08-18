using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;


namespace Web.Controllers
{
    public class AmigoController : Controller
    {
        public AmigoServices Services { get; set; }
        public AmigoController()
        {
            this.Services = new AmigoServices();
        }
        // GET: AmigoController
        public ActionResult Index()
        {
            return View(this.Services.GetAll());
        }

        public ActionResult AmigoEmail()
        {
            // por sessão
            if (this.HttpContext.Session.GetString("AmigoEmailSelecionado") != null)
            {
                var idsSelecionados = JsonConvert.DeserializeObject<List<Amigo>>(this.HttpContext.Session.GetString("AmigoEmailSelecionado"));
                ViewBag.IdsSelecionados = idsSelecionados.Select(x => x.Id.ToString());
            }
            return View(this.Services.GetAll());

            //-------------------//

            //lista normal

            //var amigo = this.Services.GetAll();
            //return View(amigo);

            //------------------//

            //por script

            //if (!String.IsNullOrWhiteSpace(ids))
            //{
                //ViewBag.IdsSelecionados = ids.Split(",");
            //}
            //return View(this.Services.GetAll()); 
        }

        public ActionResult AmigoEmailSelecionado(string ids)
        {
            //Por ssessão

            List<Amigo> amigoEmailSelecionado = new List<Amigo>();
            this.HttpContext.Session.Clear();
            if (!String.IsNullOrWhiteSpace(ids))
            {
                foreach (var item in ids.Split(","))
                {
                    amigoEmailSelecionado.Add(this.Services.GetAmigoById(new Guid(item)));

                }
            }
            this.HttpContext.Session.SetString("AmigoEmailSelecionado", JsonConvert.SerializeObject(amigoEmailSelecionado));
            return View(amigoEmailSelecionado);

            //-------------------//

            //Por script

            //List<Amigo> amigoEmailSelecionado = new List<Amigo>();
            //if (!String.IsNullOrWhiteSpace(ids))
            //{
            //foreach(var item in ids.Split(","))
            //{
            //amigoEmailSelecionado.Add(this.Services.GetAmigoById(new Guid(item)));
            //}
            //}
            //return View(amigoEmailSelecionado);
        }

        public ActionResult AmigoData()
        {
            // por sessão
            if (this.HttpContext.Session.GetString("AmigoDataSelecionada") != null)
            {
                var idsSelecionados = JsonConvert.DeserializeObject<List<Amigo>>(this.HttpContext.Session.GetString("AmigoDataSelecionada"));
                ViewBag.IdsSelecionados = idsSelecionados.Select(x => x.Id.ToString());
            }
            return View(this.Services.GetAll());

            //-------------------//

            //lista normal

            //var amigo = this.Services.GetAll();
            //return View(amigo);

            //------------------//

            //por script

            //if (!String.IsNullOrWhiteSpace(ids))
            //{
            //ViewBag.IdsSelecionados = ids.Split(",");
            //}
            //return View(this.Services.GetAll());
        }

        public ActionResult AmigoDataSelecionada(string ids)
        {
            //Por ssessão

            List<Amigo> amigoDataSelecionada = new List<Amigo>();
            this.HttpContext.Session.Clear();
            if (!String.IsNullOrWhiteSpace(ids))
            {
                foreach (var item in ids.Split(","))
                {
                    amigoDataSelecionada.Add(this.Services.GetAmigoById(new Guid(item)));

                }
            }
            this.HttpContext.Session.SetString("AmigoDataSelecionada", JsonConvert.SerializeObject(amigoDataSelecionada));
            return View(amigoDataSelecionada);

            //-------------------//

            //Por script

            //List<Amigo> amigoEmailSelecionado = new List<Amigo>();
            //if (!String.IsNullOrWhiteSpace(ids))
            //{
            //foreach(var item in ids.Split(","))
            //{
            //amigoEmailSelecionado.Add(this.Services.GetAmigoById(new Guid(item)));
            //}
            //}
            //return View(amigoEmailSelecionado);
        }


        // GET: AmigoController/Details/5
        public ActionResult Details(Guid id)
        {
            var amigo = this.Services.GetAmigoById(id);
            return View(amigo);
        }

        // GET: AmigoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Amigo amigo)
        {
            try
            {
                this.Services.Save(amigo);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("APP_ERROR", ex.Message);
                return View(amigo);
            }
        }

        // GET: AmigoController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var amigo = this.Services.GetAmigoById(id);
            return View(amigo);
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Amigo amigo)
        {
            try
            {
                amigo.Id = id;
                this.Services.Update(amigo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var amigo = this.Services.GetAmigoById(id);
            return View(amigo);
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Amigo amigo)
        {
            try
            {
                this.Services.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
