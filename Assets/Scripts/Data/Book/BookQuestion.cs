using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Question", menuName = "Book Data/Book Question")]
public class BookQuestion : ScriptableObject
{
    [TextArea] public string question;

    public BookAnswer correctAnswer;
    public List<BookAnswer> incorrectAnswers = new List<BookAnswer>();

    public List<BookAnswer> BundledAnswers { get { List<BookAnswer> bundle = new List<BookAnswer> { correctAnswer }; bundle.AddRange(incorrectAnswers); return bundle; } }
}
