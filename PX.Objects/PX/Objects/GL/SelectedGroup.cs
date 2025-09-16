// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SelectedGroup
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
public class SelectedGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string WildcardAnything = "%";

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? Group { get; set; }

  [PXInt]
  public virtual int? AccountID { get; set; }

  [PXInt]
  public virtual int? SubID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string AccountMask { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubMask { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("%")]
  public virtual string AccountMaskWildcard { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXDefault("%")]
  public virtual string SubMaskWildcard { get; set; }

  public abstract class group : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SelectedGroup.group>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelectedGroup.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SelectedGroup.subID>
  {
  }

  public abstract class accountMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelectedGroup.accountMask>
  {
  }

  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SelectedGroup.subMask>
  {
  }

  public abstract class accountMaskWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SelectedGroup.accountMaskWildcard>
  {
  }

  public abstract class subMaskWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SelectedGroup.subMaskWildcard>
  {
  }
}
