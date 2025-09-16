// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAttributeList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Common.Collection;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.SP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class CRAttributeList<TEntity> : PXSelectBase<
#nullable disable
CSAnswers>
{
  private readonly EntityHelper _helper;
  private const string CbApiValueFieldName = "Value$value";
  private const string CbApiAttributeIDFieldName = "AttributeID$value";

  public virtual bool ForceValidationInUnattendedMode { get; set; }

  public CRAttributeList(PXGraph graph)
  {
    ((PXSelectBase) this)._Graph = graph;
    this._helper = new EntityHelper(graph);
    PXView pxView;
    if (!graph.IsExport)
    {
      PXGraph pxGraph = graph;
      Select3<CSAnswers, OrderBy<Asc<CSAnswers.order>>> select3 = new Select3<CSAnswers, OrderBy<Asc<CSAnswers.order>>>();
      CRAttributeList<TEntity> crAttributeList = this;
      // ISSUE: virtual method pointer
      PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) crAttributeList, __vmethodptr(crAttributeList, SelectDelegate));
      pxView = new PXView(pxGraph, false, (BqlCommand) select3, (Delegate) pxSelectDelegate);
    }
    else
      pxView = this.GetExportOptimizedView();
    ((PXSelectBase) this).View = pxView;
    PXDependToCacheAttribute.AddDependencies(((PXSelectBase) this).View, new System.Type[1]
    {
      typeof (TEntity)
    });
    GraphHelper.EnsureCachePersistence(((PXSelectBase) this)._Graph, typeof (CSAnswers));
    PXDBAttributeAttribute.Activate(((PXSelectBase) this)._Graph.Caches[typeof (TEntity)]);
    PXGraph.FieldUpdatingEvents fieldUpdating = ((PXSelectBase) this)._Graph.FieldUpdating;
    CRAttributeList<TEntity> crAttributeList1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) crAttributeList1, __vmethodptr(crAttributeList1, FieldUpdatingHandler));
    fieldUpdating.AddHandler<CSAnswers.value>(pxFieldUpdating);
    PXGraph.FieldSelectingEvents fieldSelecting1 = ((PXSelectBase) this)._Graph.FieldSelecting;
    CRAttributeList<TEntity> crAttributeList2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) crAttributeList2, __vmethodptr(crAttributeList2, FieldSelectingHandler));
    fieldSelecting1.AddHandler<CSAnswers.value>(pxFieldSelecting1);
    PXGraph.FieldSelectingEvents fieldSelecting2 = ((PXSelectBase) this)._Graph.FieldSelecting;
    CRAttributeList<TEntity> crAttributeList3 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) crAttributeList3, __vmethodptr(crAttributeList3, IsRequiredSelectingHandler));
    fieldSelecting2.AddHandler<CSAnswers.isRequired>(pxFieldSelecting2);
    PXGraph.FieldSelectingEvents fieldSelecting3 = ((PXSelectBase) this)._Graph.FieldSelecting;
    CRAttributeList<TEntity> crAttributeList4 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting3 = new PXFieldSelecting((object) crAttributeList4, __vmethodptr(crAttributeList4, AttributeCategorySelectingHandler));
    fieldSelecting3.AddHandler<CSAnswers.attributeCategory>(pxFieldSelecting3);
    PXGraph.FieldSelectingEvents fieldSelecting4 = ((PXSelectBase) this)._Graph.FieldSelecting;
    CRAttributeList<TEntity> crAttributeList5 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting4 = new PXFieldSelecting((object) crAttributeList5, __vmethodptr(crAttributeList5, AttrFieldSelectingHandler));
    fieldSelecting4.AddHandler<CSAnswers.attributeID>(pxFieldSelecting4);
    PXGraph.RowPersistingEvents rowPersisting1 = ((PXSelectBase) this)._Graph.RowPersisting;
    CRAttributeList<TEntity> crAttributeList6 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting1 = new PXRowPersisting((object) crAttributeList6, __vmethodptr(crAttributeList6, RowPersistingHandler));
    rowPersisting1.AddHandler<CSAnswers>(pxRowPersisting1);
    PXGraph.RowPersistingEvents rowPersisting2 = ((PXSelectBase) this)._Graph.RowPersisting;
    CRAttributeList<TEntity> crAttributeList7 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting2 = new PXRowPersisting((object) crAttributeList7, __vmethodptr(crAttributeList7, ReferenceRowPersistingHandler));
    rowPersisting2.AddHandler<TEntity>(pxRowPersisting2);
    PXGraph.RowUpdatingEvents rowUpdating = ((PXSelectBase) this)._Graph.RowUpdating;
    CRAttributeList<TEntity> crAttributeList8 = this;
    // ISSUE: virtual method pointer
    PXRowUpdating pxRowUpdating = new PXRowUpdating((object) crAttributeList8, __vmethodptr(crAttributeList8, ReferenceRowUpdatingHandler));
    rowUpdating.AddHandler<TEntity>(pxRowUpdating);
    PXGraph.RowDeletedEvents rowDeleted = ((PXSelectBase) this)._Graph.RowDeleted;
    CRAttributeList<TEntity> crAttributeList9 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) crAttributeList9, __vmethodptr(crAttributeList9, ReferenceRowDeletedHandler));
    rowDeleted.AddHandler<TEntity>(pxRowDeleted);
    PXGraph.RowInsertedEvents rowInserted = ((PXSelectBase) this)._Graph.RowInserted;
    CRAttributeList<TEntity> crAttributeList10 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) crAttributeList10, __vmethodptr(crAttributeList10, RowInsertedHandler));
    rowInserted.AddHandler<TEntity>(pxRowInserted);
    ((PXCache) GraphHelper.Caches<CSAnswers>(((PXSelectBase) this)._Graph)).Fields.Add("Value$value");
    ((PXCache) GraphHelper.Caches<CSAnswers>(((PXSelectBase) this)._Graph)).Fields.Add("AttributeID$value");
    PXGraph.FieldSelectingEvents fieldSelecting5 = ((PXSelectBase) this)._Graph.FieldSelecting;
    System.Type type1 = typeof (CSAnswers);
    CRAttributeList<TEntity> crAttributeList11 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting5 = new PXFieldSelecting((object) crAttributeList11, __vmethodptr(crAttributeList11, CbApiValueFieldSelectingHandler));
    fieldSelecting5.AddHandler(type1, "Value$value", pxFieldSelecting5);
    PXGraph.FieldSelectingEvents fieldSelecting6 = ((PXSelectBase) this)._Graph.FieldSelecting;
    System.Type type2 = typeof (CSAnswers);
    CRAttributeList<TEntity> crAttributeList12 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting6 = new PXFieldSelecting((object) crAttributeList12, __vmethodptr(crAttributeList12, CbApiAttributeIdFieldSelectingHandler));
    fieldSelecting6.AddHandler(type2, "AttributeID$value", pxFieldSelecting6);
  }

  private PXView GetExportOptimizedView()
  {
    System.Type classIdField = this.GetClassIdField(((PXSelectBase) this)._Graph.Caches[typeof (TEntity)].CreateInstance());
    System.Type nestedType = typeof (TEntity).GetNestedType("noteID");
    return new PXView(((PXSelectBase) this)._Graph, true, BqlTemplate.OfCommand<Select2<CSAnswers, InnerJoin<CSAttribute, On<CSAnswers.attributeID, Equal<CSAttribute.attributeID>>, InnerJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityType, Equal<CRAttributeList<TEntity>.TypeNameConst>, And<CSAttributeGroup.entityClassID, Equal<Current<BqlPlaceholder.A>>, And<CSAnswers.refNoteID, Equal<Current<BqlPlaceholder.B>>>>>>>>.Replace<BqlPlaceholder.A>(classIdField).Replace<BqlPlaceholder.B>(nestedType).ToCommand());
  }

  protected virtual IEnumerable SelectDelegate()
  {
    return (IEnumerable) this.SelectInternal(((PXSelectBase) this)._Graph.Caches[typeof (TEntity)].Current);
  }

  protected Guid? GetNoteId(object row) => this._helper.GetEntityNoteID(row);

  protected System.Type GetClassIdField(object row)
  {
    if (row == null)
      return (System.Type) null;
    return ((PXSelectBase) this)._Graph.Caches[row.GetType()].GetAttributes(row, (string) null).OfType<CRAttributesFieldAttribute>().FirstOrDefault<CRAttributesFieldAttribute>()?.ClassIdField;
  }

  protected System.Type GetEntityTypeFromAttribute(object row)
  {
    System.Type classIdField = this.GetClassIdField(row);
    return classIdField == (System.Type) null ? (System.Type) null : classIdField.DeclaringType;
  }

  protected string GetClassId(object row)
  {
    System.Type classIdField = this.GetClassIdField(row);
    if (classIdField == (System.Type) null)
      return (string) null;
    return ((PXSelectBase) this)._Graph.Caches[row.GetType()].GetValue(row, classIdField.Name)?.ToString();
  }

  protected virtual PXCache GetAnswers() => ((PXSelectBase) this)._Graph.Caches[typeof (CSAnswers)];

  private (string id, string description) GetClassIdAndDescription(object row)
  {
    System.Type classIdField = this.GetClassIdField(row);
    if (classIdField == (System.Type) null)
      return ();
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[row.GetType()];
    string str1 = cach.GetValue(row, classIdField.Name)?.ToString();
    string str2 = cach.GetStateExt(row, classIdField.Name) is PXFieldState stateExt ? stateExt.Value?.ToString().Trim() : (string) null;
    return (str1, str2 ?? str1);
  }

  [PXInternalUseOnly]
  public IEnumerable<CSAnswers> SelectInternal(object row)
  {
    return this.SelectInternal(((PXSelectBase) this)._Graph, row);
  }

  [PXInternalUseOnly]
  public IEnumerable<CSAnswers> SelectInternal(PXGraph graph, object row)
  {
    CRAttributeList<TEntity> crAttributeList = this;
    if (row != null)
    {
      Guid? noteId = crAttributeList.GetNoteId(row);
      if (noteId.HasValue)
      {
        PXCache cach = graph.Caches[typeof (CSAnswers)];
        PXEntryStatus status = graph.Caches[row.GetType()].GetStatus(row);
        List<CSAnswers> list1;
        if (status == 2 || status == 4)
          list1 = cach.Inserted.Cast<CSAnswers>().Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
          {
            Guid? refNoteId = x.RefNoteID;
            Guid? nullable = noteId;
            return (refNoteId.HasValue == nullable.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && !"~MX~DUMMY~".Equals(x.AttributeID, StringComparison.OrdinalIgnoreCase);
          })).ToList<CSAnswers>();
        else
          list1 = PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>, And<CSAnswers.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>>>.Config>.Select(graph, new object[1]
          {
            (object) noteId
          }).FirstTableItems.ToList<CSAnswers>();
        string classId = crAttributeList.GetClassId(row);
        CRAttribute.ClassAttributeList classAttributeList = new CRAttribute.ClassAttributeList();
        if (classId != null)
        {
          classAttributeList = CRAttribute.EntityAttributes(crAttributeList.GetEntityTypeFromAttribute(row), classId);
          ((IEnumerable<CRAttribute.AttributeExt>) classAttributeList).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (_ => _.NotInClass)).Select<CRAttribute.AttributeExt, string>((Func<CRAttribute.AttributeExt, string>) (_ => _.ID)).ToList<string>().ForEach((Action<string>) (_ => ((KList<string, CRAttribute.AttributeExt>) classAttributeList).Remove(_)));
        }
        if (graph.IsImport && ((IEnumerable<string>) PXView.SortColumns).Any<string>() && ((IEnumerable<object>) PXView.Searches).Any<object>())
        {
          int index = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (x => x.Equals(typeof (CSAnswers.attributeID).Name, StringComparison.OrdinalIgnoreCase)));
          if (index >= 0 && index < PXView.Searches.Length)
          {
            object search = PXView.Searches[index];
            if (search != null)
            {
              CRAttribute.Attribute attr = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[search.ToString()] ?? ((KList<string, CRAttribute.Attribute>) CRAttribute.AttributesByDescr)[search.ToString()];
              if (attr == null)
                throw new PXSetPropertyException(crAttributeList.GetSelectInternalExceptionMessage(), new object[1]
                {
                  (object) search.ToString()
                });
              if (((KList<string, CRAttribute.AttributeExt>) classAttributeList)[attr.ID] == null)
                ((KList<string, CRAttribute.AttributeExt>) classAttributeList).Add(new CRAttribute.AttributeExt(attr, (string) null, false, true)
                {
                  NotInClass = true
                });
            }
          }
        }
        if (list1.Count != 0 || ((KList<string, CRAttribute.AttributeExt>) classAttributeList).Count != 0)
        {
          List<string> list2 = ((IEnumerable<CRAttribute.AttributeExt>) classAttributeList).Select<CRAttribute.AttributeExt, string>((Func<CRAttribute.AttributeExt, string>) (x => x.ID)).Except<string>(list1.Select<CSAnswers, string>((Func<CSAnswers, string>) (x => x.AttributeID))).ToList<string>();
          List<string> attributeIdListIntersection = ((IEnumerable<CRAttribute.AttributeExt>) classAttributeList).Select<CRAttribute.AttributeExt, string>((Func<CRAttribute.AttributeExt, string>) (x => x.ID)).Intersect<string>(list1.Select<CSAnswers, string>((Func<CSAnswers, string>) (x => x.AttributeID))).Distinct<string>().ToList<string>();
          bool isDirty = cach.IsDirty;
          List<CSAnswers> source = new List<CSAnswers>();
          foreach (string str in list2)
          {
            CRAttribute.AttributeExt attr = ((KList<string, CRAttribute.AttributeExt>) classAttributeList)[str];
            if ((!PortalHelper.IsPortalContext((PortalContexts) 3) || !attr.IsInternal || !string.IsNullOrEmpty(attr.DefaultValue)) && attr.IsActive)
            {
              CSAnswers instance = (CSAnswers) cach.CreateInstance();
              instance.AttributeID = attr.ID;
              instance.RefNoteID = noteId;
              instance.Value = crAttributeList.GetDefaultAnswerValue(attr);
              if (attr.ControlType.GetValueOrDefault() == 4)
              {
                bool result;
                if (bool.TryParse(instance.Value, out result))
                  instance.Value = Convert.ToInt32(result).ToString((IFormatProvider) CultureInfo.InvariantCulture);
                else if (instance.Value == null)
                  instance.Value = 0.ToString();
              }
              instance.IsRequired = new bool?(attr.Required);
              instance.NotInClass = new bool?(attr.NotInClass);
              Dictionary<string, object> dictionary = new Dictionary<string, object>();
              foreach (string key in cach.Keys.ToArray())
                dictionary[key] = cach.GetValue((object) instance, key);
              cach.Locate((IDictionary) dictionary);
              CSAnswers csAnswers = (CSAnswers) (cach.Locate((object) instance) ?? cach.Insert((object) instance));
              if (!PortalHelper.IsPortalContext((PortalContexts) 3) || !attr.IsInternal)
                source.Add(csAnswers);
            }
          }
          foreach (CSAnswers csAnswers in list1.Where<CSAnswers>((Func<CSAnswers, bool>) (x => attributeIdListIntersection.Contains(x.AttributeID))).ToList<CSAnswers>())
          {
            CRAttribute.AttributeExt attributeExt = ((KList<string, CRAttribute.AttributeExt>) classAttributeList)[csAnswers.AttributeID];
            if ((!PortalHelper.IsPortalContext((PortalContexts) 3) || !attributeExt.IsInternal) && attributeExt.IsActive)
            {
              if (csAnswers.Value == null && attributeExt.ControlType.GetValueOrDefault() == 4)
                csAnswers.Value = bool.FalseString;
              bool? isRequired = csAnswers.IsRequired;
              if (isRequired.HasValue)
              {
                int num1 = attributeExt.Required ? 1 : 0;
                isRequired = csAnswers.IsRequired;
                int num2 = isRequired.GetValueOrDefault() ? 1 : 0;
                if (num1 == num2 & isRequired.HasValue)
                  goto label_46;
              }
              csAnswers.IsRequired = new bool?(attributeExt.Required);
              int num;
              if (((PXSelectBase) crAttributeList).View.Cache.GetValueExt<CSAnswers.isRequired>((object) csAnswers) is PXFieldState valueExt)
              {
                isRequired = (bool?) valueExt.Value;
                num = isRequired.GetValueOrDefault() ? 1 : 0;
              }
              else
                num = 0;
              bool flag = num != 0;
              csAnswers.IsRequired = new bool?(attributeExt.Required | flag);
label_46:
              source.Add(csAnswers);
            }
          }
          cach.IsDirty = isDirty;
          List<CSAnswers> list3 = source.OrderBy<CSAnswers, int>((Func<CSAnswers, int>) (x => !((KList<string, CRAttribute.AttributeExt>) classAttributeList).Contains(x.AttributeID) ? (int) x.Order.GetValueOrDefault() : ((KList<string, CRAttribute.AttributeExt>) classAttributeList).IndexOf(x.AttributeID))).ThenBy<CSAnswers, string>((Func<CSAnswers, string>) (x => x.AttributeID)).ToList<CSAnswers>();
          short attributeOrder = 0;
          foreach (CSAnswers csAnswers in list3)
          {
            csAnswers.Order = new short?(attributeOrder++);
            yield return csAnswers;
          }
        }
      }
    }
  }

  protected virtual void FieldUpdatingHandler(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is CSAnswers row) || row.AttributeID == null)
      return;
    CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[row.AttributeID];
    if (attribute == null)
      return;
    if (e.NewValue is DateTime newValue2 && attribute.ControlType.GetValueOrDefault() == 5)
      e.NewValue = (object) newValue2.ToString("yyyy-MM-dd HH:mm:ss.fff", (IFormatProvider) CultureInfo.InvariantCulture);
    else if (e.NewValue is Decimal newValue1 && attribute.ControlType.GetValueOrDefault() == 8)
    {
      e.NewValue = (object) Math.Round(newValue1, attribute.Precision, MidpointRounding.AwayFromZero);
    }
    else
    {
      string newValue = e.NewValue as string;
      if (newValue == null)
        return;
      if (row.NotInClass.GetValueOrDefault() && e.NewValue != null)
      {
        string description = this.GetClassIdAndDescription(new EntityHelper(((PXSelectBase) this)._Graph).GetEntityRow(typeof (TEntity), row.RefNoteID)).description;
        sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The {1} class does not have the {0} attribute. Make sure the attribute name is correct or consider adding the attribute to the class.", new object[2]
        {
          (object) row.AttributeID,
          (object) description
        }));
      }
      int? controlType = attribute.ControlType;
      if (!controlType.HasValue)
        return;
      switch (controlType.GetValueOrDefault())
      {
        case 2:
          CRAttribute.AttributeValue attributeValue = attribute.Values.Find((Predicate<CRAttribute.AttributeValue>) (a => string.Equals(a.ValueID.TrimEnd(), newValue.TrimEnd(), StringComparison.OrdinalIgnoreCase))) ?? attribute.Values.Find((Predicate<CRAttribute.AttributeValue>) (a => string.Equals(a.Description, newValue, StringComparison.OrdinalIgnoreCase)));
          if (attributeValue != null)
          {
            e.NewValue = (object) attributeValue.ValueID;
            break;
          }
          sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The specified value is not valid for the {0} attribute that has the Combo type.", new object[1]
          {
            (object) row.AttributeID
          }));
          break;
        case 4:
          bool result1;
          if (bool.TryParse(newValue, out result1))
          {
            e.NewValue = (object) Convert.ToInt32(result1).ToString((IFormatProvider) CultureInfo.InvariantCulture);
            break;
          }
          if (!(newValue != "0") || !(newValue != "1"))
            break;
          sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The specified value is not valid for the {0} attribute that has the Checkbox type because this value cannot be converted to a boolean value.", new object[1]
          {
            (object) row.AttributeID
          }));
          break;
        case 5:
          if (sender.Graph.IsMobile)
            newValue = newValue.Replace("Z", "");
          DateTime result2;
          if (DateTime.TryParse(newValue, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result2))
          {
            e.NewValue = (object) result2.ToString("yyyy-MM-dd HH:mm:ss.fff", (IFormatProvider) CultureInfo.InvariantCulture);
            break;
          }
          sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The specified value is not valid for the {0} attribute that has the Datetime type because this value cannot be converted to a DateTime value.", new object[1]
          {
            (object) row.AttributeID
          }));
          break;
        case 6:
          if (Str.IsNullOrEmpty(newValue))
            break;
          string str1 = newValue;
          char[] separator = new char[1]{ ',' };
          foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            string part = str2;
            if (attribute.Values.Find((Predicate<CRAttribute.AttributeValue>) (a => string.Equals(a.ValueID, part, StringComparison.OrdinalIgnoreCase))) == null)
            {
              sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("One of the specified values is not valid for the {0} attribute that has the Multi Select Combo type. Note that the Multi Select Combo type supports identifiers and does not support descriptions.", new object[1]
              {
                (object) row.AttributeID
              }));
              break;
            }
          }
          break;
        case 8:
          Decimal result3;
          if (Decimal.TryParse(newValue, out result3))
          {
            e.NewValue = (object) Math.Round(result3, attribute.Precision, MidpointRounding.AwayFromZero);
            break;
          }
          sender.RaiseExceptionHandling<CSAnswers.value>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The specified value is not valid for the {0} attribute that has the Number type because this value cannot be converted to a decimal value.", new object[1]
          {
            (object) row.AttributeID
          }));
          break;
      }
    }
  }

  protected virtual void FieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    CSAnswers row = (CSAnswers) e.Row;
    if (row == null)
      return;
    KeyValueHelper.Attribute platformAttribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[row.AttributeID]?.ToPlatformAttribute();
    e.ReturnState = (object) KeyValueHelper.MakeFieldState(platformAttribute, "value", e.ReturnState, new int?(row.IsRequired.GetValueOrDefault() ? 1 : -1), (string) null, row.Value);
    if (platformAttribute != null && platformAttribute.ControlType == 6 && sender.Graph.IsContractBasedAPI && e.ReturnValue is string returnValue1)
    {
      PXStringState strState = e.ReturnState as PXStringState;
      if (strState != null)
      {
        PXFieldSelectingEventArgs selectingEventArgs = e;
        char[] chArray = new char[1]{ ',' };
        string str = string.Join(", ", ((IEnumerable<string>) returnValue1.Split(chArray)).Select<string, string>((Func<string, string>) (i =>
        {
          int index = Array.IndexOf<string>(strState.AllowedValues, i.Trim());
          return index >= 0 ? strState.AllowedLabels[index] : i;
        })));
        selectingEventArgs.ReturnValue = (object) str;
        goto label_7;
      }
    }
    int result;
    if (platformAttribute != null && platformAttribute.ControlType == 4 && e.ReturnValue is string returnValue2 && int.TryParse(returnValue2, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      e.ReturnValue = (object) (result != 0);
label_7:
    if (!(e.ReturnState is PXFieldState))
      return;
    PXFieldState returnState = (PXFieldState) e.ReturnState;
    IPXInterfaceField ipxInterfaceField = sender.GetAttributes((object) row, typeof (CSAnswers.value).Name).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>();
    if (ipxInterfaceField != null && ipxInterfaceField.ErrorLevel != null && !string.IsNullOrEmpty(ipxInterfaceField.ErrorText))
    {
      returnState.Error = ipxInterfaceField.ErrorText;
      returnState.ErrorLevel = ipxInterfaceField.ErrorLevel;
    }
    string str1 = sender.GetValueExt<CSAnswers.attributeCategory>((object) row) is PXFieldState valueExt ? (string) valueExt.Value : (string) (object) null;
    returnState.Enabled = str1 != "V";
    if (!((PXSelectBase) this)._Graph.IsContractBasedAPI)
      return;
    returnState.ErrorLevel = (PXErrorLevel) 0;
    returnState.Error = (string) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  internal virtual void CbApiValueFieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CSAnswers row))
      return;
    IPXInterfaceField ipxInterfaceField = (IPXInterfaceField) sender.GetAttributes<CSAnswers.value>((object) row).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    if (ipxInterfaceField != null && ipxInterfaceField.ErrorText != null)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object errorValue = ipxInterfaceField.ErrorValue;
      System.Type type = typeof (string);
      string errorText = ipxInterfaceField.ErrorText;
      bool? nullable1 = new bool?();
      bool? nullable2 = new bool?();
      int? nullable3 = new int?();
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      string str = errorText;
      bool? nullable6 = new bool?();
      bool? nullable7 = new bool?();
      bool? nullable8 = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance(errorValue, type, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, (string) null, (string) null, (string) null, str, (PXErrorLevel) 4, nullable6, nullable7, nullable8, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
    }
    e.ReturnValue = (object) row.Value;
  }

  internal virtual void CbApiAttributeIdFieldSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CSAnswers row))
      return;
    e.ReturnValue = (object) row.AttributeID;
  }

  protected virtual void AttrFieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<CSAnswers.attributeID>(sender, e.Row, false);
  }

  protected virtual void IsRequiredSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    CSAnswers row = e.Row as CSAnswers;
    object current = sender.Graph.Caches[typeof (TEntity)].Current;
    if (row == null || current == null)
      return;
    Guid? noteId = this.GetNoteId(current);
    if (e.ReturnValue != null)
      return;
    Guid? refNoteId = row.RefNoteID;
    Guid? nullable = noteId;
    if ((refNoteId.HasValue == nullable.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      return;
    if (sender.Graph.IsImport)
    {
      e.ReturnValue = (object) false;
    }
    else
    {
      string classId = this.GetClassId(current);
      CRAttribute.AttributeExt entityAttribute = ((KList<string, CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(current), classId))[row.AttributeID];
      if (entityAttribute == null)
        e.ReturnValue = (object) false;
      else if (PortalHelper.IsPortalContext((PortalContexts) 3) && entityAttribute.IsInternal)
        e.ReturnValue = (object) false;
      else
        e.ReturnValue = (object) entityAttribute.Required;
    }
  }

  protected virtual void AttributeCategorySelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CSAnswers row = e.Row as CSAnswers;
    object current = sender.Graph.Caches[typeof (TEntity)].Current;
    if (row == null || current == null)
      return;
    Guid? noteId = this.GetNoteId(current);
    if (e.ReturnValue != null)
      return;
    Guid? refNoteId = row.RefNoteID;
    Guid? nullable = noteId;
    if ((refNoteId.HasValue == nullable.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      return;
    string classId = this.GetClassId(current);
    CRAttribute.AttributeExt entityAttribute = ((KList<string, CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(current), classId))[row.AttributeID];
    e.ReturnValue = (object) (entityAttribute?.AttributeCategory ?? "A");
  }

  protected virtual void RowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2 && e.Operation != 1 || !(e.Row is CSAnswers row))
      return;
    if (!row.RefNoteID.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
      this.RowPersistDeleted(sender, (object) row);
    }
    else
    {
      if (!string.IsNullOrEmpty(row.Value))
        return;
      bool? nullable = row.IsActive;
      if (nullable.GetValueOrDefault())
      {
        CRAttribute.Attribute attribute;
        ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes).TryGetValue(row.AttributeID, ref attribute);
        string str = PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
        {
          (object) sender.GetStateExt<CSAnswers.value>((object) null).With<object, PXFieldState>((Func<object, PXFieldState>) (_ => _ as PXFieldState)).With<PXFieldState, string>((Func<PXFieldState, string>) (_ => _.DisplayName))
        });
        nullable = row.IsRequired;
        if (nullable.GetValueOrDefault())
        {
          if (sender.RaiseExceptionHandling<CSAnswers.value>(e.Row, (object) row.Value, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 5, new object[1]
          {
            (object) typeof (CSAnswers.value).Name
          })))
            goto label_9;
        }
        if (!EnumerableExtensions.IsIn<int?>(attribute.ControlType, new int?(2), new int?(6)) || !((PXSelectBase) this)._Graph.IsContractBasedAPI || row.Value == null)
        {
          ((CancelEventArgs) e).Cancel = true;
          if (sender.GetStatus((object) row) == 2)
            return;
          this.RowPersistDeleted(sender, (object) row);
          return;
        }
label_9:
        throw new PXRowPersistingException(typeof (CSAnswers.value).Name, (object) row.Value, str, new object[1]
        {
          (object) typeof (CSAnswers.value).Name
        });
      }
    }
  }

  protected virtual void RowPersistDeleted(PXCache cache, object row)
  {
    try
    {
      cache.PersistDeleted(row);
      cache.SetStatus(row, (PXEntryStatus) 4);
    }
    catch (PXLockViolationException ex)
    {
    }
    cache.ResetPersisted(row);
  }

  protected virtual void ReferenceRowDeletedHandler(PXCache sender, PXRowDeletedEventArgs e)
  {
    object row = e.Row;
    if (row == null)
      return;
    Guid? noteId = this.GetNoteId(row);
    if (!noteId.HasValue)
      return;
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (CSAnswers)];
    PXEntryStatus status = ((PXSelectBase) this)._Graph.Caches[row.GetType()].GetStatus(row);
    List<CSAnswers> list;
    if (status == 2 || status == 4)
      list = cach.Inserted.Cast<CSAnswers>().Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
      {
        Guid? refNoteId = x.RefNoteID;
        Guid? nullable = noteId;
        if (refNoteId.HasValue != nullable.HasValue)
          return false;
        return !refNoteId.HasValue || refNoteId.GetValueOrDefault() == nullable.GetValueOrDefault();
      })).ToList<CSAnswers>();
    else
      list = PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
      {
        (object) noteId
      }).FirstTableItems.ToList<CSAnswers>();
    foreach (object obj in list)
      ((PXSelectBase) this).Cache.Delete(obj);
  }

  protected virtual void ReferenceRowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
  {
    object row = e.Row;
    if (row == null)
      return;
    PXCache answers = this.GetAnswers();
    string classId = this.GetClassId(row);
    CRAttribute.ClassAttributeList classAttributeList = new CRAttribute.ClassAttributeList();
    if (classId != null)
    {
      classAttributeList = CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(row), classId);
      ((IEnumerable<CRAttribute.AttributeExt>) classAttributeList).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (_ => _.NotInClass)).Select<CRAttribute.AttributeExt, string>((Func<CRAttribute.AttributeExt, string>) (_ => _.ID)).ToList<string>().ForEach((Action<string>) (_ => ((KList<string, CRAttribute.AttributeExt>) classAttributeList).Remove(_)));
    }
    List<string> source = new List<string>();
    Guid? noteID = (Guid?) sender.GetValue(row, "NoteID");
    foreach (CSAnswers csAnswers in answers.Cached.Cast<CSAnswers>().Where<CSAnswers>((Func<CSAnswers, bool>) (x => x.RefNoteID.Equals((object) noteID))))
    {
      bool? isRequired = csAnswers.IsRequired;
      if (!isRequired.HasValue && ((PXSelectBase) this).View.Cache.GetValueExt<CSAnswers.isRequired>((object) csAnswers) is PXFieldState valueExt)
        csAnswers.IsRequired = valueExt.Value as bool?;
      if (e.Operation == 3)
        answers.Delete((object) csAnswers);
      else if (string.IsNullOrEmpty(csAnswers.Value))
      {
        isRequired = csAnswers.IsRequired;
        CRAttribute.AttributeExt attributeExt;
        if (isRequired.GetValueOrDefault() && (!((PXSelectBase) this)._Graph.UnattendedMode || this.ForceValidationInUnattendedMode) && (!((KList<string, CRAttribute.AttributeExt>) classAttributeList).TryGetValue(csAnswers.AttributeID, ref attributeExt) || attributeExt.IsActive))
        {
          string str1 = "";
          CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[csAnswers.AttributeID];
          if (attribute != null)
            str1 = attribute.Description;
          source.Add(str1);
          string str2 = PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
          {
            (object) str1
          });
          answers.RaiseExceptionHandling<CSAnswers.value>((object) csAnswers, (object) csAnswers.Value, (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 5, new object[1]
          {
            (object) typeof (CSAnswers.value).Name
          }));
          PXUIFieldAttribute.SetError<CSAnswers.value>(answers, (object) csAnswers, str2);
        }
      }
    }
    if (source.Count <= 0)
      return;
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string str = PXMessages.LocalizeFormatNoPrefix("There are empty required attributes: {0}", new object[1]
    {
      (object) string.Join(", ", source.Select<string, string>((Func<string, string>) (s => $"'{s}'")))
    });
    if (((PXSelectBase) this)._Graph.IsContractBasedAPI)
    {
      dictionary.Add("1", str);
      throw new PXOuterException(dictionary, ((PXSelectBase) this)._Graph.GetType(), row, "There are empty required attributes: {0}", new object[1]
      {
        (object) string.Join(", ", source.Select<string, string>((Func<string, string>) (s => $"'{s}'")))
      });
    }
    throw new PXException(str);
  }

  protected virtual void ReferenceRowUpdatingHandler(PXCache sender, PXRowUpdatingEventArgs e)
  {
    object row = e.Row;
    object newRow = e.NewRow;
    if (row == null || newRow == null)
      return;
    Guid? noteId = this.GetNoteId(row);
    string classId1 = this.GetClassId(row);
    string classId2 = this.GetClassId(newRow);
    if (string.Equals(classId1, classId2, StringComparison.InvariantCultureIgnoreCase))
    {
      if (!((PXSelectBase) this)._Graph.IsContractBasedAPI || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this)._Graph.Caches[typeof (CSAnswers)].Inserted))
        return;
      this.SelectInternal(newRow).ToList<CSAnswers>();
    }
    else
    {
      if (((PXSelectBase) this)._Graph.IsContractBasedAPI)
        ((PXSelectBase) this)._Graph.Caches[typeof (CSAnswers)].Clear();
      HashSet<string> newAttrList = new HashSet<string>();
      if (classId2 != null)
      {
        foreach (CRAttribute.AttributeExt entityAttribute in (KList<string, CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(newRow), classId2))
          newAttrList.Add(entityAttribute.ID);
      }
      System.Type[] relatedEntityTypes = sender.GetAttributesOfType<CRAttributesFieldAttribute>(newRow, (string) null).FirstOrDefault<CRAttributesFieldAttribute>()?.RelatedEntityTypes;
      PXGraph pxGraph = new PXGraph();
      EntityHelper entityHelper = new EntityHelper(pxGraph);
      if (relatedEntityTypes != null)
      {
        foreach (System.Type type in relatedEntityTypes)
        {
          object entityRow = entityHelper.GetEntityRow(type.DeclaringType, noteId);
          if (entityRow != null)
          {
            string classID = (string) pxGraph.Caches[type.DeclaringType].GetValue(entityRow, type.Name);
            if (classID != null)
              EnumerableExtensions.ForEach<string>(((IEnumerable<CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(type.DeclaringType, classID)).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (x => !newAttrList.Contains(x.ID))).Select<CRAttribute.AttributeExt, string>((Func<CRAttribute.AttributeExt, string>) (x => x.ID)), (Action<string>) (x => newAttrList.Add(x)));
          }
        }
      }
      foreach (PXResult<CSAnswers> pxResult in PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>>>.Config>.SelectMultiBound(sender.Graph, (object[]) null, new object[1]
      {
        (object) noteId
      }))
      {
        CSAnswers csAnswers = PXResult<CSAnswers>.op_Implicit(pxResult);
        CSAnswers copy = PXCache<CSAnswers>.CreateCopy(csAnswers);
        ((PXSelectBase) this).View.Cache.Delete((object) csAnswers);
        if (newAttrList.Contains(copy.AttributeID))
        {
          if (string.Equals(((IEnumerable<CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(row), classId1)).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (x => x.ID == copy.AttributeID && x.IsActive)).FirstOrDefault<CRAttribute.AttributeExt>()?.DefaultValue, copy.Value, StringComparison.InvariantCultureIgnoreCase))
          {
            string defaultValue = ((IEnumerable<CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(this.GetEntityTypeFromAttribute(newRow), classId2)).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (x => x.ID == copy.AttributeID && x.IsActive)).FirstOrDefault<CRAttribute.AttributeExt>()?.DefaultValue;
            copy.Value = defaultValue;
          }
          ((PXSelectBase) this).View.Cache.Insert((object) copy);
        }
      }
      if (classId2 != null)
        this.SelectInternal(newRow).ToList<CSAnswers>();
      sender.IsDirty = true;
    }
  }

  protected virtual void RowInsertedHandler(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (sender == null || sender.Graph == null || sender.Graph.IsImport)
      return;
    this.SelectInternal(e.Row).ToList<CSAnswers>();
  }

  public void CopyAttributes(
    PXGraph destGraph,
    object destination,
    PXGraph srcGraph,
    object source,
    bool copyAll)
  {
    if (destination == null || source == null)
      return;
    List<CSAnswers> list1 = GraphHelper.RowCast<CSAnswers>((IEnumerable) this.SelectInternal(srcGraph, source)).ToList<CSAnswers>();
    List<CSAnswers> list2 = GraphHelper.RowCast<CSAnswers>((IEnumerable) this.SelectInternal(destGraph, destination)).ToList<CSAnswers>();
    PXCache<CSAnswers> pxCache = GraphHelper.Caches<CSAnswers>(((PXSelectBase) this)._Graph);
    foreach (CSAnswers csAnswers1 in list2)
    {
      CSAnswers targetAttribute = csAnswers1;
      CSAnswers csAnswers2 = list1.SingleOrDefault<CSAnswers>((Func<CSAnswers, bool>) (x => x.AttributeID == targetAttribute.AttributeID));
      if (csAnswers2 != null && !string.IsNullOrEmpty(csAnswers2.Value) && !(csAnswers2.Value == targetAttribute.Value) && string.IsNullOrEmpty(targetAttribute.Value) | copyAll)
      {
        CSAnswers copy = PXCache<CSAnswers>.CreateCopy(targetAttribute);
        copy.Value = csAnswers2.Value;
        pxCache.Update(copy);
      }
    }
  }

  protected virtual void CopyAttributes(object destination, object source, bool copyall)
  {
    if (destination == null || source == null)
      return;
    List<CSAnswers> list1 = GraphHelper.RowCast<CSAnswers>((IEnumerable) this.SelectInternal(source)).ToList<CSAnswers>();
    List<CSAnswers> list2 = GraphHelper.RowCast<CSAnswers>((IEnumerable) this.SelectInternal(destination)).ToList<CSAnswers>();
    PXCache<CSAnswers> pxCache = GraphHelper.Caches<CSAnswers>(((PXSelectBase) this)._Graph);
    foreach (CSAnswers csAnswers1 in list2)
    {
      CSAnswers targetAttribute = csAnswers1;
      CSAnswers csAnswers2 = list1.SingleOrDefault<CSAnswers>((Func<CSAnswers, bool>) (x => x.AttributeID == targetAttribute.AttributeID));
      if (csAnswers2 != null && !string.IsNullOrEmpty(csAnswers2.Value) && !(csAnswers2.Value == targetAttribute.Value) && string.IsNullOrEmpty(targetAttribute.Value) | copyall)
      {
        CSAnswers copy = PXCache<CSAnswers>.CreateCopy(targetAttribute);
        copy.Value = csAnswers2.Value;
        pxCache.Update(copy);
      }
    }
  }

  public void CopyAllAttributes(object row, object src) => this.CopyAttributes(row, src, true);

  public void CopyAttributes(object row, object src) => this.CopyAttributes(row, src, false);

  protected virtual string GetDefaultAnswerValue(CRAttribute.AttributeExt attr)
  {
    return attr.DefaultValue;
  }

  protected virtual string GetSelectInternalExceptionMessage()
  {
    return "One or more Attributes are not valid.";
  }

  public class TypeNameConst : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRAttributeList<TEntity>.TypeNameConst>
  {
    public TypeNameConst()
      : base(typeof (TEntity).FullName)
    {
    }
  }
}
