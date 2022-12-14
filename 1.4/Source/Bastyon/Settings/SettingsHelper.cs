using UnityEngine;
using Verse;

namespace Bastyon
{
	[StaticConstructorOnStartup]
	public static class SettingsHelper
	{
		public static bool Settings_Button(this Listing_Standard ls, string label, Rect rect)
		{
			bool result = Widgets.ButtonText(rect, label, true, true, true);
			ls.Gap((2f));
			return result;
		}
		public static void SliderLabeled(this Listing_Standard ls, string label, ref int val, string format, float min = 0f, float max = 100f, string tooltip = null)
		{
			float num = (float)val;
			ls.SliderLabeled(label, ref num, format, min, max, tooltip);
			val = (int)num;
		}

		public static void SliderLabeled(this Listing_Standard ls, string label, ref float val, string format, float min = 0f, float max = 1f, string tooltip = null)
		{
			Rect rect = ls.GetRect(Text.LineHeight);
			Rect rect2 = GenUI.Rounded(GenUI.LeftPart(rect, 0.7f));
			Rect rect3 = GenUI.Rounded(GenUI.LeftPart(GenUI.Rounded(GenUI.RightPart(rect, 0.3f)), 0.67f));
			Rect rect4 = GenUI.Rounded(GenUI.RightPart(rect, 0.1f));
			TextAnchor anchor = Text.Anchor;
			Text.Anchor = TextAnchor.MiddleLeft;
			Widgets.Label(rect2, label);
			float num = Widgets.HorizontalSlider(rect3, val, min, max, true, null, null, null, -1f);
			val = num;
			Text.Anchor = TextAnchor.MiddleRight;
			Widgets.Label(rect4, string.Format(format, val));
			if (!GenText.NullOrEmpty(tooltip))
			{
				TooltipHandler.TipRegion(rect, tooltip);
			}
			Text.Anchor = anchor;
			ls.Gap(ls.verticalSpacing);
		}
	}
}
