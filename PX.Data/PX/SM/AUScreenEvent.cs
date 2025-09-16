// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.eventsConditional>, Or<AUScreenItem.itemType, Equal<AUScreenItemType.eventsUnconditional>>>>), Persistent = true)]
[Serializable]
public class AUScreenEvent : AUScreenItem
{
  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Event ID")]
  public override 
  #nullable disable
  string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(2, IsUnicode = false, InputMask = "")]
  [PXDefault("EC")]
  public override string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public override string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Customized")]
  public override bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Entity")]
  public virtual string Entity { get; set; }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenEvent.projectID>
  {
  }

  public new class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.itemCD>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.itemType>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.itemID>
  {
  }

  public new abstract class parentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.parentID>
  {
  }

  public new abstract class childRowCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenEvent.childRowCntr>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEvent.description>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenEvent.isActive>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEvent.entity>
  {
  }
}
