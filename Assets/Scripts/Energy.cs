using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Energy : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Material material;
	private new Rigidbody rigidbody;
	private bool isMouseOver;
	private float currentExcitement;
	private float targetExcitement;
	private const float TransitionSpeed = 3.5f;
	public bool isGrabbed;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
		material = meshRenderer.material;
		rigidbody = GetComponentInChildren<Rigidbody>();
	}

	private void Start()
	{
		StartCoroutine(Decay());
	}

	private void Update()
	{
		if (isGrabbed)
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 2;
			Vector3 mouseWorldPosition = Vector3.Scale(Camera.main.ScreenToWorldPoint(mousePosition), new Vector3(1, 1, 0));
			transform.position = Vector3.Lerp(transform.position, mouseWorldPosition, 5.0f * Time.deltaTime);
			if (Input.GetMouseButtonUp(0)) LetGo();
		}
		else
		{
			if (isMouseOver)
			{
				targetExcitement = 0.5f;
				if (Input.GetMouseButtonDown(0)) Grab();
			}
			else targetExcitement = 0.0f;
		}

		currentExcitement = Mathf.Lerp(currentExcitement, targetExcitement, TransitionSpeed * Time.deltaTime);
		material.SetFloat("_Excitement", currentExcitement);
	}

	private void Grab()
	{
		isGrabbed = true;
		rigidbody.isKinematic = true;
		targetExcitement = 1.0f;
	}

	private void LetGo()
	{
		isGrabbed = false;
		rigidbody.isKinematic = false;
		targetExcitement = 0.5f;
	}

	private IEnumerator Decay()
	{
		while (true)
		{
			yield return new WaitForSeconds(5);
			if (!meshRenderer.isVisible) Destroy(gameObject);
		}
	}

	private void OnMouseEnter()
	{ isMouseOver = true; }

	private void OnMouseExit()
	{ isMouseOver = false; }
}
