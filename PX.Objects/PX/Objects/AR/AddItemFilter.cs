// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.AddItemFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class AddItemFilter : INSiteStatusFilter
{
  protected 
  #nullable disable
  string _PriceClassID;
  protected bool? _MyOwner;
  protected int? _OwnerID;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INPriceClass.priceClassID>), DescriptionField = typeof (INItemClass.descr))]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [SubordinateOwner(DisplayName = "Price Manager")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Price  Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  public new abstract class inventory : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemFilter.inventory>
  {
  }

  public abstract class priceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemFilter.priceClassID>
  {
  }

  public abstract class currentOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemFilter.currentOwnerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddItemFilter.myOwner>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemFilter.ownerID>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddItemFilter.myWorkGroup>
  {
  }
}
