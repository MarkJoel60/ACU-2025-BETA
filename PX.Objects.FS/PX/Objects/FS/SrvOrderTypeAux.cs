// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SrvOrderTypeAux
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class SrvOrderTypeAux : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.active, Equal<True>, And<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.regularAppointment>>>>))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SrvOrderTypeAux.srvOrdType>
  {
  }
}
