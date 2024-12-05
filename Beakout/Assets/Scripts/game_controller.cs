using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_controller : MonoBehaviour
{

    //Lo qyue va entre llaves permite la lectura de las variables, pero no su modifcacion (permite el get pero no el set)
    public static int score {get; private set; } = 0;
    public static int lifes{get; private set; } = 3;
    //int sceneId = SceneManager.GetActiveScene().buildIndex;
   

    public static List<int> totalBricks = new List<int> {0, 32, 46, 0};
    


    public static void UpdateScore (int points) {
        score += points;
    }

    public static void UpadateLifes (int numLifes){
        lifes += numLifes;
         if (lifes <= 0)
        {
            ResetGame();
        }
    }

    //Metodo para resetear el juego si las vidas llegan a 0, se va a la escena de perder, q te deja resetear la partida
    public static void ResetGame()
    {
        // Resetea vidas y puntuación
        lifes = 3;
        score = 0;
        // Carga la escena de "game over"
        SceneManager.LoadScene(3);
    }
}
