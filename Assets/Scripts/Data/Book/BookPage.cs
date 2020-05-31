using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Page", menuName = "Book Data/Book Page")]
public class BookPage : ScriptableObject
{
    public string pageName = "";
    [TextArea] public string pageContent = "";
}
