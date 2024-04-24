using UnityEngine;
using System.Collections;

public class boardScript : MonoBehaviour {
	public int boards, previousBoards;
	public Animator[] boardAnim;
	public GameObject[] board;
	public AudioClip repairSound;
	public AudioClip bangSound;
	public AudioSource audio;
	

	// Use this for initialization
	void Start () {

        audio = GetComponent<AudioSource>();

        boardAnim = GetComponentsInChildren<Animator> ();
		for(int i = 0;i<6;i++)
		{
			boardAnim[i].Play("boardAnimation" + (i+1).ToString());
		}
		boards = 6;

	}
	
	/*
	// Update is called once per frame
	void Update () {
		if(boards !=previousBoards)
		{
			//previousBoards = boards;
			//boardAnim[boards-1].Play("boardAnimation" + boards.ToString());

			switch(boards)
			{
			case 1:
				boardAnim[0].Play("boardAnimation1");
				return;
			case 2:
				boardAnim[1].Play("boardAnimation2");
				return;
			case 3:
				boardAnim[2].Play("boardAnimation3");
				return;
			case 4:
				boardAnim[3].Play("boardAnimation4");
				return;
			case 5:
				boardAnim[4].Play("boardAnimation5");
				return;
			case 6:
				boardAnim[5].Play("boardAnimation6");
				return;
			}
		}
	}

	*/

	public void AddBoard()
	{
		if (boards < 6)
		{
			board[boards].SetActive(true);

			//board[boards].SendMessage("EnableBoard",SendMessageOptions.RequireReceiver);
			//board[boards].renderer.enabled = true;
			boardAnim[boards].Play("boardAnimation" + (boards+1).ToString()); //"boardAnimation1"
			boards += 1;
			audio.PlayOneShot(repairSound, 1.0f / audio.volume);
			Invoke("SlamSound", 1f);
		}
	}

    public void RemoveBoard()
    {
        if (boards > 0)
        {
            board[boards - 1].SendMessage("DisableBoard", SendMessageOptions.RequireReceiver);
            boards -= 1;

            if (boards == 0)
            {
                Renderer renderer = GetComponent<Renderer>(); // Obtén el componente Renderer
                if (renderer != null) // Verifica si el componente Renderer existe
                {
                    renderer.enabled = false;
                }
                else
                {
                    Debug.LogWarning("No se encontró el componente Renderer.");
                }
            }
        }
    }

	public void SlamSound()
	{
		audio.PlayOneShot(bangSound, 1.0f / audio.volume);
	}

}
