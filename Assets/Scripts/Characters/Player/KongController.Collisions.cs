﻿using UnityEngine;

public partial class KongController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHookTriggerEnter2D(collision);
        OnBarrelTriggerEnter2D(collision);
        OnThrowableTriggerEnter2D(collision);
        OnRopeTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnBarrelTriggerExit2D(collision);
    }
}
