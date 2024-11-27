using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    int score = 0;
    int lifes = 3;

    [SerializeField] Text txtScore;
    [SerializeField] Text txtLifes;


    public void UpdateScore (int points) {
        score += points;
    }

    public void UpadateLifes (int numLifes){
        lifes += numLifes;
    }

    void OnGUI(){
        txtScore.text = string.Format("{0, 3:D3}",score);
        txtLifes.text = lifes.ToString();
    }
}
