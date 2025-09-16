// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorProjectAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CT;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorProjectAttribute : PXSelectorAttribute
{
  public FSSelectorProjectAttribute()
    : base(typeof (Search<PX.Objects.CT.Contract.contractID, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.project>, And<PX.Objects.CT.Contract.nonProject, Equal<False>>>>))
  {
    this.SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD);
    this.DescriptionField = typeof (PX.Objects.CT.Contract.description);
  }
}
