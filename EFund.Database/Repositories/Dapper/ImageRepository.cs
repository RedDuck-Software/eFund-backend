using Dapper;
using EFund.Database.Entities;
using EFund.Domain.Models.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Database.Repositories.Dapper
{
    public class ImageRepository : RepositoryBase
    {
        public ImageRepository(string connectionString) : base(connectionString) { }

        public async Task AddAsync(DataImage dataImage) =>
            await SqlConnection.ExecuteAsync("INSERT INTO images VALUES (@Id, @Image)", dataImage);

        public async Task<DataImage> GetByKeyAsync(string id)
        {
            return (await SqlConnection.QueryAsync<DataImage>(
                "SELECT * FROM images WHERE Id = @Id", new { Id = id }))
                .FirstOrDefault();
        }
    }
}
