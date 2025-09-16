// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ManufacturerModelMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class ManufacturerModelMaint : PXGraph<ManufacturerModelMaint, FSManufacturerModel>
{
  public PXSelect<FSManufacturerModel, Where<FSManufacturerModel.manufacturerID, Equal<Optional<FSManufacturerModel.manufacturerID>>>> ManufacturerModelRecords;
  public PXSelect<FSManufacturerModel, Where<FSManufacturerModel.manufacturerModelID, Equal<Current<FSManufacturerModel.manufacturerModelID>>>> ManufacturerModelSelected;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSManufacturerModel.manufacturerModelCD, Where<FSManufacturerModel.manufacturerID, Equal<Current<FSManufacturerModel.manufacturerID>>>>), SubstituteKey = typeof (FSManufacturerModel.manufacturerModelCD))]
  protected virtual void _(
    Events.CacheAttached<FSManufacturerModel.manufacturerModelCD> e)
  {
  }
}
