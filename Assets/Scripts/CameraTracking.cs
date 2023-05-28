using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private Transform player = null;

    
    void Update()
    {
        this.transform.rotation = player.rotation;
        this.transform.position = player.position;
    }
}
