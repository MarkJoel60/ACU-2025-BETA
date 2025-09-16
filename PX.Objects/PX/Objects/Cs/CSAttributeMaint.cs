// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttributeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Caching;
using PX.CS;
using PX.Data;
using PX.Metadata;
using PX.Objects.IN.Matrix.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CS;

public class CSAttributeMaint : PXGraph<CSAttributeMaint, CSAttribute>
{
  public PXSelect<CSAttribute, Where<CSAttribute.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>> Attributes;
  public PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAttribute.attributeID>>>> CurrentAttribute;
  [PXImport(typeof (CSAttribute))]
  public PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<CSAttribute.attributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder, Asc<CSAttributeDetail.valueID>>>> AttributeDetails;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.attributeID, Equal<Required<CSAttribute.attributeID>>>> AttributeGroups;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>> AttributeScreens;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  protected virtual void CSAttribute_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    CSAttribute row = (CSAttribute) e.Row;
    this.SetControlsState(row, sender);
    CSAttributeMaint.ValidateAttributeID(sender, row);
    if (row.ControlType.GetValueOrDefault() != 7)
      return;
    string[] strArray = (string[]) null;
    if (!string.IsNullOrEmpty(row.ObjectName))
    {
      try
      {
        PXCache objCache = ((PXGraph) this).Caches[PXBuildManager.GetType(row.ObjectName, true)];
        strArray = ((IEnumerable<string>) objCache.Fields).Where<string>((Func<string, bool>) (f => objCache.GetBqlField(f) != (Type) null || f.EndsWith("_Attributes", StringComparison.OrdinalIgnoreCase))).Where<string>((Func<string, bool>) (f => !objCache.GetAttributesReadonly(f).OfType<PXDBTimestampAttribute>().Any<PXDBTimestampAttribute>())).Where<string>((Func<string, bool>) (f => !string.IsNullOrEmpty(objCache.GetStateExt((object) null, f) is PXFieldState stateExt ? stateExt.ViewName : (string) null))).Where<string>((Func<string, bool>) (f => f != "CreatedByID" && f != "LastModifiedByID")).ToArray<string>();
        if (strArray.Length == 0)
          strArray = (string[]) null;
      }
      catch
      {
      }
    }
    PXStringListAttribute.SetList<CSAttribute.fieldName>(sender, (object) row, strArray, strArray);
  }

  protected virtual void CSAttributeDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is CSAttributeDetail row))
      return;
    CSAnswers csAnswers = PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>, And<CSAnswers.value, Equal<Required<CSAnswers.value>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.AttributeID,
      (object) row.ValueID
    }));
    CSAttributeGroup csAttributeGroup = PXResultset<CSAttributeGroup>.op_Implicit(((PXSelectBase<CSAttributeGroup>) this.AttributeGroups).SelectWindowed(0, 1, new object[1]
    {
      (object) row.AttributeID
    }));
    if (csAnswers != null && csAttributeGroup != null)
      throw new PXSetPropertyException<CSAttributeDetail.attributeID>("The value ID cannot be deleted because it is in use.", (PXErrorLevel) 5);
  }

  protected virtual void CSAttributeDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is CSAttributeDetail row) || ((PXSelectBase<CSAttribute>) this.CurrentAttribute).Current == null)
      return;
    row.AttributeID = ((PXSelectBase<CSAttribute>) this.CurrentAttribute).Current.AttributeID;
  }

  protected virtual void CSAttribute_ControlType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.SetControlsState(e.Row as CSAttribute, sender);
  }

  protected virtual void CSAttribute_ControlType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CSAttribute row))
      return;
    int? controlType = row.ControlType;
    int? newValue = (int?) e.NewValue;
    int? nullable1 = controlType;
    int? nullable2 = newValue;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      this.CheckIfSettingsCanBeChanged(row.AttributeID);
    if (newValue.GetValueOrDefault() != 6)
      return;
    foreach (PXResult<CSAttributeDetail> pxResult in ((IEnumerable<PXResult<CSAttributeDetail>>) ((PXSelectBase<CSAttributeDetail>) this.AttributeDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<CSAttributeDetail>>())
    {
      CSAttributeDetail csAttributeDetail = PXResult<CSAttributeDetail>.op_Implicit(pxResult);
      if (csAttributeDetail.ValueID.Contains(","))
      {
        ((PXSelectBase) this.AttributeDetails).Cache.RaiseExceptionHandling<CSAttributeDetail.valueID>((object) csAttributeDetail, (object) csAttributeDetail.ValueID, (Exception) new PXSetPropertyException<CSAttributeDetail.valueID>("The Value ID column of the Multi Select Combo attribute type cannot contain the \",\" character.", (PXErrorLevel) 4));
        ((PXSelectBase) this.AttributeDetails).Cache.SetStatus((object) csAttributeDetail, (PXEntryStatus) 6);
      }
    }
  }

  protected virtual void CSAttribute_ObjectName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CSAttribute row) || string.Equals(row.ObjectName, (string) e.NewValue, StringComparison.OrdinalIgnoreCase))
      return;
    this.CheckIfSettingsCanBeChanged(row.AttributeID);
  }

  protected virtual void CSAttribute_ObjectName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CSAttribute row) || string.Equals((string) e.OldValue, row.ObjectName, StringComparison.OrdinalIgnoreCase))
      return;
    sender.SetValueExt<CSAttribute.fieldName>((object) row, (object) null);
  }

  protected virtual void CSAttribute_FieldName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CSAttribute row) || string.Equals(row.FieldName, (string) e.NewValue, StringComparison.OrdinalIgnoreCase))
      return;
    this.CheckIfSettingsCanBeChanged(row.AttributeID);
  }

  private void CheckIfSettingsCanBeChanged(string attributeID)
  {
    PXResultset<CSAttributeGroup> source1 = ((PXSelectBase<CSAttributeGroup>) this.AttributeGroups).Select(new object[1]
    {
      (object) attributeID
    });
    if (((IQueryable<PXResult<CSAttributeGroup>>) source1).Any<PXResult<CSAttributeGroup>>())
    {
      object[] objArray = new object[1];
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      objArray[0] = (object) string.Join(", ", (IEnumerable<string>) ((IQueryable<PXResult<CSAttributeGroup>>) source1).Select<PXResult<CSAttributeGroup>, string>(Expression.Lambda<Func<PXResult<CSAttributeGroup>, string>>((Expression) Expression.Property((Expression) Expression.Call(group, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CSAttributeGroup.get_EntityType))), parameterExpression)));
      throw new PXSetPropertyException("Cannot change the attribute settings because there are entities that already use the attribute: {0}. Remove this attribute from the entities to change the attribute settings.", objArray);
    }
    PXResultset<CSScreenAttribute> source2 = ((PXSelectBase<CSScreenAttribute>) this.AttributeScreens).Select(new object[1]
    {
      (object) attributeID
    });
    if (((IQueryable<PXResult<CSScreenAttribute>>) source2).Any<PXResult<CSScreenAttribute>>())
    {
      object[] objArray = new object[1];
      ParameterExpression instance;
      // ISSUE: method reference
      // ISSUE: method reference
      objArray[0] = (object) string.Join(", ", (IEnumerable<string>) ((IQueryable<PXResult<CSScreenAttribute>>) source2).Select<PXResult<CSScreenAttribute>, string>(Expression.Lambda<Func<PXResult<CSScreenAttribute>, string>>((Expression) Expression.Property((Expression) Expression.Call((Expression) instance, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CSScreenAttribute.get_ScreenID))), instance)));
      throw new PXSetPropertyException("Cannot change the attribute settings because there are forms that already use the attribute as a user-defined field: {0}. Remove this attribute from the forms to change the attribute settings.", objArray);
    }
  }

  protected virtual void _(Events.FieldUpdated<CSAttributeDetail.valueID> e)
  {
    string newValue = (string) e.NewValue;
    string oldValue = (string) ((Events.FieldUpdatedBase<Events.FieldUpdated<CSAttributeDetail.valueID>, object, object>) e).OldValue;
    if (string.IsNullOrWhiteSpace(oldValue) || string.Equals(newValue, oldValue, StringComparison.OrdinalIgnoreCase))
      return;
    this.CheckIfSettingsCanBeChanged(((PXSelectBase<CSAttribute>) this.Attributes).Current.AttributeID);
  }

  protected virtual void CSAttributeDetail_ValueID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    if (string.IsNullOrWhiteSpace(newValue))
    {
      e.NewValue = (object) null;
    }
    else
    {
      if (!(e.Row is CSAttributeDetail row) || newValue.Equals(row.ValueID, StringComparison.OrdinalIgnoreCase))
        return;
      CSAnswers csAnswers = PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>, And<CSAnswers.value, Equal<Required<CSAnswers.value>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) row.AttributeID,
        (object) row.ValueID
      }));
      CSAttributeGroup csAttributeGroup = PXResultset<CSAttributeGroup>.op_Implicit(((PXSelectBase<CSAttributeGroup>) this.AttributeGroups).SelectWindowed(0, 1, new object[1]
      {
        (object) row.AttributeID
      }));
      if (csAnswers != null && csAttributeGroup != null)
        throw new PXSetPropertyException<CSAttributeDetail.valueID>("The value ID cannot be changed because it is in use.", (PXErrorLevel) 4);
    }
  }

  protected virtual void CSAttribute_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CSAttribute row = (CSAttribute) e.Row;
    CSAttribute oldRow = (CSAttribute) e.OldRow;
    if (row.ControlType.GetValueOrDefault() == 1 || oldRow.ControlType.GetValueOrDefault() != 1)
      return;
    row.RegExp = (string) null;
    row.EntryMask = (string) null;
  }

  protected virtual void CSAttribute_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CSAttribute row))
      return;
    if (string.IsNullOrEmpty(row.Description))
    {
      if (sender.RaiseExceptionHandling<CSAttribute.description>(e.Row, (object) row.Description, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[description]"
      })))
        throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "description"
        });
    }
    if (!CSAttributeMaint.ValidateAttributeID(sender, row))
    {
      string str = ((PXFieldState) sender.GetStateExt((object) row, typeof (CSAttribute.attributeID).Name)).DisplayName;
      if (string.IsNullOrEmpty(str))
        str = typeof (CSAttribute.attributeID).Name;
      throw new PXSetPropertyException($"{str}: {PXUIFieldAttribute.GetError<CSAttribute.attributeID>(sender, (object) row)}");
    }
    if (row.ControlType.GetValueOrDefault() != 7)
      return;
    if (string.IsNullOrEmpty(row.ObjectName))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "Schema Object"
      });
    if (string.IsNullOrEmpty(row.FieldName))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "Schema Field"
      });
  }

  protected virtual void CSAttributeDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  protected virtual void CSAttributeDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CSAttributeDetail row = (CSAttributeDetail) e.Row;
    string valueId = row?.ValueID;
    int? controlType = (int?) ((PXSelectBase<CSAttribute>) this.Attributes).Current?.ControlType;
    if ((string.IsNullOrEmpty(valueId) ? 1 : (controlType.GetValueOrDefault() != 6 ? 1 : (!valueId.Contains(",") ? 1 : 0))) == 0)
    {
      sender.RaiseExceptionHandling<CSAttributeDetail.valueID>((object) row, (object) valueId, (Exception) new PXSetPropertyException<CSAttributeDetail.valueID>("The Value ID column of the Multi Select Combo attribute type cannot contain the \",\" character.", (PXErrorLevel) 4));
    }
    else
    {
      CSAttributeDetail original = (CSAttributeDetail) sender.GetOriginal((object) row);
      if (sender.GetStatus((object) row) != 1 || original == null)
        return;
      if (string.Equals(valueId, original.ValueID))
        return;
      try
      {
        this.CheckIfSettingsCanBeChanged(row.AttributeID);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<CSAttributeDetail.valueID>((object) row, (object) valueId, (Exception) ex);
      }
    }
  }

  private static bool ValidateAttributeID(PXCache sender, CSAttribute row)
  {
    if (row == null || string.IsNullOrEmpty(row.AttributeID))
      return true;
    if (char.IsDigit(row.AttributeID[0]))
    {
      PXUIFieldAttribute.SetWarning<CSAttribute.attributeID>(sender, (object) row, "An attribute ID cannot start with a digit.");
      return false;
    }
    if (!row.AttributeID.Contains(" "))
      return true;
    PXUIFieldAttribute.SetWarning<CSAttribute.attributeID>(sender, (object) row, "Attribute IDs cannot contain spaces.");
    return false;
  }

  private void SetControlsState(CSAttribute row, PXCache cache)
  {
    if (row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.AttributeDetails).Cache;
    int? controlType;
    int num1;
    if (row.ControlType.GetValueOrDefault() != 2)
    {
      controlType = row.ControlType;
      num1 = controlType.GetValueOrDefault() == 6 ? 1 : 0;
    }
    else
      num1 = 1;
    cache1.AllowDelete = num1 != 0;
    PXCache cache2 = ((PXSelectBase) this.AttributeDetails).Cache;
    controlType = row.ControlType;
    int num2;
    if (controlType.GetValueOrDefault() != 2)
    {
      controlType = row.ControlType;
      num2 = controlType.GetValueOrDefault() == 6 ? 1 : 0;
    }
    else
      num2 = 1;
    cache2.AllowUpdate = num2 != 0;
    PXCache cache3 = ((PXSelectBase) this.AttributeDetails).Cache;
    controlType = row.ControlType;
    int num3;
    if (controlType.GetValueOrDefault() != 2)
    {
      controlType = row.ControlType;
      num3 = controlType.GetValueOrDefault() == 6 ? 1 : 0;
    }
    else
      num3 = 1;
    cache3.AllowInsert = num3 != 0;
    CSScreenAttribute csScreenAttribute = PXResultset<CSScreenAttribute>.op_Implicit(((PXSelectBase<CSScreenAttribute>) this.AttributeScreens).SelectWindowed(0, 1, new object[1]
    {
      (object) row.AttributeID
    }));
    CSAnswers csAnswers = PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.AttributeID
    }));
    CSAttributeGroup csAttributeGroup = (CSAttributeGroup) null;
    if (csAnswers == null)
      csAttributeGroup = PXResultset<CSAttributeGroup>.op_Implicit(((PXSelectBase<CSAttributeGroup>) this.AttributeGroups).SelectWindowed(0, 1, new object[1]
      {
        (object) row.AttributeID
      }));
    bool flag = csScreenAttribute == null && csAnswers == null && csAttributeGroup == null;
    PXUIFieldAttribute.SetEnabled<CSAttribute.controlType>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<CSAttribute.objectName>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<CSAttribute.fieldName>(cache, (object) row, flag);
    cache.AllowDelete = flag;
    PXCache pxCache1 = cache;
    CSAttribute csAttribute1 = row;
    controlType = row.ControlType;
    int num4 = controlType.GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CSAttribute.entryMask>(pxCache1, (object) csAttribute1, num4 != 0);
    PXCache pxCache2 = cache;
    CSAttribute csAttribute2 = row;
    controlType = row.ControlType;
    int num5 = controlType.GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CSAttribute.regExp>(pxCache2, (object) csAttribute2, num5 != 0);
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<CSAttribute>) this.Attributes).Current != null)
    {
      if (!PXDBLocalizableStringAttribute.IsEnabled)
      {
        string list = ((PXSelectBase<CSAttribute>) this.Attributes).Current.List;
        ((PXSelectBase<CSAttribute>) this.Attributes).Current.List = (string) null;
        foreach (PXResult<CSAttributeDetail> pxResult in ((PXSelectBase<CSAttributeDetail>) this.AttributeDetails).Select(Array.Empty<object>()))
        {
          CSAttributeDetail csAttributeDetail = PXResult<CSAttributeDetail>.op_Implicit(pxResult);
          if (!string.IsNullOrEmpty(csAttributeDetail.ValueID))
          {
            if (((PXSelectBase<CSAttribute>) this.Attributes).Current.List == null)
              ((PXSelectBase<CSAttribute>) this.Attributes).Current.List = csAttributeDetail.ValueID + ((PXGraph) this).SqlDialect.WildcardFieldSeparatorChar.ToString() + csAttributeDetail.Description;
            else
              ((PXSelectBase<CSAttribute>) this.Attributes).Current.List = $"{((PXSelectBase<CSAttribute>) this.Attributes).Current.List}\t{csAttributeDetail.ValueID}{((PXGraph) this).SqlDialect.WildcardFieldSeparatorChar.ToString()}{csAttributeDetail.Description}";
          }
        }
        if (!string.Equals(list, ((PXSelectBase<CSAttribute>) this.Attributes).Current.List) && ((PXSelectBase) this.Attributes).Cache.GetStatus((object) ((PXSelectBase<CSAttribute>) this.Attributes).Current) == null)
          ((PXSelectBase) this.Attributes).Cache.SetStatus((object) ((PXSelectBase<CSAttribute>) this.Attributes).Current, (PXEntryStatus) 1);
      }
      else
      {
        bool isImport = ((PXGraph) this).IsImport;
        bool isExport = ((PXGraph) this).IsExport;
        bool copyPasteContext = ((PXGraph) this).IsCopyPasteContext;
        ((PXGraph) this).IsImport = false;
        ((PXGraph) this).IsExport = false;
        ((PXGraph) this).IsCopyPasteContext = false;
        try
        {
          if (((PXSelectBase) this.Attributes).Cache.GetValueExt((object) null, "ListTranslations") is string[] valueExt1)
          {
            string[] strArray = new string[valueExt1.Length];
            foreach (PXResult<CSAttributeDetail> pxResult in ((PXSelectBase<CSAttributeDetail>) this.AttributeDetails).Select(Array.Empty<object>()))
            {
              CSAttributeDetail csAttributeDetail = PXResult<CSAttributeDetail>.op_Implicit(pxResult);
              if (!string.IsNullOrEmpty(csAttributeDetail.ValueID))
              {
                string[] valueExt = ((PXSelectBase) this.AttributeDetails).Cache.GetValueExt((object) csAttributeDetail, "DescriptionTranslations") as string[];
                for (int index = 0; index < strArray.Length; ++index)
                {
                  strArray[index] = strArray[index] != null ? $"{strArray[index]}\t{csAttributeDetail.ValueID}" : csAttributeDetail.ValueID;
                  string str = csAttributeDetail.Description;
                  if (valueExt != null && ((IEnumerable<string>) valueExt).Any<string>((Func<string, bool>) (_ => !string.IsNullOrEmpty(_))))
                    str = index >= valueExt.Length || string.IsNullOrEmpty(valueExt[index]) ? ((IEnumerable<string>) valueExt).FirstOrDefault<string>((Func<string, bool>) (_ => !string.IsNullOrEmpty(_))) : valueExt[index];
                  strArray[index] = strArray[index] + ((PXGraph) this).SqlDialect.WildcardFieldSeparatorChar.ToString() + str;
                }
              }
            }
            ((PXSelectBase) this.Attributes).Cache.SetValueExt((object) ((PXSelectBase<CSAttribute>) this.Attributes).Current, "ListTranslations", (object) strArray);
          }
        }
        finally
        {
          ((PXGraph) this).IsImport = isImport;
          ((PXGraph) this).IsExport = isExport;
          ((PXGraph) this).IsCopyPasteContext = copyPasteContext;
        }
      }
    }
    int num = ((PXSelectBase) this.Attributes).Cache.Updated.Cast<CSAttribute>().Any<CSAttribute>() ? 1 : (((PXSelectBase) this.Attributes).Cache.Deleted.Cast<CSAttribute>().Any<CSAttribute>() ? 1 : 0);
    ((PXGraph) this).Persist();
    if (num == 0)
      return;
    ((ICacheControl) this.ScreenInfoCacheControl).InvalidateCache();
  }
}
