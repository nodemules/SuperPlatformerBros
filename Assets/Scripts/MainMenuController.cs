using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LevelLoader.ChangeLevel(LevelLoader.FirstLevel);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }
    }
}