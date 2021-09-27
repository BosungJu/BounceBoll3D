
using UnityEngine;

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
