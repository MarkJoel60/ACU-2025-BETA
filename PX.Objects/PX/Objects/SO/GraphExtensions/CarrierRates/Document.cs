// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.CarrierRates.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.CarrierRates;

/// <exclude />
public class Document : PXMappedCacheExtension
{
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  public virtual long? CuryInfoID { get; set; }

  public virtual DateTime? DocumentDate { get; set; }

  public virtual string ShipVia { get; set; }

  public virtual bool? IsPackageValid { get; set; }

  public virtual bool? ShipViaUpdateFromShopForRate { get; set; }

  public virtual bool? FreightCostIsValid { get; set; }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Document.curyInfoID>
  {
  }

  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Document.documentDate>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.shipVia>
  {
  }

  public abstract class isPackageValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Document.isPackageValid>
  {
  }

  public abstract class shipViaUpdateFromShopForRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.shipViaUpdateFromShopForRate>
  {
  }

  public abstract class freightCostIsValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.freightCostIsValid>
  {
  }
}
