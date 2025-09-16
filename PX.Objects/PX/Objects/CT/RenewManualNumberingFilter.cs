// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.RenewManualNumberingFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CT;

[Serializable]
public class RenewManualNumberingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(30, IsUnicode = true)]
  [PXDimension("CONTRACT", ValidComboRequired = false)]
  [PXUIField(DisplayName = "Contract ID")]
  [PXDefault]
  public virtual 
  #nullable disable
  string ContractCD { get; set; }

  public abstract class contractCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RenewManualNumberingFilter.contractCD>
  {
  }
}
