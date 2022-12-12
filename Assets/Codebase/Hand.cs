using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Codebase
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

	public class Hand : SerializedMonoBehaviour
	{
		public Gun gun;

		[Button]
		private void Shoot()
		{
			gun.Shoot();
		}
	}
}