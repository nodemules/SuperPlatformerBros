using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Environment
{
	public class FakeWinBlock : Block
	{

		private GameObject _sorryText;
		private float _startTime;
		private AudioSource _audioSource;
		
		public AudioClip BlockHitAudioClip;
		

		private void Start()
		{
			_sorryText = GameObject.Find("SorryFakeBlockText");
			_audioSource = GetComponent<AudioSource>();
		}

		private new void OnCollisionEnter2D(Collision2D other)
		{
			Player.Player player = other.collider.GetComponent<Player.Player>();
			if (player != null)
			{
				_audioSource.pitch = 1;
				_audioSource.PlayOneShot(BlockHitAudioClip);
				_sorryText.SetActive(true);
				Text text = _sorryText.GetComponent<Text>();
				text.enabled = true;
				Invoke("disableText", 2);
			}
		}

		private void disableText()
		{
			_audioSource.pitch = 0.7f;
			_audioSource.PlayOneShot(BlockHitAudioClip);
			_sorryText.SetActive(false);
		}
	}
}
