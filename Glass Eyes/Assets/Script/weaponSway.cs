using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSway : MonoBehaviour
{
    public float swayPos = 0.09f;
    public float transition = 3f;
    private Vector3 Center; 
    void Start()
    {
        Center = transform.localPosition;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        input.x = Mathf.Clamp(input.x, -swayPos, swayPos);
        input.y = Mathf.Clamp(input.y, -swayPos, swayPos);

        Vector3 target = new Vector3(-input.x, -input.y, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target + Center, Time.deltaTime * transition);
    

    }
}
