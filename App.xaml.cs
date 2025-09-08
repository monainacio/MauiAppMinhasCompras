using MauiAppMinhasCompras.Helpers;
using SQLite;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SqlLiteDataBaseHelper _db;

        public static SqlLiteDataBaseHelper Db
        {
            get
            {          

                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), 
                        "banco_sqlite_compras.db3");

                    _db = new SqlLiteDataBaseHelper(path);
                }
                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        
        }
    }
}
