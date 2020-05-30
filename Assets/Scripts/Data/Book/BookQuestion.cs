using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Question", menuName = "Book Data/Book Question")]
public class BookQuestion : ScriptableObject
{
    [TextArea] public string question;
}
