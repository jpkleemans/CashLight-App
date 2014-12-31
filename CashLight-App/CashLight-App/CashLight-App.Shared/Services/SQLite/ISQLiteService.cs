
namespace CashLight_App.Services.SQLite
{
    interface ISQLiteService
    {
        SQLiteConnection Context { get; set; }
    }
}
