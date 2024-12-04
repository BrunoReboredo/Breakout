using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Ball_controllerScene0 : MonoBehaviour
{
    Rigidbody2D rb ;
    float force = 4f;
    float delay = 1f;

    AudioSource sfx;
    [SerializeField] AudioClip sfxRacquet;
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxBrick;



    List<string> bricks = new List<string>{
        {"brick-y"},
        {"brick-g"},
        {"brick-a"},
        {"brick-r"}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        Invoke("LaunchBall", delay);
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

        //sonido cuando la pelota golpea un ladrillo 
        if (bricks.Contains(tag)){
            sfx.clip = sfxRacquet;
            sfx.Play();
        }

        if (tag == "racquet"){
            //implementar sonido cada vez q la pelota golpee la pala
            sfx.clip = sfxRacquet;
            sfx.Play();
        }

        //implementar sonido cada vez q la pelota golpee los muros
        if (tag == "wall-lateral" || tag == "wall-top"){
            sfx.clip = sfxWall;
            sfx.Play();
        }

    }
}