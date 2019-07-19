using UnityEngine;

public partial class KongController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            Hook = true;
            HookGameObject = collision.gameObject;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            Hook = false;
            HookGameObject = null;
        }
    }
}
