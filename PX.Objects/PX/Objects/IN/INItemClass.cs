// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.Maintenance;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INItemClassMaint))]
[PXCacheName]
public class INItemClass : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  public const 
  #nullable disable
  string Dimension = "INITEMCLASS";
  protected bool? _ExportToExternal;

  [PXDBIdentity]
  [PXUIField]
  public virtual int? ItemClassID { get; set; }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (SearchFor<INItemClass.itemClassCD>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<True>>>>>.And<FeatureInstalled<FeaturesSet.distributionModule>>>>), DescriptionField = typeof (INItemClass.descr))]
  [PXFieldDescription]
  public virtual string ItemClassCD { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "Stock Item")]
  public virtual bool? StkItem { get; set; }

  /// <summary>
  /// The field is used to populate standard settings of <see cref="T:PX.Objects.IN.INItemClass">Item Class</see> from it's parent or default one.
  /// </summary>
  [PXInt]
  public virtual int? ParentItemClassID { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField(DisplayName = "Allow Negative Quantity")]
  public virtual bool? NegQty { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField(DisplayName = "Availability Calculation Rule")]
  [PXSelector(typeof (INAvailabilityScheme.availabilitySchemeID), DescriptionField = typeof (INAvailabilityScheme.description))]
  public virtual string AvailabilitySchemeID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [INValMethod.List]
  [PXUIEnabled(typeof (INItemClass.stkItem))]
  [PXFormula(typeof (IIf<Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsNotEqual<True>>, INValMethod.standard, INItemClass.valMethod>))]
  [PXUIField(DisplayName = "Valuation Method")]
  public virtual string ValMethod { get; set; }

  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.baseUnit))]
  [INSyncUoms(new System.Type[] {typeof (INItemClass.salesUnit), typeof (INItemClass.purchaseUnit)})]
  [INUnit(DisplayName = "Base Unit")]
  public virtual string BaseUnit { get; set; }

  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.salesUnit))]
  [INUnit(null, typeof (INItemClass.baseUnit))]
  public virtual string SalesUnit { get; set; }

  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.purchaseUnit))]
  [INUnit(null, typeof (INItemClass.baseUnit))]
  public virtual string PurchaseUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.INItemClass.BaseUnit">Base Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [INSyncUoms(new System.Type[] {typeof (INItemClass.decimalSalesUnit), typeof (INItemClass.decimalPurchaseUnit)})]
  [PXUIField]
  public virtual bool? DecimalBaseUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.INItemClass.SalesUnit">Sales Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? DecimalSalesUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.INItemClass.PurchaseUnit">Purchase Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? DecimalPurchaseUnit { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<INPostClass.postClassID>), DescriptionField = typeof (INPostClass.descr))]
  [PXUIField(DisplayName = "Posting Class")]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string PostClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<INLotSerClass.lotSerClassID>), DescriptionField = typeof (INLotSerClass.descr))]
  [PXUIField(DisplayName = "Lot/Serial Class")]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string LotSerClassID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.TX.TaxCategory.active, IBqlBool>.IsEqual<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T")]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID>))]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string DeferredCode { get; set; }

  [PXDBString(1, IsFixed = true)]
  [INItemTypes.List]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField(DisplayName = "Item Type")]
  public virtual string ItemType { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXSelector(typeof (INPriceClass.priceClassID), DescriptionField = typeof (INPriceClass.description))]
  [PXUIField]
  public virtual string PriceClassID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Price Workgroup")]
  public virtual int? PriceWorkgroupID { get; set; }

  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [Owner(typeof (InventoryItem.priceWorkgroupID), DisplayName = "Price Manager")]
  public virtual int? PriceManagerID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? MinGrossProfitPct { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1000.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField(DisplayName = "Markup %")]
  public virtual Decimal? MarkupPct { get; set; }

  [PXNote(DescriptionField = typeof (INItemClass.itemClassCD), Selector = typeof (INItemClass.itemClassCD))]
  public virtual Guid? NoteID { get; set; }

  /// <summary>Demand calculation</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Demand Calculation")]
  [PXDefault("I", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.demandCalculation))]
  [PXUIVisible(typeof (Where<INItemClass.planningMethod, Equal<INPlanningMethod.inventoryReplenishment>, And<INItemClass.stkItem, Equal<True>>>))]
  [INDemandCalculation.List]
  public virtual string DemandCalculation { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [CommodityCodeTypes.List]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField(DisplayName = "Commodity Code Type")]
  public virtual string CommodityCodeType { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Commodity Code")]
  public virtual string HSTariffCode { get; set; }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Undership Threshold (%)")]
  public virtual Decimal? UndershipThreshold { get; set; }

  [PXDBDecimal(2, MinValue = 100.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Overship Threshold (%)")]
  public virtual Decimal? OvershipThreshold { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Country Of Origin")]
  [Country]
  public virtual string CountryOfOrigin { get; set; }

  /// <summary>
  /// When set to Sales, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBString]
  [PXDefault("P")]
  [PXUIEnabled(typeof (Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<False>>))]
  [PXFormula(typeof (IIf<Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>, InventoryItem.postToExpenseAccount.purchases, InventoryItem.postToExpenseAccount>))]
  [InventoryItem.postToExpenseAccount.List]
  [PXUIField(DisplayName = "Post Cost to Expenses On")]
  public virtual 
  #nullable enable
  string? PostToExpenseAccount { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBGroupMask]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>))]
  public virtual byte[] GroupMask { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included { get; set; }

  [PXString]
  [PXUIField]
  public virtual string ItemClassStrID => this.ItemClassID.ToString();

  [PXString(IsUnicode = true)]
  [PXUIField]
  [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
  public virtual string ItemClassCDWildcard
  {
    get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
    set
    {
    }
  }

  /// <summary>
  /// Contains Inventory ID Example <see cref="T:PX.Objects.IN.Matrix.DAC.Projections.IDGenerationRule" />
  /// </summary>
  [PXString]
  public virtual string SampleID { get; set; }

  /// <summary>
  /// Contains Inventory Description Example <see cref="T:PX.Objects.IN.Matrix.DAC.Projections.DescriptionGenerationRule" />
  /// </summary>
  [PXString]
  public virtual string SampleDescription { get; set; }

  /// <summary>
  /// References to Attribute which will be put as Row Attribute in Inventory Matrix by default.
  /// </summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Row Attribute ID", FieldClass = "MatrixItem")]
  [PXSelector(typeof (SearchFor<CSAttributeGroup.attributeID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.entityClassID, Equal<RTrim<Use<BqlField<INItemClass.itemClassID, IBqlInt>.FromCurrent>.AsString>>>>>, And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<Constants.DACName<InventoryItem>>>>, And<BqlOperand<CSAttributeGroup.attributeCategory, IBqlString>.IsEqual<CSAttributeGroup.attributeCategory.variant>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.attributeID, NotEqual<BqlField<INItemClass.defaultColumnMatrixAttributeID, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<Current<INItemClass.defaultColumnMatrixAttributeID>, IBqlString>.IsNull>>>), new System.Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  [PXRestrictor(typeof (Where<BqlOperand<CSAttributeGroup.isActive, IBqlBool>.IsEqual<True>>), "The {0} attribute is inactive. Specify an active attribute.", new System.Type[] {typeof (CSAttributeGroup.attributeID)})]
  public virtual string DefaultRowMatrixAttributeID { get; set; }

  /// <summary>
  /// Planning method - Decide which planning method applicable for the stock items which has used the respective Item Class.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Planning Method", FieldClass = "InvPlanning")]
  [PXDefault("N")]
  [INPlanningMethod.List]
  [PXUIVisible(typeof (Where<INItemClass.stkItem, Equal<True>>))]
  public string PlanningMethod { get; set; }

  /// <summary>Source - source of fulfillment</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXDefault("N")]
  [INReplenishmentSource.List]
  public string ReplenishmentSource { get; set; }

  /// <summary>
  /// References to Attribute which will be put as Column Attribute in Inventory Matrix by default.
  /// </summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Column Attribute ID", FieldClass = "MatrixItem")]
  [PXSelector(typeof (SearchFor<CSAttributeGroup.attributeID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.entityClassID, Equal<RTrim<Use<BqlField<INItemClass.itemClassID, IBqlInt>.FromCurrent>.AsString>>>>>, And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<Constants.DACName<InventoryItem>>>>, And<BqlOperand<CSAttributeGroup.attributeCategory, IBqlString>.IsEqual<CSAttributeGroup.attributeCategory.variant>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.attributeID, NotEqual<BqlField<INItemClass.defaultRowMatrixAttributeID, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<Current<INItemClass.defaultRowMatrixAttributeID>, IBqlString>.IsNull>>>), new System.Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  [PXRestrictor(typeof (Where<BqlOperand<CSAttributeGroup.isActive, IBqlBool>.IsEqual<True>>), "The {0} attribute is inactive. Specify an active attribute.", new System.Type[] {typeof (CSAttributeGroup.attributeID)})]
  public virtual string DefaultColumnMatrixAttributeID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? GenerationRuleCntr { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Export to External System")]
  [PXDefault(true)]
  public virtual bool? ExportToExternal
  {
    get => this._ExportToExternal;
    set => this._ExportToExternal = value;
  }

  /// <exclude />
  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Preferred Item Classes", FieldClass = "RelatedItemAssistant")]
  [PreferredItemClassesList.List]
  public virtual string PreferredItemClasses { get; set; }

  /// <exclude />
  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Price Of Suggested Items", FieldClass = "RelatedItemAssistant")]
  [PriceOfSuggestedItemsList.List]
  public virtual string PriceOfSuggestedItems { get; set; }

  public class dimension : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemClass.dimension>
  {
    public dimension()
      : base("INITEMCLASS")
    {
    }
  }

  public class PK : PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>
  {
    public static INItemClass Find(PXGraph graph, int? itemClassID, PKFindOptions options = 0)
    {
      return (INItemClass) PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.FindBy(graph, (object) itemClassID, options);
    }

    public static INItemClass FindDirty(PXGraph graph, int? itemClassID)
    {
      return PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXViewOf<INItemClass>.BasedOn<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
      {
        (object) itemClassID
      }));
    }
  }

  public static class FK
  {
    public class AvailabilityScheme : 
      PrimaryKeyOf<INAvailabilityScheme>.By<INAvailabilityScheme.availabilitySchemeID>.ForeignKeyOf<INItemClass>.By<INItemClass.availabilitySchemeID>
    {
    }

    public class PostClass : 
      PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>.ForeignKeyOf<INItemClass>.By<INItemClass.postClassID>
    {
    }

    public class PriceClass : 
      PrimaryKeyOf<INPriceClass>.By<INPriceClass.priceClassID>.ForeignKeyOf<INItemClass>.By<INItemClass.priceClassID>
    {
    }

    public class LotSerialClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<INItemClass>.By<INItemClass.lotSerClassID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<INItemClass>.By<INItemClass.taxCategoryID>
    {
    }

    public class DeferredCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<INItemClass>.By<INItemClass.deferredCode>
    {
    }

    public class PriceWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<INItemClass>.By<INItemClass.priceWorkgroupID>
    {
    }

    public class PriceManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<INItemClass>.By<INItemClass.priceManagerID>
    {
    }

    public class ContryOfOrigin : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<INItemClass>.By<INItemClass.countryOfOrigin>
    {
    }
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClass.itemClassID>
  {
  }

  public abstract class itemClassCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.itemClassCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.descr>
  {
  }

  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemClass.stkItem>
  {
  }

  public abstract class parentItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemClass.parentItemClassID>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemClass.negQty>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.availabilitySchemeID>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.valMethod>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.baseUnit>
  {
  }

  public abstract class salesUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.salesUnit>
  {
  }

  public abstract class purchaseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.purchaseUnit>
  {
  }

  public abstract class decimalBaseUnit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemClass.decimalBaseUnit>
  {
  }

  public abstract class decimalSalesUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemClass.decimalSalesUnit>
  {
  }

  public abstract class decimalPurchaseUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemClass.decimalPurchaseUnit>
  {
  }

  public abstract class postClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.postClassID>
  {
  }

  public abstract class lotSerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.lotSerClassID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.taxCategoryID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.taxCalcMode>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.deferredCode>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.itemType>
  {
  }

  public abstract class priceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.priceClassID>
  {
  }

  public abstract class priceWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClass.priceWorkgroupID>
  {
  }

  public abstract class priceManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClass.priceManagerID>
  {
  }

  public abstract class minGrossProfitPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClass.minGrossProfitPct>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemClass.markupPct>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemClass.noteID>
  {
  }

  public abstract class demandCalculation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.demandCalculation>
  {
  }

  public abstract class commodityCodeType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.commodityCodeType>
  {
  }

  public abstract class hSTariffCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.hSTariffCode>
  {
  }

  public abstract class undershipThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClass.undershipThreshold>
  {
  }

  public abstract class overshipThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClass.overshipThreshold>
  {
  }

  public abstract class countryOfOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.countryOfOrigin>
  {
  }

  public abstract class postToExpenseAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemClass.postToExpenseAccount>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClass.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemClass.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemClass.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemClass.included>
  {
  }

  public abstract class itemClassStrID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.itemClassStrID>
  {
  }

  public abstract class itemClassCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.itemClassCDWildcard>
  {
  }

  public abstract class sampleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClass.sampleID>
  {
  }

  public abstract class sampleDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.sampleDescription>
  {
  }

  public abstract class defaultRowMatrixAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.defaultRowMatrixAttributeID>
  {
  }

  public abstract class planningMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.planningMethod>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.replenishmentSource>
  {
  }

  public abstract class defaultColumnMatrixAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.defaultColumnMatrixAttributeID>
  {
  }

  public abstract class generationRuleCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemClass.generationRuleCntr>
  {
  }

  public abstract class exportToExternal : IBqlField, IBqlOperand
  {
  }

  public abstract class preferredItemClasses : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.preferredItemClasses>
  {
  }

  public abstract class priceOfSuggestedItems : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClass.priceOfSuggestedItems>
  {
  }
}
