using System;
using UnityEngine;
using Verse;

namespace MSFS_Code;

public class Settings : ModSettings
{
    public bool component;
    public int steelAmount = 20;
    public int workAmount = 1600;

    public void DoWindowContents(Rect canvas)
    {
        var list = new Listing_Standard
        {
            ColumnWidth = canvas.width
        };
        list.Begin(canvas);
        list.Gap();
        steelAmount = (int)list.SliderLabeled("MSFS.SteelAmountNew".Translate(steelAmount), steelAmount, 10f, 300.99f,
            tooltip: "MSFS.SteelAmountTipNew".Translate());
        list.Gap();
        workAmount = (int)list.SliderLabeled("MSFS.WorkAmountNew".Translate(Math.Ceiling(workAmount / 60f)), workAmount,
            575f, 2400f,
            tooltip: "MSFS.WorkAmountTipNew".Translate());
        list.Gap();
        list.CheckboxLabeled("MSFS.Component".Translate(), ref component);
        if (list.ButtonText("MSFS.Reset".Translate()))
        {
            steelAmount = 20;
            workAmount = 1600;
            component = false;
        }

        if (Controller.CurrentVersion != null)
        {
            list.Gap();
            GUI.contentColor = Color.gray;
            list.Label("MSFS.CurrentModVersion".Translate(Controller.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        list.End();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref steelAmount, "steelAmount", 20);
        Scribe_Values.Look(ref workAmount, "workAmount", 1600);
        Scribe_Values.Look(ref component, "component");
    }
}