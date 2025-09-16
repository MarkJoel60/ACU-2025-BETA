// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SubordinateOwnedFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Obsolete("Will be removed in 7.0 version")]
[Serializable]
public class SubordinateOwnedFilter : OwnedFilter
{
  [SubordinateOwner(DisplayName = "Assigned to")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSubordinateGroupSelector]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  public abstract class currentOwnerID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    SubordinateOwnedFilter.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SubordinateOwnedFilter.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SubordinateOwnedFilter.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SubordinateOwnedFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SubordinateOwnedFilter.myWorkGroup>
  {
  }
}
