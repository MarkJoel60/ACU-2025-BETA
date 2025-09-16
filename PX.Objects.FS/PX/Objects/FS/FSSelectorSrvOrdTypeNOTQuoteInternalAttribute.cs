// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorSrvOrdTypeNOTQuoteInternalAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorSrvOrdTypeNOTQuoteInternalAttribute : PXSelectorAttribute
{
  public FSSelectorSrvOrdTypeNOTQuoteInternalAttribute()
    : base(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.quote>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>), new Type[3]
    {
      typeof (FSSrvOrdType.srvOrdType),
      typeof (FSSrvOrdType.descr),
      typeof (FSSrvOrdType.behavior)
    })
  {
    this.DescriptionField = typeof (FSSrvOrdType.descr);
  }
}
