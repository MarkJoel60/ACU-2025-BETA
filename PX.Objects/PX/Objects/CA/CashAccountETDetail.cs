// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountETDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The settings for deposit to the cash account from the clearing account or accounts.
/// The presence of this record for the specific cash account and deposit account pair
/// defines a possibility to post to cash account from the specific clearing account.
/// </summary>
[PXCacheName("Entry Type for Cash Account")]
[Serializable]
public class CashAccountETDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CashAccount.cashAccountID))]
  [PXUIField(DisplayName = "AccountID", Visible = false)]
  [PXParent(typeof (Select<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CashAccountETDetail.cashAccountID>>>>))]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Entry Type ID")]
  [PXSelector(typeof (CAEntryType.entryTypeId))]
  public virtual 
  #nullable disable
  string EntryTypeID { get; set; }

  [Account(DescriptionField = typeof (PX.Objects.GL.Account.description), DisplayName = "Offset Account Override", AvoidControlAccounts = true)]
  [PXDefault]
  public virtual int? OffsetAccountID { get; set; }

  [SubAccount(typeof (CashAccountETDetail.offsetAccountID), DisplayName = "Offset Subaccount Override")]
  [PXDefault]
  public virtual int? OffsetSubID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? OffsetBranchID { get; set; }

  [PXRestrictor(typeof (Where<CashAccount.cashAccountID, NotEqual<Current<CashAccount.cashAccountID>>>), "Default offset account currency is different from the currency of the current Cash Account. You must override Reclassification account.", new Type[] {})]
  [PXRestrictor(typeof (Where<CashAccount.curyID, Equal<Current<CashAccount.curyID>>>), "Default offset account currency is different from the currency of the current Cash Account. You must override Reclassification account.", new Type[] {})]
  [CashAccountScalar]
  [PXDBScalar(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.accountID, Equal<CashAccountETDetail.offsetAccountID>, And<CashAccount.subID, Equal<CashAccountETDetail.offsetSubID>, And<CashAccount.branchID, Equal<CashAccountETDetail.offsetBranchID>>>>>))]
  public virtual int? OffsetCashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (TaxCalculationMode.taxSetting))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<CashAccountETDetail>.By<CashAccountETDetail.cashAccountID, CashAccountETDetail.entryTypeID>
  {
    public static CashAccountETDetail Find(
      PXGraph graph,
      int? cashAccountID,
      string entryTypeID,
      PKFindOptions options = 0)
    {
      return (CashAccountETDetail) PrimaryKeyOf<CashAccountETDetail>.By<CashAccountETDetail.cashAccountID, CashAccountETDetail.entryTypeID>.FindBy(graph, (object) cashAccountID, (object) entryTypeID, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.cashAccountID>
    {
    }

    public class EntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.entryTypeID>
    {
    }

    public class OffsetAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.offsetAccountID>
    {
    }

    public class OffsetSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.offsetSubID>
    {
    }

    public class OffsetBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.offsetBranchID>
    {
    }

    public class ReclassificationCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.offsetCashAccountID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CashAccountETDetail>.By<CashAccountETDetail.taxZoneID>
    {
    }
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountETDetail.cashAccountID>
  {
  }

  public abstract class entryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountETDetail.entryTypeID>
  {
  }

  public abstract class offsetAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountETDetail.offsetAccountID>
  {
  }

  public abstract class offsetSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccountETDetail.offsetSubID>
  {
  }

  public abstract class offsetBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountETDetail.offsetBranchID>
  {
  }

  public abstract class offsetCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountETDetail.offsetCashAccountID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccountETDetail.taxZoneID>
  {
  }

  public abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountETDetail.taxCalcMode>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccountETDetail.isDefault>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CashAccountETDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CashAccountETDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountETDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccountETDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CashAccountETDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountETDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccountETDetail.lastModifiedDateTime>
  {
  }
}
