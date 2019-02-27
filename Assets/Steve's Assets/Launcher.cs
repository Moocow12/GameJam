using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public static Launcher Instance;

    public void Singleton()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }


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
	void Awake  () {
        Singleton();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Mouse0) && isGripped == true)         // if we have released the left click this frame, and the grip was being held...
        {
            LaunchProjectile();         // call LaunchProjectile()
            ClearTrajectory();
            powerBar.gameObject.SetActive(false);
            isGripped = false;
            GetComponentInChildren<Grip>().transform.localPosition = new Vector3(0, 0);
        }
        if (Input.GetKey(KeyCode.Mouse0) && isGripped == true)
        {
            CreateTrajectory(dotNum);
            powerBar.gameObject.SetActive(true);
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
        SlotSelector selector = FindObjectOfType<SlotSelector>();
        Item item = selector.GetThrowingItem();
        if (item != null)
        {
            GameObject projectile = Instantiate(potionPrefab, this.transform.position, this.transform.rotation);        // spawn a potionPrefab
            projectile.GetComponent<Projectile>().Initialize(item);
            if (force > 200f) { force = 200f; }
            projectile.GetComponent<Rigidbody2D>().AddForce((angle * (force * -1)), ForceMode2D.Force);         // give the projectile an appropriate force at the appropriate angle

            float torque = Random.Range(-3f, 3f);
            projectile.GetComponent<Rigidbody2D>().AddTorque(torque);           // add a little random spin (looks nice!)
        }
    }

    private void CalculateProjectileVector()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        position = Camera.main.ScreenToWorldPoint(position);
        position = position - this.gameObject.transform.position;

        force = (Mathf.Abs(position.x) + Mathf.Abs(position.y)) * forceMultiplyer;          // the absolute value of position.x and position.y is the value for force
        if (force > 200f) { force = 200f; }
        powerBar.value = force;         // display force on a slider
        //print("Current force: " + force);

        angle = position;
        //print(angle);
    }

    private Vector3 CalculatePosition(float elapsedTime)
    {
        if (force > 200f) { force = 200f; }
        return Physics.gravity * elapsedTime * elapsedTime * .5f + (angle * (force * -trajectoryModifier)) * elapsedTime + this.transform.position;
    }

    public void ChangeProjectile(GameObject prefab)
    {
        potionPrefab = prefab;
    }
}
