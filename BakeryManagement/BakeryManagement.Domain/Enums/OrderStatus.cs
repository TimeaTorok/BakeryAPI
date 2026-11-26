namespace BakeryManagement.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        InPreparation = 3,
        ReadyForPickup = 4,
        Completed = 5,
        Cancelled = 6
    }
}