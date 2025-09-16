// Decompiled with JetBrains decompiler
// Type: PX.SM.PXEntity
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
public sealed class PXEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid _NodeID;
  private 
  #nullable disable
  string _CacheName;
  private string _MemberName;
  private string _Text;
  private string _OrderBy;
  private string _Icon;
  private string _description;

  public Guid NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  public string CacheName
  {
    get => this._CacheName;
    set => this._CacheName = value;
  }

  public string MemberName
  {
    get => this._MemberName;
    set => this._MemberName = value;
  }

  [PXUnboundKey]
  public string Path
  {
    [PXDependsOnFields(new System.Type[] {typeof (PXEntity.cacheName), typeof (PXEntity.nodeID), typeof (PXEntity.memberName)})] get
    {
      if (this._CacheName == null)
        return this._NodeID.ToString();
      if (this._MemberName == null)
        return $"{this._NodeID.ToString()}|{this._CacheName}";
      return $"{this._NodeID.ToString()}|{this._CacheName}|{this._MemberName}";
    }
  }

  [PXUIField(DisplayName = "Entity Name")]
  public string Text
  {
    [PXDependsOnFields(new System.Type[] {typeof (PXEntity.memberName), typeof (PXEntity.cacheName), typeof (PXEntity.nodeID)})] get
    {
      if (this._Text == null)
      {
        if (this._MemberName != null)
          this._Text = this._MemberName;
        else if (this._CacheName != null)
        {
          this._Text = this._CacheName;
        }
        else
        {
          Guid nodeId = this._NodeID;
          this._Text = this._NodeID.ToString();
        }
      }
      return this._Text;
    }
    set => this._Text = value;
  }

  public string OrderBy
  {
    [PXDependsOnFields(new System.Type[] {typeof (PXEntity.text)})] get
    {
      if (this._OrderBy == null)
        this._OrderBy = this.Text;
      return this._OrderBy;
    }
    set => this._OrderBy = value;
  }

  public string Icon
  {
    get => this._Icon;
    set => this._Icon = value;
  }

  [PXUIField(DisplayName = "Description")]
  public string Description
  {
    get => this._description;
    set => this._description = value;
  }

  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PXEntity.nodeID>
  {
  }

  public abstract class cacheName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PXEntity.cacheName>
  {
  }

  public abstract class memberName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PXEntity.memberName>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PXEntity.text>
  {
  }

  public abstract class orderBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PXEntity.orderBy>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PXEntity.description>
  {
  }
}
