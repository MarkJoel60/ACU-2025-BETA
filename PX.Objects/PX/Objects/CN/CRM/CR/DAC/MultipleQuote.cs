// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.CRM.CR.DAC.MultipleQuote
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CN.Common.DAC;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CN.CRM.CR.DAC;

[PXCacheName("Multiple Customers")]
[Serializable]
public class MultipleQuote : BaseCache, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? MultipleQuoteID { get; set; }

  [PXDBString]
  public virtual string OpportunityID { get; set; }

  [CustomerAndProspect(null, null, null, DisplayName = "Business Account")]
  public virtual int? BusinessAccountID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<Contact.contactID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<BAccount.bAccountID, Equal<Current<MultipleQuote.businessAccountID>>, Or<Current<MultipleQuote.businessAccountID>, IsNull>>>), DescriptionField = typeof (Contact.displayName), Filterable = true, ValidateValue = false)]
  [PXRestrictor(typeof (Where<Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.lead>>>, And<Where<BAccount.type, IsNull, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>>>), "Contact '{0}' ({1}) has opportunities for another business account.", new System.Type[] {typeof (Contact.displayName), typeof (Contact.contactID)})]
  [PXRestrictor(typeof (Where<Contact.isActive, Equal<True>>), "Contact '{0}' is inactive or closed.", new System.Type[] {typeof (Contact.displayName)})]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXUIField(DisplayName = "Contact")]
  public virtual int? ContactID { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quoted Amount")]
  public virtual Decimal? QuotedAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Estimate")]
  public virtual Decimal? CostEstimate { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Sub<MultipleQuote.quotedAmount, MultipleQuote.costEstimate>))]
  [PXUIField(DisplayName = "Gross Margin", Enabled = false)]
  public virtual Decimal? GrossMarginAbsolute { get; set; }

  [PXDecimal(2)]
  [PXFormula(typeof (Switch<Case<Where<MultipleQuote.quotedAmount, NotEqual<decimal0>>, Mult<Div<Sub<MultipleQuote.quotedAmount, MultipleQuote.costEstimate>, MultipleQuote.quotedAmount>, decimal100>>, decimal0>))]
  [PXUIField(DisplayName = "Gross Margin, %", Enabled = false)]
  public virtual Decimal? GrossMarginPercentage { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Quoted On")]
  public virtual DateTime? QuotedOn { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? IsSelected { get; set; }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Default<MultipleQuote.quotedAmount>))]
  [PXUIField(DisplayName = "Final Amount")]
  public virtual Decimal? FinalAmount { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Sub<MultipleQuote.finalAmount, MultipleQuote.costEstimate>))]
  [PXUIField(DisplayName = "Final Gross Margin", Enabled = false)]
  public virtual Decimal? FinalGrossMarginAbsolute { get; set; }

  [PXDecimal(2)]
  [PXFormula(typeof (Switch<Case<Where<MultipleQuote.finalAmount, NotEqual<decimal0>>, Mult<Div<Sub<MultipleQuote.finalAmount, MultipleQuote.costEstimate>, MultipleQuote.finalAmount>, decimal100>>, decimal0>))]
  [PXUIField(DisplayName = "Final Gross Margin, %", Enabled = false)]
  public virtual Decimal? FinalGrossMarginPercentage { get; set; }

  [PXDBCreatedByID(Visible = false)]
  public override Guid? CreatedById { get; set; }

  [PXDBLastModifiedByID(Visible = false)]
  public override Guid? LastModifiedById { get; set; }

  public abstract class multipleQuoteID : IBqlField, IBqlOperand
  {
  }

  public abstract class opportunityID : IBqlField, IBqlOperand
  {
  }

  public abstract class businessAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class contactID : IBqlField, IBqlOperand
  {
  }

  public abstract class quotedAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class costEstimate : IBqlField, IBqlOperand
  {
  }

  public abstract class grossMarginAbsolute : IBqlField, IBqlOperand
  {
  }

  public abstract class grossMarginPercentage : IBqlField, IBqlOperand
  {
  }

  public abstract class quotedOn : IBqlField, IBqlOperand
  {
  }

  public abstract class isSelected : IBqlField, IBqlOperand
  {
  }

  public abstract class finalAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class finalGrossMarginAbsolute : IBqlField, IBqlOperand
  {
  }

  public abstract class finalGrossMarginPercentage : IBqlField, IBqlOperand
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }

  public abstract class createdByID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }
}
