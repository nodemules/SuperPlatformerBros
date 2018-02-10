using System.Linq.Expressions;
using Environment;
using UnityEngine;
using UnityEngine.UI;

public class UICoinController : MonoBehaviour
{
    private static int _defaultFontSize = 18;
    private Text _coinText;
    private GameObject _coinIcon;
    private int _oldCoinCount;
    private static int _coinCount;

    public void Start()
    {
        _coinText = GetComponentInChildren<Text>();
        _coinIcon = GameObject.Find("UICoinIcon");
        _defaultFontSize = _coinText.fontSize;
    }

    private void Update()
    {
        if (_oldCoinCount < _coinCount)
        {
            CollectCoinEffect();
        }

        _oldCoinCount = _coinCount;
        _coinText.text = "x " + _coinCount;
        _coinIcon.transform.Rotate(new Vector2(0.0f, 45) * Time.deltaTime);
    }

    public static void CollectCoin()
    {
        _coinCount++;
    }

    private void CollectCoinEffect()
    {
        _coinText.fontSize = 24;
        Invoke("ResetFontSize", 1);
    }

    private void ResetFontSize()
    {
        _coinText.fontSize = _defaultFontSize;
    }

}