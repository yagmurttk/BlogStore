using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id );
        T GetById(int id);
        List<T> GetAll();
        //List<T> GetAll(Func<T, bool> predicate); // Optional: for filtering
        //T Get(Func<T, bool> predicate); // Optional: for single item retrieval
    }
}
