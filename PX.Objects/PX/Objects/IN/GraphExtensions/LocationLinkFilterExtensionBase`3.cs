// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LocationLinkFilterExtensionBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.GraphExtensions.Abstract;
using System.Collections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class LocationLinkFilterExtensionBase<TGraph, TGraphFilter, TGraphFilterLocationID> : 
  EntityLinkFilterExtensionBase<TGraph, TGraphFilter, TGraphFilterLocationID, LocationLinkFilter, LocationLinkFilter.locationID, int?>
  where TGraph : PXGraph, PXImportAttribute.IPXPrepareItems, PXImportAttribute.IPXProcess
  where TGraphFilter : class, IBqlTable, new()
  where TGraphFilterLocationID : class, IBqlField, IImplement<IBqlInt>
{
  [PXVirtualDAC]
  [PXImport]
  [PXReadOnlyView]
  public PXSelect<LocationLinkFilter> SelectedLocations;
  public PXAction<TGraphFilter> SelectLocations;

  protected override string EntityViewName => "SelectedLocations";

  public IEnumerable selectedLocations() => (IEnumerable) this.GetEntities();

  [PXButton]
  [PXUIField(DisplayName = "List")]
  public virtual void selectLocations()
  {
    ((PXSelectBase<LocationLinkFilter>) this.SelectedLocations).AskExt();
  }

  public abstract class AttachedLocationDescription<TSelf> : 
    LocationLinkFilter.AttachedLocationDescription<TSelf, TGraph>
    where TSelf : PXFieldAttachedTo<LocationLinkFilter>.By<TGraph>.AsString
  {
  }
}
