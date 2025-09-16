// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenForm
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.form>>>), Persistent = true)]
[Serializable]
public class AUScreenForm : AUScreenItem
{
  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Form ID")]
  public override 
  #nullable disable
  string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(2, IsUnicode = false, InputMask = "")]
  [PXDefault("F")]
  public override string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Caption")]
  public string Caption { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Customized")]
  public bool Customized { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Visible")]
  public bool IsVisible { get; set; }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenForm.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenForm.projectID>
  {
  }

  public new class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenForm.itemCD>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenForm.itemType>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenForm.itemID>
  {
  }

  public abstract class caption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenForm.caption>
  {
  }

  public abstract class customized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenForm.customized>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenForm.isVisible>
  {
  }
}
