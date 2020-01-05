using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager 
{


    /*Default Values*/
    static float DEFAULT_MAX_PLAYER_SPEED = 100.0f;
    static float DEFAULT_MIN_PLAYER_SPEED = 0.0f;
    static float DEFAULT_MAX_JUMP_FORCE = 50.0f; 
    static float DEFAULT_MIN_JUMP_FORCE = 10.0f;

    /*Current Values*/
    static float MAX_PLAYER_SPEED = 100.0f;
    static float MIN_PLAYER_SPEED = 0.0f;
    static float MAX_JUMP_FORCE = 50.0f;
    static float MIN_JUMP_FORCE = 10.0f;

    //Getters
    public static float GetMaxSpeed() { return MAX_PLAYER_SPEED;  }
    public static float GetMaxJump() { return MAX_JUMP_FORCE;  }
    public static float GetMinSpeed() { return MIN_PLAYER_SPEED;  }
    public static float GetMinJump() { return MIN_JUMP_FORCE;  }

    /*Increases MAX_SPEED value by "Amount"  and return the resulting value*/
    public static float IncreaseMaxSpeed(float amount) { MAX_PLAYER_SPEED += amount; return MAX_PLAYER_SPEED;  }
    /* Decreases MAX_SPEED value by "Amount"  and return the resulting value (Can't get negative values)*/
    public static float DecreaseMaxSpeed(float ammount) { MAX_PLAYER_SPEED = (MAX_PLAYER_SPEED >= ammount) ? MAX_PLAYER_SPEED -ammount : 0; return MAX_PLAYER_SPEED; }
    /* Sets the Current max speed to be Default * Modifier. Returns the resulting value */
    public static float ModifyMaxSpeed(float Modifier) { MAX_PLAYER_SPEED = DEFAULT_MAX_PLAYER_SPEED * Modifier; return MAX_PLAYER_SPEED; }

    /*Increases MAX_JUMP_FORCE value by "Amount"  and return the resulting value*/
    public static float IncreaseMaxJump(float amount) { MAX_JUMP_FORCE += amount; return MAX_JUMP_FORCE; }
    /* Decreases MAX_JUMP_FORCE value by "Amount"  and return the resulting value (Can't get negative values)*/
    public static float DecreaseMaxJump(float ammount) { MAX_JUMP_FORCE = (MAX_JUMP_FORCE >= ammount) ? MAX_JUMP_FORCE - ammount : 0; return MAX_JUMP_FORCE; }
    /* Sets the Current MAX_JUMP_FORCE to be Default * Modifier. Returns the resulting value */
    public static float ModifyMaxJump(float Modifier) { MAX_JUMP_FORCE = DEFAULT_MAX_JUMP_FORCE * Modifier; return MAX_JUMP_FORCE; }

    /*Increases MIN_PLAYER_SPEED value by "Amount"  and return the resulting value*/
    public static float IncreaseMinSpeed(float amount) { MIN_PLAYER_SPEED += amount; return MIN_PLAYER_SPEED; }
    /* Decreases MIN_PLAYER_SPEED value by "Amount"  and return the resulting value (Can't get negative values)*/
    public static float DecreaseMinSpeed(float ammount) { MIN_PLAYER_SPEED = (MIN_PLAYER_SPEED >= ammount) ? MIN_PLAYER_SPEED - ammount : 0; return MIN_PLAYER_SPEED; }
    /* Sets the Current MIN_PLAYER_SPEED to be Default * Modifier. Returns the resulting value */
    public static float ModifyMinSpeed(float Modifier) { MIN_PLAYER_SPEED = DEFAULT_MIN_PLAYER_SPEED * Modifier; return MIN_PLAYER_SPEED; }

    /*Increases MIN_JUMP_FORCE value by "Amount"  and return the resulting value*/
    public static float IncreaseMinJump(float amount) { MIN_JUMP_FORCE += amount; return MIN_JUMP_FORCE; }
    /* Decreases MIN_JUMP_FORCE value by "Amount"  and return the resulting value (Can't get negative values)*/
    public static float DecreaseMinJump(float ammount) { MIN_JUMP_FORCE = (MIN_JUMP_FORCE >= ammount) ? MIN_JUMP_FORCE - ammount : 0; return MIN_JUMP_FORCE; }
    /* Sets the Current MIN_JUMP_FORCE to be Default * Modifier. Returns the resulting value */
    public static float ModifyMinJump(float Modifier) { MIN_JUMP_FORCE = DEFAULT_MIN_JUMP_FORCE * Modifier; return MIN_JUMP_FORCE; }



}
