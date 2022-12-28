using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Codebase.Guns
{
	public abstract class Gun : MonoBehaviour, IGun
	{
		[SerializeField]
		private Transform muzzle;
		[SerializeField]
		private Projectile projectilePrefab;

		protected ObjectPool<Projectile> _projectilePool;

		private void Start()
		{
			_projectilePool = new ObjectPool<Projectile>(
				CreateProjectile, 
				GetProjectile, 
				ReleaseProjectile, 
				DestroyProjectile
			);
		}

		public virtual void Shoot()
		{
			_projectilePool.Get();
		}

		private Projectile CreateProjectile()
		{
			var obj = Instantiate(projectilePrefab);
			obj.OnRelease += _projectilePool.Release;

			return obj;
		}

		private void GetProjectile(Projectile obj)
		{
			obj.transform.position = muzzle.position;
			obj.transform.rotation = muzzle.rotation;

			obj.gameObject.SetActive(true);
		}

		private void ReleaseProjectile(Projectile obj)
		{
			obj.ResetVelocity();
			obj.gameObject.SetActive(false);
		}

		private void DestroyProjectile(Projectile obj)
		{
			obj.OnRelease -= _projectilePool.Release;
			Destroy(obj);
		}
	}
}