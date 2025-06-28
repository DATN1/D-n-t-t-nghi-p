using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MiniMapRadar : MonoBehaviour
{
    [Header("Tham chiếu UI")]
    public RectTransform radarPanel;         // Panel chứa icon radar
    public RectTransform playerIcon;         // Icon của người chơi (giữ ở giữa)
    public GameObject enemyIconPrefab;       // Prefab icon Enemy (đỏ)
    public GameObject bossIconPrefab;        // Prefab icon Boss (vàng)
    public Transform player;                 // Transform của nhân vật chính

    [Header("Cài đặt phạm vi")]
    public float radarRange = 30f;           // Phạm vi thế giới hiển thị
    public float radarSize = 75f;            // Kích thước hiển thị trên UI (bán kính pixel)

    private List<GameObject> enemyIconPool = new List<GameObject>();
    private List<GameObject> bossIconPool = new List<GameObject>();

    void LateUpdate()
    {
        UpdateEnemyIcons();
        UpdateBossIcons();
    }

    void UpdateEnemyIcons()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Tắt toàn bộ icon cũ
        for (int i = 0; i < enemyIconPool.Count; i++)
            enemyIconPool[i].SetActive(false);

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 offset = enemies[i].transform.position - player.position;
            Vector2 radarPos = new Vector2(offset.x, offset.z) / radarRange * radarSize;

            if (i >= enemyIconPool.Count)
            {
                GameObject icon = Instantiate(enemyIconPrefab, radarPanel);
                enemyIconPool.Add(icon);
            }

            GameObject iconObj = enemyIconPool[i];
            iconObj.SetActive(true);
            iconObj.GetComponent<RectTransform>().anchoredPosition = radarPos;
        }
    }

    void UpdateBossIcons()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        // Tắt toàn bộ icon cũ
        for (int i = 0; i < bossIconPool.Count; i++)
            bossIconPool[i].SetActive(false);

        for (int i = 0; i < bosses.Length; i++)
        {
            Vector3 offset = bosses[i].transform.position - player.position;
            Vector2 radarPos = new Vector2(offset.x, offset.z) / radarRange * radarSize;

            if (i >= bossIconPool.Count)
            {
                GameObject icon = Instantiate(bossIconPrefab, radarPanel);
                bossIconPool.Add(icon);
            }

            GameObject iconObj = bossIconPool[i];
            iconObj.SetActive(true);
            iconObj.GetComponent<RectTransform>().anchoredPosition = radarPos;
        }
    }
}
