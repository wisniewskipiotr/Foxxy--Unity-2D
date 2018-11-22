using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject Camera;
    public GameObject Player;
    private float CameraZOffset;
    private void FixedUpdate()
    {
        var PlayerPosition = Player.transform.position;
        var CameraPosition = new Vector3(PlayerPosition.x, PlayerPosition.y, this.CameraZOffset);
        Camera.transform.position = CameraPosition;
        Debug.Log(PlayerPosition);
    }
    // Use this for initialization
    void Start ()
    {
        this.CameraZOffset = Camera.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
