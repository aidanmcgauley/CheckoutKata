using CheckoutKata.Interfaces;
using CheckoutKata.Pricing;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public PricingRules _pricingRules;
    public Dictionary<string, int> _itemCounts = new Dictionary<string, int>();

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
        throw new NotImplementedException();
    }
}