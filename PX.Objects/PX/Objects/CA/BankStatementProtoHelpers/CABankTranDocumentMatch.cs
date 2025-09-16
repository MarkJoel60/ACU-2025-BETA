// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.CABankTranDocumentMatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementProtoHelpers;

public abstract class CABankTranDocumentMatch : PXBqlTable
{
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(6)]
  [PXUIField(DisplayName = "Match Relevance", Enabled = false)]
  public virtual Decimal? MatchRelevance { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(3)]
  [PXUIField(DisplayName = "Match Relevance, %", Enabled = false)]
  [PXFormula(typeof (Mult<CABankTranDocumentMatch.matchRelevance, decimal100>))]
  public virtual Decimal? MatchRelevancePercent { get; set; }

  public abstract 
  #nullable disable
  string GetDocumentKey();

  public abstract void BuildDocRef(CABankTranDocRef docRef);

  public abstract class matchRelevance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDocumentMatch.matchRelevance>
  {
  }

  public abstract class matchRelevancePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDocumentMatch.matchRelevancePercent>
  {
  }
}
