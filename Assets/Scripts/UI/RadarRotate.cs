using UnityEngine;

internal sealed class RadarRotate : MonoBehaviour 
{

    private void Update() 
    {
        transform.eulerAngles += new Vector3(0, 0, -180f * Time.deltaTime * 0.75f);
    }

}
