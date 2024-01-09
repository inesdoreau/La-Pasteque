using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopLimit : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Fish fishScript = collision.gameObject.GetComponent<Fish>();
            if (fishScript.hasBeenDropped)
            {
                print("Game Over");

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
