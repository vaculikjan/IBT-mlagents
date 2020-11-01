using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public PlayerMovement Player1;
    public PlayerMovement Player2;
    public PlayerHealthManager Health1;
    public PlayerHealthManager Health2;

    public void resetFight() {
        Player1.EndEpisode();
        Player2.EndEpisode();
        Health1.resetHealth();
        Health2.resetHealth();
    }
}
