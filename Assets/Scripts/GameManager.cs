using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //MAnagers de sonido
    private SFXManager sfxManager;
    private BGMManager bgmManager;

    //variable para almacenar cantidad de monedas
    private int coins;
    //variable para el texto de monedas del canvas
    public Text coinsText;

    //Función booleana para determinar si el personaje está jugando
    public bool misplaying = true;

    //Creación de la lista
    public List <GameObject> enemiesinscreen = new List<GameObject>(); 

    void Awake()
    {
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    /*Cada vez que se actualicen los frames y si presionamos en mi caso la tecla T, que
    ejecute la función Matar a todos los enemigos*/
    void Update()
    {
      
      if(Input.GetKeyDown(KeyCode.T))
      {

         Mataratodos();

      }
    }
    
    /*En la función para matar a todos es, por cada enemigo que este en la lista que lo destruya, siempre
    y cuando se ejecute en la función de arriba*/
    void Mataratodos()
    {

        foreach(GameObject enemy in enemiesinscreen)
        {

            Destroy(enemy.gameObject);

        }

    }
    //Funcion para matar a Mario
    public void DeathMario()
    {
        //Reproducimos sonido de muerte
        sfxManager.DeathSound();
        //Paramos la BGM
        bgmManager.StopBGM();
        //Lllamamos la funcion de cargar el menu principal despues de 3 segundos
        Invoke("LoadMainMenu", 3);
    }

    //Funcion para cargar el menu principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Funcion para matar goombas
    public void DeathGoomba(GameObject goomba)
    {
        //variable para el animator del goomba
        Animator goombaAnimator;
        //variable para el script del goomba
        Enemy goombaScript;
        //variable para el collider
        BoxCollider2D goombaCollider;

        //asignamos las variable
        goombaScript = goomba.GetComponent<Enemy>();
        goombaAnimator = goomba.GetComponent<Animator>();
        goombaCollider = goomba.GetComponent<BoxCollider2D>();

        //cambiamos a la animacion de muerte
        goombaAnimator.SetBool("GoombaDeath", true);

        //cambiamos la variable del goomba a false
        goombaScript.isAlive = false;

        //desactivo el collider
        goombaCollider.enabled = false;

        //destruimos el goomba
        Destroy(goomba, 0.3f);

        //llamamos la funcion del sonido de muerte del goomba
        sfxManager.GoombaSound();
    }

    //Funcion para recoger monedas
    public void Coin(GameObject moneda)
    {
        //Destruimos la moneda
        Destroy(moneda);
        //Reproducimos sonido
        sfxManager.MonedaSound();
        //Sumamos 1 al contador de monedas
        coins++;
        //Actualizamos el texto de la UI
        coinsText.text = "Coins: " + coins;
    }
}
