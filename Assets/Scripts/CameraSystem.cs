using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraSystem : MonoBehaviour
    {

        private GameObject _player;
        public Vector2 MinVector;
        public Vector2 MaxVector;

        public void Start () {
		    _player = GameObject.FindGameObjectWithTag("Player");
        }
	
        public void LateUpdate ()
        {
            float x = Mathf.Clamp(_player.transform.position.x, MinVector.x, MaxVector.x);
            float y = Mathf.Clamp(_player.transform.position.y, MinVector.y, MaxVector.y);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
    }
}
