// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenActionBaseState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUScreenActionBaseState : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  public 
  #nullable disable
  string ActionType;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string ActionName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "View")]
  [PXDefault]
  [PXViews(typeof (AUScreenDefinition.screenID))]
  public virtual string DataMember { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBInt]
  public virtual int? ActionFolderType { get; set; }

  [PXDBInt]
  public virtual int? MenuFolderType { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string MenuFolder { get; set; }

  [PXDBBool]
  public virtual bool? IsTopLevel { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Before { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string After { get; set; }

  [PXDBByte]
  public virtual byte? PlacementInCategory { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string AfterInMenu { get; set; }

  [PXDBBool]
  public virtual bool? IsEnabled { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string EnableCondition { get; set; }

  [PXDBBool]
  public virtual bool? IsVisible { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string VisibleCondition { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string DisableCondition { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string HideCondition { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Dialog Box")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Form { get; set; }

  [PXDBString(8)]
  [PXUIField(DisplayName = "Mass Processing Screen")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string MassProcessingScreen { get; set; }

  [PXDBBool]
  public virtual bool? DisablePersist { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? BatchMode { get; set; }

  [PXDBByte]
  public virtual byte? MapEnableRights { get; set; }

  [PXDBByte]
  public virtual byte? MapViewRights { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string Category { get; set; }

  [PXDBBool]
  public virtual bool? ExposedToMobile { get; set; }

  [PXDBBool]
  public virtual bool? IsLockedOnToolbar { get; set; }

  [PXDBBool]
  public virtual bool? IgnoresArchiveDisabling { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string Connotation { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.screenID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.actionName>
  {
  }

  public abstract class dataMember : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.dataMember>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.displayName>
  {
  }

  public abstract class actionFolderType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenActionBaseState.actionFolderType>
  {
  }

  public abstract class menuFolderType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenActionBaseState.menuFolderType>
  {
  }

  public abstract class menuFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.menuFolder>
  {
  }

  public abstract class isTopLevel : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseState.isTopLevel>
  {
  }

  public abstract class before : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenActionBaseState.before>
  {
  }

  public abstract class after : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenActionBaseState.after>
  {
  }

  public abstract class placementInCategory : 
    BqlType<
    #nullable enable
    IBqlByte, byte>.Field<
    #nullable disable
    AUScreenActionBaseState.placementInCategory>
  {
  }

  public abstract class afterInMenu : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.afterInMenu>
  {
  }

  public abstract class isEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenActionBaseState.isEnabled>
  {
  }

  public abstract class enableCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.enableCondition>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenActionBaseState.isVisible>
  {
  }

  public abstract class visibleCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.visibleCondition>
  {
  }

  public abstract class disableCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.disableCondition>
  {
  }

  public abstract class hideCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.hideCondition>
  {
  }

  public abstract class form : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenActionBaseState.form>
  {
  }

  public abstract class massProcessingScreen : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.massProcessingScreen>
  {
  }

  public abstract class disablePersist : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseState.disablePersist>
  {
  }

  public abstract class batchMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenActionBaseState.batchMode>
  {
  }

  public abstract class mapEnableRights : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenActionBaseState.mapEnableRights>
  {
  }

  public abstract class mapViewRights : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenActionBaseState.mapViewRights>
  {
  }

  public abstract class category : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.category>
  {
  }

  public abstract class exposedToMobile : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseState.exposedToMobile>
  {
  }

  public abstract class isLockedOnToolbar : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.isLockedOnToolbar>
  {
  }

  public abstract class ignoresArchiveDisabling : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.ignoresArchiveDisabling>
  {
  }

  public abstract class connotation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenActionBaseState.connotation>
  {
  }
}
