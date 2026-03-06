using UnityEngine;

public class MovingPlatformGrabber : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<CharacterController>().transform.SetParent(transform);
            //other.transform.SetParent(transform);
            Debug.Log("player grabbed");
        }
    }
    
    
    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<CharacterController>().transform.SetParent(null);
            //other.transform.SetParent(null);
        }
    }
    //to do: Add points to lerp from 
}
