using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class EnergySpawner : MonoBehaviour
{
	public Orb orb;
	public GameObject orbPrefab;
	public float spawnRadius;
	public float speedMin;
	public float speedMax;
	public float torqueMin;
	public float torqueMax;
	public int delayMin;
	public int delayMax;

	private void Start()
	{
		transform.LookAt(orb.transform.position);
		StartCoroutine(SpawnEnergy());
	}

	private IEnumerator SpawnEnergy()
	{
		while (true)
		{
			// Cooldown
			yield return new WaitForSeconds(Random.Range(delayMin, delayMax));

			// Trajectory
			Vector3 locationOffset = Random.insideUnitCircle.normalized;
			Vector3 location = transform.position + locationOffset * spawnRadius;
			Vector3 direction = (transform.position - location).normalized;
			Vector3 adjustedDirection = (direction + Random.insideUnitSphere * 0.5f).normalized;
			Vector3 force = adjustedDirection * Random.Range(speedMin, speedMax);

			// Creation
			GameObject orbGameobject = Instantiate(orbPrefab, location, Quaternion.identity);
			Rigidbody orbRigidbody = orbGameobject.GetComponent<Rigidbody>();
			orbRigidbody.AddForce(force, ForceMode.Impulse);
			orbRigidbody.AddTorque(Random.insideUnitCircle * Random.Range(0.25f, 1.5f));
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, spawnRadius);
	}
}