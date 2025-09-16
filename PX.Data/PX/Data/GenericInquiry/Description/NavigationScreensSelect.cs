// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Description.NavigationScreensSelect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System;

#nullable disable
namespace PX.Data.GenericInquiry.Description;

/// <summary>The class is an heir of PXOrderedSelect with a defined DAC and not standard action names
/// to avoid conflicts when there are a few views with drag and drop on a screen. </summary>
[PXDynamicButton(new string[] {"ScreensPasteLine", "ScreensResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (Messages))]
public class NavigationScreensSelect : 
  PXOrderedSelect<GIDesign, GINavigationScreen, Where<GINavigationScreen.designID, Equal<PX.Data.Current<GIDesign.designID>>>, OrderBy<Asc<GINavigationScreen.sortOrder, Asc<GINavigationScreen.lineNbr>>>>
{
  public const string ScreensPasteLineCommand = "ScreensPasteLine";
  public const string ScreensResetOrderCommand = "ScreensResetOrder";

  public NavigationScreensSelect(PXGraph graph)
    : base(graph)
  {
  }

  public NavigationScreensSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override void AddActions(PXGraph graph)
  {
    this.AddAction(graph, "ScreensPasteLine", "Paste Line", new PXButtonDelegate(((PXOrderedSelectBase<GIDesign, GINavigationScreen>) this).PasteLine));
    this.AddAction(graph, "ScreensResetOrder", "Reset Order", new PXButtonDelegate(((PXOrderedSelectBase<GIDesign, GINavigationScreen>) this).ResetOrder));
  }
}
