using UnityEngine;

public class BatmanController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;

    void Update()
    {
        // چرخش چپ/راست
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);

        // حرکت جلو/عقب
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical * moveSpeed * Time.deltaTime;

        transform.Translate(move, Space.World);
    }
}
