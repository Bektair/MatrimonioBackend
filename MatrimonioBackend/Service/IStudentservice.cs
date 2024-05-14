using MatrimonioBackend.Models;

namespace MatrimonioBackend.Service
{
    public interface IStudentservice
    {

        public IQueryable<Student> RetrieveAllStudents();

    }
}
