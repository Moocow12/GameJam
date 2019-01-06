using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public GameObject potionPrefab;
    public Slider powerBar;
    public GameObject trajectoryDotPrefab;
    private const float forceMultiplyer = 40f;
    private const float trajectoryModifier = .0165f;
    public int dotNum = 8;

    private bool isGripped = false;
    public void SetGripped(bool value) { isGripped = value; }
    private float force = 1f;
    private Vector3 angle;
    List<GameObject> dots = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Mouse0) && isGripped == true)         // if we have released the left click this frame, and the grip was being held...
        {
            LaunchProjectile();         // call LaunchProjectile()
            ClearTrajectory();
            isGripped = false;
            GetComponentInChildren<Grip>().transform.localPosition = new Vector3(0, 0);
        }
        if (Input.GetKey(KeyCode.Mouse0) && isGripped == true)
        {
            CreateTrajectory(dotNum);
        }
        
        CalculateProjectileVector();        // call CaluculateProjectileVector()
	}

    private void CreateTrajectory(int dotCount)
    {
        ClearTrajectory();
        for (int i = 0; i < dotCount; i++)
        {
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
            dots.Add(trajectoryDot);
            trajectoryDot.transform.position = CalculatePosition(.2f * i);
        }
    }

    private void ClearTrajectory()
    {
        foreach (GameObject dot in dots)
        {
            Destroy(dot.gameObject);
        }
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(potionPrefab, this.transform.position, this.transform.rotation);        // spawn a potionPrefab
        projectile.GetComponent<Rigidbody2D>().AddForce((angle * (force * -1)), ForceMode2D.Force);         // give the projectile an appropriate force at the appropriate angle

        float torque = Random.Range(-3f, 3f);
        projectile.GetComponent<Rigidbody2D>().AddTorque(torque);           // add a little random spin (looks nice!)
    }

    private void CalculateProjectileVector()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        position = Camera.main.ScreenToWorldPoint(position);
        position = position - this.gameObject.transform.position;

        force = (Mathf.Abs(position.x) + Mathf.Abs(position.y)) * forceMultiplyer;          // the absolute value of position.x and position.y is the value for force
        force = Mathf.Clamp(force, 0f, 200f);       // limit the max amount of force to launch with
        powerBar.value = force;         // display force on a slider
        //print("Current force: " + force);

        angle = position;
        //print(angle);
    }

    private Vector3 CalculatePosition(float elapsedTime)
    {
        return Physics.gravity * elapsedTime * elapsedTime * .5f + (angle * (force * -trajectoryModifier)) * elapsedTime + this.transform.position;
    }
}
