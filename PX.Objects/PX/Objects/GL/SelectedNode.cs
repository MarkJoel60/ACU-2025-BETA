// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SelectedNode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _Group;
  protected int? _AccountID;
  protected int? _SubID;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? Group
  {
    get => this._Group;
    set => this._Group = value;
  }

  [PXInt]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXInt]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("%")]
  public virtual 
  #nullable disable
  string AccountMaskWildcard { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXDefault("%")]
  public virtual string SubMaskWildcard { get; set; }

  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  public abstract class group : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SelectedNode.group>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelectedNode.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelectedNode.subID>
  {
  }

  public abstract class accountMaskWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SelectedNode.accountMaskWildcard>
  {
  }

  public abstract class subMaskWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SelectedNode.subMaskWildcard>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SelectedNode.groupMask>
  {
  }
}
