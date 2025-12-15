using CheckoutKata.Pricing;

namespace CheckoutKata.Tests;

public class CheckoutTests
{
    private Checkout _checkout;
    
    [SetUp]
    public void Setup()
    {
        var rules = new PricingRules();
        rules.AddRule(new PricingRule("A", 50, 3, 130));
        rules.AddRule(new PricingRule("B", 30, 2, 45));
        rules.AddRule(new PricingRule("C", 20));
        rules.AddRule(new PricingRule("D", 15));
        
        _checkout = new Checkout(rules);
    }

    [Test]
    public void Empty_checkout_returns_zero()
    {
        // Act
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(0));
    }
    
    [Test]
    public void Single_item_A_costs_50()
    {
        // Act
        _checkout.Scan("A");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(50));
    }
    
    [Test]
    public void Multiple_items_no_special_pricing()
    {
        // Act
        _checkout.Scan("C");
        _checkout.Scan("B");
        _checkout.Scan("D");
        _checkout.Scan("A");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(115));
    }
    
    [Test]
    public void Three_As_cost_130()
    {
        // Act
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(130));
    }
    
    [Test]
    public void Four_As_cost_180()
    {
        // Act
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(180));
    }
    
    [Test]
    public void Six_As_cost_260()
    {
        // Act
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(260));
    }
    
    [Test]
    public void Mixed_special_and_non_special_items_are_priced_correctly()
    {
        // Act
        _checkout.Scan("B");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("D");
        _checkout.Scan("C");
        int total = _checkout.GetTotalPrice();
        // Assert
        Assert.That(total, Is.EqualTo(210));
    }
    
    [Test]
    public void Total_is_updated_incrementally_as_items_are_scanned()
    {
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(0));
        _checkout.Scan("B");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(30));
        _checkout.Scan("A");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(80));
        _checkout.Scan("B");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(95));
        _checkout.Scan("D");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(110));
        _checkout.Scan("C");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(130));
        _checkout.Scan("A");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(180));
        _checkout.Scan("A");
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(210));
    }
    
    [Test]
    public void Scanning_unknown_item_throws_exception()
    {
        var ex = Assert.Throws<ArgumentException>(() => _checkout.Scan("Z"));
        Assert.That(ex.Message, Is.EqualTo("Unknown SKU: Z"));
    }
}