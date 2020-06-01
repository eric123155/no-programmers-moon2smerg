using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Answer Database", menuName = "Book Data/Databases/Book Answer Database")]
public class BookAnswerDatabase : ScriptableObject
{
    public List<BookAnswer> bookAnswers = new List<BookAnswer>();
}
