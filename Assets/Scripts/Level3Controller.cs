using UnityEngine;

public class Level3Controller : MonoBehaviour
{

    public void Update()
    {
        Physics.gravity = new Vector3(0.0f, 10.0f, 0.0f);
    }
}