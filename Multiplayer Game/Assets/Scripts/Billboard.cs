using System.Collections;
using UnityEngine;

namespace S3
{
    public class Billboard : MonoBehaviour{

        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
