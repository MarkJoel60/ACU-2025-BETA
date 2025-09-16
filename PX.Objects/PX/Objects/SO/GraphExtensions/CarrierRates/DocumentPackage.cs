// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.CarrierRates.DocumentPackage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.CarrierRates;

/// <summary>A mapped cache extension that represents a package information of the document.</summary>
public class DocumentPackage : PXMappedCacheExtension
{
  /// <summary>The box ID of the document package.</summary>
  public virtual 
  #nullable disable
  string BoxID { get; set; }

  /// <summary>The site ID of the document package.</summary>
  public virtual string SiteID { get; set; }

  /// <summary>The length of the document package.</summary>
  public virtual Decimal? Length { get; set; }

  /// <summary>The width of the document package.</summary>
  public virtual Decimal? Width { get; set; }

  /// <summary>The height of the document package.</summary>
  public virtual Decimal? Height { get; set; }

  /// <summary>The weight of the document package.</summary>
  public virtual Decimal? Weight { get; set; }

  /// <summary>The gross weight of the document package.</summary>
  public virtual Decimal? GrossWeight { get; set; }

  /// <summary>The declared value of the document package.</summary>
  public virtual Decimal? DeclaredValue { get; set; }

  /// <summary>The COD of the document package.</summary>
  public virtual Decimal? COD { get; set; }

  /// <summary>The stamp add-ons for the document package.</summary>
  public virtual string StampsAddOns { get; set; }

  public abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentPackage.boxID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentPackage.siteID>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DocumentPackage.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DocumentPackage.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DocumentPackage.height>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DocumentPackage.weight>
  {
  }

  public abstract class grossWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DocumentPackage.grossWeight>
  {
  }

  public abstract class declaredValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DocumentPackage.declaredValue>
  {
  }

  public abstract class cOD : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DocumentPackage.cOD>
  {
  }

  public abstract class stampsAddOns : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentPackage.stampsAddOns>
  {
  }
}
