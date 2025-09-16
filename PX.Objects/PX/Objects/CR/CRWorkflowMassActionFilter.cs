// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRWorkflowMassActionFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
public class CRWorkflowMassActionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Operation")]
  [PXString]
  [CRWorkflowMassActionOperation.List]
  [PXUnboundDefault("Update")]
  public virtual 
  #nullable disable
  string Operation { get; set; }

  [PXWorkflowMassProcessing(DisplayName = "Action", AddUndefinedState = false)]
  public virtual string Action { get; set; }

  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRWorkflowMassActionFilter.operation>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRWorkflowMassActionFilter.action>
  {
  }
}
