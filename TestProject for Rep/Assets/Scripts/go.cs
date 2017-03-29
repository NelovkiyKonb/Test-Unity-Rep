using UnityEngine;
using System.Collections;

public class go : MonoBehaviour {
    public float speed = 3f;
    public bool isMove = false;
    private float fastspeed;

	// Use this for initialization
	void Start () {

        fastspeed = speed;
	
	}
	
	// Update is called once per frame
	void Update () {

        float transV = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float transH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            fastspeed = 6;
        else
            fastspeed = speed;

        if (transH != 0 || transV != 0)
        {
            isMove = true;
        }
           
        else
            isMove = false;

        transform.Translate(new Vector3(transH, transV, 0));


    }
}
