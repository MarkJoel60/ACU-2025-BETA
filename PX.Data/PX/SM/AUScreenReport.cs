// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenReport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.report>>>), Persistent = true)]
[Serializable]
public class AUScreenReport : AUScreenItem
{
  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Report ID")]
  public override 
  #nullable disable
  string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(2, IsUnicode = false, InputMask = "")]
  [PXDefault("R")]
  public override string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenReport.screenID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenReport.projectID>
  {
  }

  public new abstract class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenReport.itemCD>
  {
  }

  public new abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenReport.itemType>
  {
  }

  public new abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenReport.itemID>
  {
  }
}
