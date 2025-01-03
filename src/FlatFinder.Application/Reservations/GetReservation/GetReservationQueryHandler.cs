using Dapper;
using FlatFinder.Application.Abstractions.CQRS;
using FlatFinder.Application.Abstractions.Data;
using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Application.Reservations.GetReservation
{
    internal sealed class GetReservationQueryHandler : IQueryHandler<GetReservationQuery, ReservationResponse>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public GetReservationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<ReservationResponse>> Handle(GetReservationQuery request, 
            CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.CreateConnection();
            
            const string sql = """
                SELECT
                    id AS Id,
                    flatid AS FlatId,
                    userid AS UserId,
                    duration_start AS DurationStart,
                    duration_end AS DurationEnd,                
                    priceforperiod_amount AS PriceAmount,
                    priceforperiod_currency AS PriceCurrency,
                    cleaningfee_amount AS CleaningFeeAmount,
                    cleaningfee_currency AS CleaningFeeCurrency,
                    amenitiesupcharge_amount AS AmenitiesUpChargeAmount,
                    amenitiesupcharge_currency AS AmenitiesUpChargeCurrency,
                    totalprice_amount AS TotalPriceAmount,
                    totalprice_currency AS TotalPriceCurrency,
                    status AS Status,
                    createdonutc AS CreatedOnUtc
                FROM reservations
                WHERE id = @ReservationId
                """;

            var reservation = await connection.QueryFirstOrDefaultAsync<ReservationResponse>(
                sql,
                new
                {
                    request.ReservationId
                });

            return reservation;
        }
    }
}
