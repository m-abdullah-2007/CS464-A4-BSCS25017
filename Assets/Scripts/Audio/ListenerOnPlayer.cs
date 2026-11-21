using UnityEngine;

// Keeps the one AudioListener on the player when the camera is too far away to hear from.
public class ListenerOnPlayer : MonoBehaviour
{
    Transform player;
    void LateUpdate()
    {
        if (player == null) { GameObject p = GameObject.FindWithTag("Player"); if (p) player = p.transform; else return; }
        transform.position = player.position;
    }
}
