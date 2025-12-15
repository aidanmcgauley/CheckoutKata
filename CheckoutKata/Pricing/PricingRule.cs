namespace CheckoutKata.Pricing;

public class PricingRule
{
    public string Sku  { get; set; }
    public int UnitPrice { get; set; }
    public int? SpecialQuantity { get; set; }
    public int? SpecialPrice { get; set; }

    public PricingRule(string sku, int unitPrice)
    {
        Sku = sku;
        UnitPrice = unitPrice;
        SpecialQuantity = null;
        SpecialPrice = null;
    }

    public PricingRule(string sku, int unitPrice, int specialQuantity, int specialPrice)
    {
        Sku = sku;
        UnitPrice = unitPrice;
        SpecialQuantity = specialQuantity;
        SpecialPrice = specialPrice;
    }
}