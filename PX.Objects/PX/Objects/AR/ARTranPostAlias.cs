// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranPostAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
[Serializable]
public class ARTranPostAlias : ARTranPost
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARTranPostAlias>.By<ARTranPostAlias.docType, ARTranPostAlias.refNbr, ARTranPostAlias.lineNbr, ARTranPostAlias.iD>
  {
    public static ARTranPostAlias Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id)
    {
      return (ARTranPostAlias) PrimaryKeyOf<ARTranPostAlias>.By<ARTranPostAlias.docType, ARTranPostAlias.refNbr, ARTranPostAlias.lineNbr, ARTranPostAlias.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, (PKFindOptions) 0);
    }
  }

  public new abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostAlias.iD>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostAlias.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostAlias.refNbr>
  {
  }

  public new abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranPostAlias.sourceDocType>
  {
  }

  public new abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranPostAlias.sourceRefNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostAlias.lineNbr>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostAlias.type>
  {
  }

  public new abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostAlias.curyAmt>
  {
  }

  public new abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostAlias.curyDiscAmt>
  {
  }

  public new abstract class curyWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostAlias.curyWOAmt>
  {
  }

  public new abstract class voidAdjNbr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostAlias.voidAdjNbr>
  {
  }
}
