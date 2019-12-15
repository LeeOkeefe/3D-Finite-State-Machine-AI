using UnityEngine;
using UnityEngine.UI;

public class LevelSuccess : MonoBehaviour
{
    private Text text;
    private Image image;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();

        var value = PlayerPrefs.GetInt("LevelComplete");

        if (value == 0)
        {
            text.text = "Level Failed";
            image.color = Color.red;
        }
        else
        {
            text.text = "Level Complete";
            image.color = Color.green;
        }
    }
}
