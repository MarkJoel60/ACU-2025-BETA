// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODetEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (ServiceOrderEntry))]
[PXBreakInheritance]
[PXProjection(typeof (Select<FSSODet>), Persistent = false)]
[Serializable]
public class FSSODetEmployee : FSSODet
{
  /// <summary>The identifier of the exchange rate record.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (FSSODet.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    FSSODetEmployee>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>
  {
    public static FSSODetEmployee Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSSODetEmployee) PrimaryKeyOf<FSSODetEmployee>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public new abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetEmployee.sOID>
  {
  }

  public new abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetEmployee.sODetID>
  {
  }

  public new abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetEmployee.lineRef>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSODetEmployee.curyInfoID>
  {
  }
}
