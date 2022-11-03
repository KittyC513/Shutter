using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Monster").transform;

        var dir = target.position - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
