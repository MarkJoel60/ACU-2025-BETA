// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectSummaryBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

public sealed class ProjectSummaryBudget : PXCacheExtension<
#nullable disable
PMBudget>
{
  public static bool IsActive()
  {
    return PXContext.GetScreenID()?.Replace(".", string.Empty) == "PMGI0025";
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  [PXDBCalced(typeof (Switch<Case<Where<PMBudget.type, Equal<AccountType.income>>, PMBudget.changeOrderAmount>, Zero>), typeof (Decimal))]
  public Decimal? RestrictedCoAmount { get; set; }

  public abstract class restrictedCoAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ProjectSummaryBudget.restrictedCoAmount>
  {
  }
}
