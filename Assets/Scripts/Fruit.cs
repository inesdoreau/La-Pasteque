using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitIndex;

    public bool hasBeenDropped = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBeenDropped = true;
        MovePlayer.canSpawnFish = true;
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit collidedFruit = collision.gameObject.GetComponent<Fruit>();

            if(collidedFruit.fruitIndex == fruitIndex)
            {
                if (!gameObject.activeSelf || !collision.gameObject.activeSelf)
                    return;

                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);

                if(fruitIndex < MovePlayer.Instance.fishesPrefab.Length)
                    MovePlayer.Instance.InstantiateNextFish(fruitIndex + 1, transform.position);

                MovePlayer.Instance.IncreaseScore(MovePlayer.Instance.fishesPrefab[fruitIndex].points);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
