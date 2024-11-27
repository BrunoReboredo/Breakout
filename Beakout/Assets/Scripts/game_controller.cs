using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_controller : MonoBehaviour
{
    int score = 0;

    [SerializeField] Text txtScore;


    public void UpdateScore (int points) {
        score += points;
    }
    
    void OnGUI(){
        txtScore.text = string.Format("{0, 3:D3}",score);
    }
}
