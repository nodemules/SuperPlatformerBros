using UnityEngine;

namespace System
{
    public class BackgroundMusicSystem : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip BackgroundMusicAudioClip;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = BackgroundMusicAudioClip;
            Invoke("StartBackgroundMusic", 0.5f);
        }

        public void StartBackgroundMusic()
        {
            _audioSource.Play();
        }
        
        public void StopBackgroundMusic()
        {
            _audioSource.Stop();
        }

    }
}