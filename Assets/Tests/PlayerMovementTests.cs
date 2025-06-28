using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerMovementTests
{
    private GameObject playerGO;
    private PlayerMovement movement;
    private Rigidbody rb;
    private FakeJoystick fakeJoystick;
    private PlayerHealth fakeHealth;

    [SetUp]
    public void SetUp()
    {
        // Tạo player
        playerGO = new GameObject("Player");
        rb = playerGO.AddComponent<Rigidbody>();
        movement = playerGO.AddComponent<PlayerMovement>();

        // Gán joystick giả
        fakeJoystick = new GameObject("FakeJoystick").AddComponent<FakeJoystick>();
        movement.joystick = fakeJoystick;

        // Gán PlayerHealth giả
        fakeHealth = playerGO.AddComponent<PlayerHealth>();
        fakeHealth.SetHealthForTest(100); // full máu

    }

    [UnityTest]
    public IEnumerator PlayerMoves_WhenJoystickInput()
    {
        Vector3 startPos = playerGO.transform.position;

        fakeJoystick.SetInput(1f, 0f); // input phải

        yield return new WaitForFixedUpdate(); // FixedUpdate gọi rb.MovePosition

        Vector3 endPos = playerGO.transform.position;

        Assert.Greater(endPos.x, startPos.x, "Player phải di chuyển sang phải.");
    }

    [UnityTest]
    public IEnumerator PlayerDoesNotMove_WhenDead()
    {
        fakeHealth.SetDead(); // giả lập đã chết

        Vector3 startPos = playerGO.transform.position;

        fakeJoystick.SetInput(1f, 0f); // input vẫn có

        yield return new WaitForFixedUpdate();

        Vector3 endPos = playerGO.transform.position;

        Assert.AreEqual(startPos, endPos, "Player đã chết thì không được di chuyển.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(fakeJoystick.gameObject);
    }
}
