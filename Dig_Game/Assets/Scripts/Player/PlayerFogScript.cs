using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFogScript : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    public void Update()
    {
        fieldOfView.SetOrigin(transform.position);
    }

}
