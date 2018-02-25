using UnityEngine;

namespace System
{
    public class CameraSystem : MonoBehaviour
    {
        public GameObject Backdrop;
        private GameObject _player;

        private Vector2 _originalMinVector;
        private Vector2 _originalMaxVector;

        public Vector2 MinVector;
        public Vector2 MaxVector;
        private Transform _parentTransform;

        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            _parentTransform = gameObject.GetComponentInParent<Transform>();

            SetCameraFromBackdrop();

            _originalMinVector = MinVector;
            _originalMaxVector = MaxVector;
        }

        public void LateUpdate()
        {
            float x = Mathf.Clamp(_player.transform.position.x, MinVector.x, MaxVector.x);
            float y = Mathf.Clamp(_player.transform.position.y, MinVector.y, MaxVector.y);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        public void SetCameraFromBackdrop()
        {
            if (_parentTransform == null)
            {
                Debug.LogError(
                    "An error occurred getting the parent container's transform, unable to set camera bounds");
                return;
            }

            if (Backdrop == null)
            {
                Debug.LogError("No backdrop was set, unable to set camera bounds");
                return;
            }

            Vector3 parentPosition = _parentTransform.transform.position;
            Vector3 parentLocalPosition = _parentTransform.transform.localPosition;

            Vector3 backdropPosition = Backdrop.transform.position;
            Vector3 backdropLocalPosition = Backdrop.transform.localPosition;

            float parentWidth = backdropPosition.y - parentLocalPosition.y;

            MinVector = new Vector2(parentPosition.x - parentWidth, parentPosition.y);
            MaxVector = new Vector2(parentPosition.x, MinVector.y + 2 * backdropLocalPosition.y);
        }

        public void ResetCamera()
        {
            MinVector = _originalMinVector;
            MaxVector = _originalMaxVector;
        }
    }
}