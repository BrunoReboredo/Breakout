using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Ball_controller : MonoBehaviour
{
    Rigidbody2D rb ;
    float force = 4f;
    float delay = 1f;

    AudioSource sfx;
    [SerializeField] Game_controller game;

    [SerializeField] AudioClip sfxRacquet;

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
        Invoke("LaunchBall", delay);
       // Invoke("OnCollision2D", 0f);
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
        //acutalizar puntuacion y destruir los ladrillos una vez golpeados
        if (bricks.ContainsKey(tag)){
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
        }

    }

//metodo que vuelve a lanzar la pelota si sale por abajo de los limites
    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "wall-bottom"){
            Invoke("LaunchBall", delay);
            game.UpadateLifes(-1);
        }
    }
}
