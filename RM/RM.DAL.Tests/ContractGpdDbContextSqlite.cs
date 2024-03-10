using Microsoft.EntityFrameworkCore;

namespace RM.DAL.Tests;

/// <summary>
/// Контекст работы с базой данных договоров ГПД SQLite в памяти.
/// </summary>
/// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
internal class ContractGpdDbContextSqliteInMemory(DbContextOptions options) : ContractGpdDbContextBase(options)
{
}
