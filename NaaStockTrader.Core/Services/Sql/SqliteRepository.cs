using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.Services.Sql
{
    public interface IRepository<T>
    {
        int Insert(T obj);
        int CreateTable();
        int DropTable();
        int Delete(T objectToDelete);
        List<T> Query(string query, params object[] args);
        int Execute(string query, params object[] args);
    }

    public interface ISqlTable
    {
    }

    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }

    /// <summary>
    /// SQLiteRepository used to perform SQLite operations
    /// </summary>
    public class SQLiteRepository
    {
        private readonly ISQLiteConnection _sqLiteConnection;

        /// <summary>
        /// Creates a SQLiteRepository
        /// </summary>
        /// <param name="sqLiteConnection">The platform specific ISQLiteConnection</param>
        public SQLiteRepository(ISQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;
        }

        /// <summary>
        /// Gets the concrete SQLiteConnection to perform SQLite operations with
        /// </summary>
        /// <returns>SQLiteConnection</returns>
        public SQLiteConnection GetConnection()
        {
            return _sqLiteConnection.GetConnection();
        }

        /// <summary>
        /// Inserts the object into the ojects type table
        /// </summary>
        /// <typeparam name="T">Table</typeparam>
        /// <param name="obj">Object to insert</param>
        /// <returns>The number of rows that were successfully inserted</returns>
        public int Insert<T>(T obj)
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.Insert(obj);
            }
        }

        /// <summary>
        /// Creates the specified table
        /// </summary>
        /// <typeparam name="T">Table to create</typeparam>
        /// <returns>Returns 0 if successful</returns>
        public int CreateTable<T>() where T : ISqlTable
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.CreateTable<T>();
            }
        }

        /// <summary>
        /// Drops the specified table
        /// </summary>
        /// <typeparam name="T">Table to drop</typeparam>
        /// <returns>Returns 0 if successful</returns>
        public int DropTable<T>() where T : ISqlTable
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.DropTable<T>();
            }
        }

        /// <summary>
        /// Deletes the given object out of the table
        /// </summary>
        /// <typeparam name="T">Table to delete out of</typeparam>
        /// <param name="objectToDelete"></param>
        /// <returns></returns>
        public int Delete<T>(T objectToDelete) where T : ISqlTable
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.Delete<T>(objectToDelete);
            }
        }

        /// <summary>
        /// Executes a query against the database
        /// </summary>
        /// <typeparam name="T">The type of objects expected to be returned</typeparam>
        /// <param name="query">The query string that will be executed, use '?' for the parameters as they will be replaced with the args</param>
        /// <param name="args">Arguments that will be substituted into the query string</param>
        /// <returns>List of the specified type</returns>
        public List<T> Query<T>(string query, params object[] args) where T : class
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.Query<T>(query, args);
            }
        }

        public int Execute(string query, params object[] args)
        {
            var sqLiteConnection = GetConnection();
            using (sqLiteConnection)
            {
                return sqLiteConnection.Execute(query, args);
            }
        }
    }    
}
