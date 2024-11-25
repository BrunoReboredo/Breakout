using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    [SerializeField] const float Max_X = 4f;
    [SerializeField] const float Min_X = -4f;
    [SerializeField] float speed = 4.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("racquet")){
            if(Input.GetKey("left") || Input.GetKey("a") && transform.position.x > Min_X){
                transform.Translate(Vector3.left * speed* Time.deltaTime);
            }

            if(Input.GetKey("right") || Input.GetKey("d")  && transform.position.x < Max_X){
            transform.Translate(Vector3.right * speed* Time.deltaTime);
            }

        }

   
    }
}
