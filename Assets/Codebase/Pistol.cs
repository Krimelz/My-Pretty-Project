using System.Collections;
using UnityEngine;

namespace Assets.Codebase
{
	public class Pistol : Gun
	{
		public override void Shoot()
		{
			Debug.Log($"Pistol -> {damage}!!!!!!!");
		}
	}
}