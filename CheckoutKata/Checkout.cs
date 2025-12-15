using CheckoutKata.Interfaces;
using CheckoutKata.Pricing;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    private PricingRules _pricingRules;
    private Dictionary<string, int> _itemCounts = new Dictionary<string, int>();

    public Checkout(PricingRules pricingRules)
    {
        _pricingRules = pricingRules;
    }
    
    public void Scan(string item)
    {
        _itemCounts[item] = _itemCounts.GetValueOrDefault(item, 0)+1;
    }

    public int GetTotalPrice()
    {
        // totalCost = 0;
        // totalForCurrentItem = 0;
        // Loop through itemcounts
            
            // Get rule for item
            // remainder = item % SpecialQuantity
            // dealCount = itemcount / specialquantity              (google the floor syntax)
            // total for current item = dealCount * SpecialPrice + remainder * unit cost
            // totalForCurrentItem = 0
        // totalCost += totalForCurrentItem
    }
}