using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(FadeTransition))]
public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI contentText;

    public bool IsActive { get; private set; } = false;

    private static DialogueManager _instance = null;
    public static DialogueManager Instance { get { if (!_instance) _instance = FindObjectOfType<DialogueManager>(); return _instance; } }

    public Dialogue debugDialogue;
    [SerializeField] private List<Dialogue> _dialogues = new List<Dialogue>();

    private AudioSource _audioSource;
    private FadeTransition _fadeTransition;
    private Coroutine _displayCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _fadeTransition = GetComponent<FadeTransition>();

        _fadeTransition.onUpdate = false;
        GetComponent<CanvasGroup>().alpha = 0;
    }

    private void Start() 
    {
        Display(debugDialogue);
        contentText.text = "";
    }
    
    public void Display(Dialogue dialogue) 
    {
        if (!contentText)
            return;

        _dialogues.Add(dialogue);

        if (_displayCoroutine == null)
            _displayCoroutine = StartCoroutine(DisplayDialogues());
    }

    public void Stop() 
    {
        if (_displayCoroutine != null) 
        {
            StopCoroutine(_displayCoroutine);
            _fadeTransition.targetAlpha = 0;
            _fadeTransition.Fade(2f);
        }
    }

    public IEnumerator DisplayDialogues()
    {
        _fadeTransition.targetAlpha = 1;
        yield return StartCoroutine(_fadeTransition.Transition(2f));

        while (_dialogues.Count > 0) 
        {
            if (_dialogues[0])
                yield return StartCoroutine(_dialogues[0].DialougeAnimation(this, _audioSource, contentText));
            _dialogues.RemoveAt(0);
        }

        _fadeTransition.targetAlpha = 0;
        _fadeTransition.Fade(2f);
    }
}
