using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public int x, y;

    public void Interaction()
    {
        if(GameManager.Instance.grid[x,y].state == PanelState.Vuoto) GameManager.Instance.PlaceMove(x, y, gameObject);
    }
}
