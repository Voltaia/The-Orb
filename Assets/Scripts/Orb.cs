using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Material material;
	private float currentSpikes;
	private float desiredSpikes;
	private float currentExcitement;
	private float desiredExcitement;
	private float transitionSpeed;
	private const float RestingSpikes = 0.025f;
	private const float ActiveSpikes = 0.1f;
	private const float RestingSpeed = 5.0f;
	private const float ExcitedSpeed = 2.5f;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
		material = meshRenderer.material;
	}

	private void Start()
	{
		currentSpikes = ActiveSpikes;
		desiredSpikes = RestingSpikes;

		currentExcitement = 0.0f;
		desiredExcitement = 0.0f;

		transitionSpeed = RestingSpeed;
	}

	private void Update()
	{
		currentSpikes = Mathf.Lerp(currentSpikes, desiredSpikes, transitionSpeed * Time.deltaTime);
		material.SetFloat("_Spikes", currentSpikes);

		currentExcitement = Mathf.Lerp(currentExcitement, desiredExcitement, transitionSpeed * Time.deltaTime);
		material.SetFloat("_Excitement", currentExcitement);

		if (currentExcitement > 0.99f)
		{
			desiredExcitement = 0.0f;
			desiredSpikes = RestingSpikes;
		}

		if (currentExcitement < 0.01f && desiredExcitement < 0.01f) transitionSpeed = RestingSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{ desiredSpikes = ActiveSpikes; }

	private void OnTriggerStay(Collider other)
	{
		Energy energy = other.GetComponentInParent<Energy>();
		if (energy != null && !energy.isGrabbed)
		{
			Destroy(energy.gameObject);
			desiredExcitement = 1.0f;
			desiredSpikes = ActiveSpikes * 2;
			transitionSpeed = ExcitedSpeed;
		}
	}

	private void OnTriggerExit(Collider other)
	{ desiredSpikes = RestingSpikes; }
}
