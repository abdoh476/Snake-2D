using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour {

    public static Action<string> hit;
    snake next;
    #region setter and getter for variable next
    public void setNext(snake here)
    {
        next = here;
    }
    public snake getNext()
    {
        return next;
    }
    #endregion

    public void removeTail()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hit!=null)
        {
            hit(other.tag);//send tag through Action to GM
        }
        if (other.tag=="Food")
        {
            Destroy(other.gameObject);
        }
    }
}
