namespace AccessModifiers.Protected;

public class TestInventoryController : IInventoryController
{
    public void PushInventoryItem(InventoryItem item)
    {
        throw new NotImplementedException();
    }

    // Protected member can be implemented explicitly
    // or as "public"
    InventoryItem IInventoryController.PullInventoryItem(int id)
    {
        throw new NotImplementedException();
    }

    //public InventoryItem PullInventoryItem(int id)
    //{
    //    throw new NotImplementedException();
    //}
}
