// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.AUWorkflowCategory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Data.ProjectDefinition.Workflow;

[PXCacheName("Workflow Category")]
public class AUWorkflowCategory : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXUIField(DisplayName = "Screen")]
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string CategoryName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBByte]
  public virtual byte? Placement { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string After { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowCategory.screenID>
  {
  }

  public abstract class categoryName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowCategory.categoryName>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowCategory.displayName>
  {
  }

  public abstract class placement : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  AUWorkflowCategory.placement>
  {
  }

  public abstract class after : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowCategory.after>
  {
  }
}
