using Verse;

namespace VFECore
{
	public class BastyonBaseExpandingProjectile : ExpandableProjectile
	{
		public override void DoDamage(IntVec3 pos)
		{
			base.DoDamage(pos);
			try
			{
				if (pos != launcher.Position && launcher.Map != null && GenGrid.InBounds(pos, launcher.Map))
				{
					var list = launcher.Map.thingGrid.ThingsListAt(pos);
					for (int num = list.Count - 1; num >= 0; num--)
					{
					
					}
				}
			}
			catch { };
		}
	}
}