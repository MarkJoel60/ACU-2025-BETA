// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBuilderEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

public class TaxBuilderEngine
{
  public static 
  #nullable disable
  TaxBuilder.Result Execute(PXGraph graph, string stateCode)
  {
    PXSelect<TXImportFileData, Where<TXImportFileData.stateCode, Equal<Required<TXImportFileData.stateCode>>>> pxSelect = new PXSelect<TXImportFileData, Where<TXImportFileData.stateCode, Equal<Required<TXImportFileData.stateCode>>>>(graph);
    PXSelectBase<TXImportZipFileData> pxSelectBase = (PXSelectBase<TXImportZipFileData>) new PXSelect<TXImportZipFileData, Where<TXImportZipFileData.stateCode, Equal<Required<TXImportZipFileData.stateCode>>, And<Where2<Where<TXImportZipFileData.plus4PortionOfZipCode, NotEqual<TaxBuilderEngine.ZipMin>>, Or<TXImportZipFileData.plus4PortionOfZipCode2, NotEqual<TaxBuilderEngine.ZipMax>>>>>>(graph);
    object[] objArray = new object[1]{ (object) stateCode };
    PXResultset<TXImportFileData> pxResultset1 = ((PXSelectBase<TXImportFileData>) pxSelect).Select(objArray);
    PXResultset<TXImportZipFileData> pxResultset2 = pxSelectBase.Select(new object[1]
    {
      (object) stateCode
    });
    List<TXImportFileData> data = new List<TXImportFileData>(pxResultset1.Count);
    foreach (PXResult<TXImportFileData> pxResult in pxResultset1)
    {
      TXImportFileData txImportFileData = PXResult<TXImportFileData>.op_Implicit(pxResult);
      data.Add(txImportFileData);
    }
    List<TXImportZipFileData> zipData = new List<TXImportZipFileData>(pxResultset2.Count);
    foreach (PXResult<TXImportZipFileData> pxResult in pxResultset2)
    {
      TXImportZipFileData importZipFileData = PXResult<TXImportZipFileData>.op_Implicit(pxResult);
      zipData.Add(importZipFileData);
    }
    return ((TaxBuilder) new GenericTaxBuilder(stateCode, data, zipData) ?? throw new NotImplementedException()).Execute();
  }

  public static string GetTaxZoneByAddress(PXGraph graph, IAddressBase adrress)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (adrress == null)
      throw new ArgumentNullException(nameof (adrress));
    if (string.IsNullOrWhiteSpace(adrress.CountryID))
      return (string) null;
    PXResultset<TaxZoneAddressMapping> pxResultset = (PXResultset<TaxZoneAddressMapping>) null;
    if (!string.IsNullOrWhiteSpace(adrress.PostalCode))
      pxResultset = PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.countryID, Equal<P.AsString>>>>, And<BqlOperand<TaxZoneAddressMapping.stateID, IBqlString>.IsEqual<Empty>>>>.And<BqlOperand<Required<Parameter.ofString>, IBqlString>.IsBetween<TaxZoneAddressMapping.fromPostalCode, TaxZoneAddressMapping.toPostalCodeSuffixed>>>>.Config>.Select(graph, new object[2]
      {
        (object) adrress.CountryID,
        (object) adrress.PostalCode
      });
    if ((pxResultset == null || pxResultset.Count == 0) && !string.IsNullOrWhiteSpace(adrress.State))
      pxResultset = PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.countryID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.stateID, Equal<P.AsString>>>>>.And<BqlOperand<TaxZoneAddressMapping.fromPostalCode, IBqlString>.IsEqual<Empty>>>>>.Config>.Select(graph, new object[2]
      {
        (object) adrress.CountryID,
        (object) adrress.State
      });
    if (pxResultset == null || pxResultset.Count == 0)
      pxResultset = PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.countryID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.stateID, Equal<Empty>>>>>.And<BqlOperand<TaxZoneAddressMapping.fromPostalCode, IBqlString>.IsEqual<Empty>>>>>.Config>.Select(graph, new object[1]
      {
        (object) adrress.CountryID
      });
    if ((pxResultset == null || pxResultset.Count == 0) && !string.IsNullOrWhiteSpace(adrress.PostalCode))
      pxResultset = PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.countryID, Equal<Empty>>>>, And<BqlOperand<TaxZoneAddressMapping.stateID, IBqlString>.IsEqual<Empty>>>>.And<BqlOperand<Required<Parameter.ofString>, IBqlString>.IsBetween<TaxZoneAddressMapping.fromPostalCode, TaxZoneAddressMapping.toPostalCodeSuffixed>>>>.Config>.Select(graph, new object[1]
      {
        (object) adrress.PostalCode
      });
    if (pxResultset.Count == 0)
    {
      PXTrace.WriteWarning("Failed to find record in TaxZoneAddressMapping for the given address data => country: {0}, state: {1}, postal code: {2}", new object[3]
      {
        (object) adrress.CountryID,
        (object) adrress.State,
        (object) adrress.PostalCode
      });
      return (string) null;
    }
    if (pxResultset.Count > 1)
      PXTrace.WriteWarning("{0} records returned from TaxZoneAddressMapping for the given address data => country: {0}, state: {1}, postal code: {2}", new object[3]
      {
        (object) adrress.CountryID,
        (object) adrress.State,
        (object) adrress.PostalCode
      });
    return PXResultset<TaxZoneAddressMapping>.op_Implicit(pxResultset).TaxZoneID;
  }

  public class ZipMin : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  TaxBuilderEngine.ZipMin>
  {
    public ZipMin()
      : base(1)
    {
    }
  }

  public class ZipMax : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  TaxBuilderEngine.ZipMax>
  {
    public ZipMax()
      : base(9999)
    {
    }
  }
}
