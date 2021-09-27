using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalJumpItem : Item
{

    protected override void Active(Player player) // ±¸ÇöºÎ
    {
        player.Jump(true);
    }

}
