using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleController : MonoBehaviour
{
    const long k = 9 * 10^9;

    [SerializeField]
    private GameObject massInputField;

    [SerializeField]
    private GameObject chargeInputField;

    [SerializeField]
    private GameObject particlePrefab;

    private List<GameObject> _particleList = new List<GameObject>();

    private float _childMass;
    private float _childCharge;


    void Start() {

      GameObject[] startParticles = GameObject.FindGameObjectsWithTag("Particle");

      foreach (GameObject particle in startParticles) {
        particle.GetComponent<Particle>().SetUpParticle(1, Random.Range(-0.5f, 0.5f));
        _particleList.Add(particle);
      }
    }


    void FixedUpdate() {

      if (_particleList.Count > 0) {

        //For each element in the list compare with other elements and apply force to associated particle
        for (int i=0; i<_particleList.Count-1; i++) {

          float charge1 = _particleList[i].GetComponent<Particle>().GetCharge();

          for (int j=i+1; j<_particleList.Count; j++) {

            //Determine distance between two particles
            float distance = Vector3.Distance(_particleList[i].transform.position, _particleList[j].transform.position);

            float charge2 = _particleList[j].GetComponent<Particle>().GetCharge();

            //Force = k * ((q1 * q2) / r^2)
            float force = k * (( charge1 * charge2 ) / Mathf.Pow(distance,2) );

            //Determine Direction
            Vector2 direction = new Vector2(_particleList[i].transform.position.x, _particleList[i].transform.position.y) - new Vector2(_particleList[j].transform.position.x, _particleList[j].transform.position.y);
            direction.Normalize();

            //Apply Force to paricles
            _particleList[i].GetComponent<Particle>().ApplyForce(direction * force);
            _particleList[j].GetComponent<Particle>().ApplyForce(-direction * force);
          }
        }
      }
    }


    public void SpawnParticle() {

      _childMass = float.Parse(massInputField.GetComponent<TMP_InputField>().text);
      _childCharge = float.Parse(chargeInputField.GetComponent<TMP_InputField>().text);

      //Check that mass is greater then 0
      if (_childMass > 0) {

        //Spawn new particle in a random location around the center to prevent particles from stacking
        GameObject childParticle = Instantiate(particlePrefab, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-2, 4), 0), Quaternion.identity);
        childParticle.transform.parent = gameObject.transform;
        childParticle.GetComponent<Particle>().SetUpParticle(_childMass, _childCharge);
        _particleList.Add(childParticle);
      }
    }
}
