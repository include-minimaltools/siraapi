using SiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SiraApi.Controllers
{
    public class CareerController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            var data = (from c in db.CAREER.ToList()
                        select new
                        {
                            c.ID,
                            c.FACULTY,
                            c.DESCRIPTION,
                            c.CAMPUS
                        }).ToList();
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(data)
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage InsertOrUpdate(CAREER element)
        {
            try
            {
                CAREER career = db.CAREER.FirstOrDefault(x => x.ID == element.ID);
                if (career == null)
                {
                    db.CAREER.Add(new CAREER()
                    {
                        ID = element.ID,
                        DESCRIPTION = element.DESCRIPTION,
                        FACULTY = element.FACULTY,
                        CAMPUS = element.CAMPUS,
                        DATE_CREATE = DateTime.Now,
                        USER_CREATE = "admin"
                    });
                }
                else
                {
                    career.DESCRIPTION = element.DESCRIPTION;
                    career.FACULTY = element.FACULTY;
                    career.CAMPUS = element.CAMPUS;
                    career.DATE_UPDATE = DateTime.Now;
                    career.USER_UPDATE = "admin";
                    db.Entry(career).State = System.Data.Entity.EntityState.Modified;
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