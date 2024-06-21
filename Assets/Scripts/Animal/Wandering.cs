using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    private Transform thisTransform;
    public float moveSpeed = 0.2f;
    public Vector2 decisionTime = new Vector2(1, 4);
    private float decisionTimeCount;
    private Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
    private int currentMoveDirection;

    public Vector3 areaCenter = Vector3.zero;
    public Vector3 areaSize = new Vector3(10, 0, 10);

    void Start()
    {
        thisTransform = transform;
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
        ChooseMoveDirection();
    }

    void Update()
    {
        Wander();
    }

    void Wander()
    {
        Vector3 targetPosition = thisTransform.position + moveDirections[currentMoveDirection];
        thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        currentMoveDirection = Random.Range(0, moveDirections.Length);
    }

    bool IsWithinBounds(Vector3 position)
    {
        Vector3 halfSize = areaSize / 2;
        return position.x > areaCenter.x - halfSize.x && position.x < areaCenter.x + halfSize.x &&
               position.z > areaCenter.z - halfSize.z && position.z < areaCenter.z + halfSize.z;
    }
}
