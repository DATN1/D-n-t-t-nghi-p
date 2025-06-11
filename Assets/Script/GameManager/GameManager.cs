using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public virtual bool isGameOver { get; set; } = false; // ✅ cho phép gán

    void Awake()
    {
        Instance = this;
    }



    public void EndGame()
    {
        isGameOver = true;
        Debug.Log(" GAME OVER!");
    }
}
