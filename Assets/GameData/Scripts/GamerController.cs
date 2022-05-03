using UnityEngine;

namespace Labirint
{
    public class GamerController : MonoBehaviour, ITargetFollower
    {
        private static GamerController instance;
        public static GamerController Instance { get => instance; }

        [SerializeField] private CharacterController currentCharacterController;
        [SerializeField] private float speed;
        [SerializeField] private float speedKoef;
        [SerializeField] private float rotateSpeed;

        public GameObject CurrentGameObject => gameObject;

        private bool moveIsEnable = true;

        private void Awake()
        {
            if(instance != null)
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
            GameManager.OnGameFinish += DisableInput;
            GameManager.OnGameFinish += DisableInput;
        }
        private void DisableInput()
        {
            moveIsEnable = false;
        }
        private void Update()
        {
            if (moveIsEnable)
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                float currSpeed = speed * Input.GetAxis("Vertical") * ((Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift))? speedKoef : 1f);    
                currentCharacterController.SimpleMove(forward * currSpeed);
            }
        }
        private void OnDestroy()
        {
            GameManager.OnGameFinish -= DisableInput;
            GameManager.OnGameFinish -= DisableInput;
        }
    }
}