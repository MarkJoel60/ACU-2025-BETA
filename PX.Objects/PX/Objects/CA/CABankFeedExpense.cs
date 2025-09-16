// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedExpense
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Bank Feed Expense Items")]
public class CABankFeedExpense : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CABankFeed.bankFeedID))]
  [PXParent(typeof (CABankFeedExpense.FK.BankFeed))]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CABankFeed))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1)]
  [PXDefault("S")]
  [CABankFeedMatchRule.List(false)]
  [PXUIField(DisplayName = "Rule", Required = true)]
  public virtual string MatchRule { get; set; }

  [PXDBString(1)]
  [PXDefault("C")]
  [CABankFeedMatchField.List(CABankFeedMatchField.SetOfValues.ExpenseReceipts)]
  [PXUIField(DisplayName = "Field to Match", Required = true)]
  public virtual string MatchField { get; set; }

  [PXDefault]
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Value", Required = true)]
  public virtual string MatchValue { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.expenseItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXUIField(DisplayName = "Expense Item")]
  public virtual int? InventoryItemID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip")]
  public virtual bool? DoNotCreate { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankFeedExpense>.By<CABankFeedExpense.bankFeedID, CABankFeedExpense.lineNbr>
  {
    public static CABankFeedExpense Find(
      PXGraph graph,
      string bankFeedID,
      int? bankFeedExpenseID,
      PKFindOptions options = 0)
    {
      return (CABankFeedExpense) PrimaryKeyOf<CABankFeedExpense>.By<CABankFeedExpense.bankFeedID, CABankFeedExpense.lineNbr>.FindBy(graph, (object) bankFeedID, (object) bankFeedExpenseID, options);
    }
  }

  public static class FK
  {
    public class BankFeed : 
      PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.ForeignKeyOf<CABankFeedExpense>.By<CABankFeedExpense.bankFeedID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<CABankFeedExpense>.By<CABankFeedExpense.inventoryItemID>
    {
    }
  }

  public abstract class bankFeedID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedExpense.bankFeedID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedExpense.lineNbr>
  {
  }

  public abstract class matchRule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedExpense.matchRule>
  {
  }

  public abstract class matchField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedExpense.matchField>
  {
  }

  public abstract class matchValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedExpense.matchValue>
  {
  }

  public abstract class inventoryItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankFeedExpense.inventoryItemID>
  {
  }

  public abstract class doNotCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeedExpense.doNotCreate>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankFeedExpense.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedExpense.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedExpense.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedExpense.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedExpense.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedExpense.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedExpense.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedExpense.noteid>
  {
  }
}
