using EntityModel.StudentEntity;
using System.Collections.Generic;

namespace BusinessModel.Abstraction
{
    public interface IStudentDetailBO
    {
        int InsertStudentDetail(StudentDetails model);
        int UpdateStudentDetail(StudentDetails model);
        List<StudentDetails> GetAllDeails();
        StudentDetails GetStudentByID(int id);
        int DeleteStudent(int id);
    }
}
