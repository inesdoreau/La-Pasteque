using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minXPosition = -3f;
    [SerializeField] private float maxXPosition = 3f;

    [SerializeField] private GameObject[] fruitsPrefab;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && transform.position.x > minXPosition)
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < maxXPosition)
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newFruit = Instantiate(fruitsPrefab[0]);
            newFruit.transform.position = transform.position;
        }
    }
}
