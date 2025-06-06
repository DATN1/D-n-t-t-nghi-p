using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void EndGame()
    {
        isGameOver = true;
        Debug.Log(" GAME OVER!");
    }
}
