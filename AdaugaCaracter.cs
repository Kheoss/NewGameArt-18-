using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaugaCaracter : MonoBehaviour
{
    private BoxCollider2D col;
    public Tastatura tastatura;
    private void Start()
    {
        tastatura = Camera.main.GetComponent<Tastatura>();
        col = GetComponent<BoxCollider2D>();
        Input.simulateMouseWithTouches = true;
    }
    private void FixedUpdate()
    {
       if(Input.GetMouseButtonDown(0))
        {
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (col.OverlapPoint(fingerPos))
            {
               if(this.name.Length == 1)
                {
                    tastatura.AddElement(this.name);
                }
               else if(this.name == "Send")
                {
                    tastatura.SemiSend();
                }
            }
        }
    }

}
