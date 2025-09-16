// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Standalone.FADetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.FA.Standalone;

[PXCacheName("FA Details")]
[Serializable]
public class FADetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FixedAsset" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the fixed asset.
  /// It is a required value.
  /// By default, the value is set to the current fixed asset identifier.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<PX.Objects.FA.FixedAsset, Where<PX.Objects.FA.FixedAsset.assetID, Equal<Current<FADetails.assetID>>>>))]
  [PXDBDefault(typeof (PX.Objects.FA.FixedAsset.assetID))]
  public virtual int? AssetID { get; set; }

  /// <summary>The type of the fixed asset property.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.Standalone.FADetails.propertyType.ListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [FADetails.propertyType.List]
  [PXDefault("CP")]
  [PXUIField(DisplayName = "Property Type")]
  public virtual string PropertyType { get; set; }

  /// <summary>The status of the fixed asset.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.FixedAssetStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [FixedAssetStatus.List]
  [PXDefault("A")]
  public virtual string Status { get; set; }

  /// <summary>The condition of the fixed asset.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.FA.Standalone.FADetails.condition.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [FADetails.condition.List]
  [PXDefault("G")]
  public virtual string Condition { get; set; }

  /// <summary>The acquisition date of the fixed asset.</summary>
  [PXDBDate]
  [PXDefault(typeof (POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Receipt Date")]
  public virtual DateTime? ReceiptDate { get; set; }

  /// <summary>
  /// The type of the purchase receipt.
  /// This field is a part of the compound reference to the purchasing document (<see cref="T:PX.Objects.PO.POReceipt" />).
  /// The full reference contains the <see cref="T:PX.Objects.FA.Standalone.FADetails.receiptType" /> and <see cref="T:PX.Objects.FA.Standalone.FADetails.receiptNbr" /> fields.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PO.POReceiptType.ListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true, InputMask = "")]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string ReceiptNbr { get; set; }

  /// <summary>
  /// The number of the purchase order related to the purchase document.
  /// </summary>
  /// <value>The information field.</value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Number")]
  [PXDefault(typeof (Search2<POOrder.orderNbr, InnerJoin<POReceiptLine, On<POReceiptLine.pOType, Equal<POOrder.orderType>, And<POReceiptLine.pONbr, Equal<POOrder.orderNbr>>>>, Where<POReceiptLine.receiptType, Equal<Current<FADetails.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<FADetails.receiptNbr>>>>>))]
  public virtual string PONumber { get; set; }

  /// <summary>The number of the bill.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Bill Number")]
  public virtual string BillNumber { get; set; }

  /// <summary>The name of the fixed asset manufacturer.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer")]
  public virtual string Manufacturer { get; set; }

  /// <summary>The name of the fixed asset model.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Model")]
  public virtual string Model { get; set; }

  /// <summary>The serial number of the fixed asset.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Serial Number")]
  public virtual string SerialNumber { get; set; }

  /// <summary>
  /// The number of the actual revision of the asset location.
  /// This field is a part of the compound reference to <see cref="T:PX.Objects.FA.FALocationHistory" />.
  /// The full reference contains the <see cref="T:PX.Objects.FA.Standalone.FADetails.assetID" /> and <see cref="T:PX.Objects.FA.Standalone.FADetails.locationRevID" /> fields.
  /// </summary>
  [PXDBInt]
  public virtual int? LocationRevID { get; set; }

  /// <summary>The barcode of the fixed asset.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode")]
  public virtual string Barcode { get; set; }

  /// <summary>The tag of the fixed asset.</summary>
  /// <value>
  /// The value can be entered manually or can be auto-numbered.
  /// </value>
  [PXDBString(20, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Tag Number", Enabled = false)]
  [FADetails.tagNbr.Numbering]
  public virtual string TagNbr { get; set; }

  /// <summary>The date when depreciation of the fixed asset starts.</summary>
  /// <value>
  /// The date can not be greater than <see cref="T:PX.Objects.FA.Standalone.FADetails.receiptDate" />.
  /// </value>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Placed-in-Service Date")]
  [PXFormula(typeof (IIf<Where<Current<PX.Objects.FA.FixedAsset.underConstruction>, Equal<False>>, FADetails.receiptDate, Null>))]
  public virtual DateTime? DepreciateFromDate { get; set; }

  /// <summary>
  /// The cost of the fixed asset at the time of acquisition.
  /// </summary>
  /// <value>The value can be changed during the life of the asset.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Acquisition Cost")]
  public virtual Decimal? AcquisitionCost { get; set; }

  /// <summary>The salvage amount of the fixed asset.</summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Salvage Amount")]
  public virtual Decimal? SalvageAmount { get; set; }

  /// <summary>The replacement cost of the fixed asset.</summary>
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Replacement Cost")]
  public virtual Decimal? ReplacementCost { get; set; }

  /// <summary>The date of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXDBDate]
  [PXUIField(DisplayName = "Disposal Date", Enabled = false)]
  public virtual DateTime? DisposalDate { get; set; }

  /// <summary>The date of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXDate]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.disposalDate>, Null>))]
  [PXUIField(DisplayName = "Disposal Date", Enabled = false)]
  public virtual DateTime? DisplayDisposalDate { get; set; }

  /// <summary>The period of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXDBString(6, IsFixed = true)]
  public virtual string DisposalPeriodID { get; set; }

  /// <summary>The period of fixed asset disposal.</summary>
  /// <value>The field is filled in only for disposed fixed assets.</value>
  [PXString(6, IsFixed = true)]
  public virtual string DisplayDisposalPeriodID { get; set; }

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FADisposalMethod" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the disposal method.
  /// The field is filled in only for disposed fixed assets.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (FADisposalMethod.disposalMethodID), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXUIField(DisplayName = "Disposal Method", Enabled = false)]
  public virtual int? DisposalMethodID { get; set; }

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FADisposalMethod" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the disposal method.
  /// The field is filled in only for disposed fixed assets.
  /// </value>
  [PXInt]
  [PXSelector(typeof (FADisposalMethod.disposalMethodID), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.disposalMethodID>, Null>))]
  [PXUIField(DisplayName = "Disposal Method", Enabled = false)]
  public virtual int? DisplayDisposalMethodID { get; set; }

  /// <summary>The amount of fixed asset disposal.</summary>
  /// <value>
  /// The field is filled in only for disposed fixed assets. The value of the field can be zero.
  /// </value>
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Disposal Amount", Enabled = false)]
  public virtual Decimal? SaleAmount { get; set; }

  /// <summary>The amount of fixed asset disposal.</summary>
  /// <value>
  /// The field is filled in only for disposed fixed assets. The value of the field can be zero.
  /// </value>
  [PXBaseCury]
  [PXFormula(typeof (Switch<Case<Where<FADetails.status, Equal<FixedAssetStatus.disposed>>, FADetails.saleAmount>, Null>))]
  [PXUIField(DisplayName = "Disposal Amount", Enabled = false)]
  public virtual Decimal? DisplaySaleAmount { get; set; }

  /// <summary>The name of the fixed asset warrantor.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Warrantor")]
  public virtual string Warrantor { get; set; }

  /// <summary>The expiration date of the fixed asset warranty.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBDate]
  [PXUIField(DisplayName = "Warranty Expires On")]
  public virtual DateTime? WarrantyExpirationDate { get; set; }

  /// <summary>The certificate number of the fixed asset warranty.</summary>
  /// <value>The information field, which value is entered manually.</value>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Warranty Certificate Number")]
  public virtual string WarrantyCertificateNumber { get; set; }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that the fixed asset is on hold and thus cannot be depreciated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold => new bool?(this.Status == "H");

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

  public class PK : PrimaryKeyOf<FADetails>.By<FADetails.assetID>
  {
    public static FADetails Find(PXGraph graph, int? assetID, PKFindOptions options = 0)
    {
      return (FADetails) PrimaryKeyOf<FADetails>.By<FADetails.assetID>.FindBy(graph, (object) assetID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<PX.Objects.FA.FixedAsset>.By<PX.Objects.FA.FixedAsset.assetID>.ForeignKeyOf<FADetails>.By<FADetails.assetID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<FADetails>.By<FADetails.receiptType, FADetails.receiptNbr>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.assetID>
  {
  }

  public abstract class propertyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.propertyType>
  {
    public const string Property = "CP";
    public const string GrantProperty = "GP";
    public const string Leased = "CL";
    public const string LeasedtoOthers = "LO";
    public const string Rented = "CR";
    public const string RentedtoOthers = "RO";
    public const string Credit = "CC";

    /// <summary>The type of the fixed asset property.</summary>
    /// <value>
    /// The class exposes the following values:
    /// <list type="bullet">
    /// <item> <term><c>CP</c></term> <description>Property</description> </item>
    /// <item> <term><c>GP</c></term> <description>Grant Property</description> </item>
    /// <item> <term><c>CL</c></term> <description>Leased</description> </item>
    /// <item> <term><c>LO</c></term> <description>Leased to Others</description> </item>
    /// <item> <term><c>CR</c></term> <description>Rented</description> </item>
    /// <item> <term><c>RO</c></term> <description>Rented to Others</description> </item>
    /// <item> <term><c>CC</c></term> <description>To the Credit of</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[7]
        {
          "CP",
          "GP",
          "CL",
          "LO",
          "CR",
          "RO",
          "CC"
        }, new string[7]
        {
          "Property",
          "Grant Property",
          "Leased",
          "Leased to Others",
          "Rented",
          "Rented to Others",
          "To the Credit of"
        })
      {
      }
    }

    public class property : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.property>
    {
      public property()
        : base("CP")
      {
      }
    }

    public class grantProperty : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.grantProperty>
    {
      public grantProperty()
        : base("GP")
      {
      }
    }

    public class leased : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.leased>
    {
      public leased()
        : base("CL")
      {
      }
    }

    public class leasedtoOthers : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.leasedtoOthers>
    {
      public leasedtoOthers()
        : base("LO")
      {
      }
    }

    public class rented : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.rented>
    {
      public rented()
        : base("CR")
      {
      }
    }

    public class rentedtoOthers : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADetails.propertyType.rentedtoOthers>
    {
      public rentedtoOthers()
        : base("RO")
      {
      }
    }

    public class credit : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.propertyType.credit>
    {
      public credit()
        : base("CC")
      {
      }
    }
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.status>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.condition>
  {
    public const string Good = "G";
    public const string Avg = "A";
    public const string Poor = "P";

    /// <summary>The condition of the fixed asset.</summary>
    /// <value>
    /// The class exposes the following values:
    /// <list type="bullet">
    /// <item> <term><c>G</c></term> <description>Good</description> </item>
    /// <item> <term><c>A</c></term> <description>Average</description> </item>
    /// <item> <term><c>P</c></term> <description>Poor</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "G", "A", "P" }, new string[3]
        {
          "Good",
          "Average",
          "Poor"
        })
      {
      }
    }

    public class good : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.good>
    {
      public good()
        : base("G")
      {
      }
    }

    public class avg : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.avg>
    {
      public avg()
        : base("A")
      {
      }
    }

    public class poor : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADetails.condition.poor>
    {
      public poor()
        : base("P")
      {
      }
    }
  }

  public abstract class receiptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FADetails.receiptDate>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.receiptNbr>
  {
  }

  public abstract class pONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.pONumber>
  {
  }

  public abstract class billNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.billNumber>
  {
  }

  public abstract class manufacturer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.manufacturer>
  {
  }

  public abstract class model : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.model>
  {
  }

  public abstract class serialNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.serialNumber>
  {
  }

  public abstract class locationRevID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.locationRevID>
  {
  }

  public abstract class barcode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.barcode>
  {
  }

  public abstract class tagNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.tagNbr>
  {
    public class NumberingAttribute : AutoNumberAttribute
    {
      public NumberingAttribute()
        : base(typeof (FASetup.tagNumberingID), typeof (FADetails.createdDateTime))
      {
        this.NullMode = AutoNumberAttribute.NullNumberingMode.UserNumbering;
      }
    }
  }

  public abstract class depreciateFromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.depreciateFromDate>
  {
  }

  public abstract class acquisitionCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.acquisitionCost>
  {
  }

  public abstract class salvageAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.salvageAmount>
  {
  }

  public abstract class replacementCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.replacementCost>
  {
  }

  public abstract class disposalDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FADetails.disposalDate>
  {
  }

  public abstract class displayDisposalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.displayDisposalDate>
  {
  }

  public abstract class disposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.disposalPeriodID>
  {
  }

  public abstract class displayDisposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.displayDisposalPeriodID>
  {
  }

  public abstract class disposalMethodID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADetails.disposalMethodID>
  {
  }

  public abstract class displayDisposalMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADetails.displayDisposalMethodID>
  {
  }

  public abstract class saleAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FADetails.saleAmount>
  {
  }

  public abstract class displaySaleAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADetails.displaySaleAmount>
  {
  }

  public abstract class warrantor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADetails.warrantor>
  {
  }

  public abstract class warrantyExpirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.warrantyExpirationDate>
  {
  }

  public abstract class warrantyCertificateNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.warrantyCertificateNumber>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FADetails.hold>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FADetails.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADetails.lastModifiedDateTime>
  {
  }
}
