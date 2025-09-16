// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO.LandedCosts;
using PX.Objects.PO.LandedCosts.Attributes;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Document")]
[PXPrimaryGraph(typeof (POLandedCostDocEntry))]
[PXGroupMask(typeof (InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POLandedCostDoc.vendorID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class POLandedCostDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  protected bool? _OpenDoc;
  protected bool? _Released;
  protected bool? _Hold;
  protected DateTime? _DocDate;
  protected int? _OwnerID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>Type of the document.</summary>
  /// <value>
  /// Possible values are: "LC" - Landed Cost, "LCC" - Correction, "LCR" - Reversal.
  /// </value>
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault("L")]
  [POLandedCostDocType.List]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<POLandedCostDoc.refNbr, Where<POLandedCostDoc.docType, Equal<Optional<POLandedCostDoc.docType>>>>), Filterable = true)]
  [PXFieldDescription]
  [LandedCostDocNumbering]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (Coalesce<Search<PX.Objects.CR.Location.vBranchID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POLandedCostDoc.vendorLocationID>>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = false)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is open.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Open", Visible = false)]
  public virtual bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Visible = false, Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  [PXDefault(true, typeof (POSetup.holdLandedCosts))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// Status of the document. The field is calculated based on the values of status flag. It can't be changed directly.
  /// The fields tht determine status of a document are: <see cref="F:PX.Objects.PO.LandedCosts.Attributes.POLandedCostDocStatus.Hold" />, <see cref="F:PX.Objects.PO.LandedCosts.Attributes.POLandedCostDocStatus.Balanced" />, <see cref="F:PX.Objects.PO.LandedCosts.Attributes.POLandedCostDocStatus.Released" />.
  /// </summary>
  /// <value>
  /// Possible values are:
  /// <c>"H"</c> - Hold, <c>"B"</c> - Balanced, <c>"R"</c> - Released.
  /// Defaults to Hold.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [POLandedCostDocStatus.List]
  public virtual string Status { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the amount of tax calculated with the external External Tax Provider is up to date.
  /// If this field equals <c>false</c>, the document was updated since last synchronization with the Tax Engine
  /// and taxes might need recalculation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// Get or set NonTaxable that mark current document does not impose sales taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non-Taxable", Enabled = false)]
  public virtual bool? NonTaxable { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  /// <summary>
  /// <see cref="!:FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period, to which the <see cref="P:PX.Objects.PO.POLandedCostDoc.DocDate" /> belongs, but can be overriden by user.
  /// </value>
  [POOpenPeriod(typeof (POLandedCostDoc.docDate), typeof (POLandedCostDoc.branchID), null, null, null, null, true, false, typeof (POLandedCostDoc.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// <see cref="!:FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Determined by the <see cref="P:PX.Objects.PO.POLandedCostDoc.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.PO.POLandedCostDoc.FinPeriodID" />
  /// the value of this field can't be overriden by user.
  /// </value>
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Transaction Period")]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
  /// </summary>
  [LandedCostVendorActive]
  [PXDefault]
  [PXForeignReference(typeof (POLandedCostDoc.FK.Vendor))]
  public virtual int? VendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location">Location</see> of the <see cref="T:PX.Objects.AP.Vendor">Vendor</see>, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field. Defaults to vendor's <see cref="!:Vendor.DefLocationID">default location</see>.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<POLandedCostDoc.vendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AP.Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<POLandedCostDoc.vendorID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<POLandedCostDoc.vendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? VendorLocationID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.taxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// Counter of the document lines, used <i>internally</i> to assign numbers to newly created lines.
  /// It is not recommended to rely on this fields to determine the exact count of lines, because it might not reflect the latter under various conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.curyID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// Get or set CreateBill that mark current document create bill on release.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (POSetup.autoCreateLCAP))]
  [PXUIField(DisplayName = "Create Bill", Enabled = true)]
  public virtual bool? CreateBill { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [POLandedCostDocVendorRefNbr]
  public virtual string VendorRefNbr { get; set; }

  /// <summary>
  /// The document total presented in the currency of the document. (See <see cref="P:PX.Objects.PO.POLandedCostDoc.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.lineTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  /// <summary>
  /// The document total presented in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  /// <summary>
  /// The document total presented in the currency of the document. (See <see cref="P:PX.Objects.PO.POLandedCostDoc.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.docTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDocTotal { get; set; }

  /// <summary>
  /// The document total presented in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocTotal { get; set; }

  /// <summary>
  /// The total allocated amount of the document.
  /// Given in the <see cref="P:PX.Objects.PO.POLandedCostDoc.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.allocatedTotal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryAllocatedTotal { get; set; }

  /// <summary>
  /// The total allocated amount of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedTotal { get; set; }

  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.controlTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlTotal { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.termsID, Where2<FeatureInstalled<FeaturesSet.vendorRelations>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.payToVendorID>>, Or2<Not<FeatureInstalled<FeaturesSet.vendorRelations>>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>>>>>>))]
  [PXUIField]
  [Terms(typeof (POLandedCostDoc.billDate), typeof (POLandedCostDoc.dueDate), typeof (POLandedCostDoc.discDate), typeof (POLandedCostDoc.curyDocTotal), typeof (POLandedCostDoc.curyDiscAmt), typeof (POLandedCostDoc.curyTaxTotal), typeof (POLandedCostDoc.branchID))]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID { get; set; }

  [PXDBDate]
  [PXUIField]
  [PXFormula(typeof (POLandedCostDoc.docDate))]
  [PXDefault(typeof (POLandedCostDoc.docDate))]
  public virtual DateTime? BillDate { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DueDate { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DiscDate { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.discAmt))]
  [PXUIField]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vTaxZoneID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POLandedCostDoc.vendorLocationID>>>>>))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor, whom the AP bill will belong to.
  /// </value>
  [PXFormula(typeof (Validate<POLandedCostDoc.curyID>))]
  [POLandedCostPayToVendor(CacheGlobal = true, Filterable = true)]
  [PXDefault]
  [PXForeignReference(typeof (POLandedCostDoc.FK.PayToVendor))]
  public virtual int? PayToVendorID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  [PXDefault(typeof (PX.Objects.AP.Vendor.ownerID))]
  [Owner(typeof (POLandedCostDoc.workgroupID))]
  public virtual int? OwnerID { get; set; }

  int? IAssign.WorkgroupID
  {
    get => this.WorkgroupID;
    set => this.WorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.OwnerID;
    set => this.OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? APDocCreated { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? INDocCreated { get; set; }

  /// <summary>
  /// The part of the document total that is exempt from VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are marked as <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>
  /// and are neither <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see> nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.PO.POLandedCostDoc.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The part of the document total that is exempt from VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are marked as <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>
  /// and are neither <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see> nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The part of the document total, which is subject to VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are neither <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>,
  /// nor <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see>, nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.PO.POLandedCostDoc.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (POLandedCostDoc.curyInfoID), typeof (POLandedCostDoc.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The part of the document total, which is subject to VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are neither <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>,
  /// nor <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see>, nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  [PXSearchable(128 /*0x80*/, "{0}: {1} - {3}", new System.Type[] {typeof (POLandedCostDoc.docType), typeof (POLandedCostDoc.refNbr), typeof (POLandedCostDoc.vendorID), typeof (PX.Objects.AP.Vendor.acctName)}, new System.Type[] {typeof (POLandedCostDoc.vendorRefNbr)}, NumberFields = new System.Type[] {typeof (POLandedCostDoc.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (POLandedCostDoc.docDate), typeof (POLandedCostDoc.status), typeof (POLandedCostDoc.vendorRefNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (POLandedCostDoc.curyDocTotal)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POLandedCostDoc.vendorID>>>), SelectForFastIndexing = typeof (Select2<POLandedCostDoc, InnerJoin<PX.Objects.AP.Vendor, On<POLandedCostDoc.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<POLandedCostDoc.refNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POLandedCostDoc.vendorID>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<POLandedCostDoc.refNbr>>>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>
  {
    public static POLandedCostDoc Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (POLandedCostDoc) PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.vendorID>
    {
    }

    public class VandorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.vendorID, POLandedCostDoc.vendorLocationID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.curyInfoID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.termsID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.taxZoneID>
    {
    }

    public class PayToVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.payToVendorID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.workgroupID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<POLandedCostDoc>.By<POLandedCostDoc.ownerID>
    {
    }
  }

  public class Events : PXEntityEventBase<POLandedCostDoc>.Container<POLandedCostDoc.Events>
  {
    public PXEntityEvent<POLandedCostDoc> InventoryAdjustmentCreated;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.selected>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.refNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.branchID>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.openDoc>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.hold>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.status>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.isTaxValid>
  {
  }

  public abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.nonTaxable>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLandedCostDoc.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDoc.tranPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.vendorID>
  {
    public class PreventEditBAccountVOrgBAccountID<TGraph> : 
      PreventEditBAccountRestrictToBase<PX.Objects.CR.BAccount.vOrgBAccountID, TGraph, POLandedCostDoc, SelectFromBase<POLandedCostDoc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
      #nullable enable
      POLandedCostDoc.vendorID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>>
      where TGraph : 
      #nullable disable
      PXGraph
    {
      protected override string GetErrorMessage(
        PX.Objects.CR.BAccount baccount,
        POLandedCostDoc document,
        string documentBaseCurrency)
      {
        return PXMessages.LocalizeFormatNoPrefix("A branch with the base currency other than {0} cannot be associated with the {1} vendor because {1} is selected in the {2} landed cost.", new object[3]
        {
          (object) documentBaseCurrency,
          (object) baccount.AcctCD,
          (object) document.RefNbr
        });
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnVendorMaint : 
      POLandedCostDoc.vendorID.PreventEditBAccountVOrgBAccountID<VendorMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnCustomerMaint : 
      POLandedCostDoc.vendorID.PreventEditBAccountVOrgBAccountID<CustomerMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POLandedCostDoc.vendorLocationID>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDoc.taxTotal>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.lineCntr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostDoc.curyInfoID>
  {
  }

  public abstract class createBill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.createBill>
  {
  }

  public abstract class vendorRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDoc.vendorRefNbr>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDoc.lineTotal>
  {
  }

  public abstract class curyDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyDocTotal>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDoc.docTotal>
  {
  }

  public abstract class curyAllocatedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyAllocatedTotal>
  {
  }

  public abstract class allocatedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.allocatedTotal>
  {
  }

  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyControlTotal>
  {
  }

  public abstract class controlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.controlTotal>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.termsID>
  {
  }

  public abstract class billDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLandedCostDoc.billDate>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLandedCostDoc.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLandedCostDoc.discDate>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDoc.discAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDoc.taxZoneID>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.payToVendorID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDoc.ownerID>
  {
  }

  public abstract class aPDocCreated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.aPDocCreated>
  {
  }

  public abstract class iNDocCreated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDoc.iNDocCreated>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDoc.vatTaxableTotal>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostDoc.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostDoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostDoc.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLandedCostDoc.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDoc.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostDoc.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostDoc.Tstamp>
  {
  }
}
