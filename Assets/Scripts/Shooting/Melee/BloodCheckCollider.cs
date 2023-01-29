using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCheckCollider : MonoBehaviour
{
    private List<GameObject> touchedEnemies = new List<GameObject>();
    private static Transform player;
    [SerializeField] private GameObject deathMarker;
    private GameObject selectedEnemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        
    }


    void SelectEnemyToHighlight()
    {
        CalculateClosestEnemy();
        if (touchedEnemies[0].GetComponent<EnemyMob>() == null) { return; }

        selectedEnemy.GetComponent<EnemyMob>().SetDeathMarker(false);
        selectedEnemy = touchedEnemies[0];
        selectedEnemy.GetComponent<EnemyMob>().SetDeathMarker(true);
    }
    void CalculateClosestEnemy()
    {
        touchedEnemies.Sort(SortByDistance);
    }

    static int SortByDistance(GameObject a, GameObject b)
    {
        return Vector3.Distance(player.position, a.transform.position).CompareTo(Vector3.Distance(player.position, b.transform.position));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!touchedEnemies.Contains(collision.gameObject))
            {
                touchedEnemies.Add(collision.gameObject);
            }
        }
        SelectEnemyToHighlight();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (touchedEnemies.Contains(collision.gameObject))
        {
            touchedEnemies.Remove(collision.gameObject);
        }
        SelectEnemyToHighlight();
    }




}
