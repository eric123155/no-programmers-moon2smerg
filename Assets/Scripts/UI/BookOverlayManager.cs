using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BookOverlayManager : MonoBehaviour
{
    public BookPageDatabase pageDatabase;
    public Button nextButton;
    public Button previousButton;
    [Space]
    public TextMeshProUGUI pageText;
    public TextMeshProUGUI pageContentText;
    [Space]
    public UnityEvent onShowUI = new UnityEvent();
    public UnityEvent onHideUI = new UnityEvent();

    public bool IsActive { get; private set; } = false;

    private static BookOverlayManager _instance = null;
    public static BookOverlayManager Instance { get { if(!_instance) _instance = FindObjectOfType<BookOverlayManager>(); return _instance; } }

    private int _pageIndex = 0;

    private void Awake() 
    {
        if (pageDatabase.bookPages.Count > 0) 
        {
            SetText();
            pageText.text = "Page " + (_pageIndex + 1) + " of " + pageDatabase.bookPages.Count;
        }
    }
    private void OnEnable() 
    {
        if (nextButton)
            nextButton.onClick.AddListener(NextPage);
        if (previousButton)
            previousButton.onClick.AddListener(PreviousPage);
    }
    private void OnDisable() 
    {
        if (nextButton)
            nextButton.onClick.RemoveListener(NextPage);
        if (previousButton)
            previousButton.onClick.RemoveListener(PreviousPage);
    }

    public void ShowUI() 
    {
        if (IsActive)
        return;
        
        onShowUI?.Invoke();
        IsActive = !IsActive;
    }
    public void HideUI() 
    {
        if (!IsActive)
            return;
        
        onHideUI?.Invoke();
        IsActive = !IsActive;
    }

    public void NextPage()
    {
        if (_pageIndex < pageDatabase.bookPages.Count -1) 
        {
            _pageIndex++;
            SetText();
            pageText.text = "Page " + (_pageIndex + 1) + " of " + pageDatabase.bookPages.Count;
        }
    }
    public void PreviousPage() 
    {
        if (_pageIndex > 0)
        {
            _pageIndex--;
            SetText();
            pageText.text = "Page " + (_pageIndex + 1) + " of " + pageDatabase.bookPages.Count;
        }
    }

    private void SetText() 
    {
        pageContentText.text = "<align=center><uppercase>" + pageDatabase.bookPages[_pageIndex].pageName + "\n\n</uppercase><align=left>" + pageDatabase.bookPages[_pageIndex].pageContent;
    }
}
