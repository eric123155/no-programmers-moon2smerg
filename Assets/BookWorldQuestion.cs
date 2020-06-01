using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BookWorldQuestion : MonoBehaviour
{
    public BookQuestion bookQuestion;
    public bool IsActive { get; private set; } = true;

    public void RenderBookQuestion() 
    {
        IsActive = false;
        BookQuestionManager.Instance.DisplayBookQuestion(bookQuestion);
    }
}
