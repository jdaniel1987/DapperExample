using Dapper;
using DapperExample.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Immutable;

namespace DapperExample.Infrastructure.Repositories;

public class MarkRepository
{
    private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DapperExample;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public async Task<IReadOnlyCollection<Mark>> GetMarks()
    {
        string sql = @"SELECT [Id]
                              ,[Name]
                              ,[Surname]
                              ,[Score]
                          FROM [Marks] 
                          ORDER BY Surname";
        using var db = new SqlConnection(_connectionString);
        var marks = await db.QueryAsync<Mark>(sql);

        return marks.ToImmutableArray();
    }

    public async Task<Mark> GetMark(int id)
    {
        string sql = @"SELECT [Id]
                              ,[Name]
                              ,[Surname]
                              ,[Score]
                          FROM [Marks] 
                          WHERE Id = @id";
        var db = new SqlConnection(_connectionString);
        var mark = await db.QueryFirstAsync<Mark>(sql, new { id });

        return mark;
    }

    public async Task<int> CreateMark(Mark mark)
    {
        string sql = @"INSERT INTO [Marks] ([Name], [Surname], [Score])
                            OUTPUT INSERTED.Id
                           VALUES (@name, @surname, @mark);";

        var db = new SqlConnection(_connectionString);
        var id = await db.QuerySingleAsync<int>(sql, new
        {
            name = mark.Name,
            surname = mark.Surname,
            mark = mark.Score,
        });

        return id;
    }

    public async Task UpdateMark(Mark mark, int id)
    {
        string sql = @"UPDATE [Marks] 
                           SET
                                [Name] = @name, 
                                [Surname] = @surname, 
                                [Score] = @mark 
                           WHERE 
                                [Id] = @id";
        var db = new SqlConnection(_connectionString);
        await db.QueryAsync(sql, new
        {
            id,
            name = mark.Name,
            surname = mark.Surname,
            mark = mark.Score,
        });
    }

    public async Task DeleteMark(int id)
    {
        string sql = @"DELETE FROM [Marks]       
                           WHERE [Id] = @id";
        var db = new SqlConnection(_connectionString);
        await db.QueryAsync(sql, new { id });
    }
}
