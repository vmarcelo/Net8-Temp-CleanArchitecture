namespace FlatFinder.Application.Flats.SearchFlats
{
    public sealed class FlatResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public decimal Price { get; init; }

        public string Currency { get; init; } = default!;

        public AddressResponse Address { get; set; } = default!;
    }
}
