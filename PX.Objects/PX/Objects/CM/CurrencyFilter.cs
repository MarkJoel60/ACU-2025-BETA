// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CM;

/// <exclude />
[PXHidden]
public class CurrencyFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(5, IsUnicode = true)]
  public virtual 
  #nullable disable
  string FromCuryID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string ToCuryID { get; set; }

  public abstract class fromCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyFilter.fromCuryID>
  {
  }

  public abstract class toCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyFilter.toCuryID>
  {
  }
}
