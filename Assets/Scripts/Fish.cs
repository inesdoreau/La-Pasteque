using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int fishIndex;

    public bool hasBeenDropped = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBeenDropped = true;
        MovePlayer.canSpawnFish = true;
        if (collision.gameObject.CompareTag("Fish"))
        {
            Fish collidedFruit = collision.gameObject.GetComponent<Fish>();

            if (collidedFruit.fishIndex == fishIndex)
            {
                if (!gameObject.activeSelf || !collision.gameObject.activeSelf)
                    return;

                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);

                if (fishIndex < MovePlayer.Instance.fishesPrefab.Length)
                    MovePlayer.Instance.InstantiateNextFish(fishIndex + 1, transform.position);

                MovePlayer.Instance.IncreaseScore(MovePlayer.Instance.fishesPrefab[fishIndex].points);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
