// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenFieldForm
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.fieldForm>>>), Persistent = true)]
[Serializable]
public class AUScreenFieldForm : AUScreenItem
{
  [PXDBString(2, IsUnicode = false, InputMask = "", IsKey = true)]
  [PXDefault("FF")]
  public override 
  #nullable disable
  string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (AUScreenForm.itemID))]
  [PXParent(typeof (Select<AUScreenForm, Where<AUScreenForm.screenID, Equal<Current<AUScreenFieldForm.screenID>>, And<AUScreenForm.projectID, Equal<Current<AUScreenFieldForm.projectID>>, And<AUScreenForm.itemID, Equal<Current<AUScreenFieldForm.parentID>>>>>>))]
  public override string ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Field ID")]
  public override string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldForm.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenFieldForm.projectID>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldForm.itemType>
  {
  }

  public new abstract class parentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldForm.parentID>
  {
  }

  public new abstract class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldForm.itemCD>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldForm.itemID>
  {
  }
}
