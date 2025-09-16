// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemClassMaintExt.UnitsOfMeasure
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.INItemClassMaintExt;

public class UnitsOfMeasure : 
  INUnitLotSerClassBase<
  #nullable disable
  INItemClassMaint, INUnit.itemClassID, INUnitType.itemClass, INItemClass, INItemClass.itemClassID, INItemClass.baseUnit, INItemClass.salesUnit, INItemClass.purchaseUnit, INItemClass.lotSerClassID>
{
  [PXDependToCache(new Type[] {typeof (INItemClass)})]
  public FbqlSelect<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INUnit.itemClassID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.AsOptional>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INUnit.toUnit, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.baseUnit, IBqlString>.AsOptional>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INUnit.fromUnit, IBqlString>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.baseUnit, IBqlString>.AsOptional>>>, 
  #nullable disable
  INUnit>.View classunits;

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase) this.classunits).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>();
  }

  protected override IEnumerable<INUnit> SelectOwnConversions(string baseUnit)
  {
    return (IEnumerable<INUnit>) ((PXSelectBase<INUnit>) this.classunits).SelectMain(new object[3]
    {
      (object) this.ParentCurrent.ItemClassID,
      (object) baseUnit,
      (object) baseUnit
    });
  }

  protected override IEnumerable<INUnit> SelectParentConversions(string baseUnit)
  {
    return GraphHelper.RowCast<INUnit>((IEnumerable) PXSelectBase<INUnit, PXViewOf<INUnit>.BasedOn<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, Equal<INUnitType.global>>>>, And<BqlOperand<INUnit.toUnit, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INUnit.fromUnit, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) baseUnit,
      (object) baseUnit
    }));
  }

  protected override INUnit GetBaseUnit(int? parentID, string baseUnit)
  {
    return this.ResolveConversion((short) 2, baseUnit, baseUnit, new int?(0), parentID);
  }

  protected override INUnit CreateUnitCopy(int? parentID, INUnit unit)
  {
    INUnit copy = PXCache<INUnit>.CreateCopy(unit);
    copy.ItemClassID = new int?();
    copy.UnitType = new short?();
    copy.RecordID = new long?();
    return copy;
  }

  protected override void InitBaseUnit(int? parentID, string newValue)
  {
    this.InsertConversion(parentID, newValue, newValue);
  }

  private void InsertConversion(int? parentID, string fromUnit, string toUnit)
  {
    if (string.IsNullOrEmpty(fromUnit) || INUnit.UK.ByItemClass.FindDirty((PXGraph) this.Base, parentID, fromUnit) != null || ((PXGraph) this.Base).IsCopyPasteContext)
      return;
    this.UnitCache.Insert(this.ResolveConversion((short) 2, fromUnit, toUnit, new int?(0), parentID));
  }

  protected override void ValidateUnitConversions(INItemClass validatedItem)
  {
    if (validatedItem == null)
      return;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INUnit>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<INUnit.toUnit>(),
      (PXDataField) new PXDataFieldValue<INUnit.unitType>((object) (short) 2),
      (PXDataField) new PXDataFieldValue<INUnit.itemClassID>((object) validatedItem.ItemClassID),
      (PXDataField) new PXDataFieldValue<INUnit.toUnit>((PXDbType) 12, new int?(6), (object) validatedItem.BaseUnit, (PXComp) 1)
    }))
    {
      if (pxDataRecord != null)
        throw new PXException("The {0} value specified in the To Unit box differs from the {2} base unit specified for the {1} item class. To resolve the issue, please contact your Acumatica support provider.", new object[3]
        {
          (object) pxDataRecord.GetString(0),
          (object) validatedItem.ItemClassCD,
          (object) validatedItem.BaseUnit
        });
    }
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (INItemClass.itemClassID))]
  protected virtual void _(Events.CacheAttached<INUnit.itemClassID> eventArgs)
  {
  }

  protected virtual void _(
    Events.FieldUpdated<INItemClass, INItemClass.salesUnit> e)
  {
    this.InsertConversion(e.Row.ItemClassID, e.Row.SalesUnit, e.Row.BaseUnit);
  }

  protected virtual void _(
    Events.FieldUpdated<INItemClass, INItemClass.purchaseUnit> e)
  {
    this.InsertConversion(e.Row.ItemClassID, e.Row.PurchaseUnit, e.Row.BaseUnit);
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    UnitsOfMeasure.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    Command command = script.FirstOrDefault<Command>((Func<Command, bool>) (c => c.ObjectName.Equals("classunits", StringComparison.OrdinalIgnoreCase) && c.FieldName.Equals("ItemClassID", StringComparison.OrdinalIgnoreCase)));
    if (command == null)
      return;
    int index = script.IndexOf(command);
    script.Remove(command);
    containers.RemoveAt(index);
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
