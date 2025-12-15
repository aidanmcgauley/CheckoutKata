using CheckoutKata.Interfaces;
using CheckoutKata.Pricing;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    private readonly PricingRules _pricingRules;
    private readonly Dictionary<string, int> _itemCounts = new Dictionary<string, int>();

    public Checkout(PricingRules pricingRules)
    {
        _pricingRules = pricingRules;
    }
    
    public void Scan(string item)
    {
        var rule = _pricingRules.GetRule(item);
        if (rule is null)
        {
            throw new ArgumentException($"Unknown SKU: {item}");
        }
        
        _itemCounts[item] = _itemCounts.GetValueOrDefault(item, 0)+1;
    }

    public int GetTotalPrice()
    {
        int total = 0;
        foreach (var count in _itemCounts)
        {
            var rule = _pricingRules.GetRule(count.Key);
            if (rule.SpecialQuantity.HasValue && rule.SpecialPrice.HasValue)
            {
                int specialCount = count.Value / rule.SpecialQuantity.Value;
                int remainder = count.Value % rule.SpecialQuantity.Value;
                
                total += specialCount * rule.SpecialPrice.Value;
                total += remainder * rule.UnitPrice;
            }
            else
            {
                total += count.Value * rule.UnitPrice;
            }
        }
        return total;
    }
}