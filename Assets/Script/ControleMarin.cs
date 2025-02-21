using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControleMarin : MonoBehaviour
{
    // variables de mouvement et contrôle
    [SerializeField] private float _vitessePromenade;
    [SerializeField] private float _vitessePromenadeEX;
    private Rigidbody _rb;
    private Vector3 directionInput;

    // variables de contrôle d'animation
    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;
    private float _rotationVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    void OnProme(InputValue directionBase)
    {
        Debug.Log("Avancer");
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade * _vitessePromenadeEX;
        directionInput = new Vector3(0f, directionAvecVitesse.x, directionAvecVitesse.y);

        if (directionAvecVitesse.x > 0)
        {
            _animator.SetFloat("VitesseV", 1);
        }

        if (directionAvecVitesse.x < 0)
        {
            _animator.SetFloat("VitesseV", -1);
        }

        if (directionAvecVitesse.x == 0)
        {
            _animator.SetFloat("VitesseV", 0);
        }

        if (directionAvecVitesse.y > 0)
        {
            _animator.SetFloat("VitesseH", 1);
        }

        if (directionAvecVitesse.y < 0)
        {
            _animator.SetFloat("VitesseH", -1);
        }

        if (directionAvecVitesse.y == 0)
        {
            _animator.SetFloat("VitesseH", 0);
        }


    }

    void OnBoost(InputValue monBouton)
    {
        Debug.Log("Smash " + monBouton.isPressed);
        if (monBouton.isPressed)
        {
            _vitessePromenadeEX = 2f;
            _animator.SetBool("Boost", true);
        }
        else
        {
            _vitessePromenadeEX = 1f;
            _animator.SetBool("Boost", false);
        }
    }

   
    void FixedUpdate()
    {
        // calculer et appliquer la translation
        Vector3 mouvement = directionInput;
        // si on a une direction d'input
        
        // appliquer la vitesse de translation
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        // calculer un modifiant pour la vitesse d'animation
        Vector3 vitesseSurPlaneX = new Vector3(0f, _rb.velocity.y, 0f);
        Vector3 vitesseSurPlaneY = new Vector3(0f, 0f, _rb.velocity.z);

        _animator.SetFloat("DeplacementV", vitesseSurPlaneY.magnitude);
        
        _animator.SetFloat("DeplacementH", vitesseSurPlaneX.magnitude);
    }



}

