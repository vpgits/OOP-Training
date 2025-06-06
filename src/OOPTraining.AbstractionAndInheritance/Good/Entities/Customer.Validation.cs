namespace OOPTraining.AbstractionAndInheritance.Good.Entities;

public partial class Customer
{
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) &&
               !string.IsNullOrWhiteSpace(Email) &&
               !string.IsNullOrWhiteSpace(Phone);
    }
}