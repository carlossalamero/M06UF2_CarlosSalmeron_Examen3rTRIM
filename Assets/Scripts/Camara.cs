using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    
    public Transform target;

    public Vector3 offset;

    public Vector2 limitx;

    public Vector2 limity;

    public float interpolationRatio;

    GameManager gamemanager;
    
    
    void Start()
    {

        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    void FixedUpdate()
    {
        /* Si el jugador está jugando que ejecute una combinación de vector determinando la posición del transform del mario 
        para que lo siga pero cuando llegue a unos límites que no pase de ahí, tanto en el eje X como en el Y y en el caso del límite Z 
        se usa para la profundidad desde donde graba la camara*/
        
        if(gamemanager.misplaying == true)
        {
	
        Vector3 Camera = target.position + offset; 
        float limitedXposition = Mathf.Clamp(Camera.x, limitx.x, limitx.y);
        float limitedYposition = Mathf.Clamp(Camera.y, limity.x, limity.y);
        Vector3 limitedPosition = new Vector3(limitedXposition, limitedYposition, Camera.z);
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, limitedPosition, interpolationRatio);

        transform.position = lerpedPosition;
        }
    }
}
