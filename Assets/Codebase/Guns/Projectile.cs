using System;
using UnityEngine;

namespace Codebase.Guns
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Projectile : MonoBehaviour
	{
		public event Action<Projectile> OnRelease;

		[SerializeField]
		protected int damage;
		[SerializeField]
		protected float speed;
		[SerializeField]
		protected float lifeTime;

		protected Rigidbody _rigidbody;

		private float _elapsedTime;

		public abstract void AddForce();

		public void ResetVelocity()
		{
			_rigidbody.velocity = Vector3.zero;
		}

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			if (_elapsedTime < lifeTime)
			{
				_elapsedTime += Time.deltaTime;
			}
			else
			{
				OnRelease?.Invoke(this);
				_elapsedTime = 0f;
			}
		}
	}
}