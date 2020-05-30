using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Page Database", menuName = "Book Data/Databases/Book Page Database")]
public class BookPageDatabase : ScriptableObject
{
    public List<BookPage> bookPages = new List<BookPage>();
}
