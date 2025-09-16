// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedAccountMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Defines the mapping between the bank feed account and the cash account.
/// </summary>
[PXCacheName("Bank Feed Account Mapping")]
public class CABankFeedAccountMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The unique identifier of the record that represents the Acumatica specific bank feed account map id.
  /// </summary>
  [PXDBGuid(true, IsKey = true)]
  public virtual Guid? BankFeedAccountMapID { get; set; }

  /// <summary>The Bank feed id from the bank feed definition.</summary>
  [PXDBString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  /// <summary>The line number from the bank feed detail.</summary>
  [PXDBInt]
  public virtual int? LineNbr { get; set; }

  /// <summary>Bank Feed type (P - Plaid, M - MX, T - Test Plaid).</summary>
  [PXDBString]
  [PXDefault]
  [CABankFeedType.List]
  [PXUIField(DisplayName = "Bank Feed Type")]
  public virtual string Type { get; set; }

  /// <summary>The bank feed specific bank identifier.</summary>
  [PXDBString(50, IsUnicode = true)]
  public virtual string InstitutionID { get; set; }

  /// <summary>The bank feed specific account name.</summary>
  [PXDBString(250)]
  public virtual string AccountName { get; set; }

  /// <summary>The bank feed specific account mask.</summary>
  [PXDBString(50)]
  public virtual string AccountMask { get; set; }

  /// <summary>
  /// The combination of bank feed specific account name and bank feed specific account mask.
  /// </summary>
  [PXString]
  [PXDBCalced(typeof (IIf<BqlOperand<CABankFeedAccountMapping.accountMask, IBqlString>.IsNull, CABankFeedAccountMapping.accountName, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<CABankFeedAccountMapping.accountName, Space>>, IBqlString>.Concat<CABankFeedAccountMapping.accountMask>>), typeof (string))]
  public string AccountNameMask { get; set; }

  /// <summary>
  /// The Acumatica specific cash account id which is linked to a bank feed account.
  /// </summary>
  [PXDBInt]
  [PXSelector(typeof (Search<CashAccount.cashAccountID>), SubstituteKey = typeof (CashAccount.cashAccountCD), DescriptionField = typeof (CashAccount.descr))]
  public virtual int? CashAccountID { get; set; }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  /// <exclude />
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  /// <exclude />
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.bankFeedAccountMapID>
  {
    public static CABankFeedAccountMapping Find(PXGraph graph, Guid? bankFeedAccountMapID)
    {
      return (CABankFeedAccountMapping) PrimaryKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.bankFeedAccountMapID>.FindBy(graph, (object) bankFeedAccountMapID, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class BankFeed : 
      PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.ForeignKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.bankFeedID>
    {
    }

    public class BankFeedDetail : 
      PrimaryKeyOf<CABankFeedDetail>.By<CABankFeedDetail.bankFeedID, CABankFeedDetail.lineNbr>.ForeignKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.bankFeedID, CABankFeedAccountMapping.lineNbr>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.cashAccountID>
    {
    }
  }

  public abstract class bankFeedAccountMapID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedAccountMapping.bankFeedAccountMapID>
  {
  }

  public abstract class bankFeedID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.bankFeedID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedAccountMapping.lineNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedAccountMapping.type>
  {
  }

  public abstract class institutionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.institutionID>
  {
  }

  public abstract class accountName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.accountName>
  {
  }

  public abstract class accountMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.accountMask>
  {
  }

  public abstract class accountNameMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.accountNameMask>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankFeedAccountMapping.cashAccountID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedAccountMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedAccountMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedAccountMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedAccountMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedAccountMapping.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CABankFeedAccountMapping.Tstamp>
  {
  }
}
