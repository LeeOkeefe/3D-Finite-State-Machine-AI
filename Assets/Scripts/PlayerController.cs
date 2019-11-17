using UnityEngine;

internal sealed class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private GameObject cameraPivot;

    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private Animator _anim;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private PlayerStats _playerStats = new PlayerStats();

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        _anim.SetFloat(Horizontal, horizontalMovement);
        _anim.SetFloat(Vertical, verticalMovement);

        _moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
        CameraControl();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Time.deltaTime * speed * _moveDirection;
    }

    private void CameraControl()
    {
        var screenRect = new Rect(0, 0, Screen.width, Screen.height);

        if (!screenRect.Contains(Input.mousePosition))
            return;

        var v = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        var h = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

        cameraPivot.transform.Rotate(-v, 0, 0);
        transform.Rotate(0, h, 0);

        if (cameraPivot.transform.localEulerAngles.x <= 340 && cameraPivot.transform.localEulerAngles.x >= 90)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(340, 0, 0);
        }
        if (cameraPivot.transform.localEulerAngles.x >= 35 && cameraPivot.transform.localEulerAngles.x <= 90)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(35, 0, 0);
        }
    }
}
