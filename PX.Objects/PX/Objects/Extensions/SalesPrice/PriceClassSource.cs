// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesPrice.PriceClassSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.SalesPrice;

/// <summary>A mapped cache extension that provides information about the source of the price class.</summary>
public class PriceClassSource : PXMappedCacheExtension
{
  /// <exclude />
  protected 
  #nullable disable
  string _PriceClassID;

  /// <summary>The identifier of the price class in the system.</summary>
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  /// <exclude />
  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PriceClassSource.priceClassID>
  {
  }
}
