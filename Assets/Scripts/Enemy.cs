using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variable para controlar la velocidad del goomba
    public float speed = 4.5f;
    //variable para saber en que direccion se mueve en el eje X
    private int directionX = 1;

    //variable para almacener el rigidbody del enemigo
    private Rigidbody2D rigidBody;

    //variable para saber si el goomba esta muerto
    public bool isAlive = true;

    private GameManager gameManager;

    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlive)
        {
            //Añade velocidad en el eje x
            rigidBody.velocity = new Vector2(directionX * speed, rigidBody.velocity.y);
        }
        else
        {
            //Detiene el movimiento del goomba
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        //si detecta collision con un objeto con tag Pared o Goombas
        if(hit.gameObject.tag == "Pared" || hit.gameObject.tag == "Goombas")
        {
            Debug.Log("me he chocado");

            //si nos movemos a la derecha cambiara la direccion de movimiento a la izquierda
            if(directionX == 1)
            {
                directionX = -1;
            }
            //si nos movemos a la izquierda la cambia a la derecha
            else
            {
                directionX = 1;
            }

        }
        //si choca con el mario lo destruye
        else if(hit.gameObject.tag == "MuereMario")
        {
            Destroy(hit.gameObject);
            gameManager.DeathMario();
        }
    }

    /*Cada vez que este visible el enemigo que ese objeto(this.gameObject), lo añada a la lista*/
    public void OnBecameVisible()
    {

        gameManager.enemiesinscreen.Add(this.gameObject);

    }

    
    /*Cada vez que no este visible el enemigo que ese objeto(this.gameObject), lo quite a la lista*/
    public void OnBecameInvisible()
    {

        gameManager.enemiesinscreen.Remove(this.gameObject);

    }

}
