using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.Models.ModelPetETutor;

namespace Vet_App_For_Freelancers.DataAccess
{
    public class TutorDataAccess : BaseData<Tutor>
    {
        public TutorDataAccess(SQLiteConnection connection) : base(connection) { }

        public List<Tutor> SearchTutorByName(string name)
        {
            try
            {
                return _connection.Table<Tutor>().Where(t => t.NomeTutor.Contains(name)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching tutor by name: {ex.Message}");
                return new List<Tutor>();
            }
        }

        public List<Tutor> GetAllTutors()
        {
            try
            {
                return _connection.Table<Tutor>().OrderBy(t => t.NomeTutor).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all tutors: {ex.Message}");
                return new List<Tutor>();
            }
        }
    }
}
