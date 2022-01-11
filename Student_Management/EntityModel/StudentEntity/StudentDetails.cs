using System;

namespace EntityModel.StudentEntity
{
    public class StudentDetails
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public int RollNo { get; set; }

        public byte Age { get; set; }

        public string Class { get; set; }

        public string Sec { get; set; }

        public DateTime CreatedOn { get; set; }

        public short CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public short? LastModifiedBy { get; set; }

        public Guid UID { get; set; }

        public bool IsActive { get; set; }
    }

}
