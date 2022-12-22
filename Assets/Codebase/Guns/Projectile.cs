using System;
using UnityEngine;

namespace Codebase.Guns
{
	public class Projectile : MonoBehaviour
	{
		public event Action<Projectile> OnRelease;

		[SerializeField]
		private int damage;
		[SerializeField]
		private float speed;
		[SerializeField]
		private float lifeTime;

		private float elapsedTime;

		private void Update()
		{
			Move();

			if (elapsedTime < lifeTime)
			{
				elapsedTime += Time.deltaTime;
			}
			else
			{
				OnRelease?.Invoke(this);
			}
		}

		private void Move()
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
		}
	}
}