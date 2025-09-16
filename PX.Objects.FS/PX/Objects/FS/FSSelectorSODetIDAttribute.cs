// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorSODetIDAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorSODetIDAttribute : PXSelectorAttribute
{
  public FSSelectorSODetIDAttribute()
    : base(typeof (Search<FSSODet.sODetID, Where<FSSODet.sOID, Equal<Current<FSAppointment.sOID>>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Scheduled>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Completed>>>>>>), new Type[5]
    {
      typeof (FSSODet.lineRef),
      typeof (FSSODet.lineType),
      typeof (FSSODet.status),
      typeof (FSSODet.inventoryID),
      typeof (FSSODet.lastModifiedDateTime)
    })
  {
    this.SubstituteKey = typeof (FSSODet.lineRef);
  }
}
