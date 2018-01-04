using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	const float skinWidth = 0.015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D boxCollider;
	RayCastOrigins raycastOrigins;

	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
	}

	void Update () {
		UpdateRayCastOrigins ();
		CalculateRaySpacing ();

		for (int i = 0; i < verticalRayCount; i++) {
			Debug.DrawRay (raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.down, Color.red);
		}

		for (int i = 0; i < horizontalRayCount; i++) {
			Debug.DrawRay (raycastOrigins.topRight + Vector2.down * horizontalRaySpacing * i, Vector2.right, Color.red);
		}
	}

	void UpdateRayCastOrigins() {
		Bounds bounds = boxCollider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing () {
		Bounds bounds = boxCollider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RayCastOrigins {
		public Vector2 topLeft, topRight, bottomLeft, bottomRight;
	}
}
