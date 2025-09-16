// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenPopupProp
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
public class AUScreenPopupProp : AUScreenItemProp
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenPopup.itemID))]
  [PXParent(typeof (Select<AUScreenPopup, Where<AUScreenPopup.screenID, Equal<Current<AUScreenPopupProp.screenID>>, And<AUScreenPopup.projectID, Equal<Current<AUScreenPopupProp.projectID>>, And<AUScreenPopup.itemID, Equal<Current<AUScreenPopupProp.itemID>>>>>>))]
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
  AUScreenPopupProp.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenPopupProp.projectID>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenPopupProp.itemID>
  {
  }
}
