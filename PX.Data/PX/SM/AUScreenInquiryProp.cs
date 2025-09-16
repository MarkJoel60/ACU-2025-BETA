// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenInquiryProp
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
public class AUScreenInquiryProp : AUScreenItemProp
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenInquiry.itemID))]
  [PXParent(typeof (Select<AUScreenInquiry, Where<AUScreenInquiry.screenID, Equal<Current<AUScreenInquiryProp.screenID>>, And<AUScreenInquiry.projectID, Equal<Current<AUScreenInquiryProp.projectID>>, And<AUScreenInquiry.itemID, Equal<Current<AUScreenInquiryProp.itemID>>>>>>))]
  public override 
  #nullable disable
  string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public override string PropertyID
  {
    get => this._propertyID;
    set => this._propertyID = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Override")]
  public override string OverrideValue
  {
    get => this._OverrideValue;
    set => this._OverrideValue = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public override string PropertyValue
  {
    get => this._PropertyValue;
    set => this._PropertyValue = value;
  }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryProp.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenInquiryProp.projectID>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenInquiryProp.itemID>
  {
  }

  public new abstract class propertyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryProp.propertyID>
  {
  }

  public new abstract class overrideValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryProp.overrideValue>
  {
  }

  public new abstract class propertyValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryProp.propertyValue>
  {
  }
}
