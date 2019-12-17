using UnityEngine;

namespace UI
{
    internal sealed class RadarRotate : MonoBehaviour 
    {
        // https://blog.theknightsofunity.com/implementing-minimap-unity/
        //
        private void Update() 
        {
            transform.eulerAngles += new Vector3(0, 0, -180f * Time.deltaTime * 0.75f);
        }
    }
}
