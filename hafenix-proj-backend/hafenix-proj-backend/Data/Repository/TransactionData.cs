using Dapper;
using hafenix_proj_backend.Data.Interface;
using hafenix_proj_backend.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace hafenix_proj_backend.Data.Repository
{
    public class TransactionData : ITransactionData
    {
        private readonly string _connectionString;

        public TransactionData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddTransactionRecord(Transaction transaction)
        {
            string query = @"INSERT INTO Transactions (FullNameHeb, FullNameEng, BirthDate, UserId, ActionType, AccountNumber, Amount)
                             VALUES (@FullNameHeb, @FullNameEng, @BirthDate, @UserId, @ActionType, @AccountNumber, @Amount); SELECT LAST_INSERT_ID();";

            await using var connection = new MySqlConnection(_connectionString);
            var lastInsertId = await connection.QuerySingleAsync<int>(query, new
            {
                transaction.FullNameHeb,
                transaction.FullNameEng,
                transaction.BirthDate,
                transaction.UserId,
                transaction.ActionType,
                transaction.AccountNumber,
                transaction.Amount
            });

            return lastInsertId;

        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int userId, int pageSize, int pageNumber) {
            int offSet = pageSize * (pageNumber - 1);
            string query = @"SELECT Transaction_ID, FullNameHeb, FullNameEng, BirthDate, UserId, ActionType, AccountNumber, Amount FROM Transaction
                            WHERE UserId=@userId ORDER BY Transaction_ID LIMIT pageSize OFFSET @offSet;";

            await using var connection = new MySqlConnection(_connectionString);
            var transactions = await connection.QueryAsync<Transaction>(query, new {userId, pageSize, pageNumber, offSet});
            return transactions;

        }

        public async Task<int> GetTotalTransactionsNumber(int userId)
        {
            string query = @"SELECT COUNT(*) FROM Transaction WHERE UserId=@userId;";
            await using var connection = new MySqlConnection(_connectionString);
            var totalTransations = await connection.QuerySingleAsync<int>(query, new { userId }, commandTimeout: 60);
            return totalTransations;
        }


    }
}
