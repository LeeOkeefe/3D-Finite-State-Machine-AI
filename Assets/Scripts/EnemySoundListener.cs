using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundListener : MonoBehaviour
{
    public void TriggerSound(float volumeScale)
    {
        Debug.Log($"I heard something at volume {volumeScale}!");
    }
}
