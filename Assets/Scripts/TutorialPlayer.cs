using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    //[SerializeField] GameObject[] guidelines;
    [SerializeField] GameObject[] enemies1;
    [SerializeField] GameObject[] enemies2;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Go to game Collider")
        {
            TransitionManager.Instance.FadeScene("Gameplay");
        }
        if (other.gameObject.name == "Replay tutorial Collider")
        {
            TransitionManager.Instance.FadeScene("Tutorial");
        }

        if (other.gameObject.name == "KeyPoint2")
        {
            foreach (GameObject enemy in enemies1)
            {
                enemy.SetActive(true);
            }
        }

        if (other.gameObject.name == "KeyPoint3")
        {
            foreach (GameObject enemy in enemies2)
            {
                enemy.SetActive(true);
            }
        }
    }
}
