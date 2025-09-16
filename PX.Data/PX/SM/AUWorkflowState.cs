// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow State")]
public class AUWorkflowState : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflow.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUWorkflow.workflowGUID))]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Identifier")]
  public virtual string Identifier { get; set; }

  [PXDBInt]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXLineNbr(typeof (AUWorkflow))]
  [PXParent(typeof (Select<AUWorkflow, Where2<Where<AUWorkflow.screenID, Equal<Current<AUWorkflowState.screenID>>>, PX.Data.And<Where<AUWorkflow.workflowGUID, Equal<Current<AUWorkflowState.workflowGUID>>>>>>))]
  public virtual int? StateLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Initial State of the Workflow")]
  public bool? IsInitial { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Layout")]
  public virtual string Layout { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Next State")]
  public virtual string NextState { get; set; }

  [PXDBString(1, IsUnicode = true)]
  [PXUIField(DisplayName = "State Type")]
  public virtual string StateType { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Parent State")]
  public virtual string ParentState { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Skip Condition")]
  public virtual Guid? SkipConditionID { get; set; }

  /// <summary>If true, an entity in the state cannot be saved.</summary>
  internal bool IsNonPersistent { get; set; }

  public string GetLayoutOrDefault()
  {
    return !string.IsNullOrEmpty(this.Layout) ? this.Layout : AUWorkflowState.GetNextDefaultLayout(this.StateLineNbr.GetValueOrDefault());
  }

  /// <summary>
  /// Get default X and Y coordinates in the format for new workflow state.
  /// </summary>
  /// <param name="lineNumber">Count of existing states.</param>
  /// <param name="maxInlineItemsCount">Count of possible inline states on diagram.</param>
  /// <returns>Formatted X and Y coordinates for new state.</returns>
  public static string GetNextDefaultLayout(int lineNumber, int maxInlineItemsCount = 5)
  {
    int num1 = 50;
    int num2 = 200;
    int num3 = 250;
    int num4 = 150;
    int num5 = 200;
    int num6 = lineNumber % maxInlineItemsCount;
    if (num6 != 0)
      num5 += num3 * num6;
    int num7 = num1 + num4 * lineNumber - num2 * (lineNumber / maxInlineItemsCount);
    return $"{num5} {num7}";
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.screenID>
  {
  }

  public abstract class workflowGUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUWorkflowState.workflowGUID>
  {
  }

  public abstract class identifier : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.identifier>
  {
  }

  public abstract class stateLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowState.stateLineNbr>
  {
  }

  public abstract class isInitial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowState.isInitial>
  {
  }

  public abstract class layout : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.layout>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.description>
  {
  }

  public abstract class nextState : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.nextState>
  {
  }

  public abstract class stateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.stateType>
  {
  }

  public abstract class parentState : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowState.parentState>
  {
  }

  public abstract class skipConditionID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowState.skipConditionID>
  {
  }
}
