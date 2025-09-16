// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenExtraAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenExtraAction : PXBqlTable, IScreenItem, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string ActionName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBBool]
  public virtual bool? DisplayOnMainToolbar { get; set; }

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
  public virtual bool? DisablePersist { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string Connotation { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; } = new bool?(true);

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Extra Action Type")]
  public virtual string ActionDiscriminator { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Dialog Box Before Action Execution")]
  public virtual string BeforeRunForm { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Dialog Box After Action Execution")]
  public virtual string AfterRunForm { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Action Parameters")]
  public virtual string Settings { get; set; }

  public string GetBeforeRunFormName()
  {
    return string.IsNullOrEmpty(this.BeforeRunForm) ? (string) null : $"Before{this.ActionDiscriminator}{this.ActionName}";
  }

  public string GetAfterRunFormName()
  {
    return string.IsNullOrEmpty(this.AfterRunForm) ? (string) null : $"After{this.ActionDiscriminator}{this.ActionName}";
  }

  public AUScreenActionState ToAU()
  {
    AUScreenActionState au = new AUScreenActionState();
    au.ScreenID = this.ScreenID;
    au.Form = this.GetBeforeRunFormName();
    au.DisplayName = this.DisplayName;
    au.ActionName = this.ActionName;
    au.MenuFolderType = new int?();
    au.IsTopLevel = this.DisplayOnMainToolbar;
    au.IsLockedOnToolbar = this.IsLockedOnToolbar;
    au.DisableCondition = bool.FalseString;
    au.HideCondition = bool.FalseString;
    au.DisablePersist = this.DisablePersist;
    au.MapEnableRights = this.MapEnableRights;
    au.MapViewRights = this.MapViewRights;
    au.Category = this.Category;
    au.ExposedToMobile = this.ExposedToMobile;
    au.Connotation = this.Connotation;
    au.IsActive = new bool?(true);
    au.ExtraData = new ScreenActionExtraData(this.ActionDiscriminator, this.BeforeRunForm, this.AfterRunForm, this.Settings);
    return au;
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenExtraAction.screenID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.actionName>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.displayName>
  {
  }

  public abstract class displayOnMainToolbar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenExtraAction.displayOnMainToolbar>
  {
  }

  public abstract class mapEnableRights : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenExtraAction.mapEnableRights>
  {
  }

  public abstract class mapViewRights : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenExtraAction.mapViewRights>
  {
  }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenExtraAction.category>
  {
  }

  public abstract class exposedToMobile : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenExtraAction.exposedToMobile>
  {
  }

  public abstract class isLockedOnToolbar : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.isLockedOnToolbar>
  {
  }

  public abstract class disablePersist : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenExtraAction.disablePersist>
  {
  }

  public abstract class connotation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.connotation>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenExtraAction.isActive>
  {
  }

  public abstract class actionDiscriminator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.actionDiscriminator>
  {
  }

  public abstract class beforeRunForm : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.beforeRunForm>
  {
  }

  public abstract class afterRunForm : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenExtraAction.afterRunForm>
  {
  }

  public abstract class settings : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenExtraAction.settings>
  {
  }
}
