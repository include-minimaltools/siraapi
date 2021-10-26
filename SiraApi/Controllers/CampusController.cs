using SiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace SiraApi.Controllers
{
    public class CampusController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(from c in db.CAMPUS.ToList() select new { c.ID, c.DESCRIPTION, c.ADDRESS })
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage InsertOrUpdate(CAMPUS element)
        {
            try
            {
                CAMPUS campus = db.CAMPUS.FirstOrDefault(x => x.ID == element.ID);
                if (campus == null)
                {
                    db.CAMPUS.Add(new CAMPUS()
                    {
                        ID = element.ID,
                        DESCRIPTION = element.DESCRIPTION,
                        ADDRESS = element.ADDRESS,
                        DATE_CREATE = element.DATE_CREATE = DateTime.Now,
                        USER_CREATE = "admin"
                    });
                }
                else
                {
                    campus.ADDRESS = element.ADDRESS;
                    campus.DESCRIPTION = element.DESCRIPTION;
                    campus.DATE_UPDATE = DateTime.Now;
                    campus.USER_UPDATE = "admin";
                    db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }    
        }
    }
}