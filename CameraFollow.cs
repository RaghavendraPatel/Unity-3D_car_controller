using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform car;
    
    public Vector3 offset;
    void FixedUpdate()
    {
        Vector3 _targetPos = car.position +
                            car.forward * offset.z +
                            car.right * offset.x +
                            car.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, 0.3f);
        transform.rotation = Quaternion.Slerp(transform.rotation,car.rotation,0.3f);
        transform.rotation = Quaternion.Euler(new Vector3(0,transform.rotation.eulerAngles.y,0));
    }
}
