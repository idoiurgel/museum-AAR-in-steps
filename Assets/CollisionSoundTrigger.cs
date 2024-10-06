using UnityEngine;
using System.Collections;

public class CollisionSoundTrigger : MonoBehaviour
{
    public AudioSource paintingSoundSource;       // The AudioSource for the painting sound
    public AudioSource ambientSoundSource;        // Reference to the centralized ambient sound AudioSource
    public float fadeDuration = 1.0f;             // Duration for fading in/out sounds
    private bool hasPlayed = false;               // To track if the sound has played

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;  // Prevent multiple triggers
            paintingSoundSource.Play();  // Play the painting sound

            // Fade out the ambient sound
            StartCoroutine(FadeOutAudio(ambientSoundSource));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasPlayed)
        {
            hasPlayed = false;  // Reset for future triggers

            // Fade in the ambient sound
            StartCoroutine(FadeInAudio(ambientSoundSource));

            // Fade out the painting sound
            StartCoroutine(FadeOutAudio(paintingSoundSource));
        }
    }

    // Coroutine to fade out an AudioSource
    private IEnumerator FadeOutAudio(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;  // Reset volume for future fades
    }

    // Coroutine to fade in an AudioSource
    private IEnumerator FadeInAudio(AudioSource audioSource)
    {
        float targetVolume = audioSource.volume;
        audioSource.Play();
        audioSource.volume = 0f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
