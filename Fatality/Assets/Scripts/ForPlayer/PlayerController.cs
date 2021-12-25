using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float x, y;
    public float speed;

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(PanelManager.pManager.panelOpenStatus == false)
            transform.Translate(new Vector2(x * Time.deltaTime * speed, y * Time.deltaTime * speed));

    }
}
