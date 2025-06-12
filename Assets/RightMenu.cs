using UnityEngine;
using DG.Tweening;

public class RightMenu : MonoBehaviour
{
    [SerializeField]
    private Vector3 _opennedPosition;
    [SerializeField]
    private Vector3 _closedPosition;

    [SerializeField]
    private float _movementSpeed;


    private void Awake()
    {
        transform.position = _closedPosition;
    }

    public void Open()
    {
        transform.DOMove(_opennedPosition, _movementSpeed);
    }

    public void Close()
    {
        transform.DOMove(_closedPosition, _movementSpeed);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _closedPosition);
        Gizmos.DrawSphere(_closedPosition, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _opennedPosition);
        Gizmos.DrawSphere(_opennedPosition, 0.1f);
    }
}
