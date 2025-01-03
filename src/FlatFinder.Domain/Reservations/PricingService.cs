using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Shared;

namespace FlatFinder.Domain.Reservations
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Flat flat, DateRange period)
        {
            var currency = flat.Price.Currency;
            var priceForPeriod = new Money(flat.Price.Amount * period.LengthInDays, currency);

            decimal percentageUpCharge = 0;

            foreach (var amenity in flat.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.SeaView or Amenity.ForestView => 0.05m,
                    Amenity.WiFi => 0.02m,
                    Amenity.Parking => 0.01m,
                    Amenity.Gym => 0.03m,
                    _ => 0
                };
            }

            var amenitiesUpCharge = Money.Zero(currency);
            if(percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
            }

            var totalPrice = Money.Zero();
            totalPrice += priceForPeriod;
            totalPrice += amenitiesUpCharge;

            if (!flat.CleaningFee.IsZero())
            {
                totalPrice += flat.CleaningFee;
            }

            return new PricingDetails(priceForPeriod,flat.CleaningFee,amenitiesUpCharge,totalPrice);

        }
    }
}
