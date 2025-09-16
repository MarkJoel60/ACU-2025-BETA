// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PRTaxCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

[PXCacheName("Payroll Tax Code")]
[Serializable]
public class PRTaxCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public int? TaxID { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual 
  #nullable disable
  string CountryID { get; set; }

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that the tax code was soft deleted.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDeleted { get; set; }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PRTaxCode.taxID>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PRTaxCode.countryID>
  {
  }

  public abstract class isDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PRTaxCode.isDeleted>
  {
  }
}
