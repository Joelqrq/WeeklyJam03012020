using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLibrary
{ 


    //Returns Vector representing the direction and magnitude of the jump
    public static Vector3 CalculateJump(Vector3 UpVector, Vector3 Speed)
    {
        return CalculateJumpDirection(UpVector) * CalculateJumpForce(Speed); 
    }


    private static Vector3 CalculateJumpDirection(Vector3 UpVector)
    {
        return Vector3.Lerp(UpVector, Vector3.up, Mathf.Abs(Vector3.Dot(UpVector, Vector3.up)) );
    }

    private static float CalculateJumpForce(Vector3 Speed)
    {
        return Mathf.Lerp(PlayerManager.GetMinJump(),PlayerManager.GetMaxJump(), ( Speed.magnitude / PlayerManager.GetMaxSpeed() ) ); 
    }
}
