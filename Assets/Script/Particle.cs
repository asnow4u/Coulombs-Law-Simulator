using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    private float _mass;
    private float _charge;
    private Rigidbody2D _rb;

    [SerializeField]
    private Material posMaterial;

    [SerializeField]
    private Material negMaterial;

    [SerializeField]
    private Material neutMaterial;


    public void ApplyForce(Vector2 force) {
      // _rb.AddForce(force, ForceMode2D.Impulse);
        _rb.AddForce(force, ForceMode2D.Force);
    }


    public float GetCharge() {
      return _charge;
    }


    public void SetUpParticle(float initMass, float initCharge) {

      _mass = initMass;
      _rb = GetComponent<Rigidbody2D>();
      _rb.mass = _mass;
      _charge = initCharge;

      if (_charge > 0) {

        GetComponent<MeshRenderer>().material = posMaterial;

      } else if (_charge < 0) {

        GetComponent<MeshRenderer>().material = negMaterial;

      } else {

        GetComponent<MeshRenderer>().material = neutMaterial;
      }
    }


    public void OnMouseDown() {

      if (_charge > 0) {
        GetComponent<MeshRenderer>().material = negMaterial;
      }

      else if (_charge < 0) {
        GetComponent<MeshRenderer>().material = posMaterial;
      }

      _charge *= -1;
    }
}
