// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventProp
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
public class AUScreenEventProp : AUScreenItemProp
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenEvent.itemID))]
  [PXParent(typeof (Select<AUScreenEvent, Where<AUScreenEvent.screenID, Equal<Current<AUScreenEventProp.screenID>>, And<AUScreenEvent.projectID, Equal<Current<AUScreenEventProp.projectID>>, And<AUScreenEvent.itemID, Equal<Current<AUScreenEventProp.itemID>>>>>>))]
  public override 
  #nullable disable
  string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEventProp.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenEventProp.projectID>
  {
  }

  public new class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEventProp.itemID>
  {
  }
}
