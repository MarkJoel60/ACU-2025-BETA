// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenInquiryNavProp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItemProp, Where<AUScreenItemProp.propertyType, Equal<NavAttributeType>>>), Persistent = true)]
[Serializable]
public class AUScreenInquiryNavProp : AUScreenItemProp
{
  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUScreenInquiry.itemID))]
  [PXParent(typeof (Select<AUScreenInquiry, Where<AUScreenInquiry.screenID, Equal<Current<AUScreenInquiryNavProp.screenID>>, And<AUScreenInquiry.projectID, Equal<Current<AUScreenInquiryNavProp.projectID>>, And<AUScreenInquiry.itemID, Equal<Current<AUScreenInquiryNavProp.itemID>>>>>>))]
  public override 
  #nullable disable
  string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault("N")]
  public override string PropertyType
  {
    get => this._propertyType;
    set => this._propertyType = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Field")]
  public override string PropertyID
  {
    get => this._propertyID;
    set => this._propertyID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Parameter")]
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
    AUScreenInquiryNavProp.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenInquiryNavProp.projectID>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenInquiryNavProp.itemID>
  {
  }

  public new abstract class propertyType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryNavProp.propertyType>
  {
  }

  public new abstract class propertyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryNavProp.propertyID>
  {
  }

  public new abstract class propertyValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenInquiryNavProp.propertyValue>
  {
  }
}
