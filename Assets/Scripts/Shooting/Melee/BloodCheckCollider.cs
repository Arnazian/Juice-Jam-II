using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCheckCollider : MonoBehaviour
{
    [SerializeField] private Collider2D bloodCheck;
    private List<GameObject> touchedEnemies = new List<GameObject>();
    private static Transform player;
    private GameObject selectedEnemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public GameObject GetSelectedEnemy() { return selectedEnemy; }
    public void UnselectEnemy() { selectedEnemy = null; }
    public void SetBloodCheckColliderStatus(bool newStatus) { bloodCheck.enabled = newStatus; }

    #region CalculateClosestEnemy
    void SelectEnemyToHighlight()
    {
        if (selectedEnemy != null) { selectedEnemy.GetComponent<EnemyMobHealthManager>().SetDeathMarker(false); }
        if (touchedEnemies.Count <= 0) { return; }
        CalculateClosestEnemy();
        if (touchedEnemies[0].GetComponent<EnemyMobHealthManager>() == null) { return; }        
        selectedEnemy = touchedEnemies[0];
        selectedEnemy.GetComponent<EnemyMobHealthManager>().SetDeathMarker(true);
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
    #endregion




}
