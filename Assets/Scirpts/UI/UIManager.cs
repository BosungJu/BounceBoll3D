using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;

    public Button itemButton;

    private void ChangeItem()
    {

    }

    private void ActiveItem()
    {
        player.item.ActiveCallBack(player);
    }
}
