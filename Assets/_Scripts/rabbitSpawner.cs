using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitSpawner : MonoBehaviour
{
    public GameObject Rabbit;
    public GameObject Burrow;
    private bool backHome = false;
    private bool canReproduce = false;
    private bool surviveToNextDay = false;
    public int amount;
    private float Home = 0f;

    // Start is called before the first frame update.
    void Start() 
    {
        Debug.Log("New Rabbit Spawned");
        amount = 0;
    }

    // Update is called once per frame.
    void Update()
    {
        // Getting the distance between the rabbit and the burrow.
        float Home = Vector3.Distance (gameObject.transform.position, Burrow.transform.position); // There seems to be a disconnect between this Update Home variable and the Home variable in #TriggerCaller. Fix that ASAP.
    }

    // Triggerenter on food for rabbits
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Carrot") {
            amount++;
            Debug.Log("Rabbit has eaten a carrot");
            Debug.Log(amount);
            if (amount > 2) { // 2 makes survival to be 3 carrots needed.
                surviveToNextDay = true;
                Debug.Log("Rabbit has eaten enough carrots to live");
                if (amount > 3) { // 3 makes reproduction to be 4 carrots needed.
                    canReproduce = true;
                    Debug.Log("Rabbit has eaten enough carrots to reproduce");
                }
            }
        }
    }

    public void TriggerCaller() {
        // When the rabbit gets close to a burrow, backhome will be true.
        if (Home <= 1.35) {
            backHome = true;
            Debug.Log("Backhome = true");
        }

        // Ensures that backhome returns to false after the rabbit leaves.
        if (Home >= 1.5) {
            backHome = false;
            Debug.Log("Backhome = false");
        }

        // Calling reproduce methods.
        if (backHome == true && canReproduce == true) {
            Reproduce();
            //NextDay();
            Debug.Log("Calling reproduce method");
        }
    }
    // Method is called when rabbits reproduce.
    public void Reproduce() {
        Instantiate(Rabbit, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("Rabbit instantiated.");
    }
    //public void NextDay() {
    //    WaitUntil();
    //    //stuff
    //}
}
