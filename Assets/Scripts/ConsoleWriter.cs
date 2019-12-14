using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ConsoleWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        ConsoleMessage($"Dear agent, " +
                       $"{Environment.NewLine}" +
                       $"{Environment.NewLine}" +
                       $"Find the two bits of information and get out without being seen. " +
                       $"It's worth searching the warden's office and mortuary." +
                       $"{Environment.NewLine}" +
                       $"{Environment.NewLine}" +
                       $"P.S. You'll have to leave the same way you came in.");
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
