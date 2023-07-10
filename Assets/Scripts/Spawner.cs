using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] WordReader wordReader;
    [SerializeField] GameObject wordPrefab;
    [SerializeField] GameObject bkgAnims;
    [SerializeField] float direction = 1.0f;
    [SerializeField] Spawner otherSpawner;
    float maxY;
    float minY;
    float maxSpawnX;
    float minSpawnX;
    float minFont = 50;
    float maxFont = 130;

    private float screenHeight = 1080f;
    private float xSpawnRange = 400;

    [SerializeField] float timer = 0.5f;
    private float ticks = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // set maxY and min Y
        maxY = gameObject.transform.position.y + screenHeight / 2; // this is probably not good, but i want to finish lmfao
        minY = gameObject.transform.position.y - screenHeight / 2;

        minSpawnX = gameObject.transform.position.x - xSpawnRange /2;
        maxSpawnX = gameObject.transform.position.x + xSpawnRange / 2;

        // spawn a couple of objects to get the party started
        for(int i = 0; i < 20; i++)
        {
            spawnWord();
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        ticks += Time.deltaTime;
        if (ticks > timer)
        {
            spawnWord();
            ticks = 0.0f;
        }
    }

    private void spawnWord()
    {
        Transform myTransform = GetComponent<Transform>();

        // get a random height and starting x for the object
        float randHeight = Random.Range(minY, maxY);
        float randXPos = Random.Range(minSpawnX, maxSpawnX);

        GameObject newWord = Instantiate(wordPrefab, new Vector3(randXPos , randHeight , myTransform.position.z) , Quaternion.identity);

        // nest the new object under correct parent
        newWord.transform.parent = bkgAnims.transform;

        // modify parameters of the word
        newWord.GetComponent<TextAnim>().setSpeed(getRandomSpeed());
        newWord.GetComponent<TextAnim>().setDirection(direction);
        newWord.GetComponent<TextAnim>().setKillX(otherSpawner.transform.position.x);
        newWord.GetComponent<TMP_Text>().SetText(wordReader.getRandomWord());
        newWord.GetComponent<TMP_Text>().fontSize = Random.Range(minFont, maxFont);
    }

    private float getRandomSpeed()
    {
        float speed = Random.Range(50.0f, 100.0f);
        return speed;
    }
}
