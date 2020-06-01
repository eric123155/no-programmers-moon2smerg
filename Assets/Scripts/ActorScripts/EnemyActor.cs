using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    [SerializeField] private GameStage deadPlayerStage = null;

    public bool canKill = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (deadPlayerStage && canKill && other.gameObject.tag == "Player")
            deadPlayerStage.Invoke();
    }
}
