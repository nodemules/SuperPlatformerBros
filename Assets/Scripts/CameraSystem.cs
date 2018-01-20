using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraSystem : MonoBehaviour
    {
        private GameObject _player;

        private Vector2 _originalMinVector;
        private Vector2 _originalMaxVector;

        public Vector2 MinVector;
        public Vector2 MaxVector;

        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            
            _originalMinVector = MinVector;
            _originalMaxVector = MaxVector;
        }

        public void Update()
        {
            
        }

        public void LateUpdate()
        {
            float x = Mathf.Clamp(_player.transform.position.x, MinVector.x, MaxVector.x);
            float y = Mathf.Clamp(_player.transform.position.y, MinVector.y, MaxVector.y);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        public void ResetCamera()
        {
            MinVector = _originalMinVector;
            MaxVector = _originalMaxVector;
        }

//        private void OnCollisionEnter2D(Collision2D other)
//        {
//            GameObject otherGameObject = other.gameObject;
//            print("Colliding with otherGameObject=" + otherGameObject.name);
//        }
//
//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            GameObject otherGameObject = other.gameObject;
//            print("Colliding with Trigger otherGameObject=" + otherGameObject.name);
//            if (otherGameObject.name == "HiddenWinBlockCluster")
//            {
//                MinVector = new Vector2(MinVector.x, MinVector.y + other.offset.y);
//            }
//        }
//
//        private void OnTriggerExit2D(Collider2D other)
//        {
//            GameObject otherGameObject = other.gameObject;
//            print("Exitiing collision with Trigger otherGameObject=" + otherGameObject.name);
//            if (otherGameObject.name == "HiddenWinBlockCluster")
//            {
//                MinVector = new Vector2(_originalMinVector.x, _originalMinVector.y);
//            }
//        }
    }
}