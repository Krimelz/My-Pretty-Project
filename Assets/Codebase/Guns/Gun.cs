using UnityEngine;

namespace Codebase.Guns
{
	public abstract class Gun : MonoBehaviour, IGun
	{
		public new string name;
		public float damage;

		public virtual void Shoot()
		{
			Debug.Log($"{name} -> {damage}");
		}
	}
}