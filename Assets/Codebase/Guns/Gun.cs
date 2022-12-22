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

		private ObjectPool<Projectile> _objectPool;

		private void Start()
		{
			_objectPool = new ObjectPool<Projectile>(CreateProjectile, GetProjectile, ReleaseProjectile, DestroyProjectile);
		}

		private void ReleaseProjectile(Projectile obj)
		{
			obj.gameObject.SetActive(false);
		}

		private void GetProjectile(Projectile obj)
		{
			obj.transform.position = muzzle.position;
			obj.transform.rotation = muzzle.rotation;

			obj.gameObject.SetActive(true);
		}

		private Projectile CreateProjectile()
		{
			var obj = Instantiate(projectilePrefab);
			obj.OnRelease += _objectPool.Release;

			return obj;
		}

		private void DestroyProjectile(Projectile obj)
		{
			obj.OnRelease -= _objectPool.Release;
			Destroy(obj);
		}

		public virtual void Shoot()
		{
			_objectPool.Get();
		}
	}
}