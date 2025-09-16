// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastHistoryInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The projection DAC that represents a <see cref="T:PX.Objects.PM.PMForecastHistory">project budget forecast history</see> connected with associated <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see>.
/// </summary>
[PXHidden]
[PXBreakInheritance]
[PXProjection(typeof (Select2<PMForecastHistory, InnerJoin<PMAccountGroup, On<PMForecastHistory.accountGroupID, Equal<PMAccountGroup.groupID>>>>), Persistent = false)]
public class PMForecastHistoryInfo : PMForecastHistory
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.Type" />
  [PXDBString(1, BqlField = typeof (PMAccountGroup.type))]
  public virtual 
  #nullable disable
  string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.IsExpense" />
  [PXDBBool(BqlField = typeof (PMAccountGroup.isExpense))]
  public virtual bool? IsExpense { get; set; }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastHistoryInfo.accountGroupType>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMForecastHistoryInfo.isExpense>
  {
  }
}
