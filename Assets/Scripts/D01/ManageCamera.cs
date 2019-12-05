using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCamera : MonoBehaviour {

    private GameManager instance;

    private GameObject	red;
	private GameObject	yellow;
	private GameObject	blue;

    private GameObject  currentPlayer;

    public Vector3      offset;

    public Camera       camera;

    public void setupCamera(GameObject red, GameObject yellow, GameObject blue)
    {
        this.red = red;
        this.yellow = yellow;
        this.blue = blue;

        red.GetComponent<PlayerScript_ex00>().active = true;
        yellow.GetComponent<PlayerScript_ex00>().active = false;
        blue.GetComponent<PlayerScript_ex00>().active = false;

        yellow.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        blue.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        currentPlayer = red;
        offset = new Vector3(0, 2, -10);
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            changePlayer(red);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            changePlayer(yellow);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            changePlayer(blue);
    }

    private void changePlayer(GameObject newPlayer)
    {
        currentPlayer.GetComponent<PlayerScript_ex00>().active = false;
        newPlayer.GetComponent<PlayerScript_ex00>().active = true;

        newPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        currentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        currentPlayer = newPlayer;
    }

	private void LateUpdate(){
        camera.transform.position = currentPlayer.transform.position + offset;
	}
}
