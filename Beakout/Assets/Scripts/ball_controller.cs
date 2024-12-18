using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Ball_controller : MonoBehaviour
{
    Rigidbody2D rb ;
    float force = 5f;
    float delay = 1f;
    AudioSource sfx;
    int hitCount = 0;
    int destroyedBricks = 0;
    int sceneId;
    bool halved;
    [SerializeField] float forceInc;
    [SerializeField] AudioClip sfxRacquet;
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxLifeLost;
    [SerializeField] Transform racquet;

    [SerializeField] AudioClip sfxLevelPass;


    Dictionary<string, int> bricks = new Dictionary<string, int>{
        {"brick-v", 1},
        {"brick-p", 5},
        {"brick-o", 7},
        {"brick-y", 10},
        {"brick-g", 15},
        {"brick-a", 18},
        {"brick-r", 20}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sfx = GetComponent<AudioSource>();
        Invoke("LaunchBall", delay);
        //cargar en la variable la id de la escena activa en la que se encuentra el juego
        sceneId = SceneManager.GetActiveScene().buildIndex;
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
            Game_controller.UpdateScore(bricks[tag]);
            Destroy(other.gameObject);

            //se suma el contador de bricks rotos, al llegar al limite de la lista totalbricks, se pasa de nivel
            destroyedBricks ++;
            if(destroyedBricks == Game_controller.totalBricks[sceneId]){
                sfx.clip = sfxLevelPass;
                sfx.Play();
                //una vez rotos todos los ladrillos, la pelota se queda a zero e invisible de velocidad para evitar bugs
                rb.linearVelocity = Vector2.zero;
                GetComponent<SpriteRenderer>().enabled = false;
                //invocar la siguiente escena con un delay de 3 segundos
                Invoke("ChangeScene", 3);
            }
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

        //reducir a la mitad el tamaño de la pala si golpea el limite superior
        if(tag == "wall-top" && !halved){
            HalveRacquet(true);
        }

        if (tag == "wall-lateral" || tag == "wall-top" || tag == "brick-gr"){
            sfx.clip = sfxWall;
            sfx.Play();
        }

    }

    //metodo para cargar la siguiente escena una vez completada la actual
void ChangeScene()
{
    // Si las vidas llegan a 0, se carga la escena de Game Over
    if (Game_controller.lifes <= 0)
    {
        Game_controller.ResetGame();
        return;
    }

    // Si hay un nivel siguiente
    if (sceneId + 1 < Game_controller.totalBricks.Count - 1)
    {
        SceneManager.LoadScene(sceneId + 1); // Avanza al siguiente nivel
    }
    else
    {
        // Si es el último nivel, carga la escena final
        SceneManager.LoadScene(4);
    }
}

    //metodo que vuelve a lanzar la pelota si sale por abajo de los limites
    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "wall-bottom"){
            sfx.clip = sfxLifeLost;
            sfx.Play();
            Invoke("LaunchBall", delay);
            
            Game_controller.UpadateLifes(-1);
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