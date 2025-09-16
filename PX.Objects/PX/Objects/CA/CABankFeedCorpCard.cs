// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedCorpCard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.Objects.EP.DAC;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Bank Feed Corporate Cards")]
public class CABankFeedCorpCard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CABankFeed.bankFeedID))]
  [PXParent(typeof (CABankFeedCorpCard.FK.BankFeed))]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CABankFeed))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(100)]
  [PXDefault]
  [PXUIField(DisplayName = "Account Name", Required = true)]
  [PXSelector(typeof (Search<CABankFeedDetail.accountID, Where<CABankFeedDetail.bankFeedID, Equal<Current<CABankFeed.bankFeedID>>, And<CABankFeedDetail.cashAccountID, IsNotNull>>>), new Type[] {typeof (CABankFeedDetail.accountName), typeof (CABankFeedDetail.accountMask)})]
  public virtual string AccountID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<CashAccount.cashAccountID>), SubstituteKey = typeof (CashAccount.cashAccountCD), DescriptionField = typeof (CashAccount.descr))]
  [PXUIField(DisplayName = "Cash Account", Enabled = false)]
  public virtual int? CashAccountID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Corporate Card ID", Required = true)]
  [PXSelector(typeof (Search<CACorpCard.corpCardID, Where<CACorpCard.isActive, Equal<True>, And<CACorpCard.cashAccountID, Equal<Current<CABankFeedCorpCard.cashAccountID>>>>>), SubstituteKey = typeof (CACorpCard.corpCardCD), DescriptionField = typeof (CACorpCard.name))]
  public virtual int? CorpCardID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<CABankFeedCorpCard.corpCardID, CACorpCard.cardNumber>))]
  [PXUIField(DisplayName = "Card Number", Enabled = false)]
  public virtual string CardNumber { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<CABankFeedCorpCard.corpCardID, CACorpCard.name>))]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public virtual string CardName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (Search2<EPEmployee.bAccountID, InnerJoin<EPEmployeeCorpCardLink, On<EPEmployee.bAccountID, Equal<EPEmployeeCorpCardLink.employeeID>>>, Where<EPEmployeeCorpCardLink.corpCardID, Equal<Current<CABankFeedCorpCard.corpCardID>>>>), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Employee ID", Required = true)]
  public virtual int? EmployeeID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<CABankFeedCorpCard.employeeID, EPEmployee.acctName>))]
  [PXUIField(DisplayName = "Employee Name", Enabled = false)]
  public virtual string EmployeeName { get; set; }

  [PXDBString(1)]
  [PXDefault("N")]
  [CABankFeedMatchField.List(CABankFeedMatchField.SetOfValues.CorporateCard)]
  [PXUIField(DisplayName = "Field to Match")]
  public virtual string MatchField { get; set; }

  [PXDBString(1)]
  [PXDefault("N")]
  [CABankFeedMatchRule.List(true)]
  [PXUIField(DisplayName = "Rule")]
  public virtual string MatchRule { get; set; }

  [PXDefault]
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Value", Enabled = false)]
  public virtual string MatchValue { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

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

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankFeedCorpCard>.By<CABankFeedCorpCard.bankFeedID, CABankFeedCorpCard.lineNbr>
  {
    public static CABankFeedCorpCard Find(
      PXGraph graph,
      string bankFeedID,
      int? bankFeedCorpCardID,
      PKFindOptions options = 0)
    {
      return (CABankFeedCorpCard) PrimaryKeyOf<CABankFeedCorpCard>.By<CABankFeedCorpCard.bankFeedID, CABankFeedCorpCard.lineNbr>.FindBy(graph, (object) bankFeedID, (object) bankFeedCorpCardID, options);
    }
  }

  public static class FK
  {
    public class BankFeed : 
      PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.ForeignKeyOf<CABankFeedCorpCard>.By<CABankFeedCorpCard.bankFeedID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<CABankFeedCorpCard>.By<CABankFeedCorpCard.employeeID>
    {
    }
  }

  public abstract class bankFeedID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.bankFeedID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedCorpCard.lineNbr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.accountID>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedCorpCard.cashAccountID>
  {
  }

  public abstract class corpCardID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedCorpCard.corpCardID>
  {
  }

  public abstract class cardNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.cardNumber>
  {
  }

  public abstract class cardName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.cardName>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedCorpCard.employeeID>
  {
  }

  public abstract class employeeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedCorpCard.employeeName>
  {
  }

  public abstract class matchField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.matchField>
  {
  }

  public abstract class matchRule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.matchRule>
  {
  }

  public abstract class matchValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedCorpCard.matchValue>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankFeedCorpCard.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedCorpCard.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedCorpCard.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedCorpCard.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedCorpCard.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedCorpCard.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedCorpCard.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedCorpCard.noteid>
  {
  }
}
