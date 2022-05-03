using TMPro;
using UnityEngine;

namespace Labirint
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameFinishPanel;

        private void Start()
        {
            SetRightInfo();
            UnitManager.OnAllyChangeState += SetRightInfo;
            GameManager.OnGameOver += ShowGameOverPanel;
            GameManager.OnGameFinish += ShowGameFinishPanel;
        }
        private void SetRightInfo()
        {
            textObject.text = "Follower: " + UnitManager.FollowingAllyCount + "/" +  UnitManager.AllyCount;
        }

        private void ShowGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }
        private void ShowGameFinishPanel()
        {
            gameFinishPanel.SetActive(true);
        }
        private void OnDestroy()
        {
            UnitManager.OnAllyChangeState -= SetRightInfo;
            GameManager.OnGameOver -= ShowGameOverPanel;
            GameManager.OnGameFinish -= ShowGameFinishPanel;
        }
    }
}