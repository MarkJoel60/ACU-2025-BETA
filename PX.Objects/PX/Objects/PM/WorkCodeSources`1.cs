// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.WorkCodeSources`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.PM;

public abstract class WorkCodeSources<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<PMWorkCodeCostCodeRange, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMWorkCodeCostCodeRange.workCodeID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMWorkCode.workCodeID, IBqlString>.FromCurrent>>, 
  #nullable disable
  PMWorkCodeCostCodeRange>.View CostCodeRanges;
  public FbqlSelect<SelectFromBase<PMWorkCodeProjectTaskSource, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMWorkCodeProjectTaskSource.workCodeID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMWorkCode.workCodeID, IBqlString>.FromCurrent>>, 
  #nullable disable
  PMWorkCodeProjectTaskSource>.View ProjectTaskSources;
  public FbqlSelect<SelectFromBase<PMWorkCodeLaborItemSource, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMWorkCodeLaborItemSource.workCodeID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMWorkCode.workCodeID, IBqlString>.FromCurrent>>, 
  #nullable disable
  PMWorkCodeLaborItemSource>.View LaborItemSources;
}
