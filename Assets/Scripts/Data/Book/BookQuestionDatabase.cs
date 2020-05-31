using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Question Database", menuName ="Book Data/Databases/Book Question Database")]
public class BookQuestionDatabase : ScriptableObject
{
    public List<BookQuestion> bookQuestions = new List<BookQuestion>();
}
