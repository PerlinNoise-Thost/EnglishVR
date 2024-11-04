using System.Collections;
using UnityEngine;

public class Controller_UI : Controller_Base,IInitialization
{
    public UI_Screen UIScreen;
    public string DataSetSequence => "UI_All";
    public IEnumerator Data_Set()
    {
        yield return StartCoroutine(UIScreen.IniUI());
    }
}