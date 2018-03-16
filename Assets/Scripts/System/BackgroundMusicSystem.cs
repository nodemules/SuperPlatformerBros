using Interfaces;
using UnityEngine;

namespace System
{
    public class BackgroundMusicSystem : MonoBehaviour, ITriggerable
    {
        private AudioSource _audioSource;
        public AudioClip BackgroundMusicAudioClip;
        private bool _isPlaying;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource.enabled)
            {
                Invoke("StartBackgroundMusic", 0.5f);
            }
        }

        public void StartBackgroundMusic()
        {
            if (BackgroundMusicAudioClip == null || _isPlaying)
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

        public void Trigger()
        {
                foreach (Transform siblingTrack in transform.parent)
                {
                    siblingTrack.GetComponent<BackgroundMusicSystem>().StopBackgroundMusic();
                }

                _audioSource.enabled = true;
                StartBackgroundMusic();
        }
    }
}