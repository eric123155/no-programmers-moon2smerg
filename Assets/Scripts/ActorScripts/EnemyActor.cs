using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    [SerializeField] private GameStage deadPlayerStage = null;

    private void OnTriggerEnter(Collider other) 
    {
        if (deadPlayerStage && other.gameObject.tag == "Player")
            deadPlayerStage.Invoke();
    }
}
