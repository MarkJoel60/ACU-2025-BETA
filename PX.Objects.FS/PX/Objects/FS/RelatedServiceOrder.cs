// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RelatedServiceOrder
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
public class RelatedServiceOrder : FSServiceOrder
{
  [PXDBLong]
  public override long? CuryInfoID { get; set; }

  public new abstract class srvOrdType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    RelatedServiceOrder.srvOrdType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedServiceOrder.refNbr>
  {
  }

  public new abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedServiceOrder.sOID>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    RelatedServiceOrder.curyInfoID>
  {
  }
}
