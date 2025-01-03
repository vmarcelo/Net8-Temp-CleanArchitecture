using Dapper;
using FlatFinder.Application.Abstractions.CQRS;
using FlatFinder.Application.Abstractions.Data;
using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Reservations;

namespace FlatFinder.Application.Flats.SearchFlats
{
    internal sealed class SearchFlatsQueryHandler
        : IQueryHandler<SearchFlatsQuery, IReadOnlyList<FlatResponse>>
    {
        private static readonly int[] ActiveReservationStatuses =
        {
            (int)ReservationStatus.Reserved,
            (int)ReservationStatus.Confirmed,
            (int)ReservationStatus.Completed
        };
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public SearchFlatsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<FlatResponse>>> Handle(SearchFlatsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.StartDate > request.EndDate)
            {
                return new List<FlatResponse>();
            }

            using var connection = sqlConnectionFactory.CreateConnection();

            const string sql = """
                DECLARE @ActiveReservationStatuses TABLE (Status INT);
                INSERT INTO @ActiveReservationStatuses (Status)
                VALUES (@Status1), (@Status2), (@Status3);

                SELECT
                    a.id AS Id,
                    a.name AS Name,
                    a.description AS Description,
                    a.price_amount AS Price,
                    a.price_currency AS Currency,
                    a.address_country AS Country,
                    a.address_state AS State,
                    a.address_zipcode AS ZipCode,
                    a.address_city AS City,
                    a.address_street AS Street
                FROM flats AS a
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM reservations AS b
                    WHERE
                        b.flatid = a.id AND
                        b.duration_start <= @EndDate AND
                        b.duration_end >= @StartDate AND
                        b.status IN (SELECT Status FROM @ActiveReservationStatuses)
                )
                """;

            var flats = await connection
                .QueryAsync<FlatResponse, AddressResponse, FlatResponse>(
                    sql,
                    (flat, address) =>
                    {
                        flat.Address = address;

                        return flat;
                    },
                    new
                    {
                        Status1 = ActiveReservationStatuses[0],
                        Status2 = ActiveReservationStatuses[1],
                        Status3 = ActiveReservationStatuses[2],
                        request.StartDate,
                        request.EndDate                        
                    },
                    splitOn: "Country");

            return flats.ToList();
        }
    }
}
