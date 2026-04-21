using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth = .7f;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position,
                                                   new Vector3(target.transform.position.x,target.transform.position.y, -10),
                                                   smooth * Time.deltaTime);
        }
    }
}
