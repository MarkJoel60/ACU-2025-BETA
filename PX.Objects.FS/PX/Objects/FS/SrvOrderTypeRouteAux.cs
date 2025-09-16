// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SrvOrderTypeRouteAux
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class SrvOrderTypeRouteAux : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault(typeof (Coalesce<Search2<FSxUserPreferences.dfltSrvOrdType, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSxUserPreferences.dfltSrvOrdType>>>, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>>>>, Search<FSRouteSetup.dfltSrvOrdType>>))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.active, Equal<True>, And<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>>>>))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SrvOrderTypeRouteAux.srvOrdType>
  {
  }
}
