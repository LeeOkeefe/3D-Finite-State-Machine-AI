using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ConsoleWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        ConsoleMessage("Dear agent, " +
                       $"{Environment.NewLine}" +
                       $"{Environment.NewLine}" +
                       "Locate the two parts of information and leave without being detected. " +
                       "I'd suggest searching the warden's office, also the mortuary downstairs." +
                       $"{Environment.NewLine}" +
                       $"{Environment.NewLine}" +
                       "P.S. You'll have to leave the same way you came in.");
    }

    public void ConsoleMessage(string message)
    {
        StartCoroutine(nameof(TypeWriterEffect), message);
    }

    private IEnumerator TypeWriterEffect(string message)
    {
        var i = 0;
        text.text = string.Empty;

        while (i < message.Length)
        {
            text.text += message[i++];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
