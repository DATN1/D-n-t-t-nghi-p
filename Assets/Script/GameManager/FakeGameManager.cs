using UnityEngine;

public class FakeGameManager : MonoBehaviour
{
    public static GameManagerStub StubInstance; //  thêm biến truy cập

    void Awake()
    {
        StubInstance = new GameManagerStub();
        GameManager.Instance = StubInstance;
    }

    public class GameManagerStub : GameManager
    {
        private bool _isOver = false;

        public override bool isGameOver => _isOver;

        public void SetGameOver(bool value)
        {
            _isOver = value;
        }
    }
}
