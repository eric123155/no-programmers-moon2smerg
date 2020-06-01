using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BookQuestionManager : MonoBehaviour
{
    public Button buttonQuestionTemplate;
    [Space]
    public TMPro.TextMeshProUGUI bookIndexText;
    public TMPro.TextMeshProUGUI questionContentText;
    public Transform layout;
    [Space]
    public UnityEvent onShowUI = new UnityEvent();
    public UnityEvent onHideUI = new UnityEvent();

    public UnityEvent onSucceeded = new UnityEvent();
    public UnityEvent onFailed = new UnityEvent();

    public bool IsActive { get; private set; } = false;

    private static BookQuestionManager _instance = null;
    public static BookQuestionManager Instance { get { if (!_instance) _instance = FindObjectOfType<BookQuestionManager>(); return _instance; } }

    private void OnDisable() 
    {
        RemoveButtons();
    }

    public void DisplayBookQuestion(BookQuestion bookQuestion)
    {
        if (IsActive)
            return;

        RemoveButtons();
        onShowUI?.Invoke();
        IsActive = !IsActive;
        questionContentText.text = bookQuestion.question;
        bookIndexText.text = "Book " + (BookManager.Instance.BooksAnswered + 1) + " of " + "#";
        SetButtons(bookQuestion);
    }

    public void HideUI() 
    {
        if (!IsActive)
            return;

        onHideUI?.Invoke();
        IsActive = !IsActive;
    }

    public void RemoveButtons() 
    {
        foreach (Transform buttonTransform in layout) 
        {
            buttonTransform.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(buttonTransform.gameObject);
        }
    }

    public void SetButtons(BookQuestion bookQuestion) 
    {
        List<BookAnswer> bundledAnswers = bookQuestion.BundledAnswers;
        List<Button> buttons = new List<Button>();

        if (bundledAnswers.Count == 0)
            return;

        for (int i = 0; i < bundledAnswers.Count; i++) 
            buttons.Add(Instantiate(buttonQuestionTemplate, layout).GetComponent<Button>());

        for (int i = 0; i < buttons.Count; i++) 
        {
            int randomIndex = Random.Range(0, bundledAnswers.Count);
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = bundledAnswers[randomIndex].content;

            if (bundledAnswers[randomIndex] == bookQuestion.correctAnswer)
                buttons[i].onClick.AddListener(OnCorrect);
            else buttons[i].onClick.AddListener(OnIncorrect);
            buttons[i].onClick.AddListener(HideUI);

            bundledAnswers.RemoveAt(randomIndex);
        }
    }

    public void OnCorrect() 
    {
        BookManager.Instance.AnsweredCorrectly();
        BookManager.Instance.SpawnBook();
        onSucceeded?.Invoke();
    }
    public void OnIncorrect() 
    {
        ChatOverlay.Instance.ShowMessage();
        BookManager.Instance.SpawnBook();
        onFailed?.Invoke();
    }
}
