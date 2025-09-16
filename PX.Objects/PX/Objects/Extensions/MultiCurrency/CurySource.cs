// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.CurySource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.MultiCurrency;

/// <summary>A mapped cache extension that contains information on the currency source.</summary>
public class CurySource : PXMappedCacheExtension
{
  /// <summary>The identifier of the currency in the system.</summary>
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  /// <summary>The identifier of the currency rate type in the system.</summary>
  public virtual string CuryRateTypeID { get; set; }

  /// <summary>A property that indicates (if set to <tt>true</tt>) that the currency of the customer documents (<see cref="P:PX.Objects.Extensions.MultiCurrency.CurySource.CuryID" />) can be overridden by a user during document entry.</summary>
  public virtual bool? AllowOverrideCury { get; set; }

  /// <summary>A property that indicates (if set to <tt>true</tt>) that the currency rate for the customer documents (which is calculated by the system based on the currency rate history) can be overridden
  /// by a user during document entry.</summary>
  public virtual bool? AllowOverrideRate { get; set; }

  /// <exclude />
  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurySource.curyID>
  {
  }

  /// <exclude />
  public abstract class curyRateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurySource.curyRateTypeID>
  {
  }

  /// <exclude />
  public abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CurySource.allowOverrideCury>
  {
  }

  /// <exclude />
  public abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CurySource.allowOverrideRate>
  {
  }
}
