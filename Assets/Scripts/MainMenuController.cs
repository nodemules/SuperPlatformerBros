using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LevelLoader.ChangeLevel(LevelLoader.FirstLevel);
        }
    }
}