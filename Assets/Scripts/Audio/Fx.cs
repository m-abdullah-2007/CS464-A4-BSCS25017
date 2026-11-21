using UnityEngine;

public static class Fx
{
    static GameObject burst;

    // one call for a pickup: sound + a self-destroying particle burst
    public static void Pickup(Vector3 position)
    {
        SfxPlayer.Do(Sfx.Pickup);
        if (burst == null) burst = Resources.Load<GameObject>("PickupBurst");
        if (burst != null) Object.Instantiate(burst, position, Quaternion.identity);
    }
}
