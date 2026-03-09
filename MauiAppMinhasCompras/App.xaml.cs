using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{


    // Variável e propriedade usadas para acessar o banco SQLite,
    // garante uma única conexão para todo o aplicativo
    public partial class App : Application
    {
    static SQLiteDatabaseHelper _db;
        public static SQLiteDatabaseHelper Db
   {
     get
     {
      if (_db == null)
     {



//cria o caminho onde o banco
//SQLite será armazenado no dispositivo usando Path.
//Combine e Environment.GetFolderPath.
//Depois disso, ele cria uma instância da classe SQLiteDatabaseHelper, 
//passando o caminho do banco para permitir o acesso e manipulação dos dados

string path = Path.Combine(
Environment.GetFolderPath(
Environment.SpecialFolder.LocalApplicationData),
 "banco_sqlite_compras.db3");
_db = new SQLiteDatabaseHelper(path);
_db = new SQLiteDatabaseHelper("...db3");
 }

 return _db;

}
  }


public App()
 {
InitializeComponent();
  //MainPage = new AppShell();

 //definição da tela inicial
 //permite navegar entre as telas do aplicativo
MainPage = new NavigationPage(new Views.ListaProduto());       

          
        }
    }
}
