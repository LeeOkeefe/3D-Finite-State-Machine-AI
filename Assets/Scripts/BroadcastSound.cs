using System.Linq;
using Guard;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BroadcastSound : MonoBehaviour
{
    private AudioSource m_MySource;

    private void Awake()
    {
        m_MySource = GetComponent<AudioSource>();
    }
    
    public void Broadcast(AudioClip clip, float maxDistance)
    {
        m_MySource.maxDistance = maxDistance;
        m_MySource.PlayOneShot(clip);
        BroadcastSoundPlayed();
    }

    public void Broadcast()
    {
        if (m_MySource.isPlaying)
            return;

        m_MySource.PlayOneShot(m_MySource.clip);
        BroadcastSoundPlayed();
    }

    private void BroadcastSoundPlayed()
    {
        var maxDistance = m_MySource.maxDistance;

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);

        var listeners = colliders.Where(s => s.GetComponent<GuardSoundListener>() != null);

        foreach (var listener in listeners)
        {
            var distance = Vector3.Magnitude(transform.position - listener.transform.position);

            var volumeBasedOnDistance = 1 - (distance / maxDistance);

            listener.GetComponent<GuardSoundListener>().TriggerSound(volumeBasedOnDistance, transform.position);
        }
    }
}
