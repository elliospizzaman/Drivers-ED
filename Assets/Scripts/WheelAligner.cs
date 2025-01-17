﻿using UnityEngine;

/// <summary>
/// This script aligns a wheel's position to represent the suspension and match the
/// cooresponding WheelCollider. It will also match that collider's roation. This
/// script is meant to be used with any car that is powered by the WheelColliders.
/// </summary>
public class WheelAligner : MonoBehaviour {
	public WheelCollider CorrespondingCollider;

	float CurrentRotation = 0f;
	
	void Update () {
		// ===== Update Suspension =====
		RaycastHit hit;
		// Get Wheel Collider's center
		Vector3 ColliderCenterPoint = CorrespondingCollider.transform.TransformPoint(CorrespondingCollider.center);
		// Cast a ray out from the wheel collider's center the distance of the suspension and set transform position
		if (Physics.Raycast(ColliderCenterPoint, -CorrespondingCollider.transform.up, out hit, CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius)) {
			transform.position = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
		} else {
			transform.position = ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
		}

		// =====Update Rotation =====
		transform.rotation = CorrespondingCollider.transform.rotation * Quaternion.Euler (CurrentRotation, CorrespondingCollider.steerAngle, 0);
		CurrentRotation += CorrespondingCollider.rpm * (360 / 60) * Time.deltaTime;
	}
}
