// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAdjust2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class APAdjust2 : APAdjust
{
  [PXDBLong]
  public override long? AdjdCuryInfoID
  {
    get => base.AdjdCuryInfoID;
    set => base.AdjdCuryInfoID = value;
  }

  [PXDBLong]
  public override long? AdjgCuryInfoID
  {
    get => base.AdjgCuryInfoID;
    set => base.AdjgCuryInfoID = value;
  }

  [PXDBLong]
  public override long? AdjdOrigCuryInfoID
  {
    get => base.AdjdOrigCuryInfoID;
    set => base.AdjdOrigCuryInfoID = value;
  }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    APAdjust2>.By<APAdjust2.adjgDocType, APAdjust2.adjgRefNbr, APAdjust2.adjNbr, APAdjust2.adjdDocType, APAdjust2.adjdRefNbr, APAdjust2.adjdLineNbr>
  {
    public static APAdjust2 Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      int? adjdLineNb,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APAdjust2>.By<APAdjust2.adjgDocType, APAdjust2.adjgRefNbr, APAdjust2.adjNbr, APAdjust2.adjdDocType, APAdjust2.adjdRefNbr, APAdjust2.adjdLineNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, (object) adjdLineNb, options);
    }
  }

  public new static class FK
  {
    public class AdjgDoc : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APAdjust2>.By<APAdjust2.adjgDocType, APAdjust2.adjgRefNbr>
    {
    }

    public class AdjdDoc : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APAdjust2>.By<APAdjust2.adjdDocType, APAdjust2.adjdRefNbr>
    {
    }
  }

  public new abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.adjgRefNbr>
  {
  }

  public new abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.adjgDocType>
  {
  }

  public new abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.adjdRefNbr>
  {
  }

  public new abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.adjdDocType>
  {
  }

  public new abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust2.adjdLineNbr>
  {
  }

  public new abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust2.adjNbr>
  {
  }

  public new abstract class adjgFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust2.adjgFinPeriodID>
  {
  }

  public new abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust2.adjgTranPeriodID>
  {
  }

  public new abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust2.voidAdjNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust2.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust2.voided>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust2.noteID>
  {
  }

  public new abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust2.curyAdjdAmt>
  {
  }

  public new abstract class stubNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.stubNbr>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust2.pendingPPD>
  {
  }

  public new abstract class invoiceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust2.invoiceID>
  {
  }

  public new abstract class paymentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust2.paymentID>
  {
  }

  public new abstract class memoID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust2.memoID>
  {
  }
}
