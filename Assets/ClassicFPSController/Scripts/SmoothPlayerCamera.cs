using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SmoothPlayerCamera : MonoBehaviour
{
    [SerializeField] private float height = 0.5f;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float distanceLimit = 1f;
    [SerializeField] public BetterPlayerMovement player;

    private Vector3 oldPos;
    private Quaternion oldRot;

    
    private void Start() {
        oldPos = transform.position;
        oldRot = transform.rotation;
    }

    private void LateUpdate() {
        Vector3 targetPos = player.transform.position + new Vector3(0f, height, 0f);

        // Lerp position
        transform.position = Vector3.Lerp(oldPos, targetPos, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(oldRot, Quaternion.Euler(player.InputRot), turnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) > distanceLimit) {
            transform.position = targetPos;
        }

        oldPos = transform.position;
        oldRot = transform.rotation;
    }
}
