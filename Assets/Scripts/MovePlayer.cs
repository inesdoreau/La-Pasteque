using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minXPosition;
    [SerializeField] private float maxXPosition;

    private float currentMinXPosition;
    private float currentMaxXPosition;

    [SerializeField] private SpriteRenderer currentFishPreview;
    private int currentFishIndex;
    [SerializeField] private SpriteRenderer nextFishPreview;
    private int nextFishIndex;


    public FishesList[] fishesPrefab;

    [SerializeField] private TextMeshProUGUI scoreText; 
    int currentScore = 0;

    public static bool canSpawnFish = true;

    public static MovePlayer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton for MovePlayer already exist, destroying the old one");
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        nextFishIndex = Random.Range(0, 5);
        LoadNextFish();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && transform.position.x > currentMinXPosition)
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < currentMaxXPosition)
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canSpawnFish)
            {
                canSpawnFish = false;
                InstantiateNextFish(currentFishIndex, currentFishPreview.transform.position);
                LoadNextFish() ;
            }
        }
    }

    public void InstantiateNextFish(int index, Vector3 position)
    {
        GameObject newFish = Instantiate(fishesPrefab[index].prefab);
        Fish newFshScript = newFish.GetComponent<Fish>();
        newFshScript.fishIndex = index;
        newFish.transform.position = position;
    }

    private void LoadNextFish()
    {
        currentFishIndex = nextFishIndex;
        nextFishIndex = Random.Range(0, 5);
        nextFishPreview.sprite = fishesPrefab[nextFishIndex].prefab.GetComponent<SpriteRenderer>().sprite;
        currentFishPreview.sprite = fishesPrefab[currentFishIndex].prefab.GetComponent<SpriteRenderer>().sprite;
        currentFishPreview.transform.localScale = fishesPrefab[currentFishIndex].prefab.transform.localScale;
        CalculatePlayerBound();
    }

    private void CalculatePlayerBound()
    {
        currentMinXPosition = minXPosition + currentFishPreview.bounds.size.x/2;
        currentMaxXPosition = maxXPosition - currentFishPreview.bounds.size.x/2;

        if (transform.position.x < currentMinXPosition)
        {
            transform.position = new Vector3(currentMinXPosition, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > currentMaxXPosition)
        {
            transform.position = new Vector3(currentMaxXPosition, transform.position.y, transform.position.z);
        }
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }
}

[System.Serializable]
public class FishesList
{
    public GameObject prefab;
    public int points;
}
