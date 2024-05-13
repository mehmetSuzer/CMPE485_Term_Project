using UnityEngine;

public class WarCryController : MonoBehaviour
{
    public AudioClip warCry;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.transform.position = transform.position;
        audioSource.spatialBlend = 1.0f;
        
        //audioSource.minDistance = 5.0f; // Minimum distance where the sound is audible at full volume
        //audioSource.maxDistance = 30.0f; // Maximum distance where the sound can be heard

        audioSource.clip = warCry;
        audioSource.loop = true;
    }

    void Update()
    {
        if (!audioSource.isPlaying && Input.GetKeyDown(KeyCode.B))
        {
            audioSource.Play();
        }
    }
}
