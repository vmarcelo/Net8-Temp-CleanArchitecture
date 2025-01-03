using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Shared;

namespace FlatFinder.Domain.Flats
{
    public sealed class Flat : Entity
    {
        public Flat(
            Guid id,
            Name name,
            Description description,
            Address address,
            Money price,
            Money cleaningFee,
            List<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            Amenities = amenities;
        }

        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public List<Amenity> Amenities { get; set; } = new();
        public DateTime? LastBookedOnUtc { get; internal set; }

        private Flat()
        {

        }
    }
}
