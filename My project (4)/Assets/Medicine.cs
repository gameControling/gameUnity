using UnityEngine;
using System.Collections;

public class Medicine : MonoBehaviour
{
    [SerializeField] private float heal = 20;
    [SerializeField] private float respawnTime = 5f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 100f;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private bool _isEnabled = true;

    private void Start()
    {
        _collider = GetComponentInChildren<Collider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isEnabled && other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeHeal(heal);
            Disable();
        }
    }

    private void Disable()
    {
        _isEnabled = false;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(respawnTime);
        Enable();
    }

    private void Enable()
    {
        _isEnabled = true;
        _collider.enabled = true;
        _meshRenderer.enabled = true;
    }
}
