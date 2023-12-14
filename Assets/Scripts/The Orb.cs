using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrb : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Material material;
	private bool isMouseOver;
	private float currentSpikes = 0.0f;
	private const float RestingSpikes = 0.025f;
	private const float ActiveSpikes = 0.1f;
	private const float TransitionSpeed = 0.02f;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
		material = meshRenderer.material;
	}

	private void OnMouseEnter()
	{ isMouseOver = true; }

	private void OnMouseExit()
	{ isMouseOver = false; }

	private void Update()
	{
		if (isMouseOver) currentSpikes = Mathf.Lerp(currentSpikes, ActiveSpikes, TransitionSpeed);
		else currentSpikes = Mathf.Lerp(currentSpikes, RestingSpikes, TransitionSpeed);
		material.SetFloat("_Spikes", currentSpikes);
	}
}
