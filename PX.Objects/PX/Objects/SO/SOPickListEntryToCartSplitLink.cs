// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickListEntryToCartSplitLink
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
namespace PX.Objects.SO;

[PXCacheName]
public class SOPickListEntryToCartSplitLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? PickerNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (SOPickListEntryToCartSplitLink.FK.PickListEntry))]
  public virtual int? EntryNbr { get; set; }

  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (SOPickListEntryToCartSplitLink.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (SOPickListEntryToCartSplitLink.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (SOPickListEntryToCartSplitLink.FK.CartSplit))]
  public virtual int? CartSplitLineNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.worksheetNbr, SOPickListEntryToCartSplitLink.pickerNbr, SOPickListEntryToCartSplitLink.entryNbr, SOPickListEntryToCartSplitLink.siteID, SOPickListEntryToCartSplitLink.cartID, SOPickListEntryToCartSplitLink.cartSplitLineNbr>
  {
    public static SOPickListEntryToCartSplitLink Find(
      PXGraph graph,
      string worksheetNbr,
      int? pickerNbr,
      int? entryNbr,
      int? siteID,
      int? cartID,
      int? cartSplitLineNbr,
      PKFindOptions options = 0)
    {
      return (SOPickListEntryToCartSplitLink) PrimaryKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.worksheetNbr, SOPickListEntryToCartSplitLink.pickerNbr, SOPickListEntryToCartSplitLink.entryNbr, SOPickListEntryToCartSplitLink.siteID, SOPickListEntryToCartSplitLink.cartID, SOPickListEntryToCartSplitLink.cartSplitLineNbr>.FindBy(graph, (object) worksheetNbr, (object) pickerNbr, (object) entryNbr, (object) siteID, (object) cartID, (object) cartSplitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.worksheetNbr>
    {
    }

    public class Picker : 
      PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.worksheetNbr, SOPickListEntryToCartSplitLink.pickerNbr>
    {
    }

    public class PickListEntry : 
      PrimaryKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr, SOPickerListEntry.pickerNbr, SOPickerListEntry.entryNbr>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.worksheetNbr, SOPickListEntryToCartSplitLink.pickerNbr, SOPickListEntryToCartSplitLink.entryNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.siteID, SOPickListEntryToCartSplitLink.cartID>
    {
    }

    public class CartSplit : 
      PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>.ForeignKeyOf<SOPickListEntryToCartSplitLink>.By<SOPickListEntryToCartSplitLink.siteID, SOPickListEntryToCartSplitLink.cartID, SOPickListEntryToCartSplitLink.cartSplitLineNbr>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.worksheetNbr>
  {
  }

  public abstract class pickerNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.pickerNbr>
  {
  }

  public abstract class entryNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.entryNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickListEntryToCartSplitLink.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickListEntryToCartSplitLink.cartID>
  {
  }

  public abstract class cartSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.cartSplitLineNbr>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickListEntryToCartSplitLink.qty>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOPickListEntryToCartSplitLink.Tstamp>
  {
  }
}
