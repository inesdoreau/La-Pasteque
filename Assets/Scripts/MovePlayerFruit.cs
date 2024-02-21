using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePlayerFruit : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float borderOffset = 0.2f;
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    private float currentMinXPosition;
    private float currentMaxXPosition;

    [SerializeField] private SpriteRenderer currentFruitPreview;
    private int currentFruitIndex;
    [SerializeField] private SpriteRenderer nextFruitPreview;
    private int nextFruitIndex;

    public FruitList[] fruitsPrefab;

    public static bool canSpawnFruit = true;

    public static MovePlayerFruit Instance { get; private set; }
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
        nextFruitIndex = Random.Range(0, 4);
        LoadNextFruit();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > currentMinXPosition)
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < currentMaxXPosition)
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSpawnFruit)
            {
                canSpawnFruit = false;
                InstantiateNextFruit(currentFruitIndex, currentFruitPreview.transform.position);
                LoadNextFruit();
            }
        }
    }

    public void InstantiateNextFruit(int index, Vector3 position)
    {
        GameObject newFruit = Instantiate(fruitsPrefab[index].prefab, GameManager.Instance.allFruits);
        //newFruit.transform.parent = GameManager.Instance.allFruits;
        Fruit newFruitScript = newFruit.GetComponent<Fruit>();
        newFruitScript.fruitIndex = index;
        newFruit.transform.position = position;
    }

    private void LoadNextFruit()
    {
        currentFruitIndex = nextFruitIndex;
        nextFruitIndex = Random.Range(0, 4);
        nextFruitPreview.color = fruitsPrefab[nextFruitIndex].color;
        currentFruitPreview.color = fruitsPrefab[currentFruitIndex].color;
        currentFruitPreview.transform.localScale = fruitsPrefab[currentFruitIndex].prefab.transform.localScale ;
        CalculatePlayerBound();
    }

    private void CalculatePlayerBound()
    {
        currentMinXPosition = leftBorder.position.x + currentFruitPreview.bounds.size.x / 2 + borderOffset;
        currentMaxXPosition = rightBorder.position.x - currentFruitPreview.bounds.size.x / 2 - borderOffset;

        if (transform.position.x < currentMinXPosition)
        {
            transform.position = new Vector3(currentMinXPosition, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > currentMaxXPosition)
        {
            transform.position = new Vector3(currentMaxXPosition, transform.position.y, transform.position.z);
        }
    }
}

[System.Serializable]
public class FruitList
{
    public GameObject prefab;
    public Color color;
    public int points;
}