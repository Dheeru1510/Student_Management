using BusinessModel.Implementation;
using EntityModel.StudentEntity;
using EntityModel.UserEntity;
using Student_Management.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Student_Management.Controllers
{
    [CustomAction]
    [BasicAuthentication]
    public class StudentController : ApiController
    {
        [HttpPost]
        [Route("api/SaveStudent")]
        public Object SaveStudentDetails([FromBody] StudentDetails data)
        {
            try
            {
                FileLogger.Log("Start SaveStudentDetails-- Submit student details");
                if (!string.IsNullOrEmpty(UserInfo.Uid))
                    data.UID = new Guid(UserInfo.Uid);
                data.CreatedOn = DateTime.Now;
                data.CreatedBy = -1;
                var res = new StudentDetailBO().InsertStudentDetail(data);
                if (res > 0)
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "Information saved successfully" });
                }
                else
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed", data = ex.Message });
            }
            finally
            {
                FileLogger.Log("End SaveStudentDetails-- Submit student details");
            }
        }

        [HttpPut]
        [Route("api/UpdateStudent")]
        public Object UpdateStudentDetail([FromBody] StudentDetails data)
        {
            try
            {
                FileLogger.Log("Start UpdateStudentDetail-- Update student details");
                string msg = "";
                if (data.ID <= 0)
                {
                    msg = "Failed";
                }
                else
                {
                    data.LastModifiedOn = DateTime.Now;
                    data.LastModifiedBy = -2;
                    var res = new StudentDetailBO().UpdateStudentDetail(data);
                    if (res > 0)
                    {
                        msg = "Information updated successfully";
                    }
                    else
                    {
                        msg = "Failed";
                    }
                }
                if (msg == "Failed")
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = msg });
                }
                else
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = msg });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed", data = ex.Message });
            }
            finally
            {
                FileLogger.Log("End UpdateStudentDetail-- Update student details");
            }
        }

        [HttpGet]
        [Route("api/GetAllStudent")]
        public Object GetAllStudent()
        {
            try
            {
                FileLogger.Log("Start GetAllStudent-- get all student records");
                var lst = new StudentDetailBO().GetAllDeails();
                if (lst.Count > 0)
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "Total records-" + lst.Count, data = lst });
                }
                else
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "No records found!" });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed", data = ex.Message });
            }
            finally
            {
                FileLogger.Log("End GetAllStudent-- get all student records");
            }
        }

        [HttpGet]
        [Route("api/GetStudentByID/{id}")]
        public Object GetStudentByID(int id)
        {
            try
            {
                FileLogger.Log("Start GetStudentByID-- get student records");
                var objStudentDetail = new StudentDetailBO().GetStudentByID(id);
                if (objStudentDetail != null)
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, data = objStudentDetail });
                }
                else
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "No records found!" });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed", data = ex.Message });
            }
            finally
            {
                FileLogger.Log("End GetStudentByID-- get student records");
            }
        }

        [HttpDelete]
        [Route("api/DeleteStudent/{id}")]
        public Object DeleteStudent(int id)
        {
            try
            {
                FileLogger.Log("Start DeleteStudent-- delete student detail");
                var res = new StudentDetailBO().DeleteStudent(id);
                if (res > 0)
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "Information deleted successfully!" });
                }
                else
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, message = "Failed", data = ex.Message });
            }
            finally
            {
                FileLogger.Log("End DeleteStudent-- delete student detail");
            }
        }
    }
}
