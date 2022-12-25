using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform target;
    public float timeToKillIfNotSeenEnemy = 5;
     GameObject player;

    bool PlayerIsSeen = false;
    public float range = 10;


    private void Start()
    {
        player = FindObjectOfType<Player_Script>().gameObject;
    }


    private void Update()
    {
        killMeIfIHaventSeenPlayer();
        if (canMove == false)
            return;


        if(toAttack()==true)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        }


    }

    bool toAttack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < range)
        {
            PlayerIsSeen = true;
            return true;
        }
        else
        {
            PlayerIsSeen = false;
            return false;
        }
    }
    bool canMove = true;
    public void die()
    {
        canMove = false;
        AudioManager.instance.playDamageEnemy();

        //animation


        //

        Destroy(gameObject, 0.2f);
    }
    float timeInSec = 0;
    void killMeIfIHaventSeenPlayer()
    {
        if (PlayerIsSeen == false)
        {
            timeInSec = timeInSec + Time.deltaTime;
            if (timeInSec > timeToKillIfNotSeenEnemy)
                Destroy(gameObject);


        }else
        {
            timeInSec = 0;
        }
    }

}
