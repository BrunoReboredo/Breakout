using UnityEngine;

public class ball_controller : MonoBehaviour
{
    Rigidbody2D rb ;
    float force = 4f;
    float delay = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("LaunchBall", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LaunchBall (){
        //resetar posicion pelota
        transform.position = Vector3.zero;
        //resetar velocidad pelota
        rb.linearVelocity = Vector2.zero;

        //obtener la direccion de la pelota (aleatoria y siempre "para abajo en un angulo de 45º")
        float dirX, dirY = -1;
        dirX = Random.Range(0,2) == 0? -1 : 1;
        Vector2 dir = new Vector2 (dirX, dirY);
        dir.Normalize(); //normalizar el vector para q tenga modulo 1 siempre

        //lanzar la pelota
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
}
