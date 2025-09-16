// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LocationLinkFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;

#nullable enable
namespace PX.Objects.IN.GraphExtensions;

/// <exclude />
[PXCacheName("Location List Filter")]
public class LocationLinkFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Location(IsKey = true)]
  public virtual int? LocationID { get; set; }

  public abstract class locationID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  LocationLinkFilter.locationID>
  {
  }

  [PXUIField(DisplayName = "Description", Enabled = false)]
  public abstract class AttachedLocationDescription<TSelf, TGraph> : 
    PXFieldAttachedTo<LocationLinkFilter>.By<TGraph>.AsString.Named<TSelf>
    where TSelf : PXFieldAttachedTo<LocationLinkFilter>.By<TGraph>.AsString
    where TGraph : PXGraph
  {
    public override string GetValue(LocationLinkFilter Row)
    {
      return INLocation.PK.Find((PXGraph) this.Base, (int?) Row?.LocationID)?.Descr;
    }
  }
}
