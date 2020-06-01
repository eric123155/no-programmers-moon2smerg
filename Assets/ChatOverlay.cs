using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChatOverlay : MonoBehaviour
{
    public Transform chatMessageTemplate;

    public bool IsActive { get; private set; } = false;

    private static ChatOverlay _instance = null;
    public static ChatOverlay Instance { get { if (!_instance) _instance = FindObjectOfType<ChatOverlay>(); return _instance; } }

    public void ShowMessage() 
    {
        StartCoroutine(DisplayMessage());
    }

    private IEnumerator DisplayMessage() 
    {
        Transform chatMessage = Instantiate(chatMessageTemplate, transform);
        FadeTransition fadeTransition = chatMessage.GetComponent<FadeTransition>();
        fadeTransition.SetTargetAlpha(1);
        chatMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        fadeTransition.SetTargetAlpha(0);
        yield return new WaitForSeconds(5);
        Destroy(chatMessage.gameObject);
    }
}
