using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public Vector2 center;
    public float radius = 15f;
    
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        // Debug.Log("T is: " + t);
        // Debug.Log("Mag is: " + centerOffset.magnitude);
        if(t < 0.9f)
        {
            return Vector2.zero;
        } else 
        {
            // Debug.Log("Centering");
            Vector2 moveCenter = centerOffset * t * t;
            // Debug.Log(moveCenter);
            return moveCenter;
        }   
        
    }

}
