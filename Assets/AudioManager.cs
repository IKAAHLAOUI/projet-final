using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip walk;
    public AudioClip damage;
    public AudioClip damageEnemy;
    AudioSource source;
    public static AudioManager instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       source= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playWalk()
    {
        source.PlayOneShot(walk);
    }

    public void playDamage()
    {
        source.PlayOneShot(damage);
    }
    public void playDamageEnemy()
    {
        source.PlayOneShot(damageEnemy);
    }
}
