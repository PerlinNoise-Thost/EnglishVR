using UnityEngine;
using UnityEngine.UI;

public class test_k1 : MonoBehaviour
{
    public BaiDu_STT BDSTT;
    public Text _text;

    public void OnClick_1()
    {
        StartCoroutine(BDSTT.StartSTT((tempString) => _text.text = tempString));
    }

    public void Onclick_2()
    {
        BDSTT.EndSTT();
    }
}