using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TextObject;

    private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = TextObject.GetComponent<TextMeshPro>();
    }


    public void setText(string text)
    {
        this.text.text = text;

    }

}
