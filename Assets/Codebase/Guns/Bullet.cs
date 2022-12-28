using Codebase.Guns;
using UnityEngine;

namespace Assets.Codebase.Guns
{
	public class Bullet : Projectile
	{
		public override void AddForce()
		{
			_rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
		}
	}
}