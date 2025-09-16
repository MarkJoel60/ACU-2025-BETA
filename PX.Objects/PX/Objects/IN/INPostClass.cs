// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPostClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INPostClassMaint))]
[PXCacheName]
[Serializable]
public class INPostClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PostClassID;
  protected string _Descr;
  protected int? _ReasonCodeSubID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected int? _COGSAcctID;
  protected int? _COGSSubID;
  protected int? _StdCstRevAcctID;
  protected int? _StdCstRevSubID;
  protected int? _StdCstVarAcctID;
  protected int? _StdCstVarSubID;
  protected int? _PPVAcctID;
  protected int? _PPVSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected string _PIReasonCode;
  protected string _InvtAcctDefault;
  protected bool? _COGSSubFromSales;
  protected string _COGSAcctDefault;
  protected string _SalesAcctDefault;
  protected string _StdCstRevAcctDefault;
  protected string _StdCstVarAcctDefault;
  protected string _PPVAcctDefault;
  protected string _POAccrualAcctDefault;
  protected string _InvtSubMask;
  protected string _COGSSubMask;
  protected string _SalesSubMask;
  protected string _StdCstVarSubMask;
  protected string _StdCstRevSubMask;
  protected string _PPVSubMask;
  protected string _POAccrualSubMask;
  protected int? _LCVarianceAcctID;
  protected int? _LCVarianceSubID;
  protected string _LCVarianceAcctDefault;
  protected string _LCVarianceSubMask;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INPostClass.postClassID>))]
  [PXFieldDescription]
  public virtual string PostClassID
  {
    get => this._PostClassID;
    set => this._PostClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [SubAccount]
  [PXForeignReference(typeof (INPostClass.FK.ReasonCodeSubaccount))]
  public virtual int? ReasonCodeSubID
  {
    get => this._ReasonCodeSubID;
    set => this._ReasonCodeSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.SalesAccount))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [SubAccount(typeof (INPostClass.salesAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.SalesSubaccount))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.InventoryAccrualAccount))]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [SubAccount(typeof (INPostClass.invtAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.InventoryAccrualSubaccount))]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.COGSAccount))]
  public virtual int? COGSAcctID
  {
    get => this._COGSAcctID;
    set => this._COGSAcctID = value;
  }

  [SubAccount(typeof (INPostClass.cOGSAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.COGSSubaccount))]
  public virtual int? COGSSubID
  {
    get => this._COGSSubID;
    set => this._COGSSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.StandardCostRevaluationAccount))]
  public virtual int? StdCstRevAcctID
  {
    get => this._StdCstRevAcctID;
    set => this._StdCstRevAcctID = value;
  }

  [SubAccount(typeof (INPostClass.stdCstRevAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.StandardCostRevaluationSubaccount))]
  public virtual int? StdCstRevSubID
  {
    get => this._StdCstRevSubID;
    set => this._StdCstRevSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.StandardCostVarianceAccount))]
  public virtual int? StdCstVarAcctID
  {
    get => this._StdCstVarAcctID;
    set => this._StdCstVarAcctID = value;
  }

  [SubAccount(typeof (INPostClass.stdCstVarAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.StandardCostVarianceSubaccount))]
  public virtual int? StdCstVarSubID
  {
    get => this._StdCstVarSubID;
    set => this._StdCstVarSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.PPVAccount))]
  public virtual int? PPVAcctID
  {
    get => this._PPVAcctID;
    set => this._PPVAcctID = value;
  }

  [SubAccount(typeof (INPostClass.pPVAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.PPVSubaccount))]
  public virtual int? PPVSubID
  {
    get => this._PPVSubID;
    set => this._PPVSubID = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.POAccrualAccount))]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [SubAccount(typeof (INPostClass.pOAccrualAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.POAccrualSubaccount))]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Phys.Inventory Reason Code")]
  [PXForeignReference(typeof (INPostClass.FK.PIReasonCode))]
  public virtual string PIReasonCode
  {
    get => this._PIReasonCode;
    set => this._PIReasonCode = value;
  }

  /// <summary>Purchase Receipt Correction Reason Code</summary>
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Purchase Receipt Correction Reason Code")]
  [PXForeignReference(typeof (INPostClass.FK.CorrectionReasonCode))]
  public virtual string CorrectionReasonCode { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Inventory/Accrual Account from")]
  [INAcctSubDefault.ClassList]
  [PXDefault("I")]
  public virtual string InvtAcctDefault
  {
    get => this._InvtAcctDefault;
    set => this._InvtAcctDefault = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy COGS Sub. from Sales", FieldClass = "SUBACCOUNT")]
  public virtual bool? COGSSubFromSales
  {
    get => this._COGSSubFromSales;
    set => this._COGSSubFromSales = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use COGS/Expense Account from")]
  [INAcctSubDefault.SalesCOGSList]
  [PXDefault("I")]
  public virtual string COGSAcctDefault
  {
    get => this._COGSAcctDefault;
    set => this._COGSAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Sales Account from")]
  [INAcctSubDefault.SalesCOGSList]
  [PXDefault("I")]
  public virtual string SalesAcctDefault
  {
    get => this._SalesAcctDefault;
    set => this._SalesAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Std. Cost Revaluation Account from")]
  [INAcctSubDefault.ClassList]
  [PXDefault("I")]
  public virtual string StdCstRevAcctDefault
  {
    get => this._StdCstRevAcctDefault;
    set => this._StdCstRevAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Std. Cost Variance Account from")]
  [INAcctSubDefault.ClassList]
  [PXDefault("I")]
  public virtual string StdCstVarAcctDefault
  {
    get => this._StdCstVarAcctDefault;
    set => this._StdCstVarAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Purchase Price Variance Account from")]
  [INAcctSubDefault.ClassList]
  [PXDefault("I")]
  public virtual string PPVAcctDefault
  {
    get => this._PPVAcctDefault;
    set => this._PPVAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use PO Accrual Account from")]
  [INAcctSubDefault.POAccrualList]
  [PXDefault("I")]
  public virtual string POAccrualAcctDefault
  {
    get => this._POAccrualAcctDefault;
    set => this._POAccrualAcctDefault = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Inventory/Accrual Sub. from")]
  public virtual string InvtSubMask
  {
    get => this._InvtSubMask;
    set => this._InvtSubMask = value;
  }

  [PXDefault]
  [SalesCOGSSubAccountMask(DisplayName = "Combine COGS/Expense Sub. from")]
  public virtual string COGSSubMask
  {
    get => this._COGSSubMask;
    set => this._COGSSubMask = value;
  }

  [PXDefault]
  [SalesCOGSSubAccountMask(DisplayName = "Combine Sales Sub. from")]
  public virtual string SalesSubMask
  {
    get => this._SalesSubMask;
    set => this._SalesSubMask = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Std. Cost Variance Sub. from")]
  public virtual string StdCstVarSubMask
  {
    get => this._StdCstVarSubMask;
    set => this._StdCstVarSubMask = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Std. Cost Revaluation Sub. from")]
  public virtual string StdCstRevSubMask
  {
    get => this._StdCstRevSubMask;
    set => this._StdCstRevSubMask = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Purchase Price Variance Sub. from")]
  public virtual string PPVSubMask
  {
    get => this._PPVSubMask;
    set => this._PPVSubMask = value;
  }

  [PXDefault]
  [POAccrualSubAccountMask(DisplayName = "Combine PO Accrual Sub. from")]
  public virtual string POAccrualSubMask
  {
    get => this._POAccrualSubMask;
    set => this._POAccrualSubMask = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.LandedCostVarianceAccount))]
  public virtual int? LCVarianceAcctID
  {
    get => this._LCVarianceAcctID;
    set => this._LCVarianceAcctID = value;
  }

  [SubAccount(typeof (INPostClass.lCVarianceAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.LandedCostVarianceSubaccount))]
  public virtual int? LCVarianceSubID
  {
    get => this._LCVarianceSubID;
    set => this._LCVarianceSubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Landed Cost Variance Account from")]
  [INAcctSubDefault.ClassList]
  [PXDefault("I")]
  public virtual string LCVarianceAcctDefault
  {
    get => this._LCVarianceAcctDefault;
    set => this._LCVarianceAcctDefault = value;
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Landed Cost Variance Sub. from")]
  public virtual string LCVarianceSubMask
  {
    get => this._LCVarianceSubMask;
    set => this._LCVarianceSubMask = value;
  }

  [Account]
  [PXForeignReference(typeof (INPostClass.FK.DeferralAccount))]
  public int? DeferralAcctID { get; set; }

  [SubAccount(typeof (INPostClass.deferralAcctID))]
  [PXForeignReference(typeof (INPostClass.FK.DeferralSubaccount))]
  public int? DeferralSubID { get; set; }

  [PXNote(DescriptionField = typeof (INPostClass.postClassID), Selector = typeof (INPostClass.postClassID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>
  {
    public static INPostClass Find(PXGraph graph, string postClassID, PKFindOptions options = 0)
    {
      return (INPostClass) PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>.FindBy(graph, (object) postClassID, options);
    }
  }

  public static class FK
  {
    public class ReasonCodeSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.reasonCodeSubID>
    {
    }

    public class InventoryAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.invtAcctID>
    {
    }

    public class InventoryAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.invtSubID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.cOGSSubID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.salesSubID>
    {
    }

    public class StandardCostRevaluationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.stdCstRevAcctID>
    {
    }

    public class StandardCostRevaluationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.stdCstRevSubID>
    {
    }

    public class PPVAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.pPVAcctID>
    {
    }

    public class PPVSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.pPVSubID>
    {
    }

    public class StandardCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.stdCstVarAcctID>
    {
    }

    public class StandardCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.stdCstVarSubID>
    {
    }

    public class POAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.pOAccrualAcctID>
    {
    }

    public class POAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.pOAccrualSubID>
    {
    }

    public class LandedCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.lCVarianceAcctID>
    {
    }

    public class LandedCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.lCVarianceSubID>
    {
    }

    public class DeferralAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPostClass>.By<INPostClass.deferralAcctID>
    {
    }

    public class DeferralSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPostClass>.By<INPostClass.deferralSubID>
    {
    }

    public class PIReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INPostClass>.By<INPostClass.pIReasonCode>
    {
    }

    public class CorrectionReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INPostClass>.By<INPostClass.correctionReasonCode>
    {
    }
  }

  public abstract class postClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.postClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.descr>
  {
  }

  public abstract class reasonCodeSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.reasonCodeSubID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.salesSubID>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.cOGSSubID>
  {
  }

  public abstract class stdCstRevAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.stdCstRevAcctID>
  {
  }

  public abstract class stdCstRevSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.stdCstRevSubID>
  {
  }

  public abstract class stdCstVarAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.stdCstVarAcctID>
  {
  }

  public abstract class stdCstVarSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.stdCstVarSubID>
  {
  }

  public abstract class pPVAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.pPVAcctID>
  {
  }

  public abstract class pPVSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.pPVSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.pOAccrualSubID>
  {
  }

  public abstract class pIReasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.pIReasonCode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INPostClass.CorrectionReasonCode" />
  public abstract class correctionReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.correctionReasonCode>
  {
  }

  public abstract class invtAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.invtAcctDefault>
  {
  }

  public abstract class cOGSSubFromSales : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INPostClass.cOGSSubFromSales>
  {
  }

  public abstract class cOGSAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.cOGSAcctDefault>
  {
  }

  public abstract class salesAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.salesAcctDefault>
  {
  }

  public abstract class stdCstRevAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.stdCstRevAcctDefault>
  {
  }

  public abstract class stdCstVarAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.stdCstVarAcctDefault>
  {
  }

  public abstract class pPVAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.pPVAcctDefault>
  {
  }

  public abstract class pOAccrualAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.pOAccrualAcctDefault>
  {
  }

  public abstract class invtSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.invtSubMask>
  {
  }

  public abstract class cOGSSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.cOGSSubMask>
  {
  }

  public abstract class salesSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.salesSubMask>
  {
  }

  public abstract class stdCstVarSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.stdCstVarSubMask>
  {
  }

  public abstract class stdCstRevSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.stdCstRevSubMask>
  {
  }

  public abstract class pPVSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPostClass.pPVSubMask>
  {
  }

  public abstract class pOAccrualSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.pOAccrualSubMask>
  {
  }

  public abstract class lCVarianceAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.lCVarianceAcctID>
  {
  }

  public abstract class lCVarianceSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.lCVarianceSubID>
  {
  }

  public abstract class lCVarianceAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.lCVarianceAcctDefault>
  {
  }

  public abstract class lCVarianceSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.lCVarianceSubMask>
  {
  }

  public abstract class deferralAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.deferralAcctID>
  {
  }

  public abstract class deferralSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPostClass.deferralSubID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPostClass.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPostClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPostClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INPostClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPostClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPostClass.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPostClass.Tstamp>
  {
  }
}
