namespace Codebase.Guns
{
	public class Pistol : Gun
	{
		public override void Shoot()
		{
			var projectile = _projectilePool.Get();
			projectile.AddForce();
		}
	}
}