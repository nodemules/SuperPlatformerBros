using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UICoinController : MonoBehaviour
{
    private Text _coinText;
    private static int _coinCount;

    public void Start()
    {
        _coinText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        _coinText.text = "x " + _coinCount;
    }

    public static void CollectCoin()
    {
        _coinCount++;
    }
}