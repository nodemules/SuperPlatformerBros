using UnityEngine;

namespace System
{
    public class CameraSystem : MonoBehaviour
    {
        public GameObject Backdrop;
        private Player.Player _player;

        private Vector2 _originalMinVector;
        private Vector2 _originalMaxVector;

        public bool EnableCameraBoundaries;

        public Vector2 MinVector;
        public Vector2 MaxVector;

        private Transform _parentTransform;

        public void Start()
        {
            _player = transform.parent.gameObject.GetComponentInChildren<Player.Player>();
            _parentTransform = GetComponentInParent<Transform>();

            SetCameraFromBackdrop();

            if (MinVector.Equals(new Vector2()) || MaxVector.Equals(new Vector2()))
            {
                SetDefaults();
            }

            _originalMinVector = MinVector;
            _originalMaxVector = MaxVector;
        }

        public void LateUpdate()
        {
            if (!EnableCameraBoundaries)
            {
                return;
            }

            if (_player == null)
            {
                CameraSystemDebug.PrintPlayerNotFound();
                return;
            }

            Vector3 playerPos = _player.transform.position;
            float x = Mathf.Clamp(playerPos.x, MinVector.x, MaxVector.x);
            float y = Mathf.Clamp(playerPos.y, MinVector.y, MaxVector.y);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        private void SetCameraFromBackdrop()
        {
            if (_parentTransform == null)
            {
                CameraSystemDebug.PrintParentTransformNotFound();
                return;
            }

            if (Backdrop == null)
            {
                CameraSystemDebug.PrintBackdropNotAttached();
                return;
            }

            Vector3 pPos = _parentTransform.transform.position;
            Vector3 pLocPos = _parentTransform.transform.localPosition;

            Vector3 bdPos = Backdrop.transform.position;
            Vector3 bdLocPos = Backdrop.transform.localPosition;

            float parentWidth = bdPos.y - pLocPos.y;

            MinVector = new Vector2(pPos.x - parentWidth, pPos.y);
            MaxVector = new Vector2(pPos.x, MinVector.y + 2 * bdLocPos.y);
        }

        public void SetDefaults()
        {
            if (_player == null)
            {
                CameraSystemDebug.PrintPlayerNotFound();
                return;
            }

            Vector3 playerPos = _player.transform.position;
            MinVector = new Vector2(-50 + playerPos.x, -10 + playerPos.y);
            MaxVector = new Vector2(50 + playerPos.x, 50 + playerPos.y);
        }

        public void ResetCamera()
        {
            MinVector = _originalMinVector;
            MaxVector = _originalMaxVector;
        }

        private static class CameraSystemDebug
        {
            public static void PrintPlayerNotFound()
            {
                Debug.LogError("Cannot clamp Camera boundary, no <Player> was found in the scene.");
                PrintHierarchySuggestion("Player");
            }

            public static void PrintBackdropNotAttached()
            {
                print(
                    "No Backdrop was attached to the <CameraSystem>, unable to set camera bounds");
            }

            private static void PrintHierarchySuggestion(string missing)
            {
                string message =
                    "Ensure the <CameraSystem> Component is attached to a GameObject<Camera> " +
                    "that is a direct descendant of the LevelContainer";

                if (missing != null)
                {
                    string sibling = " and that a GameObject<" + missing +
                                     "> is in the Scene as a child of LevelContainer";
                    message += sibling;
                }

                print(message);
            }

            public static void PrintParentTransformNotFound()
            {
                Debug.LogError(
                    "An error occurred getting the parent container's transform, unable to set camera bounds");
            }
        }
    }
}