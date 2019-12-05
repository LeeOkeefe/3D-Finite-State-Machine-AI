using UnityEngine;

namespace Assets.Scripts.Player
{
    internal sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject cameraPivot;
        [SerializeField] private float speed;

        private Vector3 m_MoveDirection;
        private Rigidbody m_Rb;
        private Animator m_Anim;

        private static float MouseSensitivity => PlayerPrefs.GetFloat("Sensitivity", 30f);
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
            m_Anim = GetComponent<Animator>();
        }

        private void Update()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            m_Anim.SetFloat(Horizontal, horizontalMovement);
            m_Anim.SetFloat(Vertical, verticalMovement);

            m_MoveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
            CameraControl();
        }

        private void FixedUpdate()
        {
            var movement = Time.deltaTime * speed * m_MoveDirection;
            m_Rb.velocity = new Vector3(movement.x, m_Rb.velocity.y, movement.z);
        }

        /// <summary>
        /// Controls the pivot of the camera in accordance to the mouse position
        /// </summary>
        private void CameraControl()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);

            if (!screenRect.Contains(Input.mousePosition))
                return;

            var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
            var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;

            cameraPivot.transform.Rotate(-vertical, 0, 0);
            transform.Rotate(0, horizontal, 0);

            var local = cameraPivot.transform.localEulerAngles;

            if (local.x <= 340 && local.x >= 90)
            {
                cameraPivot.transform.localEulerAngles = new Vector3(340, 0, 0);
            }
            if (local.x >= -45 && local.x <= 120)
            {
                cameraPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
