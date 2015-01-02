
namespace CashLight_App.Services.SQLite
{
    public interface ISQLiteService
    {
        SQLiteConnection Context { get; set; }
    }
}
