using SiraApi.Models;
using SiraApi.ModelsView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SiraApi.Controllers
{
    public class StudentCourseController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCoursesByIdStudent(string carnet)
        {
            var data = (from sc in db.STUDENT_COURSE
                        join c in db.COURSE on sc.ID_COURSE equals c.ID
                        where sc.ID_STUDENT == carnet
                        select new { c.ID, c.DESCRIPTION, c.CREDITS, c.FRECUENCY, c.HOURS, c.ID_CAREER }).ToList();

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(data)
            };
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpStatusCode RemoveStudentCourses(STUDENT_COURSE element)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                STUDENT_COURSE studentCourse = db.STUDENT_COURSE.Where(x => x.ID_STUDENT == element.ID_STUDENT && x.ID_COURSE == element.ID_COURSE).FirstOrDefault();
               // var a = db.STUDENT_COURSE.Find(element.ID_STUDENT, element.ID_COURSE);
                if(studentCourse != null)
                {
                  //  db.STUDENT_COURSE.Remove(studentCourse);
                    db.Entry(studentCourse).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpStatusCode EnrollStudentsWithCourse(EnrollView elements)
        {
            try
            {
                foreach(var student in elements.Students)
                {
                    foreach(var course in elements.Courses)
                    {
                        if(db.STUDENT_COURSE.Where(x => x.ID_COURSE == course.ID && x.ID_STUDENT == student.CARNET).ToList().Count == 0)
                        {
                            db.STUDENT_COURSE.Add(new STUDENT_COURSE
                            {
                                ID_STUDENT = student.CARNET,
                                ID_COURSE = course.ID,
                                DATE_CREATE = DateTime.Now,
                                USER_CREATE = "admin"
                            });
                        }
                    }
                }
                db.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}