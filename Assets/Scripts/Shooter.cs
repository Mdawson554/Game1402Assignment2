using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootObject;
    [SerializeField] private float shootForce;
    [SerializeField] private float arrowDestroyTime;
    private GameObject _arrow;
    
    private bool canShoot = true;
    

    void OnEnable()
    {
        shootInput.Enable();
        shootInput.performed += Shoot;
    }

    void OnDisable()
    {
        shootInput.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (canShoot)
        {
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, shootPoint.rotation);

            // apply a force
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * shootPoint.forward);
        }
        canShoot = false;
    }

    private IEnumerator DestroyArrow()
    {
        {
            yield return new WaitForSeconds(arrowDestroyTime);
            Destroy(_arrow);
        }
        canShoot =  true;
        yield return  null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyArrow());
    }
}
