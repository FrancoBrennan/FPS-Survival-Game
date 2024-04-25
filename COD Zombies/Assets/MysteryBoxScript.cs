using UnityEngine;
using System.Collections;

public class MysteryBoxScript : MonoBehaviour {
	Animator controller;
	Animation animate;
	public bool openBox = false, boxIsOpen, canTakeWeapon, triggerWeapon;

	public GameObject[] guns;
	public int[] gunIndex;
	public int mysteryBoxPrice = 950;
	public bool showMysteryBoxGUI;
	public GUISkin mySkin;
	public AudioClip boxMusic;
	int weaponIndex;
	public Puntaje killsPantalla;

	public int selectedWeapon = 0;
	public float timer;
	public int counter, counterCompare;
	public Transform gunPosition;

	public PickUpText2 pickUpText;
    public OpenBoxText openBoxText;

	public bool botonApretado=true;
	//public MostrarBoton botonComprar;
	//public MostrarBoton botonCambiarArma;

    // Use this for initialization
    void Start() {
		controller = GetComponentsInChildren<Animator>()[0];
		animate = GetComponentsInChildren<Animation>()[0];

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (openBox)
		{
			openBox = false;
			OpenMysteryBox();
			canTakeWeapon = false;
			triggerWeapon = true;
		}
		if ((controller.GetCurrentAnimatorStateInfo(0).IsName("LidOpen")) || animate.IsPlaying("liftAnim"))
		{
			timer += Time.deltaTime;
			boxIsOpen = true;
			if (timer < 4.0f && counter < counterCompare)
			{
				counter++;

			}
			else if (counter == counterCompare)
			{
				counter = 0;
				RandomizeWeapon();
				counterCompare++;
			}
			else if (triggerWeapon) {
				canTakeWeapon = true;
				triggerWeapon = false;
			}
			guns[selectedWeapon].transform.position = gunPosition.transform.position;
		}
		else if (boxIsOpen)
		{
			CloseLid();
			counter = 0;
			counterCompare = 0;
			timer = 0;
			if (!animate.IsPlaying("liftAnim") && !animate.IsPlaying("LidClose"))
			{
				boxIsOpen = false;
			}

			//Closed();

		}
		//else if(boxIsClosing)
		//{
		/*if(!controller.animation.IsPlaying("LidClose"))
		{
			boxIsClosing = false;
			boxIsOpen = false;

		}*/
		//}
	}

	public void OpenMysteryBox()
	{
		OpenLid();
		RunGunMovement();
		boxIsOpen = true;
		GetComponent<AudioSource>().clip = boxMusic;
		GetComponent<AudioSource>().Play();
	}

	void OpenLid()
	{
		controller.Play("LidOpen");
	}

	void CloseLid()
	{
		controller.Play("LidClose");
		//boxIsClosing = true;
	}

	void Closed()
	{
		controller.Play("Closed");
	}

	
	void RunGunMovement()
	{
		animate.Play ();
	}
	void RandomizeWeapon()
	{

		int rand = Random.Range(0, guns.Length);
		while(rand == selectedWeapon)
		{
			rand = Random.Range(0, guns.Length);
		}
		selectedWeapon = rand;


		for(int i = 0; i < guns.Length; i++)
		{
			if(i != selectedWeapon)
				guns[i].SetActive(false);
		}
		guns[selectedWeapon].SetActive(true);
		guns[selectedWeapon].transform.position = gunPosition.transform.position;
	}
	
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
		{
			
			if(!boxIsOpen) {
				openBoxText.MostrarTexto();
				//botonComprar.Mostrar();
            }

			if(Input.GetKeyDown("e") /*botonApretado==true*/ && killsPantalla.GetComponent<Puntaje>().puntaje.valor >= mysteryBoxPrice && !boxIsOpen && !openBox)
			{
				openBoxText.QuitarTexto();
                //botonComprar.Quitar();
                openBox = true;
                killsPantalla.GetComponent<Puntaje>().puntaje.valor -= mysteryBoxPrice;
				//botonCambiarArma.Mostrar();
				pickUpText.MostrarTexto();
				botonApretado = false;
            }

			if(canTakeWeapon && Input.GetKeyDown("e") /*botonApretado==true*/) {
                //botonCambiarArma.Quitar();
				pickUpText.QuitarTexto();
                ControladorDeArmas controlador = FindObjectOfType<ControladorDeArmas>();

				controlador.ReiniciarArmaActual();
				controlador.CambiarArmaActual(gunIndex[selectedWeapon]);

				for(int i = 0; i < guns.Length; i++)
				{
					guns[i].SetActive(false);
				}

				
				CloseLid();
                canTakeWeapon = false;
				botonApretado=false;

            }

			if (canTakeWeapon)
			{
                pickUpText.MostrarTexto();
                //botonCambiarArma.Mostrar();
            }
		}
    }

    public void BotonApretado()
    {
        botonApretado = true;
    }

    public void BotonSuelto()
    {
        botonApretado = false;
    }

    void OnTriggerExit(Collider other){
		if (other.CompareTag("Player"))
		{
            //showMysteryBoxGUI = false;
			//botonCambiarArma.Quitar();
            //botonComprar.Quitar();
			pickUpText.QuitarTexto();
			openBoxText.QuitarTexto();

            canTakeWeapon = false;

        }
    }
}
