using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Interface;

namespace Vet_App_For_Freelancers.Data
{
    public class BaseData<T> : IBaseData<T> where T : new()
    {
        protected readonly SQLiteConnection _connection;

        public BaseData(SQLiteConnection connection)
        {
            _connection = connection;
            _connection.CreateTable<T>(); // Cria a tabela correspondente
        }

        public List<T> GetAll()
        {
            try
            {
                return _connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Listar Itens: {ex.Message}");
                return new List<T>();
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _connection.Find<T>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Listar Itens por Id: {ex.Message}");
                return default(T);
            }
        }

        public int Insert(T entity)
        {
            try
            {
                return _connection.Insert(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Inserir item: {ex.Message}");
                return 0;
            }
        }

        public int Update(T entity)
        {
            try
            {
                return _connection.Update(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Atualizar Item: {ex.Message}");
                return 0;
            }
        }

        public int Delete(T entity)
        {
            try
            {
                return _connection.Delete(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Deletar Item: {ex.Message}");
                return 0;
            }
        }
    }
}
