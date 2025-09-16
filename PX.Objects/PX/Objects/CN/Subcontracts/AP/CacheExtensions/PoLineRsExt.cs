// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.PoLineRsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

public sealed class PoLineRsExt : PXCacheExtension<
#nullable disable
POLineRS>
{
  [PXString]
  [PXUIField(DisplayName = "Subcontract Nbr.")]
  public string SubcontractNbr => this.Base.OrderNbr;

  [PXDate]
  [PXUIField(DisplayName = "Date")]
  public DateTime? SubcontractDate => this.Base.OrderDate;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class subcontractNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PoLineRsExt.subcontractNbr>
  {
  }

  public abstract class subcontractDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PoLineRsExt.subcontractDate>
  {
  }
}
