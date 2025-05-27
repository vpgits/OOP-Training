namespace OOPTraining.Encapsulation.Models.Bad;

public class BadPizza
{
    
    public string size;                    
    public List<string> toppings;          
    public double price;                   
    public string customerName;            

    public BadPizza()
    {
        toppings = new List<string>();     
        size = "";                         
        price = 0;                         
        customerName = "";
    }

    
    public void AddTopping(string topping)
    {
        toppings.Add(topping);             
    }

    
    public List<string> GetToppings()
    {
        return toppings;                   
    }

    
    public void CalculatePrice()
    {
        if (size == "small") price = 8;    
        if (size == "medium") price = 10;  
        if (size == "large") price = 12;   

        price += toppings.Count * 1.5;     
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Pizza for {customerName}: {size} with {toppings.Count} toppings - ${price}");
    }
}