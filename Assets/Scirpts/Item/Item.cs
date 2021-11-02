
using UnityEngine;

public enum ItemType
{
    VerticalJump = 0,
    HorizontalJump = 1,
    VerticalTeleport = 2,
    HorizontalTeleport = 3

}

public class Item : MonoBehaviour
{
    public void ActiveCallBack(Player player)
    {
        Active(player);
    }
    
    protected virtual void Active(Player player) 
    {
        throw new System.NotImplementedException();
    }

}
