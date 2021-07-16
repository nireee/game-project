using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : Zoomable
{

    public SpriteRenderer Sprite;
    private int closedOrderInLayer;

    private void Start()
    {
        ParentStart();
        closedOrderInLayer = Sprite.sortingOrder;
    }
    protected override bool openPuzzleCheck()
    {
        int orderInLayer = Item.OrderInLayer + 2;
        Sprite.sortingOrder = orderInLayer;
        return true;
    }

    protected override void CloseZoomableChild()
    {
        Sprite.sortingOrder = closedOrderInLayer;
    }
}
