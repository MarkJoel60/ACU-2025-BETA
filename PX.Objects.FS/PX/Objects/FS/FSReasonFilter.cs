// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSReasonFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSReasonFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true)]
  [ListField_ReasonType.ListAtrribute]
  [PXDefault("CAPP")]
  [PXUIField(DisplayName = "Reason Type")]
  public virtual 
  #nullable disable
  string ReasonType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorWorkflow]
  [PXDefault]
  public virtual int? WFID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Workflow Stage")]
  [FSSelectorWorkflowStageInReason]
  [PXDefault]
  public virtual int? WFStageID { get; set; }

  public abstract class reasonType : ListField_ReasonType
  {
  }

  public abstract class wFID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSReasonFilter.wFID>
  {
  }

  public abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSReasonFilter.wFStageID>
  {
  }
}
