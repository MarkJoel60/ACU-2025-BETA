// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow")]
public class AUWorkflow : AUWorkflowBaseTable, IBqlTable, IBqlTableSystemDataStorage, IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflowDefinition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowDefinition))]
  [PXParent(typeof (Select<AUWorkflowDefinition, Where<AUWorkflowDefinition.screenID, Equal<Current<AUWorkflow.screenID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Workflow Type")]
  public virtual string WorkflowID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Workflow Subtype")]
  public virtual string WorkflowSubID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Workflow Name")]
  public virtual string Description { get; set; }

  [PXDBString(IsUnicode = false)]
  [PXUIField(DisplayName = "Layout")]
  public virtual string LayoutClient { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflow.screenID>
  {
  }

  public abstract class workflowGUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUWorkflow.workflowGUID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflow.lineNbr>
  {
  }

  public abstract class workflowID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflow.workflowID>
  {
  }

  public abstract class workflowSubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflow.workflowSubID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflow.description>
  {
  }

  public abstract class layoutClient : IBqlField, IBqlOperand
  {
  }
}
