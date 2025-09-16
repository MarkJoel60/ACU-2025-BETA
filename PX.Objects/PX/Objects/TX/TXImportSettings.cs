// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXImportSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class TXImportSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxableCategoryID;
  protected string _FreightCategoryID;
  protected string _ServiceCategoryID;
  protected string _LaborCategoryID;

  [Obsolete]
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (TaxCategory.taxCategoryID))]
  [PXUIField(DisplayName = "Tax Taxable Category")]
  public virtual string TaxableCategoryID
  {
    get => this._TaxableCategoryID;
    set => this._TaxableCategoryID = value;
  }

  [Obsolete]
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (TaxCategory.taxCategoryID))]
  [PXUIField(DisplayName = "Tax Freight Category")]
  public virtual string FreightCategoryID
  {
    get => this._FreightCategoryID;
    set => this._FreightCategoryID = value;
  }

  [Obsolete]
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (TaxCategory.taxCategoryID))]
  [PXUIField(DisplayName = "Tax Service Category")]
  public virtual string ServiceCategoryID
  {
    get => this._ServiceCategoryID;
    set => this._ServiceCategoryID = value;
  }

  [Obsolete]
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (TaxCategory.taxCategoryID))]
  [PXUIField(DisplayName = "Tax Labor Category")]
  public virtual string LaborCategoryID
  {
    get => this._LaborCategoryID;
    set => this._LaborCategoryID = value;
  }

  public static class FK
  {
    public class TaxTaxableCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TXImportSettings>.By<TXImportSettings.taxableCategoryID>
    {
    }

    public class TaxFreightCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TXImportSettings>.By<TXImportSettings.freightCategoryID>
    {
    }

    public class TaxServiceCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TXImportSettings>.By<TXImportSettings.serviceCategoryID>
    {
    }

    public class TaxLaborCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TXImportSettings>.By<TXImportSettings.laborCategoryID>
    {
    }
  }

  [Obsolete]
  public abstract class taxableCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportSettings.taxableCategoryID>
  {
  }

  [Obsolete]
  public abstract class freightCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportSettings.freightCategoryID>
  {
  }

  [Obsolete]
  public abstract class serviceCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportSettings.serviceCategoryID>
  {
  }

  [Obsolete]
  public abstract class laborCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportSettings.laborCategoryID>
  {
  }
}
