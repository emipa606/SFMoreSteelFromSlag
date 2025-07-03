using System;
using UnityEngine;
using Verse;

namespace MSFS_Code;

public class Settings : ModSettings
{
    public bool Component;
    public int SteelAmount = 20;
    public int WorkAmount = 1600;

    public void DoWindowContents(Rect canvas)
    {
        var list = new Listing_Standard
        {
            ColumnWidth = canvas.width
        };
        list.Begin(canvas);
        list.Gap();
        SteelAmount = (int)list.SliderLabeled("MSFS.SteelAmountNew".Translate(SteelAmount), SteelAmount, 10f, 300.99f,
            tooltip: "MSFS.SteelAmountTipNew".Translate());
        list.Gap();
        WorkAmount = (int)list.SliderLabeled("MSFS.WorkAmountNew".Translate(Math.Ceiling(WorkAmount / 60f)), WorkAmount,
            575f, 2400f,
            tooltip: "MSFS.WorkAmountTipNew".Translate());
        list.Gap();
        list.CheckboxLabeled("MSFS.Component".Translate(), ref Component);
        if (list.ButtonText("MSFS.Reset".Translate()))
        {
            SteelAmount = 20;
            WorkAmount = 1600;
            Component = false;
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
        Scribe_Values.Look(ref SteelAmount, "steelAmount", 20);
        Scribe_Values.Look(ref WorkAmount, "workAmount", 1600);
        Scribe_Values.Look(ref Component, "component");
    }
}