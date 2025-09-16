// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiDescriptorMaster
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiDescriptorMaster : WikiDescriptor
{
  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField]
  public override Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(InputMask = "", IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXWikiSelector(typeof (WikiDescriptor.name), CheckRights = false)]
  public override 
  #nullable disable
  string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorMaster.pageID>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptorMaster.name>
  {
  }

  public new abstract class articleType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiDescriptorMaster.articleType>
  {
  }
}
