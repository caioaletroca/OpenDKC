using UnityEngine;

/// <summary>
/// Defines extensions methods for <see cref="LayerMask"/>
/// </summary>
public static class LayerMaskExtensions
{
    /// <summary>
    /// Checkes if a <see cref="GameObject"/> is in the layer
    /// </summary>
    /// <param name="layers"></param>
    /// <param name="gameObject">The game object to check</param>
    /// <returns></returns>
    public static bool Contains(this LayerMask layers, GameObject gameObject) => 0 != (layers.value & (1 << gameObject.layer));
}
