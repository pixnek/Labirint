using UnityEngine;

namespace Labirint
{
    public class GameUIButton : MonoBehaviour
    {
        [SerializeField] private GameUIButtonType type;

        public void OnClick()
        {
            switch (type)
            {
                case GameUIButtonType.Restart:
                    GameManager.Restart();
                    break;
            }
        }
    }
}