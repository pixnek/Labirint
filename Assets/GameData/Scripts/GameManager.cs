using UnityEngine;
using UnityEngine.SceneManagement;

namespace Labirint
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public delegate void GameOverAction();
        public static GameOverAction OnGameOver;
        public delegate void GameFinishAction();
        public static GameFinishAction OnGameFinish;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {

                instance = this;
            }
        }
        private void Start()
        {
            UnitManager.SetTargetFollowing(GamerController.Instance);
        }
        public void Restart_Local()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void GameOver_Local()
        {
            OnGameOver?.Invoke();
        }
        private void GameFinish_Local()
        {
            OnGameFinish?.Invoke();
        }
        public static void GameOver()
        {
            if (instance != null)
            {
                instance.GameOver_Local();
            }
        }
        public static void GameFinish()
        {
            if (instance != null)
            {
                instance.GameFinish_Local();
            }
        }
        public static void Restart()
        {
            if(instance != null)
            {
                instance.Restart_Local();
            }
        }
    }
}