using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    [SerializeField] AudioClip sfxStart;
    AudioSource sfx;
    [SerializeField] Transform racquet;
    [SerializeField] GameObject ball;
    [SerializeField] Text msg;
    float duration = 3f;


    void Start(){
        sfx = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.anyKeyDown){
            //desactivar pelota (hacer invisible)
            ball.SetActive(false);
            //desailitar mensaje de pulsar boton para empezar
            msg.enabled = false;
            sfx.Play();
            StartCoroutine("ChangeScale");        
        }
    }

    //cambiar la escala de la raqueta de ocupar todo el ancho del principio (7.5), al esperado para el juegio (1)
    IEnumerator ChangeScale()
    {
        Vector3 initialScale = racquet.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, 1, initialScale.z);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            racquet.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       SceneManager.LoadScene(1);
    }
}
