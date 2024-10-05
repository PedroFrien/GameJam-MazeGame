using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;

    [Range(0f, 10f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [Range(0.1f, 3f)] // You can adjust this range as needed
    public float speed = 1f; // Default speed set to 1 (normal speed)

    [HideInInspector]
    public AudioSource source;

    // Dictionary to track which GameObjects are currently playing this sound
    [HideInInspector]
    public HashSet<GameObject> playingObjects = new HashSet<GameObject>();
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Method to play sound from a GameObject
    public void PlaySound(string name, Vector3 position, GameObject playingObject)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Check if the sound is already playing from this GameObject
        if (s.playingObjects.Contains(playingObject))
        {
            return; // Exit if the sound is already playing from this GameObject
        }

        s.source.transform.position = position;

        // Set the playback speed based on the speed variable
        s.source.pitch = s.speed;

        s.source.Play();
        s.playingObjects.Add(playingObject); // Mark the sound as playing from this GameObject

        // Reset the playing state when the sound finishes
        StartCoroutine(ResetPlayingState(s, playingObject));
    }

    private IEnumerator ResetPlayingState(Sound s, GameObject playingObject)
    {
        // Wait until the clip finishes playing
        yield return new WaitForSeconds(s.clip.length / s.speed);
        s.playingObjects.Remove(playingObject); // Reset the playing state for this GameObject
    }
}
