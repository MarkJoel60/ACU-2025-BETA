// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PRDeductCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

[PXCacheName("Payroll Deduction and Benefit Code")]
[Serializable]
public class PRDeductCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(DisplayName = "Code ID")]
  public int? CodeID { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual 
  #nullable disable
  string CountryID { get; set; }

  public abstract class codeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PRDeductCode.codeID>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PRDeductCode.countryID>
  {
  }
}
