using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hook : MonoBehaviour
{
    #region Public Properties

    public BoxCollider2D Collider;

    public LayerMask ActivatorLayer;

    #endregion

    #region Unity Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ActivatorLayer.Contains(collision.gameObject))
        {
            Collider.size = new Vector2(Collider.size.x, Collider.size.y * 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ActivatorLayer.Contains(collision.gameObject))
        {
            
            
        }
    }

    #endregion
}
