using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Range(1,500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;
    
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public float sqrMaxSpeed;
    public float sqrNeighborRadius;
    public float sqrAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return sqrAvoidanceRadius;} }

    void Start()
    {
        sqrMaxSpeed = maxSpeed * maxSpeed;
        sqrNeighborRadius = neighborRadius * neighborRadius;
        sqrAvoidanceRadius = sqrNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * startingCount * AgentDensity, 
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);

        }
    }

    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > sqrMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
            
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
                                                           