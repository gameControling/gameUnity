using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    public AudioClip[] sounds;
    public SoundsArrays[] soundsArrays;

    private AudioSource audioSource => GetComponent<AudioSource>();

    public void PlaySound(int i, float volume = 1f, bool destrou = false, bool random = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        AudioClip clip = random ? soundsArrays[i].soundsArray[Random.Range(0, soundsArrays[i].soundsArray.Length)] : sounds[i];
        audioSource.pitch = Random.Range(p1, p2);

        if(destrou)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    [System.Serializable] 
    public class SoundsArrays
    {
        public AudioClip[] soundsArray;
    }
}
