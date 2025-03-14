using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Zorg ervoor dat de boot die wegvaart nooit meer terug komt
public class DeleteBoat : MonoBehaviour
{
    public void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}
