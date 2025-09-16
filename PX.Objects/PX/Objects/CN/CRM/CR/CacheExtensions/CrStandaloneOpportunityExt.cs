// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.CRM.CR.CacheExtensions.CrStandaloneOpportunityExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CN.CRM.CR.CacheExtensions;

public sealed class CrStandaloneOpportunityExt : PXCacheExtension<CROpportunity>
{
  [PXDBBaseCury(null, null)]
  public Decimal? Cost { get; set; }

  [PXDBBool]
  public bool? MultipleAccounts { get; set; }

  [Obsolete]
  [PXDBBaseCury(null, null)]
  public Decimal? QuotedAmount { get; set; }

  [Obsolete]
  [PXDBBaseCury(null, null)]
  public Decimal? TotalAmount { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class cost : IBqlField, IBqlOperand
  {
  }

  public abstract class multipleAccounts : IBqlField, IBqlOperand
  {
  }

  public abstract class quotedAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class totalAmount : IBqlField, IBqlOperand
  {
  }
}
