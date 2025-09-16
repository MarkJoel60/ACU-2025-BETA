// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AddItemParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class AddItemParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _CuryID;

  [Vendor]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [NullableSite]
  public virtual int? SiteID { get; set; }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemParameters.vendorID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemParameters.curyID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemParameters.siteID>
  {
  }
}
