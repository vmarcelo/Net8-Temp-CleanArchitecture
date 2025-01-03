using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Flats
{
    public static class FlatErrors
    {
        public static Error NotFound = new("Flat.NotFound", "The flat was not found");
    }
}
