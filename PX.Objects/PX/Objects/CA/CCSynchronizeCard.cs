// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCSynchronizeCard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Update;
using PX.Objects.AR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>Contains credit cards data loaded by service</summary>
[Serializable]
public class CCSynchronizeCard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _CreatedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual int? RecordID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center ID", Enabled = false)]
  public virtual 
  #nullable disable
  string CCProcessingCenterID { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center Cust. Profile ID", Enabled = false)]
  public virtual string CustomerCCPID { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDefault]
  public virtual string CustomerCCPIDHash { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true, IsViewDecrypted = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center Payment Profile ID", Enabled = false)]
  public virtual string PaymentCCPID { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center Cust. ID", Enabled = false)]
  public virtual string PCCustomerID { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center Cust. Descr.", Enabled = false)]
  public virtual string PCCustomerDescription { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true, IsViewDecrypted = true)]
  [PXUIField(DisplayName = "Proc. Center Cust. Email", Enabled = false)]
  public virtual string PCCustomerEmail { get; set; }

  /// <summary>Type of a card associated with the card.</summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Card Type", Enabled = false)]
  [PX.Objects.AR.CardType.List]
  public virtual string CardType { get; set; }

  /// <summary>
  /// Original card type value received from the processing center.
  /// </summary>
  [PXDBString(25, IsFixed = true)]
  [PXUIField(DisplayName = "Proc. Center Card Type", Enabled = false)]
  public virtual string ProcCenterCardTypeCode { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Card Number")]
  public virtual string CardNumber { get; set; }

  [PXDBDateString(DateFormat = "MM/yy")]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true, IsViewDecrypted = true)]
  [PXUIField(DisplayName = "Proc. Center Payment Profile First Name", Enabled = false)]
  public virtual string FirstName { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true, IsViewDecrypted = true)]
  [PXUIField(DisplayName = "Proc. Center Payment Profile Last Name", Enabled = false)]
  public virtual string LastName { get; set; }

  [Customer(typeof (Search<PX.Objects.AR.Customer.bAccountID>), new Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName)}, DisplayName = "Customer ID")]
  public virtual int? BAccountID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("CHC")]
  [PaymentMethodType.List]
  [PXUIField]
  public virtual string PaymentType { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search5<CCProcessingCenterPmntMethod.paymentMethodID, InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethod.paymentType, Equal<Current<CCSynchronizeCard.paymentType>>>>>, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCSynchronizeCard.cCProcessingCenterID>>, And<PaymentMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<PaymentMethod.useForAR, Equal<True>>>>>, Aggregate<GroupBy<CCProcessingCenterPmntMethod.paymentMethodID>>>), new Type[] {typeof (CCProcessingCenterPmntMethod.paymentMethodID), typeof (PaymentMethod.descr)})]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual string PaymentMethodID { get; set; }

  [CashAccount(null, typeof (Search2<CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CCSynchronizeCard.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
  [PXUIField(DisplayName = "Cash Account")]
  public virtual int? CashAccountID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Imported { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public static string GetSha1HashString(string input)
  {
    byte[] sha = PXCriptoHelper.CalculateSHA(input);
    string empty = string.Empty;
    foreach (byte num in sha)
      empty += num.ToString("X2");
    return empty;
  }

  public class PK : PrimaryKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.recordID>
  {
    public static CCSynchronizeCard Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (CCSynchronizeCard) PrimaryKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.bAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.cashAccountID>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.cCProcessingCenterID>
    {
    }

    public class ProcessingCenterPaymentMethod : 
      PrimaryKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.processingCenterID, CCProcessingCenterPmntMethod.paymentMethodID>.ForeignKeyOf<CCSynchronizeCard>.By<CCSynchronizeCard.cCProcessingCenterID, CCSynchronizeCard.paymentMethodID>
    {
    }
  }

  public abstract class recordID : IBqlField, IBqlOperand
  {
  }

  public abstract class cCProcessingCenterID : IBqlField, IBqlOperand
  {
  }

  public abstract class customerCCPID : IBqlField, IBqlOperand
  {
  }

  public abstract class customerCCPIDHash : IBqlField, IBqlOperand
  {
  }

  public abstract class paymentCCPID : IBqlField, IBqlOperand
  {
  }

  public abstract class pCCustomerID : IBqlField, IBqlOperand
  {
  }

  public abstract class pCCustomerDescription : IBqlField, IBqlOperand
  {
  }

  public abstract class pCCustomerEmail : IBqlField, IBqlOperand
  {
  }

  public abstract class cardType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCSynchronizeCard.cardType>
  {
  }

  public abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCSynchronizeCard.procCenterCardTypeCode>
  {
  }

  public abstract class cardNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class expirationDate : IBqlField, IBqlOperand
  {
  }

  public abstract class firstName : IBqlField, IBqlOperand
  {
  }

  public abstract class lastName : IBqlField, IBqlOperand
  {
  }

  public abstract class bAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class paymentType : IBqlField, IBqlOperand
  {
  }

  public abstract class paymentMethodID : IBqlField, IBqlOperand
  {
  }

  public abstract class cashAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class imported : IBqlField, IBqlOperand
  {
  }

  public abstract class createdDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class selected : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }
}
