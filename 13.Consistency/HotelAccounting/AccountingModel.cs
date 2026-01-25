using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting;

public class AccountingModel : ModelBase
{
    private double price;
    private int nightsCount;
    private double discount;
    private double total;

    public double Price
    {
        get => price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price must be non-negative");

            price = value;
            total = CalculateTotal(price, nightsCount, discount);
            Notify(nameof(Price));
            Notify(nameof(Total));
        }
    }

    public int NightsCount
    {
        get => nightsCount;
        set
        {
            if (value <= 0)
                throw new ArgumentException("NightsCount must be positive");

            nightsCount = value;
            total = CalculateTotal(price, nightsCount, discount);
            Notify(nameof(NightsCount));
            Notify(nameof(Total));
        }
    }

    public double Discount
    {
        get => discount;
        set
        {
            if (value > 100)
                throw new ArgumentException("Discount cannot exceed 100");

            discount = value;
            total = CalculateTotal(price, nightsCount, discount);
            Notify(nameof(Discount));
            Notify(nameof(Total));
        }
    }

    public double Total
    {
        get => total;
        set
        {
            if (value < 0)
                throw new ArgumentException("Total must be non-negative");

            var expectedTotal = CalculateTotal(price, nightsCount, discount);

            if (nightsCount > 0 && price > 0)
            {
                var newDiscount = 100 * (1 - value / (price * nightsCount));
                if (newDiscount > 100)
                    throw new ArgumentException("Total value results in invalid discount");

                discount = newDiscount;
                total = value;
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
            else
            {
                if (value != 0)
                    throw new ArgumentException("Total must be zero when Price or NightsCount is zero");
                total = value;
                Notify(nameof(Total));
            }
        }
    }

    private double CalculateTotal(double price, int nightsCount, double discount)
    {
        return price * nightsCount * (1 - discount / 100);
    }
}
