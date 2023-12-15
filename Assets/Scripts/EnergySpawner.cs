using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnergySpawner : MonoBehaviour
{
	public Orb orb;
	public GameObject orbPrefab;

	private void Start()
	{
		transform.LookAt(orb.transform.position);
		SpawnEnergy();
	}

	private async void SpawnEnergy()
	{
		while (true)
		{
			await Task.Delay(2000);
			GameObject orbGameobject = Instantiate(orbPrefab, transform.position, Quaternion.identity);
			Rigidbody orbRigidbody = orbGameobject.GetComponent<Rigidbody>();
			orbRigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
		}
	}
}
