using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") //collision.CompareTag("Player")
        {
            if (PanelManager.pManager.panelOpenStatus != true)
            {
                PanelManager.pManager.OpenPanel(gameObject);
            }
        }
    }
}
