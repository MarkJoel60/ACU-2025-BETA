// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenFieldFormProp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItemProp, Where<AUScreenItemProp.propertyType, Equal<StringEmpty>>>), Persistent = true)]
[Serializable]
public class AUScreenFieldFormProp : AUScreenItemProp
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenFieldForm.itemID))]
  [PXParent(typeof (Select<AUScreenFieldForm, Where<AUScreenFieldForm.screenID, Equal<Current<AUScreenFieldFormProp.screenID>>, And<AUScreenFieldForm.projectID, Equal<Current<AUScreenFieldFormProp.projectID>>, And<AUScreenFieldForm.itemID, Equal<Current<AUScreenFieldFormProp.itemID>>>>>>))]
  public override 
  #nullable disable
  string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Base Value", Enabled = false)]
  public override string OriginalValue
  {
    get => this._OriginalValue;
    set => this._OriginalValue = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Custom Value", Enabled = true)]
  public override string OverrideValue
  {
    get => this._OverrideValue;
    set => this._OverrideValue = value;
  }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldFormProp.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenFieldFormProp.projectID>
  {
  }

  public new class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldFormProp.itemID>
  {
  }

  public new abstract class originalValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldFormProp.originalValue>
  {
  }

  public new abstract class overrideValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldFormProp.overrideValue>
  {
  }
}
