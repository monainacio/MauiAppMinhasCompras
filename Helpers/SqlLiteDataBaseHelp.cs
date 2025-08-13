using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiAppMinhasCompras.Models;
using SQLitePCL;


namespace MauiAppMinhasCompras.Helpers
{
    internal class SqlLiteDataBaseHelp
    {
        readonly SQLiteAsyncConnection _conn;
        public SqlLiteDataBaseHelp(string path) 
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTablesAsync<Produto>().Wait();
        
        }

        public int Insert(Produto p)
        { 
            return _conn.InsertAsync
        
        
        }

        public void Update(Produto p) { }

        public void Delete(int id) { }

        public void getAll() { }

        public void Search(string q) { }
    }
}
