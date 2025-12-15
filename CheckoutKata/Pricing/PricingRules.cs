namespace CheckoutKata.Pricing;

public class PricingRules
{
    private readonly Dictionary<string, PricingRule> _rules;

    public PricingRules()
    {
        _rules = new Dictionary<string, PricingRule>();
    }

    public void AddRule(PricingRule rule)
    {
        _rules.Add(rule.Sku, rule);
    }

    public PricingRule? GetRule(string sku)
    {
        return _rules.GetValueOrDefault(sku, null);
    }
}