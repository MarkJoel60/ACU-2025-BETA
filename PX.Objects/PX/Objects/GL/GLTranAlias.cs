// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class GLTranAlias : GLTran
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    GLTranAlias>.By<GLTranAlias.module, GLTranAlias.batchNbr, GLTranAlias.lineNbr>
  {
    public static GLTranAlias Find(
      PXGraph graph,
      string module,
      string batchNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (GLTranAlias) PrimaryKeyOf<GLTranAlias>.By<GLTranAlias.module, GLTranAlias.batchNbr, GLTranAlias.lineNbr>.FindBy(graph, (object) module, (object) batchNbr, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.ledgerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.curyInfoID>
    {
    }

    public class Batch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.module, GLTranAlias.batchNbr>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.subID>
    {
    }

    public class OriginalTransaction : 
      PrimaryKeyOf<GLTranAlias>.By<GLTranAlias.module, GLTranAlias.batchNbr, GLTranAlias.lineNbr>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.origModule, GLTranAlias.origBatchNbr, GLTranAlias.origLineNbr>
    {
    }

    public class OriginalAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.origAccountID>
    {
    }

    public class OriginalSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.origSubID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.inventoryID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.tranID>.ForeignKeyOf<GLTranAlias>.By<GLTranAlias.cATranID>
    {
    }
  }

  public new abstract class includedInReclassHistory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranAlias.includedInReclassHistory>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.branchID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.batchNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.lineNbr>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.ledgerID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.subID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.taskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.costCodeID>
  {
  }

  public new abstract class isNonPM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.isNonPM>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.refNbr>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.inventoryID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranAlias.qty>
  {
  }

  public new abstract class debitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranAlias.debitAmt>
  {
  }

  public new abstract class creditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranAlias.creditAmt>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranAlias.curyInfoID>
  {
  }

  public new abstract class curyDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranAlias.curyDebitAmt>
  {
  }

  public new abstract class curyCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranAlias.curyCreditAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.released>
  {
  }

  public new abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.posted>
  {
  }

  public new abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.nonBillable>
  {
  }

  public new abstract class isInterCompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranAlias.isInterCompany>
  {
  }

  public new abstract class summPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.summPost>
  {
  }

  public new abstract class zeroPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.zeroPost>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.origModule>
  {
  }

  public new abstract class origBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.origBatchNbr>
  {
  }

  public new abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.origLineNbr>
  {
  }

  public new abstract class origAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.origAccountID>
  {
  }

  public new abstract class origSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.origSubID>
  {
  }

  public new abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.tranID>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.tranType>
  {
  }

  public new abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.tranClass>
  {
  }

  public new abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.tranDesc>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranAlias.tranDate>
  {
  }

  public new abstract class tranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.tranLineNbr>
  {
  }

  public new abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.referenceID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.tranPeriodID>
  {
  }

  public new abstract class postYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.postYear>
  {
  }

  public new abstract class tranYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.tranYear>
  {
  }

  public new abstract class nextPostYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.nextPostYear>
  {
  }

  public new abstract class nextTranYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.nextTranYear>
  {
  }

  public new abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranAlias.cATranID>
  {
  }

  public new abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranAlias.pMTranID>
  {
  }

  public new abstract class origPMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranAlias.origPMTranID>
  {
  }

  public new abstract class ledgerBalanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.ledgerBalanceType>
  {
  }

  public new abstract class accountRequireUnits : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranAlias.accountRequireUnits>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.taxID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.taxCategoryID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTranAlias.noteID>
  {
  }

  public new abstract class reclassificationProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranAlias.reclassificationProhibited>
  {
  }

  public new abstract class reclassBatchModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.reclassBatchModule>
  {
  }

  public new abstract class reclassBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.reclassBatchNbr>
  {
  }

  public new abstract class isReclassReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranAlias.isReclassReverse>
  {
  }

  public new abstract class reclassType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranAlias.reclassType>
  {
  }

  public new abstract class curyReclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranAlias.curyReclassRemainingAmt>
  {
  }

  public new abstract class reclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranAlias.reclassRemainingAmt>
  {
  }

  public new abstract class reclassified : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranAlias.reclassified>
  {
  }

  public new abstract class reclassOrigTranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTranAlias.reclassOrigTranDate>
  {
  }

  public new abstract class reclassSourceTranModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.reclassSourceTranModule>
  {
  }

  public new abstract class reclassSourceTranBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranAlias.reclassSourceTranBatchNbr>
  {
  }

  public new abstract class reclassSourceTranLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranAlias.reclassSourceTranLineNbr>
  {
  }

  public new abstract class reclassSeqNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranAlias.reclassSeqNbr>
  {
  }

  public new abstract class reclassTotalCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranAlias.reclassTotalCount>
  {
  }

  public new abstract class reclassReleasedCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranAlias.reclassReleasedCount>
  {
  }
}
