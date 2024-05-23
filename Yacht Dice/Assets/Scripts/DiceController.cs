using UnityEngine;

public class DiceController : MonoBehaviour
{

    private static Rigidbody rb;
    public Vector3 diceVelocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        diceVelocity = rb.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.diceNumber = 0;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(Vector3.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);

        }
    }
}
