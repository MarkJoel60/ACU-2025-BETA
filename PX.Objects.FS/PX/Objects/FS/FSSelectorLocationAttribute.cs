// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorLocationAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorLocationAttribute : PXDimensionSelectorAttribute
{
  public FSSelectorLocationAttribute()
    : base("LOCATION", typeof (Search<PX.Objects.CR.Location.locationID>), typeof (PX.Objects.CR.Location.locationCD))
  {
    this.DescriptionField = typeof (PX.Objects.CR.Location.descr);
    this.DirtyRead = true;
  }

  public FSSelectorLocationAttribute(System.Type currentBAccountID)
    : base("LOCATION", BqlCommand.Compose(new System.Type[7]
    {
      typeof (Search<,>),
      typeof (PX.Objects.CR.Location.locationID),
      typeof (Where<,>),
      typeof (PX.Objects.CR.Location.bAccountID),
      typeof (Equal<>),
      typeof (Current<>),
      currentBAccountID
    }), typeof (PX.Objects.CR.Location.locationCD))
  {
    this.DescriptionField = typeof (PX.Objects.CR.Location.descr);
    this.DirtyRead = true;
  }
}
