using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Interface
{
    public interface IBaseData<T>
    {
        List<T> GetAll(); 
        T GetById(int id); 
        int Insert(T entity); 
        int Update(T entity); 
        int Delete(T entity);
    }
}
