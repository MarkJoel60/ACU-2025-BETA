// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranMatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA.BankStatementProtoHelpers;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Bank Transaction Match")]
[Serializable]
public class CABankTranMatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? TranID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (CABankTran.lineCntrMatch))]
  [PXParent(typeof (Select<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTranMatch.tranID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsKey = true)]
  [CABankTranMatch.matchType.List]
  [PXDefault("M")]
  public virtual 
  #nullable disable
  string MatchType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [CABankTranType.List]
  public virtual string TranType { get; set; }

  [PXDBLong]
  public virtual long? CATranID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {"AP", "AR"}, new string[] {"AP", "AR"})]
  public virtual string DocModule { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string TaxCategoryID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string DocRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? ReferenceID { get; set; }

  [PXDBDecimal]
  [PXDefault]
  public virtual Decimal? CuryAmt { get; set; }

  [PXDBDecimal]
  [PXUnboundFormula(typeof (IIf<Where<CABankTranMatch.matchType, Equal<CABankTranMatch.matchType.match>>, CABankTranMatch.curyApplAmt, decimal0>), typeof (SumCalc<CABankTran.curyApplAmtMatch>))]
  public virtual Decimal? CuryApplAmt { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryApplTaxableAmt { get; set; }

  [PXDBDecimal]
  public virtual Decimal? CuryApplTaxAmt { get; set; }

  [PXDBBool]
  public virtual bool? IsCharge { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public static void Redirect(PXGraph graph, CABankTranMatch match)
  {
    if (match.DocModule == "AP" && match.DocType == "CBT" && match.DocRefNbr != null)
    {
      CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<CABatch>) instance.Document).Current = PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelect<CABatch, Where<CABatch.batchNbr, Equal<Required<CATran.origRefNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) match.DocRefNbr
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    if (match.CATranID.HasValue)
    {
      CATran.Redirect((PXCache) null, PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CABankTranMatch.cATranID>>>>.Config>.Select(graph, new object[1]
      {
        (object) match.CATranID
      })));
    }
    else
    {
      if (match.DocModule == null || match.DocType == null || match.DocRefNbr == null)
        return;
      RedirectionToOrigDoc.TryRedirect(match.DocType, match.DocRefNbr, match.DocModule);
    }
  }

  public void Copy(CABankTranDocRef docRef)
  {
    this.CATranID = docRef.CATranID;
    this.DocModule = docRef.DocModule;
    this.DocType = docRef.DocType;
    this.DocRefNbr = docRef.DocRefNbr;
    this.ReferenceID = docRef.ReferenceID;
    int num;
    if (docRef.CuryDiscAmt.HasValue)
    {
      DateTime? nullable = docRef.TranDate;
      if (nullable.HasValue)
      {
        nullable = docRef.DiscDate;
        if (nullable.HasValue)
        {
          nullable = docRef.TranDate;
          DateTime dateTime1 = nullable.Value;
          nullable = docRef.DiscDate;
          DateTime dateTime2 = nullable.Value;
          num = dateTime1 <= dateTime2 ? 1 : 0;
          goto label_5;
        }
      }
    }
    num = 0;
label_5:
    bool flag = num != 0;
    Decimal? curyTranAmt = docRef.CuryTranAmt;
    Decimal? nullable1 = flag ? docRef.CuryDiscAmt : new Decimal?(0M);
    this.CuryApplAmt = curyTranAmt.HasValue & nullable1.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  public class PK : 
    PrimaryKeyOf<CABankTranMatch>.By<CABankTranMatch.tranID, CABankTranMatch.lineNbr, CABankTranMatch.matchType>
  {
    public static CABankTranMatch Find(
      PXGraph graph,
      int? tranID,
      int? lineNbr,
      string matchType,
      PKFindOptions options = 0)
    {
      return (CABankTranMatch) PrimaryKeyOf<CABankTranMatch>.By<CABankTranMatch.tranID, CABankTranMatch.lineNbr, CABankTranMatch.matchType>.FindBy(graph, (object) tranID, (object) lineNbr, (object) matchType, options);
    }
  }

  public static class FK
  {
    public class BankTransaction : 
      PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>.ForeignKeyOf<CABankTranMatch>.By<CABankTranMatch.tranID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.tranID>.ForeignKeyOf<CABankTranMatch>.By<CABankTranMatch.cATranID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CABankTranMatch>.By<CABankTranMatch.referenceID>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<CABankTranMatch>.By<CABankTranMatch.docType, CABankTranMatch.docRefNbr>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<CABankTranMatch>.By<CABankTranMatch.docType, CABankTranMatch.docRefNbr>
    {
    }
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranMatch.tranID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranMatch.lineNbr>
  {
  }

  public abstract class matchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch.matchType>
  {
    public const string Match = "M";
    public const string Charge = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "M", "C" }, new string[2]
        {
          "Match",
          "Charge"
        })
      {
      }
    }

    public class match : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CABankTranMatch.matchType.match>
    {
      public match()
        : base("M")
      {
      }
    }

    public class charge : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CABankTranMatch.matchType.charge>
    {
      public charge()
        : base("C")
      {
      }
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch.tranType>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranMatch.cATranID>
  {
  }

  public abstract class docModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch.docModule>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch.docType>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranMatch.taxCategoryID>
  {
  }

  public abstract class docRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranMatch.docRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranMatch.referenceID>
  {
  }

  public abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranMatch.curyAmt>
  {
  }

  public abstract class curyApplAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranMatch.curyApplAmt>
  {
  }

  public abstract class curyApplTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranMatch.curyApplTaxableAmt>
  {
  }

  public abstract class curyApplTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranMatch.curyApplTaxAmt>
  {
  }

  public abstract class isCharge : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranMatch.isCharge>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranMatch.curyInfoID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTranMatch.Tstamp>
  {
  }
}
