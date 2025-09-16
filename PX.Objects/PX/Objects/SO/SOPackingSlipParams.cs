// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackingSlipParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Packing Slip Parameters")]
public class SOPackingSlipParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (SearchFor<SOPickingWorksheet.worksheetNbr>.In<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>>.Where<BqlChainableConditionLite<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.batch>>>.OrderBy<BqlField<SOPickingWorksheet.worksheetNbr, IBqlString>.Desc>>))]
  public virtual 
  #nullable disable
  string BatchWorksheetNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (SearchFor<SOShipment.shipmentNbr>.In<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<SOPackingSlipParams.batchWorksheetNbr>, IsNull>>>>.Or<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsEqual<BqlField<SOPackingSlipParams.batchWorksheetNbr, IBqlString>.FromCurrent.NoDefault>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.OrderBy<BqlField<SOShipment.shipmentNbr, IBqlString>.Desc>>))]
  public virtual string BatchShipmentNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (SearchFor<SOPickingWorksheet.worksheetNbr>.In<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>>.Where<BqlChainableConditionLite<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.wave>>>.OrderBy<BqlField<SOPickingWorksheet.worksheetNbr, IBqlString>.Desc>>))]
  public virtual string WaveWorksheetNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (SearchFor<SOShipment.shipmentNbr>.In<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<SOPackingSlipParams.waveWorksheetNbr>, IsNull>>>>.Or<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsEqual<BqlField<SOPackingSlipParams.waveWorksheetNbr, IBqlString>.FromCurrent.NoDefault>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.OrderBy<BqlField<SOShipment.shipmentNbr, IBqlString>.Desc>>))]
  public virtual string WaveShipmentNbr { get; set; }

  public abstract class batchWorksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackingSlipParams.batchWorksheetNbr>
  {
  }

  public abstract class batchShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackingSlipParams.batchShipmentNbr>
  {
  }

  public abstract class waveWorksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackingSlipParams.waveWorksheetNbr>
  {
  }

  public abstract class waveShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackingSlipParams.waveShipmentNbr>
  {
  }
}
