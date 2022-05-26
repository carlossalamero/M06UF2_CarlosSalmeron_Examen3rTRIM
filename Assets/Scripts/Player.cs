using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI    ;

public class Player : MonoBehaviour
{
    //variables para la velocidad y fuerza de salto
    public float speed = 5f;
    public float jumpForce = 10f;
    //variable para saber si estamos en el suelo
    public bool isGrounded;
    //variable para almacenar el input de movimiento
    float dirX;
    
    //variables de componentes
    public SpriteRenderer spriteRenderer;
    public Animator _animator;
    Rigidbody2D _rBody;
    private GameManager gameManager;

    //prefab de la bala
    public GameObject bulletPref;
    //posicion desde la que se dispara la bala
    public Transform bulletSpawn;

    //posicion del hitbox
    public Transform attackHitBox;
    //rango de ataque
    public float attackRange;
    //layer del enemigo
    public LayerMask enemyLayer; 


    void Awake()
    {
        //asiganamos los componentes a las variables
        _animator = GetComponent<Animator>();
        _rBody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        Debug.Log(dirX);
        
        if(dirX == -1)
        {
            //spriteRenderer.flipX = true;
            //rotamos el personaje 180 grados
            transform.rotation = Quaternion.Euler(0,180,0);
            _animator.SetBool("Running", true);
        }
        else if(dirX == 1)
        {
            //spriteRenderer.flipX = false;
            //volvemos a poner el personaje en su rotacion original
            transform.rotation = Quaternion.Euler(0,0,0);
            _animator.SetBool("Running", true);
            
        }
        else
        {
            _animator.SetBool("Running", false);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            _rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetBool("Jumping", true);
        }
        /*Condición de si pulsamos la tecla correspondiente a fuego1 la bala(bulletPref) ejecutará la función instantiate
        para salir la bala dependiendo de la position y rotación del generador de balas(bulletSpawn.position, bulletSpawn.rotation)*/
        if(Input.GetButtonDown("Fire1"))
        {

              Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);  

        }
    }

    void FixedUpdate()
    {
        _rBody.velocity = new Vector2(dirX * speed, _rBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Si el objeto que entra en el trigger tiene el tag de Goombas
        if(collider.gameObject.tag == "Goombas")
        {
            Debug.Log("Goomba muerto");
            //llamamos a la funcion DeathGoomba del script GameManager
            gameManager.DeathGoomba(collider.gameObject);
        }

        //Si el trigger entra en la deadzone
        if(collider.gameObject.CompareTag("DeadZone"))
        {
            Debug.Log("Estoy muerto");
            gameManager.DeathMario();
        }

        //Si tocamos una moneda
        if(collider.gameObject.tag == "Coin")
        {
            gameManager.Coin(collider.gameObject);
        }
    }
}
