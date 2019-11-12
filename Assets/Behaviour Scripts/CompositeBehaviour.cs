using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weight;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weight.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weight[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weight[i] * weight[i])
                {
                    partialMove.Normalize();
                    partialMove *= weight[i];
                    move += partialMove;
                } else 
                {
                    // Debug.Log("Partial " + i + " " + partialMove);
                    move += partialMove;
                }
            }
        }
        // Debug.Log("Final Move: " + move);
        return move;
    }

}
