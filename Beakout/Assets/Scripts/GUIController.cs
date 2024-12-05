using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    [SerializeField] Text txtScore;
    [SerializeField] Text txtLifes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnGUI(){
        txtScore.text = string.Format("{0, 3:D3}",Game_controller.score);
        txtLifes.text = Game_controller.lifes.ToString();

        
    }
}
