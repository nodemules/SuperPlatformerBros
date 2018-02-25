using System;
using Interfaces;
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
            Player.Player player = other.GetComponent<Player.Player>();
            if (player != null)
            {
                Collider2D localCollider2D = gameObject.GetComponent<Collider2D>();
                CameraSystem cameraSystem = _camera.GetComponent<CameraSystem>();

                cameraSystem.MinVector.x += localCollider2D.offset.x;
                cameraSystem.MinVector.y += localCollider2D.offset.y;
                cameraSystem.MaxVector.x -= localCollider2D.offset.x;
                cameraSystem.MaxVector.y -= localCollider2D.offset.y;
                
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Player.Player player = other.GetComponent<Player.Player>();
            if (player != null)
            {
                CameraSystem cameraSystem = _camera.GetComponent<CameraSystem>();
                cameraSystem.ResetCamera();
            }
        }
    }
}