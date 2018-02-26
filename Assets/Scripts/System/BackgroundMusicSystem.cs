using UnityEngine;

namespace System
{
    public class BackgroundMusicSystem : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip BackgroundMusicAudioClip;
        private bool _isPlaying;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            Invoke("StartBackgroundMusic", 0.5f);
        }

        public void StartBackgroundMusic()
        {

            if (BackgroundMusicAudioClip == null || _isPlaying )
            {
                return;
            }

            _isPlaying = true;
            _audioSource.clip = BackgroundMusicAudioClip;
            _audioSource.Play();
        }
        
        public void StopBackgroundMusic()
        {
            _audioSource.Stop();
            _isPlaying = false;
        }

    }
}