// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCodeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PR.Standalone;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Objects.PM;

[PXDBString(15)]
[PXUIField(DisplayName = "WCC Code", FieldClass = "Construction")]
[PXRestrictor(typeof (Where<BqlOperand<PMWorkCode.isActive, IBqlBool>.IsEqual<True>>), "The {0} workers' compensation code is not marked as Active on the Workers' Compensation Codes form.", new Type[] {typeof (PMWorkCode.workCodeID)})]
public class PMWorkCodeAttribute : PXEntityAttribute, IPXFieldDefaultingSubscriber
{
  protected Type _CostCodeField;
  protected Type _ProjectField;
  protected Type _ProjectTaskField;
  protected Type _LaborItemField;
  protected Type _EmployeeIDField;

  public PMWorkCodeAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (PMWorkCode.workCodeID), new Type[2]
    {
      typeof (PMWorkCode.workCodeID),
      typeof (PMWorkCode.description)
    })
    {
      DescriptionField = typeof (PMWorkCode.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public PMWorkCodeAttribute(
    Type costCodeField,
    Type projectField,
    Type projectTaskField,
    Type laborItemField,
    Type employeeIDField)
    : this()
  {
    this._CostCodeField = costCodeField;
    this._ProjectField = projectField;
    this._ProjectTaskField = projectTaskField;
    this._LaborItemField = laborItemField;
    this._EmployeeIDField = employeeIDField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (this.DBAttribute is PXDBStringAttribute dbAttribute && ((PXDBFieldAttribute) dbAttribute).IsKey)
    {
      StringBuilder stringBuilder = new StringBuilder(">");
      for (int index = 0; index < dbAttribute.Length; ++index)
        stringBuilder.Append("a");
      PXDBStringAttribute.SetInputMask(sender, ((PXEventSubscriberAttribute) this)._FieldName, stringBuilder.ToString());
    }
    foreach (Type field in new List<Type>()
    {
      this._CostCodeField,
      this._ProjectField,
      this._ProjectTaskField,
      this._LaborItemField,
      this._EmployeeIDField
    })
      this.SetFieldUpdatedHandler(sender, field);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this._ProjectField != (Type) null && this._ProjectTaskField != (Type) null)
    {
      int? projectID = (int?) sender.GetValue(e.Row, this._ProjectField.Name);
      int? nullable = (int?) sender.GetValue(e.Row, this._ProjectTaskField.Name);
      if (projectID.HasValue && !ProjectDefaultAttribute.IsNonProject(projectID))
      {
        PMWorkCodeProjectTaskSource projectTaskSource = ((PXSelectBase<PMWorkCodeProjectTaskSource>) new FbqlSelect<SelectFromBase<PMWorkCodeProjectTaskSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMWorkCode>.On<PMWorkCodeProjectTaskSource.FK.WorkCode>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCode.isActive, Equal<True>>>>, And<BqlOperand<PMWorkCodeProjectTaskSource.projectID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeProjectTaskSource.projectTaskID, Equal<P.AsInt>>>>>.Or<BqlOperand<PMWorkCodeProjectTaskSource.projectTaskID, IBqlInt>.IsNull>>>.Order<By<BqlField<PMWorkCodeProjectTaskSource.projectTaskID, IBqlInt>.Desc>>, PMWorkCodeProjectTaskSource>.View(sender.Graph)).SelectSingle(new object[2]
        {
          (object) projectID,
          (object) nullable
        });
        if (projectTaskSource != null)
        {
          e.NewValue = (object) projectTaskSource.WorkCodeID;
          return;
        }
      }
    }
    if (this._LaborItemField != (Type) null)
    {
      int? nullable = (int?) sender.GetValue(e.Row, this._LaborItemField.Name);
      if (nullable.HasValue)
      {
        PMWorkCodeLaborItemSource codeLaborItemSource = ((PXSelectBase<PMWorkCodeLaborItemSource>) new FbqlSelect<SelectFromBase<PMWorkCodeLaborItemSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMWorkCode>.On<PMWorkCodeLaborItemSource.FK.WorkCode>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCode.isActive, Equal<True>>>>>.And<BqlOperand<PMWorkCodeLaborItemSource.laborItemID, IBqlInt>.IsEqual<P.AsInt>>>, PMWorkCodeLaborItemSource>.View(sender.Graph)).SelectSingle(new object[1]
        {
          (object) nullable
        });
        if (codeLaborItemSource != null)
        {
          e.NewValue = (object) codeLaborItemSource.WorkCodeID;
          return;
        }
      }
    }
    if (this._CostCodeField != (Type) null && PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
    {
      int? costCodeID = (int?) sender.GetValue(e.Row, this._CostCodeField.Name);
      if (costCodeID.HasValue)
      {
        int? nullable = costCodeID;
        int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
        if (!(nullable.GetValueOrDefault() == defaultCostCode & nullable.HasValue))
        {
          PMCostCode pmCostCode = PMCostCode.PK.Find(sender.Graph, costCodeID);
          if (pmCostCode != null)
          {
            PMWorkCodeCostCodeRange codeCostCodeRange = ((PXSelectBase<PMWorkCodeCostCodeRange>) new FbqlSelect<SelectFromBase<PMWorkCodeCostCodeRange, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMWorkCode>.On<PMWorkCodeCostCodeRange.FK.WorkCode>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCode.isActive, Equal<True>>>>, And<BqlOperand<PMWorkCodeCostCodeRange.costCodeFrom, IBqlString>.IsLessEqual<P.AsString>>>>.And<BqlOperand<PMWorkCodeCostCodeRange.costCodeTo, IBqlString>.IsGreaterEqual<P.AsString>>>, PMWorkCodeCostCodeRange>.View(sender.Graph)).SelectSingle(new object[2]
            {
              (object) pmCostCode.CostCodeCD,
              (object) pmCostCode.CostCodeCD
            });
            if (codeCostCodeRange != null)
            {
              e.NewValue = (object) codeCostCodeRange.WorkCodeID;
              return;
            }
          }
        }
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
    {
      int? employeeId = this.GetEmployeeID(sender, e.Row);
      if (employeeId.HasValue)
      {
        PREmployee prEmployee = PREmployee.PK.Find(sender.Graph, employeeId);
        if (prEmployee != null && !string.IsNullOrEmpty(prEmployee.WorkCodeID))
        {
          e.NewValue = (object) prEmployee.WorkCodeID;
          return;
        }
      }
    }
    e.NewValue = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
  }

  protected virtual int? GetEmployeeID(PXCache sender, object row)
  {
    return !(this._EmployeeIDField == (Type) null) ? (int?) sender.GetValue(row, this._EmployeeIDField.Name) : new int?();
  }

  protected void SetFieldUpdatedHandler(PXCache sender, Type field)
  {
    if (field == (Type) null)
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(sender.GetItemType(), field.Name, new PXFieldUpdated((object) this, __methodptr(\u003CSetFieldUpdatedHandler\u003Eb__10_0)));
  }
}
