using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [TextArea] public string content;
    public float timeWaitPerLetter;
    public float timeWaitAfterFinished;
    public AudioClip clipPerLetter;

    public IEnumerator DialougeAnimation(DialogueManager dialogueManager, AudioSource audioSource, TMPro.TextMeshProUGUI text)
    {
        audioSource.clip = clipPerLetter;
        text.maxVisibleCharacters = 0;
        text.text = content;
        for (int i = 0; i < text.text.Length; i++)
        {
            yield return dialogueManager.StartCoroutine(YieldPause());
            audioSource.pitch = Random.Range(1.75f, 1.95f);
            audioSource.volume = Random.Range(0.15f, 0.25f);
            text.maxVisibleCharacters++;
            if (text.text[i] == ' ' || text.text[i] == ',' || text.text[i] == '.' || text.text[i] == ':') 
            {
                audioSource.Stop();
                if (text.text[i] == ',' || text.text[i] == '.')
                    yield return dialogueManager.StartCoroutine(YieldPause(timeWaitPerLetter * 2));
                if (text.text[i] == ':')
                    yield return dialogueManager.StartCoroutine(YieldPause(timeWaitPerLetter * 4));
            }
            else audioSource.Play();
        }
        audioSource.Stop();
        yield return dialogueManager.StartCoroutine(YieldPause(timeWaitAfterFinished));
    }

    private IEnumerator YieldPause()
    {
        yield return new WaitForSeconds(timeWaitPerLetter);
    }
    private IEnumerator YieldPause(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
