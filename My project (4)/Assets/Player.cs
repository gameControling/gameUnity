using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IPhysicInteractor
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Text interactionText;
    [SerializeField] private float interactionDistance = 2.0f;
    [field: SerializeField] public float kickPower { get; set; }

    private float Hp = 100f;

    private CharacterController _controller;
    private Camera _camera;
    private float _rotationY = 0f;
    private float _rotationX = 0f;
    private InputAction _moveAction, _lookAction, _jumpAction, _sprintAction, _interactAction;
    private float _verticalVelocity;
    private float _groundedTimer;
    [CanBeNull] private IInteractable _interactable = null;

    private void Rotate(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _interactAction = InputSystem.actions.FindAction("Interact");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveAndRotate();
        CheckInteraction();
        if (_interactAction.triggered && _interactable != null)
        {
            _interactable.Interact(gameObject);
        }
    }

    private void MoveAndRotate()
    {
        bool groundedPlayer = _controller.isGrounded;
        if (groundedPlayer)
        {
            _groundedTimer = 0.2f;
        }

        if (_groundedTimer > 0)
        {
            _groundedTimer -= Time.deltaTime;
        }

        if (groundedPlayer && _verticalVelocity < 0)
        {
            _verticalVelocity = 0f;
        }

        _verticalVelocity -= gravity * Time.deltaTime;
        Vector3 move = transform.forward * _moveAction.ReadValue<Vector2>().y +
                       transform.right * _moveAction.ReadValue<Vector2>().x;
        move *= _sprintAction.ReadValue<float>() > 0.1f ? sprintSpeed : moveSpeed;
        if (_jumpAction.triggered)
        {
            if (_groundedTimer > 0)
            {
                _groundedTimer = 0;

                _verticalVelocity += Mathf.Sqrt(jumpForce * 2 * gravity);
            }
        }

        move.y = _verticalVelocity;
        _controller.Move(move * Time.deltaTime);

        Vector2 rotationVector = _lookAction.ReadValue<Vector2>();
        Rotate(rotationVector);
        _rotationX = Mathf.Clamp(_rotationX + rotationVector.y * rotationSpeed * Time.deltaTime, -90, 90);
        _camera.transform.localRotation = Quaternion.Euler(-_rotationX, 0, 0);
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;

        if(Hp > 0)
        {
            Debug.Log("Hp: " + Hp);
        }
        else
        {
            Debug.Log("dead");
        }
    }

    public void TakeHeal(float heal)
    {
        if (Hp < 100)
        {
            Hp += heal;
            Debug.Log("Heal: " + heal);
        }

    }

    private void CheckInteraction()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _interactable = interactable;
                // Debug.Log("interactable: " + _interactable);
                //interactionText.text = _interactable.InteractionHintText;
            }
            else
            {
                _interactable = null;
               // interactionText.text = "";
            }
        }
        else
        {
            _interactable = null;
            //interactionText.text = "";
        }
    }
}
