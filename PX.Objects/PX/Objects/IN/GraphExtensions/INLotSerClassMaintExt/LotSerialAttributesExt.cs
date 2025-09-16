// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INLotSerClassMaintExt.LotSerialAttributesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Update;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.INLotSerClassMaintExt;

public class LotSerialAttributesExt : LotSerialAttributeConfigurationBase<
#nullable disable
INLotSerClassMaint>
{
  private const string DeletedDatabaseRecordField = "DeletedDatabaseRecord";
  private const string CompanyIDField = "CompanyID";
  private const int ChunkSize = 1000;
  public FbqlSelect<SelectFromBase<INLotSerClassAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAttribute>.On<INLotSerClassAttribute.FK.Attribute>>>.Where<BqlOperand<
  #nullable enable
  INLotSerClassAttribute.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.AsOptional>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INLotSerClassAttribute.sortOrder, IBqlShort>.Asc>>, 
  #nullable disable
  INLotSerClassAttribute>.View Attributes;
  public FbqlSelect<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INItemLotSerialAttribute.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.IN.InventoryItem.lotSerClassID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INItemLotSerialAttribute.attributeID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INLotSerClassAttribute.attributeID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INItemLotSerialAttribute>.View ItemAttributes;
  public FbqlSelect<SelectFromBase<INRegisterItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INRegisterItemLotSerialAttributesHeader.FK.InventoryItem>>>.Where<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INLotSerClassAttribute.lotSerClassID, IBqlString>.FromCurrent>>, 
  #nullable disable
  INRegisterItemLotSerialAttributesHeader>.View InventoryLotSerialAttributeHeader;
  public FbqlSelect<SelectFromBase<POReceiptItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<POReceiptItemLotSerialAttributesHeader.FK.InventoryItem>>>.Where<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INLotSerClassAttribute.lotSerClassID, IBqlString>.FromCurrent>>, 
  #nullable disable
  POReceiptItemLotSerialAttributesHeader>.View POReceiptLotSerialAttributeHeader;
  public FbqlSelect<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.FromCurrent>>, 
  #nullable disable
  INLotSerClass>.View CurrentLotSerClass;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(PX.Data.Events.RowSelected<INLotSerClass> e)
  {
    ((PXSelectBase) this.Attributes).AllowInsert = ((PXSelectBase) this.Attributes).AllowDelete = ((PXSelectBase) this.Attributes).AllowUpdate = this.IsLotSerialAttributesApplicable(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerAssign> e)
  {
    if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerAssign>, INLotSerClass, object>) e).NewValue != "R") || object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerAssign>, INLotSerClass, object>) e).NewValue, e.OldValue) || this.ValidateAttributeValues(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerAssign>, INLotSerClass, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerTrack> e)
  {
    if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerTrack>, INLotSerClass, object>) e).NewValue == "N") || object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerTrack>, INLotSerClass, object>) e).NewValue, e.OldValue) || this.ValidateAttributeValues(e.Row))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLotSerClass, INLotSerClass.lotSerTrack>, INLotSerClass, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INLotSerClass> e)
  {
    if (this.IsLotSerialAttributesApplicable(e.Row))
      return;
    this.DeleteAllLotSerialAttributes(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INLotSerClass> e)
  {
    INLotSerClass row = e.Row;
    if (row == null || e.Operation != 2 && e.Operation != 1 || !row.UseLotSerSpecificDetails.GetValueOrDefault())
      return;
    Dictionary<string, List<PX.Objects.IN.InventoryItem>> dictionary1 = GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) PXSelectBase<INLotSerClass, PXViewOf<INLotSerClass>.BasedOn<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<INLotSerClass.lotSerClassID>>>, FbqlJoins.Inner<ARSalesPrice>.On<BqlOperand<ARSalesPrice.inventoryID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.LotSerClassID
    })).GroupBy<PX.Objects.IN.InventoryItem, string>((Func<PX.Objects.IN.InventoryItem, string>) (_ => _.InventoryCD)).ToDictionary<IGrouping<string, PX.Objects.IN.InventoryItem>, string, List<PX.Objects.IN.InventoryItem>>((Func<IGrouping<string, PX.Objects.IN.InventoryItem>, string>) (group => group.Key), (Func<IGrouping<string, PX.Objects.IN.InventoryItem>, List<PX.Objects.IN.InventoryItem>>) (group => group.ToList<PX.Objects.IN.InventoryItem>()));
    if (dictionary1.Any<KeyValuePair<string, List<PX.Objects.IN.InventoryItem>>>())
    {
      string str = string.Join(", ", (IEnumerable<string>) dictionary1.Keys);
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "The check box cannot be selected because prices are specified on the Sales Prices (AR202000) form for the following items of the class: {0}.", new object[1]
      {
        (object) str
      });
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INLotSerClass>>) e).Cache.RaiseExceptionHandling<INLotSerClass.useLotSerSpecificDetails>((object) e.Row, (object) false, (Exception) propertyException))
        throw propertyException;
    }
    else
    {
      Dictionary<string, List<PX.Objects.IN.InventoryItem>> dictionary2 = GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) PXSelectBase<INLotSerClass, PXViewOf<INLotSerClass>.BasedOn<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<INLotSerClass.lotSerClassID>>>, FbqlJoins.Inner<ARPriceWorksheetDetail>.On<BqlOperand<ARPriceWorksheetDetail.inventoryID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Inner<ARPriceWorksheet>.On<BqlOperand<ARPriceWorksheet.refNbr, IBqlString>.IsEqual<ARPriceWorksheetDetail.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerClass.lotSerClassID, Equal<P.AsString>>>>>.And<BqlOperand<ARPriceWorksheet.status, IBqlString>.IsNotEqual<SPWorksheetStatus.released>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) row.LotSerClassID
      })).GroupBy<PX.Objects.IN.InventoryItem, string>((Func<PX.Objects.IN.InventoryItem, string>) (_ => _.InventoryCD)).ToDictionary<IGrouping<string, PX.Objects.IN.InventoryItem>, string, List<PX.Objects.IN.InventoryItem>>((Func<IGrouping<string, PX.Objects.IN.InventoryItem>, string>) (group => group.Key), (Func<IGrouping<string, PX.Objects.IN.InventoryItem>, List<PX.Objects.IN.InventoryItem>>) (group => group.ToList<PX.Objects.IN.InventoryItem>()));
      if (!dictionary2.Any<KeyValuePair<string, List<PX.Objects.IN.InventoryItem>>>())
        return;
      string str = string.Join(", ", (IEnumerable<string>) dictionary2.Keys);
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "The check box cannot be selected because there is at least one unreleased worksheet on the Sales Price Worksheets (AR202010) form for the following items of the class: {0}.", new object[1]
      {
        (object) str
      });
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INLotSerClass>>) e).Cache.RaiseExceptionHandling<INLotSerClass.useLotSerSpecificDetails>((object) e.Row, (object) false, (Exception) propertyException))
        throw propertyException;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<INLotSerClassAttribute> e)
  {
    PXUIFieldAttribute.SetEnabled<INLotSerClassAttribute.attributeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INLotSerClassAttribute>>) e).Cache, (object) e.Row, e.Row?.AttributeID == null || ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INLotSerClassAttribute>>) e).Cache.GetStatus((object) e.Row) == 2);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INLotSerClassAttribute> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INLotSerClassAttribute>>) e).Cache.GetStatus((object) e.Row) == 2 || ((PXSelectBase<INLotSerClass>) this.Base.lotserclass).Current == null || ((PXSelectBase) this.Base.lotserclass).Cache.GetStatus((object) ((PXSelectBase<INLotSerClass>) this.Base.lotserclass).Current) == 3)
      return;
    if (e.Row.IsActive.GetValueOrDefault())
      throw new PXException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (this.FindReleasedAttributeValues(e.Row))
      throw new PXException("The attribute cannot be deleted because at least one of its values is assigned to an item that has already been received.");
    if (((PXGraph) this.Base).IsContractBasedAPI || !this.FindUnreleasedAttributeValues(e.Row) || ((PXSelectBase<INLotSerClassAttribute>) this.Attributes).Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INLotSerClassAttribute> e)
  {
    this.ClearAttributeValues(e.Row);
  }

  protected override bool IsAttributeCacheDirty() => ((PXSelectBase) this.Attributes).Cache.IsDirty;

  protected virtual bool IsLotSerialAttributesApplicable(INLotSerClass lotSerClass)
  {
    return lotSerClass?.LotSerAssign == "R" && lotSerClass.LotSerTrack != "N";
  }

  protected virtual void DeleteAllLotSerialAttributes(INLotSerClass lotSerClass)
  {
    foreach (INLotSerClassAttribute serClassAttribute1 in GraphHelper.RowCast<INLotSerClassAttribute>((IEnumerable) ((PXSelectBase) this.Attributes).View.SelectMultiBound((object[]) new INLotSerClass[1]
    {
      lotSerClass
    }, Array.Empty<object>())))
    {
      INLotSerClassAttribute serClassAttribute2 = serClassAttribute1;
      if (serClassAttribute2.IsActive.GetValueOrDefault())
      {
        serClassAttribute2.IsActive = new bool?(false);
        serClassAttribute2 = ((PXSelectBase<INLotSerClassAttribute>) this.Attributes).Update(serClassAttribute1);
      }
      ((PXSelectBase<INLotSerClassAttribute>) this.Attributes).Delete(serClassAttribute2);
    }
  }

  protected virtual bool ValidateAttributeValues(INLotSerClass lotserClass)
  {
    foreach (INLotSerClassAttribute lotSerClassAttribute in GraphHelper.RowCast<INLotSerClassAttribute>((IEnumerable) ((PXSelectBase) this.Attributes).View.SelectMultiBound((object[]) new INLotSerClass[1]
    {
      lotserClass
    }, Array.Empty<object>())))
    {
      if (this.FindReleasedAttributeValues(lotSerClassAttribute))
        throw new PXSetPropertyException((IBqlTable) lotserClass, "The attribute cannot be deleted because at least one of its values is assigned to an item that has already been received.");
    }
    foreach (INLotSerClassAttribute lotSerClassAttribute in GraphHelper.RowCast<INLotSerClassAttribute>((IEnumerable) ((PXSelectBase) this.Attributes).View.SelectMultiBound((object[]) new INLotSerClass[1]
    {
      lotserClass
    }, Array.Empty<object>())))
    {
      if (!((PXGraph) this.Base).IsContractBasedAPI && this.FindUnreleasedAttributeValues(lotSerClassAttribute) && ((PXSelectBase<INLotSerClassAttribute>) this.Attributes).Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) != 1)
        return false;
    }
    return true;
  }

  private void ClearAttributeValues(INLotSerClassAttribute lotSerClassAttribute)
  {
    string attributeId = lotSerClassAttribute.AttributeID;
    if (this.GetKeyValueAttribute(attributeId, true) == null)
      return;
    int num1 = 0;
    int num2 = 0;
    foreach (INRegisterItemLotSerialAttributesHeader attributesHeader in GraphHelper.RowCast<INRegisterItemLotSerialAttributesHeader>((IEnumerable) ((PXSelectBase) this.InventoryLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, -1, ref num2)))
      this.SetValue<INRegisterItemLotSerialAttributesHeader>(attributesHeader, attributeId, (object) null);
    int num3 = 0;
    int num4 = 0;
    foreach (POReceiptItemLotSerialAttributesHeader attributesHeader in GraphHelper.RowCast<POReceiptItemLotSerialAttributesHeader>((IEnumerable) ((PXSelectBase) this.POReceiptLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num3, -1, ref num4)))
      this.SetValue<POReceiptItemLotSerialAttributesHeader>(attributesHeader, attributeId, (object) null);
  }

  private bool FindUnreleasedAttributeValues(INLotSerClassAttribute lotSerClassAttribute)
  {
    string attributeId = lotSerClassAttribute.AttributeID;
    if (this.GetKeyValueAttribute(attributeId, true) == null)
      return false;
    int num1 = 0;
    int num2 = 0;
    ((PXSelectBase) this.InventoryLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, 1, ref num2);
    if (num2 > 0)
      return true;
    num1 = 0;
    num2 = 0;
    ((PXSelectBase) this.POReceiptLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, 1, ref num2);
    return num2 > 0;
  }

  private bool FindReleasedAttributeValues(INLotSerClassAttribute lotSerClassAttribute)
  {
    if (this.GetKeyValueAttribute(lotSerClassAttribute.AttributeID, true) == null)
      return false;
    PXViewOf<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.FK.InventoryItem>>>.Where<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<BqlField<INLotSerClassAttribute.lotSerClassID, IBqlString>.FromCurrent>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.FK.InventoryItem>>>.Where<BqlOperand<PX.Objects.IN.InventoryItem.lotSerClassID, IBqlString>.IsEqual<BqlField<INLotSerClassAttribute.lotSerClassID, IBqlString>.FromCurrent>>>.ReadOnly((PXGraph) this.Base);
    int num1 = 0;
    int num2 = 0;
    ((PXSelectBase) readOnly).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(lotSerClassAttribute.AttributeID), (PXCondition) 12)
    }, ref num1, 1, ref num2);
    return num2 > 0;
  }

  protected override void ExecutePerformPersist(
    PXGraph.IPersistPerformer persister,
    Action<PXGraph.IPersistPerformer> base_PerformPersist)
  {
    List<INLotSerClassAttribute> attributes1 = new List<INLotSerClassAttribute>();
    List<INLotSerClassAttribute> attributes2 = new List<INLotSerClassAttribute>();
    List<INLotSerClassAttribute> attributes3 = new List<INLotSerClassAttribute>();
    foreach (INLotSerClassAttribute serClassAttribute in GraphHelper.RowCast<INLotSerClassAttribute>(((PXSelectBase) this.Attributes).Cache.Cached))
    {
      switch (((PXSelectBase) this.Attributes).Cache.GetStatus((object) serClassAttribute) - 1)
      {
        case 0:
          attributes2.Add(serClassAttribute);
          continue;
        case 1:
          attributes1.Add(serClassAttribute);
          continue;
        case 2:
          attributes3.Add(serClassAttribute);
          continue;
        default:
          continue;
      }
    }
    INLotSerClass lotSerClass = GraphHelper.RowCast<INLotSerClass>(((PXSelectBase) this.Base.lotserclass).Cache.Updated).SingleOrDefault<INLotSerClass>();
    Lazy<PointDbmsBase> lazy = new Lazy<PointDbmsBase>((Func<PointDbmsBase>) (() => PXDatabase.Provider.CreateDbServicesPointWithCurrentTransaction()));
    base.ExecutePerformPersist(persister, base_PerformPersist);
    if (lotSerClass == null)
      return;
    if (attributes1.Count > 0)
      this.InsertItemAttributes(lazy.Value, lotSerClass, attributes1);
    if (attributes2.Count > 0)
      this.UpdateItemAttributes(lotSerClass, attributes2);
    if (attributes3.Count <= 0)
      return;
    this.DeleteItemAttributes(lazy.Value, lotSerClass, attributes3);
  }

  private void InsertItemAttributes(
    PointDbmsBase point,
    INLotSerClass lotSerClass,
    List<INLotSerClassAttribute> attributes)
  {
    for (int count = 0; count < attributes.Count; count += 1000)
    {
      IEnumerable<string> attributeIDs = attributes.Skip<INLotSerClassAttribute>(count).Take<INLotSerClassAttribute>(1000).Select<INLotSerClassAttribute, string>((Func<INLotSerClassAttribute, string>) (a => a.AttributeID));
      this.InsertItemAttributes(point, lotSerClass, attributeIDs);
    }
  }

  private void InsertItemAttributes(
    PointDbmsBase point,
    INLotSerClass lotSerClass,
    IEnumerable<string> attributeIDs)
  {
    int currentCompany = PXInstanceHelper.CurrentCompany;
    TableHeader table = point.Schema.GetTable("INLotSerClassAttribute");
    CmdInsertSelect cmdInsertSelect = new CmdInsertSelect(point.Schema.GetTable("INItemLotSerialAttribute"), table, new List<YaqlJoin>()
    {
      (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("InventoryItem", (string) null), Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "InventoryItem"), currentCompany), Yaql.eq<int>((YaqlScalar) Yaql.column("DeletedDatabaseRecord", "InventoryItem"), 0)), Yaql.eq<INLotSerClassAttribute.lotSerClassID, PX.Objects.IN.InventoryItem.lotSerClassID>("<declaring_type_name>", "<declaring_type_name>")))
    }, (string) null)
    {
      AssignValues = {
        {
          "inventoryID",
          (YaqlScalar) Yaql.column<PX.Objects.IN.InventoryItem.inventoryID>((string) null)
        }
      },
      Condition = Yaql.and(Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "INLotSerClassAttribute"), currentCompany), Yaql.eq<string>((YaqlScalar) Yaql.column<INLotSerClassAttribute.lotSerClassID>((string) null), lotSerClass.LotSerClassID)), Yaql.isIn<string>((YaqlScalar) Yaql.column<INLotSerClassAttribute.attributeID>((string) null), attributeIDs)), Yaql.not(Yaql.exists((YaqlTable) Yaql.schemaTable("INItemLotSerialAttribute", (string) null), Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), currentCompany), Yaql.eq<INItemLotSerialAttribute.inventoryID, PX.Objects.IN.InventoryItem.inventoryID>("<declaring_type_name>", "<declaring_type_name>")), Yaql.eq<INItemLotSerialAttribute.attributeID, INLotSerClassAttribute.attributeID>("<declaring_type_name>", "<declaring_type_name>")))))
    };
    point.executeSingleCommand((CommandBase) cmdInsertSelect, new ExecutionContext((IExecutionObserver) null), false);
    PXTransactionScope.TableModified(typeof (INItemLotSerialAttribute));
  }

  private void UpdateItemAttributes(
    INLotSerClass lotSerClass,
    List<INLotSerClassAttribute> attributes)
  {
    for (int count = 0; count < attributes.Count; count += 1000)
    {
      IEnumerable<string> attributeIDs = attributes.Skip<INLotSerClassAttribute>(count).Take<INLotSerClassAttribute>(1000).Select<INLotSerClassAttribute, string>((Func<INLotSerClassAttribute, string>) (a => a.AttributeID));
      this.UpdateItemAttributes(lotSerClass, attributeIDs);
    }
  }

  private void UpdateItemAttributes(INLotSerClass lotSerClass, IEnumerable<string> attributeIDs)
  {
    PXUpdateJoin<Set<INItemLotSerialAttribute.isActive, INLotSerClassAttribute.isActive, Set<INItemLotSerialAttribute.required, INLotSerClassAttribute.required, Set<INItemLotSerialAttribute.sortOrder, INLotSerClassAttribute.sortOrder>>>, INItemLotSerialAttribute, InnerJoin<INLotSerClassAttribute, On<INLotSerClassAttribute.attributeID, Equal<INItemLotSerialAttribute.attributeID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INItemLotSerialAttribute.inventoryID>, And<INLotSerClassAttribute.lotSerClassID, Equal<PX.Objects.IN.InventoryItem.lotSerClassID>>>>>, Where<PX.Objects.IN.InventoryItem.lotSerClassID, Equal<Required<INLotSerClass.lotSerClassID>>, And<INItemLotSerialAttribute.attributeID, In<Required<INItemLotSerialAttribute.attributeID>>>>>.Update((PXGraph) this.Base, new object[2]
    {
      (object) lotSerClass.LotSerClassID,
      (object) attributeIDs.ToArray<string>()
    });
  }

  private void DeleteItemAttributes(
    PointDbmsBase point,
    INLotSerClass lotSerClass,
    List<INLotSerClassAttribute> attributes)
  {
    for (int count = 0; count < attributes.Count; count += 1000)
    {
      IEnumerable<string> attributeIDs = attributes.Skip<INLotSerClassAttribute>(count).Take<INLotSerClassAttribute>(1000).Select<INLotSerClassAttribute, string>((Func<INLotSerClassAttribute, string>) (a => a.AttributeID));
      this.DeleteItemAttributes(point, lotSerClass, attributeIDs);
    }
  }

  private void DeleteItemAttributes(
    PointDbmsBase point,
    INLotSerClass lotSerClass,
    IEnumerable<string> attributeIDs)
  {
    int currentCompany = PXInstanceHelper.CurrentCompany;
    CmdDelete cmdDelete = new CmdDelete(new YaqlSchemaTable("INItemLotSerialAttribute", (string) null), new List<YaqlJoin>()
    {
      (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("InventoryItem", (string) null), Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "InventoryItem"), currentCompany), Yaql.eq<int>((YaqlScalar) Yaql.column("DeletedDatabaseRecord", "InventoryItem"), 0)), Yaql.eq<INItemLotSerialAttribute.inventoryID, PX.Objects.IN.InventoryItem.inventoryID>("<declaring_type_name>", "<declaring_type_name>")))
    })
    {
      Condition = Yaql.and(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "INItemLotSerialAttribute"), currentCompany), Yaql.eq<string>((YaqlScalar) Yaql.column<PX.Objects.IN.InventoryItem.lotSerClassID>((string) null), lotSerClass.LotSerClassID)), Yaql.isIn<string>((YaqlScalar) Yaql.column<INItemLotSerialAttribute.attributeID>((string) null), attributeIDs))
    };
    point.executeSingleCommand((CommandBase) cmdDelete, new ExecutionContext((IExecutionObserver) null), false);
    PXTransactionScope.TableModified(typeof (INItemLotSerialAttribute));
  }
}
