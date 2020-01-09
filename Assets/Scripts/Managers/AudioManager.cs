using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource SFXSource;

    public List<AudioClip> LaserClipList = new List<AudioClip>();

    public void PlayLaserSFX()
    {
        int random = Random.Range(0, LaserClipList.Count);
        SFXSource.PlayOneShot(LaserClipList[random]);
    }
}
