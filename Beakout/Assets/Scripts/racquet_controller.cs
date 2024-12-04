using UnityEngine;
using System.Collections;

public class Racquet_controller : MonoBehaviour
{
    const float Max_X = 3.2f;
    const float Min_X = -3.2f; 
    [SerializeField] float speed = 4.2f;



    void Start(){
        
    }

    void Update()
    {
        float positionX = transform.position.x; // Posición actual en el eje X

        if ((Input.GetKey("left") || Input.GetKey("a")) && positionX > Min_X)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if ((Input.GetKey("right") || Input.GetKey("d")) && positionX < Max_X)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }

    

}