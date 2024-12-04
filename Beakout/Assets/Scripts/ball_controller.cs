using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Ball_controller : MonoBehaviour
{
    Rigidbody2D rb ;
    float force = 4f;
    float delay = 1f;

    AudioSource sfx;
    int hitCount = 0;

    bool halved;

    [SerializeField] float forceInc;
    [SerializeField] Game_controller game;

    [SerializeField] AudioClip sfxRacquet;
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxLifeLost;
    [SerializeField] Transform racquet;

    [SerializeField] Racquet_controller racquetScale;



    Dictionary<string, int> bricks = new Dictionary<string, int>{
        {"brick-y", 10},
        {"brick-g", 15},
        {"brick-a", 20},
        {"brick-r", 25}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        //Invoke("LaunchBall", delay);
        // Escuchar el evento de la raqueta
        racquetScale.OnScaleComplete += () =>
        {
            Debug.Log("paso por aqui");
            Invoke("LaunchBall", delay);
        };
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

    private void OnCollisionEnter2D (Collision2D other){
        string tag = other.gameObject.tag;

        //acutalizar puntuacion y destruir los ladrillos una vez golpeados. Este if sirve para cuando se golpea a los ladrillos con la pelota
        if (bricks.ContainsKey(tag)){
            sfx.clip = sfxRacquet;
            sfx.Play();
            game.UpdateScore(bricks[tag]);
            Destroy(other.gameObject);
        }

        if (tag == "racquet"){

            //implementar sonido cada vez q la pelota golpee la pala
            sfx.clip = sfxRacquet;
            sfx.Play();
            //obtener la posicion de la pala
            Vector3 racquet = other.gameObject.transform.position;

            //punto de contacto entre la pala y la pelota
            Vector2 contact = other.GetContact(0).point;

            // if para detectar si el punto de colision de la pelota esta mas a la izquierda o derecha, y devolver la pelota en la direccion en la que vino, sentido opuesto
            if(rb.linearVelocity.x < 0 && contact.x > racquet.x || rb.linearVelocity.x > 0 && contact.x < racquet.x){
                rb.linearVelocity = new Vector2 (-rb.linearVelocity.x, rb.linearVelocity.y);
            } 

            //incrementar la velocidad cada multiplo de 4
            hitCount ++;
            if (hitCount % 4 == 0){
                //Debug.Log("aumento de velocidad");
                rb.AddForce(rb.linearVelocity.normalized * forceInc, ForceMode2D.Impulse);
            }           
        }

        //reducir a la mitad el tamaño de la pala si golpeael limite superior
        if(tag == "wall-top" && !halved){
            HalveRacquet(true);
        }

        if (tag == "wall-lateral" || tag == "wall-top"){
            sfx.clip = sfxWall;
            sfx.Play();
        }

    }

//metodo que vuelve a lanzar la pelota si sale por abajo de los limites
    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "wall-bottom"){
            sfx.clip = sfxLifeLost;
            sfx.Play();
            Invoke("LaunchBall", delay);
            game.UpadateLifes(-1);
            //restaurar el numero de golpes a 0
            hitCount = 0;
            //restaurar el tamaño de la pala
            if(halved){
                HalveRacquet(false);
            }
           
        }
    }

    void HalveRacquet (bool halve){
        halved = halve;
        Vector3 scale = racquet.transform.localScale;
        racquet.transform.localScale = halved ?
            new Vector3(scale.x , scale.y * 0.5f, scale.z):
            new Vector3(scale.x , scale.y * 2f, scale.z);
    }


}
