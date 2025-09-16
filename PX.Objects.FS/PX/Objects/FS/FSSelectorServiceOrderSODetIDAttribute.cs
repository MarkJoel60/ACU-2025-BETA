// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorServiceOrderSODetIDAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorServiceOrderSODetIDAttribute : PXSelectorAttribute
{
  public FSSelectorServiceOrderSODetIDAttribute()
    : base(typeof (Search<FSSODet.lineRef, Where<FSSODet.sOID, Equal<Current<FSServiceOrder.sOID>>, And<FSSODet.lineType, Equal<FSLineType.Service>>>>), new Type[5]
    {
      typeof (FSSODet.lineRef),
      typeof (FSSODet.lineType),
      typeof (FSSODet.status),
      typeof (FSSODet.inventoryID),
      typeof (FSSODet.tranDesc)
    })
  {
    this.SubstituteKey = typeof (FSSODet.lineRef);
    this.DescriptionField = typeof (FSSODet.inventoryID);
    this.DirtyRead = true;
  }
}
