using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;
using Services.Discount.Model;
using Shared.Util;

namespace Services.Discount.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;


        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Model.Discount>("SELECT * FROM discount");

            return Response.Return(Response.ResponseStatusEnum.Success, "İndirimler listelendi.", discounts);
        }

        public async Task<Response> FindById(int id)
        {
            var discount =
                (await _dbConnection.QueryAsync<Model.Discount>("SELECT * FROM discount WHERE id=@id",
                    new { id = id }))
                .SingleOrDefault();

            if (discount is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "İndirim bulunamadı.", null, 404);
            }

            return Response.Return(Response.ResponseStatusEnum.Success, "İndirim bulundu.", discount);
        }

        public async Task<Response> Save(Model.Discount discount)
        {
            var status =
                await _dbConnection.ExecuteAsync(
                    "INSERT INTO discounts (userid, rate, code) VALUES (@UserId, @Rate, @Code)", discount);

            if (status > 0)
            {
                return Response.Return(Response.ResponseStatusEnum.Success, "İndirim kayıt edildi.", null);
            }

            return Response.Return(Response.ResponseStatusEnum.Error, "İndirim kayıt edilemedi.", null, 500);
        }

        public async Task<Response> Update(Model.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync(
                "UPDATE discounts SET userid=@UserId, rate=@Rate, code=@Code WHERE id=@Id", new
                {
                    Id = discount.Id,
                    UserId = discount.UserId,
                    Rate = discount.Rate,
                    Code = discount.Code,
                });

            if (status > 0)
            {
                return Response.Return(Response.ResponseStatusEnum.Success, "İndirim güncellendi.", null);
            }

            return Response.Return(Response.ResponseStatusEnum.Error, "İndirim güncellenemedi.", null, 500);
        }

        public async Task<Response> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("DELETE FROM discounts WHERE id=@Id", new { Id = id, });

            if (status > 0)
            {
                return Response.Return(Response.ResponseStatusEnum.Success, "İndirim silindi.", null);
            }

            return Response.Return(Response.ResponseStatusEnum.Error, "İndirim silinemedi.", null, 500);
        }

        public async Task<Response> FindByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Model.Discount>(
                "SELECT * FROM discounts where userid=@UserId, code=@Code", new
                {
                    UserId = userId,
                    Code = code
                });


            var hasDiscount = discount.SingleOrDefault();

            if (hasDiscount is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "İndirim bulunamadı.", null, 404);
            }

            return Response.Return(Response.ResponseStatusEnum.Success, "İndirim bulundu.", hasDiscount);
        }
    }
}