using UnityEngine;
using System.Collections;

public class Racquet_controller : MonoBehaviour
{
    const float Max_X = 3.2f;
    const float Min_X = -3.2f; 
    [SerializeField] float speed = 4.2f;
    [SerializeField] float scaleChangeDuration = 1.5f; // Duración del cambio de escala en segundos

        // Evento para notificar que la escalada ha terminado
    public delegate void ScaleComplete();
    public event ScaleComplete OnScaleComplete;


    void Start(){
        
        StartCoroutine(ChangeScale(new Vector3(transform.localScale.x, 1f, transform.localScale.z), scaleChangeDuration));
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

    //cambiar la escala de la raqueta de ocupar todo el ancho del principio (7.5), al esperado para el juegio (1)
    IEnumerator ChangeScale(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que la escala final sea exacta
        transform.localScale = targetScale; 

        // Notificar que la escalada ha terminado
        OnScaleComplete?.Invoke();
    }
}