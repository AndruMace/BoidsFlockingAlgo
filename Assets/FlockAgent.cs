﻿using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    // COLLIDER IS NEEDED FOR NEIGHBOR CHECK
    Collider2D agentCollider;
    public Collider2D AgentCollider {get {return agentCollider;} }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
