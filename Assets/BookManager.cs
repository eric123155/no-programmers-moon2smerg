using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BookManager : MonoBehaviour
{
    public GameObject bookPrefab = null;
    public BookQuestionDatabase questionDatabase;

    private static BookManager _instance = null;
    public static BookManager Instance { get { if (!_instance) _instance = FindObjectOfType<BookManager>(); return _instance; } }

    public int BooksAnswered { get; private set; } = 0;

    private List<BookQuestion> _unansweredQuestions = new List<BookQuestion>();
    private List<Transform> _spawnLocations = new List<Transform>();
    private Transform _currentSpawnLocation = null;
    private GameObject _currentSpawnedBook = null;
    private BookQuestion _currentQuestion = null;
    private int _count;

    private void Awake()
    {
        _unansweredQuestions.AddRange(questionDatabase.bookQuestions);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("BookSpawn").ToArray();
        for (int i = 0; i < objs.Length; i++)
            _spawnLocations.Add(objs[i].transform);

        for (int i = 0; i < _spawnLocations.Count; i++)
            _spawnLocations[i].gameObject.SetActive(false);
    }

    public void SpawnBook()
    {

        if (!bookPrefab || _spawnLocations.Count == 0 || _unansweredQuestions.Count == 0)
            return;
        if (_currentSpawnedBook && _currentSpawnedBook.GetComponent<BookWorldQuestion>().IsActive)
            return;

        EelAI eelAI = FindObjectOfType<EelAI>();
        if (_count == 0) 
        {
            eelAI.state = SimpleAI.State.Wander;
            eelAI.GetComponent<EnemyActor>().canKill = true;
            DialogueManager.Instance.Stop();
        }

        Destroy(_currentSpawnedBook);

        int randomQuestionIndex = Random.Range(0, _unansweredQuestions.Count - 1);
        _currentQuestion = _unansweredQuestions[randomQuestionIndex];

        _currentSpawnedBook = Instantiate(bookPrefab);
        _currentSpawnedBook.GetComponent<BookWorldQuestion>().bookQuestion = _currentQuestion;

        List<Transform> locationsWithoutPrevious = new List<Transform>();
        locationsWithoutPrevious.AddRange(_spawnLocations);
        if (_currentSpawnLocation)
            locationsWithoutPrevious.Remove(_currentSpawnLocation);

        int randomLocationIndex = Random.Range(0, locationsWithoutPrevious.Count - 1);

        _currentSpawnLocation = locationsWithoutPrevious[randomLocationIndex];
        _currentSpawnedBook.transform.position = new Vector3(_currentSpawnLocation.position.x, bookPrefab.transform.position.y, _currentSpawnLocation.position.z);
    }

    public void AnsweredCorrectly() 
    {
        _unansweredQuestions.Remove(_currentQuestion);
        BooksAnswered++;
        if (_unansweredQuestions.Count == 0)
            Debug.Log("OUT OF QUESTIONS");
    }
}
