// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAEntryType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>An entry type that can be used in cash management.</summary>
[PXPrimaryGraph(typeof (EntryTypeMaint))]
[PXCacheName("CA Entry Type")]
[Serializable]
public class CAEntryType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string EntryTypeId { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CA")]
  [BatchModule.CashManagerList]
  [PXUIField]
  public virtual string Module { get; set; }

  [CashAccountScalar]
  [PXDBScalar(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.accountID, Equal<CAEntryType.accountID>, And<CashAccount.subID, Equal<CAEntryType.subID>, And<CashAccount.branchID, Equal<CAEntryType.branchID>>>>>))]
  public virtual int? CashAccountID { get; set; }

  [Account(DescriptionField = typeof (PX.Objects.GL.Account.description), DisplayName = "Default Offset Account", Enabled = false, AvoidControlAccounts = true)]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (CAEntryType.accountID), DisplayName = "Default Offset Subaccount", Enabled = false)]
  public virtual int? SubID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Business Account", Enabled = false)]
  [PXVendorCustomerSelector(typeof (CAEntryType.module))]
  public virtual int? ReferenceID { get; set; }

  [PXDefault("D")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField]
  public virtual string DrCr { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? UseToReclassifyPayments { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deduct from Payment")]
  public bool? Consolidate { get; set; }

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

  public class PK : PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>
  {
    public static CAEntryType Find(PXGraph graph, string entryTypeId, PKFindOptions options = 0)
    {
      return (CAEntryType) PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.FindBy(graph, (object) entryTypeId, options);
    }
  }

  public static class FK
  {
    public class DefaultOffsetBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CAEntryType>.By<CAEntryType.branchID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CAEntryType>.By<CAEntryType.cashAccountID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CAEntryType>.By<CAEntryType.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<CAEntryType>.By<CAEntryType.subID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CAEntryType>.By<CAEntryType.referenceID>
    {
    }
  }

  public abstract class entryTypeId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAEntryType.entryTypeId>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAEntryType.module>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEntryType.cashAccountID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEntryType.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEntryType.subID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEntryType.branchID>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEntryType.referenceID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAEntryType.drCr>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAEntryType.descr>
  {
  }

  public abstract class useToReclassifyPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAEntryType.useToReclassifyPayments>
  {
  }

  public abstract class consolidate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAEntryType.consolidate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CAEntryType.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CAEntryType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAEntryType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAEntryType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CAEntryType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAEntryType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAEntryType.lastModifiedDateTime>
  {
  }
}
