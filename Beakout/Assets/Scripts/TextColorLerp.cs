using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextColorLerp : MonoBehaviour
{

    [SerializeField] Text msg;
    [SerializeField] float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       StartCoroutine("ChangeColor");
    }


    IEnumerator ChangeColor(){
        float t = 0;
        while (t<time){
            t += Time.deltaTime;
            msg.color = Color.Lerp(Color.black, Color.white, t/time);
            yield return null;
        }
        //reiniciar la corutina
        StartCoroutine("ChangeColor");
    }
}