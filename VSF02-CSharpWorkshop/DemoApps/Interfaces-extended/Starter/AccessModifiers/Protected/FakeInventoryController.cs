namespace AccessModifiers.Protected;

public class FakeInventoryController : IInventoryController
{
    private Dictionary<int, int> inventory = new Dictionary<int, int>();

    // This is a public interface member that is implemented
    // explicitly. This item can be explicit or non-explicit.
    void IInventoryController.PushInventoryItem(InventoryItem item)
    {
        if (inventory.ContainsKey(item.Id))
            inventory[item.Id] += 1;
        else
            inventory.Add(item.Id, 1);
    }

    // Protected interface members can be implemented explicitly.
    // Non-explicit implementation as "public" is also allowed.
    InventoryItem IInventoryController.PullInventoryItem(int id)
    {
        if (inventory.ContainsKey(id) && inventory[id] > 0)
        {
            inventory[id] -= 1;
            return new InventoryItem(id);
        }
        else
        {
            throw new Exception("Item not in inventory");
        }
    }
}
