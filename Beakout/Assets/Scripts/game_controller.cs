using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{

    //Lo qyue va entre llaves permite la lectura de las variables, pero no su modifcacion (permite el get pero no el set)
    public static int score {get; private set; } = 0;
    public static int lifes{get; private set; } = 3;

    public static List<int> totalBricks = new List<int> {0, 32, 46};



    public static void UpdateScore (int points) {
        score += points;
    }

    public static void UpadateLifes (int numLifes){
        lifes += numLifes;
    }


}
