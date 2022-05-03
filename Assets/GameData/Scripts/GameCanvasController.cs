using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Labirint
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject;
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameFinishPanel;

        private void Start()
        {
            SetRightInfo();
            UnitManager.OnAllyChangeState += SetRightInfo;
            UnitManager.OnChangeDistanceEnemyToGamern += ChangeSliderValue;
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
        private void ChangeSliderValue(float percent)
        {
            if(percent == -1f)
            {
                slider.gameObject.SetActive(false);
            }
            else
            {
                slider.gameObject.SetActive(true);
                slider.value = slider.maxValue - slider.maxValue * percent;
            }
        }
        private void OnDestroy()
        {
            UnitManager.OnAllyChangeState -= SetRightInfo;
            UnitManager.OnChangeDistanceEnemyToGamern -= ChangeSliderValue;
            GameManager.OnGameOver -= ShowGameOverPanel;
            GameManager.OnGameFinish -= ShowGameFinishPanel;
        }
    }
}