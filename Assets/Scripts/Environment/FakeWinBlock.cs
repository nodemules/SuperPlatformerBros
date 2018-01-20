using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Environment
{
	public class FakeWinBlock : Block
	{

		private GameObject _sorryText;
		private float _startTime;

		private void Start()
		{
			_sorryText = GameObject.Find("SorryFakeBlockText");
		}

		private new void OnCollisionEnter2D(Collision2D other)
		{
			Player.Player player = other.collider.GetComponent<Player.Player>();
			if (player != null)
			{
				_sorryText.SetActive(true);
				Text text = _sorryText.GetComponent<Text>();
				text.enabled = true;
			}
		}
	}
}
