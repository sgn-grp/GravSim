using UnityEngine;

public class moveObject : MonoBehaviour
{
    public Vector3 velocity;
    public float inx = 0.0f;
    public float iny = 0.0f;
    public float inz = 0.0f;
    public float mass = 1f;

    public void setvelocity()
    {
        velocity = new Vector3(inx, iny, inz);
    }
    void Update()
    {
        transform.position += (velocity * Time.deltaTime);
    }
}
