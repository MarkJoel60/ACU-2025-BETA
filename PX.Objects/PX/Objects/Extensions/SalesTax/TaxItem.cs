// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.TaxItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.TX;

#nullable enable
namespace PX.Objects.Extensions.SalesTax;

/// <summary>A supplementary class that represents a tax item.</summary>
public class TaxItem : ITaxDetail
{
  /// <summary>The identifier of the tax.</summary>
  public virtual 
  #nullable disable
  string TaxID { get; set; }

  /// <exclude />
  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxItem.taxID>
  {
  }
}
