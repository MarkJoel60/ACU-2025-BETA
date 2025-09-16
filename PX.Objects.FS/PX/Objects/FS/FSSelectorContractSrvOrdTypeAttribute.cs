// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorContractSrvOrdTypeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorContractSrvOrdTypeAttribute : PXSelectorAttribute
{
  public FSSelectorContractSrvOrdTypeAttribute()
    : base(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.quote>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.routeAppointment>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>>))
  {
    this.DescriptionField = typeof (FSSrvOrdType.descr);
  }
}
