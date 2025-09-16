// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.AddLotSerialHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

/// <exclude />
[PXCacheName("AddLotSerialHeader")]
public class AddLotSerialHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INLotSerClass">Lot/Serial Class</see>, to which the Inventory Item belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLotSerClass.LotSerClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (SearchFor<INLotSerClass.lotSerClassID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerClass.lotSerTrack, In3<INLotSerTrack.lotNumbered, INLotSerTrack.serialNumbered>>>>>.And<BqlOperand<INLotSerClass.lotSerAssign, IBqlString>.IsEqual<INLotSerAssign.whenReceived>>>), DescriptionField = typeof (INLotSerClass.descr))]
  [PXUIField(DisplayName = "Lot/Serial Class")]
  public virtual 
  #nullable disable
  string LotSerClassID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">Inventory Item</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [AnyInventory(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLotSerClass>.On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<AddLotSerialHeader.lotSerClassID>, IsNull>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<BqlField<AddLotSerialHeader.lotSerClassID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INLotSerClass.lotSerTrack, IBqlString>.IsIn<INLotSerTrack.lotNumbered, INLotSerTrack.serialNumbered>>>>.And<BqlOperand<INLotSerClass.lotSerAssign, IBqlString>.IsEqual<INLotSerAssign.whenReceived>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Inventory Item")]
  public virtual int? InventoryID { get; set; }

  /// <summary>A search string.</summary>
  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Search")]
  public virtual string Search { get; set; }

  /// <summary>
  /// Array to store attribute identifiers (CSAttribute.attributeID) of lot/serial attributes.
  /// </summary>
  public virtual string[] AttributeIdentifiers { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Available Items Only")]
  public virtual bool? OnlyAvailable { get; set; }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? ShowLocation { get; set; }

  [PXInt]
  [PXDefault(typeof (PX.Objects.IN.INRegister.branchID))]
  public virtual int? BranchID { get; set; }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddLotSerialHeader.lotSerClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddLotSerialHeader.inventoryID>
  {
  }

  public abstract class search : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddLotSerialHeader.search>
  {
  }

  public abstract class attributeIdentifiers : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AddLotSerialHeader.attributeIdentifiers>
  {
  }

  public abstract class onlyAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AddLotSerialHeader.onlyAvailable>
  {
  }

  public abstract class showLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddLotSerialHeader.showLocation>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddLotSerialHeader.branchID>
  {
  }
}
