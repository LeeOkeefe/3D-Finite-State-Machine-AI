using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Guard;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BroadcastSound : MonoBehaviour
{
    private AudioSource mySource;

    private void Awake()
    {
        mySource = GetComponent<AudioSource>();

        Debug.Log("Hello world!");

        BroadcastSoundPlayed();
    }
    
    public void Broadcast(AudioClip clip, float maxDistance)
    {
        mySource.maxDistance = maxDistance;
        mySource.PlayOneShot(clip);
        BroadcastSoundPlayed();
    }

    private void BroadcastSoundPlayed()
    {
        var maxDistance = mySource.maxDistance;

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);

        Debug.Log($"Found {colliders.Length} colliders");

        var listeners = colliders.Where(s => s.GetComponent<GuardSoundListener>() != null);

        foreach (var listener in listeners)
        {
            var distance = Vector3.Magnitude(transform.position - listener.transform.position);

            var volumeBasedOnDistance = 1 - (distance / maxDistance);

            listener.GetComponent<GuardSoundListener>().TriggerSound(volumeBasedOnDistance);
        }
    }
}
