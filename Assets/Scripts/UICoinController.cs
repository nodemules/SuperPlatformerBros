using UnityEngine;
using UnityEngine.UI;

public class UICoinController : MonoBehaviour
{
    private Text _coinText;
    private GameObject _coinIcon;
    private static int _coinCount;
   
    public void Start()
    {
        _coinText = GetComponentInChildren<Text>();
        _coinIcon = GameObject.Find("UICoinIcon");
    }

    private void Update()
    {     
        _coinText.text = "x " + _coinCount;
        _coinIcon.transform.Rotate(new Vector2(0.0f, 45) * Time.deltaTime);
    }

    public static void CollectCoin()
    {
        _coinCount++;
    }
}