using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class EnemyAttackTests
{
    private GameObject enemyGO;
    private GameObject playerGO;
    private EnemyAttack enemyAttack;
    private PlayerHealth playerHealth;

    [SetUp]
    public void SetUp()
    {
        // Giả lập GameManager nếu chưa có
        if (GameManager.Instance == null)
        {
            GameObject gm = new GameObject("GameManager");
            gm.AddComponent<FakeGameManager>();
        }

        // Tạo player
        playerGO = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerGO.tag = "Player";
        playerHealth = playerGO.AddComponent<PlayerHealth>();
        playerHealth.SetHealthForTest(100); // ➕ hàm bổ sung để setup máu test

        // Tạo enemy có EnemyAttack
        enemyGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemyGO.transform.position = playerGO.transform.position; // overlap
        enemyAttack = enemyGO.AddComponent<EnemyAttack>();
        enemyAttack.damage = 10;
        enemyAttack.attackCooldown = 0.5f;
    }

    [UnityTest]
    public IEnumerator Enemy_Attacks_Player_Once()
    {
        int beforeHP = playerHealth.GetCurrentHealth();

        // Gọi OnTriggerStay giả lập
        enemyAttack.TryAttack(playerGO);


        yield return new WaitForSeconds(0.1f); // dưới cooldown

        int afterHP = playerHealth.GetCurrentHealth();

        Assert.AreEqual(beforeHP - enemyAttack.damage, afterHP, "Player phải mất đúng lượng máu khi bị tấn công");
    }

    [UnityTest]
    public IEnumerator Enemy_DoesNot_Attack_Twice_Within_Cooldown()
    {
        int beforeHP = playerHealth.GetCurrentHealth();

        // Gọi OnTriggerStay 2 lần sát nhau
        enemyAttack.TryAttack(playerGO);

        yield return new WaitForSeconds(0.1f);
        enemyAttack.TryAttack(playerGO);


        yield return new WaitForSeconds(0.1f); // tổng 0.2s < cooldown

        int afterHP = playerHealth.GetCurrentHealth();
        Assert.AreEqual(beforeHP - enemyAttack.damage, afterHP, "Chỉ được tấn công 1 lần trong cooldown");
    }


    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(enemyGO);
    }
}
