using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float timeBetweenSpawningOfEnemies = 2;
    public int numberOfLakes = 15;
    public int numberOfDecors = 100;
    public GameObject lake,lakeParent,decorParent;
    List<GameObject> listOfLakes = new List<GameObject>();
    public List<GameObject> listOfDecors = new List<GameObject>();
    public GameObject enemy;
    public GameObject panel;
   public GameObject replayButton;
    public List<string> listOfWords = new List<string>();
    public string choosenWord;
    public TextMeshProUGUI choosenWordText;
    void Start()
    {
        spawnLakes();
        spawnPanels();
        spawnDecors();
        replayButton.SetActive(false);
        int indexrandomWord = Random.Range(0, listOfWords.Count);
        choosenWord = listOfWords[indexrandomWord];
        choosenWordText.text = choosenWord;

    }

    // Update is called once per frame


    bool isSpawning = false;
    void Update()
    {
        if(player.GetComponent<Player_Script>().canmove==false)
            replayButton.SetActive(true);
        if (isSpawning==false)
        StartCoroutine(spawnEnemies());
    }
    IEnumerator spawnEnemies()
    {
        isSpawning = true;
        Vector3 positionOfPlayer = player.transform.position;
        yield return new WaitForSeconds(timeBetweenSpawningOfEnemies);
       GameObject enemyClone= Instantiate(enemy);
       
        Vector3 positionOfEnemy;
        float offestX = Random.Range(-15, 15);
        float offestY = Random.Range(-15, 15);


       
            positionOfEnemy = new Vector3(positionOfPlayer.x + offestX, positionOfPlayer.y + offestY, 0);
        enemyClone.transform.position = positionOfEnemy;
        isSpawning = false;

    }
    int numberOfSpawnedLakes = 0, numberOfSpawnedDecors;
    void spawnLakes()
    {
        while (numberOfSpawnedLakes <= numberOfLakes)
        {
            GameObject lakeClone = Instantiate(lake);
            float posX = Random.Range(-346, 324);
            float posY = Random.Range(-484, 460);
            Vector3 lakePosition = new Vector3(posX, posY);
            lakeClone.transform.position = lakePosition;
            lakeClone.transform.parent = lakeParent.transform;
            listOfLakes.Add(lakeClone);

            numberOfSpawnedLakes++;
        }
    }
    void spawnPanels()
    {
        foreach(GameObject lake in listOfLakes)
        {
            
            GameObject p1 = Instantiate(panel);
            GameObject p2 = Instantiate(panel);
            GameObject p3 = Instantiate(panel);
            GameObject p4 = Instantiate(panel);

            // Vector3 pos = new Vector3((player.transform.position.x - lake.transform.position.x) / 2, (player.transform.position.y - lake.transform.position.y) / 2, 0);
            p1.transform.position = lake.transform.position+new Vector3(30,0,0);
            p2.transform.position = lake.transform.position - new Vector3(30, 0, 0);
            p3.transform.position = lake.transform.position + new Vector3(0, 30, 0);
            p4.transform.position = lake.transform.position - new Vector3(0, 30, 0);

           
            p1.transform.localScale = new Vector3(-1, 1, 1);
           
            
            p3.transform.eulerAngles = new Vector3(0, 0, -90);
           
            p4.transform.eulerAngles = new Vector3(0, 0, 90);


        }
    }
    public void replay()
    {
        SceneManager.LoadScene(2);
    }
    void spawnDecors()
    {
        while (numberOfSpawnedDecors <= numberOfDecors)
        {
            int randIndex = Random.Range(0, listOfDecors.Count);

            GameObject decor = Instantiate(listOfDecors[randIndex]);
            float posX = Random.Range(-346, 324);
            float posY = Random.Range(-484, 460);
            Vector3 decorPos = new Vector3(posX, posY);
            decor.transform.position = decorPos;
            decor.transform.parent = decorParent.transform;


            numberOfSpawnedDecors++;
        }
    }
}
