using System;
using Interfaces;
using PlayerCharacter;
using UnityEngine;

namespace TriggerArea
{
    public class CameraBoundary : MonoBehaviour, IBoundary
    {
        private GameObject _camera;

        public bool IsObstacle
        {
            get { return false; }
        }

        private void Start()
        {
            _camera = GameObject.Find("MainCamera");
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Collider2D localCollider2D = gameObject.GetComponent<Collider2D>();
                CameraSystem cameraSystem = _camera.GetComponent<CameraSystem>();

                cameraSystem.MinVector = localCollider2D.bounds.min;
                cameraSystem.MaxVector = localCollider2D.bounds.max;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                CameraSystem cameraSystem = _camera.GetComponent<CameraSystem>();
                cameraSystem.ResetCamera();
            }
        }
    }
}