// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateInvoice.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateInvoice;

/// <exclude />
[PXHidden]
public class Document : PXMappedCacheExtension
{
  public virtual string OpportunityID { get; set; }

  public virtual Guid? QuoteID { get; set; }

  public virtual string Subject { get; set; }

  public virtual int? BAccountID { get; set; }

  public virtual int? LocationID { get; set; }

  public virtual int? ContactID { get; set; }

  public virtual string TaxZoneID { get; set; }

  public virtual string TaxCalcMode { get; set; }

  public virtual bool? ManualTotalEntry { get; set; }

  public virtual string CuryID { get; set; }

  public virtual long? CuryInfoID { get; set; }

  public virtual Decimal? CuryAmount { get; set; }

  public virtual Decimal? CuryDiscTot { get; set; }

  public virtual int? ProjectID { get; set; }

  public virtual int? BranchID { get; set; }

  public virtual Guid? NoteID { get; set; }

  public virtual DateTime? CloseDate { get; set; }

  public virtual string TermsID { get; set; }

  public virtual string ExternalTaxExemptionNumber { get; set; }

  public virtual string AvalaraCustomerUsageType { get; set; }

  public virtual bool? IsPrimary { get; set; }

  public abstract class opportunityID : IBqlField, IBqlOperand
  {
  }

  public abstract class quoteID : IBqlField, IBqlOperand
  {
  }

  public abstract class subject : IBqlField, IBqlOperand
  {
  }

  public abstract class bAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class locationID : IBqlField, IBqlOperand
  {
  }

  public abstract class contactID : IBqlField, IBqlOperand
  {
  }

  public abstract class taxZoneID : IBqlField, IBqlOperand
  {
  }

  public abstract class taxCalcMode : IBqlField, IBqlOperand
  {
  }

  public abstract class manualTotalEntry : IBqlField, IBqlOperand
  {
  }

  public abstract class curyID : IBqlField, IBqlOperand
  {
  }

  public abstract class curyInfoID : IBqlField, IBqlOperand
  {
  }

  public abstract class curyAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class curyDiscTot : IBqlField, IBqlOperand
  {
  }

  public abstract class projectID : IBqlField, IBqlOperand
  {
  }

  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }

  public abstract class closeDate : IBqlField, IBqlOperand
  {
  }

  public abstract class termsID : IBqlField, IBqlOperand
  {
  }

  public abstract class externalTaxExemptionNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class avalaraCustomerUsageType : IBqlField, IBqlOperand
  {
  }

  public abstract class isPrimary : IBqlField, IBqlOperand
  {
  }
}
