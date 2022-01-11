using BusinessModel.Abstraction;
using DataModel;
using EntityModel.StudentEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Implementation
{
    public class StudentDetailBO : IStudentDetailBO
    {
        public int InsertStudentDetail(StudentDetails model)
        {
            using (var _dbContext = new StudentManagementEntities())
            {
                var objStudentDetail = _dbContext.StudentDetails.Where(p => p.ID == model.ID && p.IsActive == true).FirstOrDefault();
                if (objStudentDetail == null)
                {
                    var objStudent = new StudentDetail()
                    {
                        Name = model.Name,
                        FatherName = model.FatherName,
                        MotherName = model.MotherName,
                        RollNo = model.RollNo,
                        Age = model.Age,
                        Class = model.Class,
                        Sec = model.Sec,
                        CreatedOn = model.CreatedOn,
                        CreatedBy = model.CreatedBy,
                        UID = model.UID,
                        IsActive = true
                    };
                    _dbContext.StudentDetails.Add(objStudent);
                }
                return _dbContext.SaveChanges();
            }
        }

        public int UpdateStudentDetail(StudentDetails model)
        {
            using (var _dbContext = new StudentManagementEntities())
            {
                var res = -1;
                var objStudentDetail = _dbContext.StudentDetails.Where(p => p.ID == model.ID && p.IsActive == true).FirstOrDefault();
                if (objStudentDetail != null)
                {
                    objStudentDetail.Name = model.Name;
                    objStudentDetail.FatherName = model.FatherName;
                    objStudentDetail.MotherName = model.MotherName;
                    objStudentDetail.RollNo = model.RollNo;
                    objStudentDetail.Age = model.Age;
                    objStudentDetail.Class = model.Class;
                    objStudentDetail.Sec = model.Sec;
                    objStudentDetail.LastModifiedBy = model.LastModifiedBy;
                    objStudentDetail.LastModifiedOn = model.LastModifiedOn;
                    _dbContext.Entry(objStudentDetail).State = System.Data.Entity.EntityState.Modified;
                    res = _dbContext.SaveChanges();
                }
                return res;
            }
        }

        public List<StudentDetails> GetAllDeails()
        {
            var lst = new List<StudentDetails>();
            using (var db = new StudentManagementEntities())
            {
                var lstStu = db.StudentDetails.Where(p => p.IsActive == true).ToList();
                if (lstStu.Count > 0)
                {
                    foreach (var item in lstStu)
                    {
                        lst.Add(new StudentDetails()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            FatherName = item.FatherName,
                            MotherName = item.MotherName,
                            RollNo = item.RollNo,
                            Age = item.Age,
                            Class = item.Class,
                            Sec = item.Sec,
                            CreatedOn = item.CreatedOn,
                            CreatedBy = item.CreatedBy,
                            LastModifiedOn = item.LastModifiedOn,
                            LastModifiedBy = item.LastModifiedBy,
                            UID = item.UID,
                            IsActive = item.IsActive
                        });
                    }
                }
            }
            return lst;
        }

        public StudentDetails GetStudentByID(int id)
        {
            using (var db = new StudentManagementEntities())
            {
                var objSutdent = db.StudentDetails.Where(p => p.ID == id && p.IsActive == true).FirstOrDefault();
                if (objSutdent != null)
                {
                    var objStudents = new StudentDetails()
                    {
                        ID = objSutdent.ID,
                        Name = objSutdent.Name,
                        FatherName = objSutdent.FatherName,
                        MotherName = objSutdent.MotherName,
                        RollNo = objSutdent.RollNo,
                        Age = objSutdent.Age,
                        Class = objSutdent.Class,
                        Sec = objSutdent.Sec,
                        CreatedOn = objSutdent.CreatedOn,
                        CreatedBy = objSutdent.CreatedBy,
                        LastModifiedOn = objSutdent.LastModifiedOn,
                        LastModifiedBy = objSutdent.LastModifiedBy,
                        UID = objSutdent.UID,
                        IsActive = objSutdent.IsActive
                    };
                    return objStudents;
                }
                else
                {
                    return null;
                }
            }
        }

        public int DeleteStudent(int id)
        {
            using (var db = new StudentManagementEntities())
            {
                var objStudentDetail = db.StudentDetails.Where(p => p.ID == id && p.IsActive == true).FirstOrDefault();
                if (objStudentDetail != null)
                {
                    objStudentDetail.IsActive = false;
                    objStudentDetail.LastModifiedOn = DateTime.Now;
                    objStudentDetail.LastModifiedBy = -2;
                    db.Entry(objStudentDetail).State = System.Data.Entity.EntityState.Modified;
                    return db.SaveChanges();
                }
            }
            return -1;
        }
    }
}
