// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBaseUpdateProcess`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.CR.Extensions.Cache;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR;

public abstract class CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField> : 
  CRBaseMassProcess<TGraph, TPrimary>,
  IMassProcess<TPrimary>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimary : class, IBqlTable, IPXSelectable, new()
  where TMarkAttribute : PXEventSubscriberAttribute
  where TClassField : IBqlField
{
  public PXSelect<FieldValue, Where<FieldValue.attributeID, IsNull>, OrderBy<Asc<FieldValue.order>>> Fields;
  public PXSelect<FieldValue, Where<FieldValue.attributeID, IsNotNull>, OrderBy<Asc<FieldValue.order>>> Attributes;
  public PXSelect<CSAnswers> answers;
  public PXFilter<UpdateSummary> wizardSummary;
  private readonly List<string> _suffixes;
  public PXAction<TPrimary> WizardNext;

  protected CRBaseUpdateProcess()
  {
    ((PXGraph) this).Actions["Schedule"].SetVisible(false);
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
    CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetAttributeSuffixes((PXGraph) this, ref this._suffixes);
    PXUIFieldAttribute.SetDisplayName<FieldValue.displayName>(((PXSelectBase) this.Fields).Cache, "Field");
  }

  public IEnumerable fields(PXAdapter adapter)
  {
    return (IEnumerable) ((PXGraph) this).Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (row => row.AttributeID == null));
  }

  public IEnumerable attributes(PXAdapter adapter)
  {
    this.RestrictAttributesByClass();
    return (IEnumerable) ((PXGraph) this).Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (row => row.AttributeID != null && !row.Hidden.GetValueOrDefault()));
  }

  protected virtual void FieldValue_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    FieldValue newRow = (FieldValue) e.NewRow;
    FieldValue row = (FieldValue) e.Row;
    newRow.Selected = new bool?(newRow.Selected.GetValueOrDefault() || row.Value != newRow.Value);
  }

  protected virtual void FieldValue_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXMassProcessHelper.InitValueFieldState(((PXGraph) this).Caches[typeof (TPrimary)], e.Row as FieldValue);
  }

  protected static void GetAttributeSuffixes(PXGraph graph, ref List<string> suffixes)
  {
    suffixes = suffixes ?? new List<string>(((IEnumerable<PropertyInfo>) graph.Caches[typeof (TPrimary)].BqlTable.GetProperties(BindingFlags.Instance | BindingFlags.Public)).SelectMany<PropertyInfo, object, string>((Func<PropertyInfo, IEnumerable<object>>) (p => ((IEnumerable<object>) p.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (atr => atr is PXDBAttributeAttribute))), (Func<PropertyInfo, object, string>) ((p, atr) => p.Name)));
  }

  public static IEnumerable<FieldValue> GetMarkedProperties(PXGraph graph, ref int firstSortOrder)
  {
    PXCache cache = graph.Caches[typeof (TPrimary)];
    int order = firstSortOrder;
    List<FieldValue> list = cache.GetFields_MassUpdatable().Select(fieldname => new
    {
      fieldname = fieldname,
      state = cache.GetStateExt((object) null, fieldname) as PXFieldState
    }).Where(t => t.state != null).Select(t => new FieldValue()
    {
      Selected = new bool?(false),
      CacheName = typeof (TPrimary).FullName,
      Name = t.fieldname,
      DisplayName = t.state.DisplayName,
      AttributeID = (string) null,
      Order = new int?(order++)
    }).ToList<FieldValue>();
    firstSortOrder = order;
    return (IEnumerable<FieldValue>) list;
  }

  public static IEnumerable<FieldValue> GetAttributeProperties(
    PXGraph graph,
    ref int firstSortOrder)
  {
    return CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetAttributeProperties(graph, ref firstSortOrder, (List<string>) null);
  }

  public static IEnumerable<FieldValue> GetAttributeProperties(
    PXGraph graph,
    ref int firstSortOrder,
    List<string> suffixes)
  {
    PXCache cach = graph.Caches[typeof (TPrimary)];
    int num = firstSortOrder;
    List<FieldValue> attributeProperties = new List<FieldValue>();
    CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetAttributeSuffixes(graph, ref suffixes);
    foreach (string field1 in (List<string>) graph.Caches[typeof (TPrimary)].Fields)
    {
      string field = field1;
      if (suffixes.Any<string>((Func<string, bool>) (suffix => field.EndsWith($"_{suffix}"))) && cach.GetStateExt((object) null, field) is PXFieldState stateExt)
      {
        string str1 = stateExt.DisplayName;
        string str2 = field;
        string local = field;
        using (IEnumerator<string> enumerator = suffixes.Where<string>((Func<string, bool>) (suffix => local.EndsWith($"_{suffix}"))).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            str2 = field.Replace($"_{current}", string.Empty);
            str1 = stateExt.DisplayName.Replace($"${current}$-", string.Empty);
          }
        }
        attributeProperties.Add(new FieldValue()
        {
          Selected = new bool?(false),
          CacheName = typeof (TPrimary).FullName,
          Name = field,
          DisplayName = str1,
          AttributeID = str2,
          Order = new int?(num++ + 1000)
        });
      }
    }
    firstSortOrder = num;
    return (IEnumerable<FieldValue>) attributeProperties;
  }

  public static IEnumerable<FieldValue> GetProcessingProperties(
    PXGraph graph,
    ref int firstSortOrder)
  {
    return CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetProcessingProperties(graph, ref firstSortOrder, (List<string>) null);
  }

  public static IEnumerable<FieldValue> GetProcessingProperties(
    PXGraph graph,
    ref int firstSortOrder,
    List<string> suffixes)
  {
    return CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetMarkedProperties(graph, ref firstSortOrder).Union<FieldValue>(CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetAttributeProperties(graph, ref firstSortOrder, suffixes));
  }

  protected virtual IEnumerable<FieldValue> ProcessingProperties
  {
    get
    {
      int firstSortOrder = 0;
      return CRBaseUpdateProcess<TGraph, TPrimary, TMarkAttribute, TClassField>.GetProcessingProperties((PXGraph) this, ref firstSortOrder, this._suffixes);
    }
  }

  protected void FillPropertyValue(PXGraph graph, string viewName)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (FieldValue)];
    cach.Clear();
    foreach (FieldValue processingProperty in this.ProcessingProperties)
      cach.Insert((object) processingProperty);
    cach.IsDirty = false;
  }

  protected Dictionary<string, bool> GetClassAttributes(string ClassID)
  {
    return GraphHelper.RowCast<CSAttributeGroup>((IEnumerable) PXSelectBase<CSAttributeGroup, PXSelectJoin<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.entityClassID, Equal<Required<TClassField>>, And<CSAttributeGroup.entityType, Equal<Required<CSAttributeGroup.entityType>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ClassID,
      (object) typeof (TClassField).ReflectedType.FullName
    })).ToDictionary<CSAttributeGroup, string, bool>((Func<CSAttributeGroup, string>) (g => g.AttributeID), (Func<CSAttributeGroup, bool>) (g => g.Required.GetValueOrDefault()));
  }

  public virtual void RestrictAttributesByClass()
  {
    PXCache<TPrimary> pxCache = GraphHelper.Caches<TPrimary>((PXGraph) this);
    HashSet<object> source = new HashSet<object>((IEnumerable<object>) ((PXCache) pxCache).Updated.Cast<TPrimary>().Where<TPrimary>((Func<TPrimary, bool>) (entity => entity.Selected.GetValueOrDefault())).Select<TPrimary, object>(new Func<TPrimary, object>(((PXCache) pxCache).GetValue<TClassField>)).ToList<object>());
    FieldValue fieldValue1 = ((PXGraph) this).Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().FirstOrDefault<FieldValue>((Func<FieldValue, bool>) (field => string.Equals(field.Name, typeof (TClassField).Name, StringComparison.CurrentCultureIgnoreCase)));
    bool? nullable1;
    string str;
    if (fieldValue1 != null)
    {
      nullable1 = fieldValue1.Selected;
      bool flag = false;
      if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue) && fieldValue1.Value != null)
      {
        str = fieldValue1.Value;
        goto label_4;
      }
    }
    str = (string) null;
label_4:
    if (str == null)
      str = source.Count == 1 ? source.FirstOrDefault<object>() as string : (string) null;
    string ClassID = str;
    Dictionary<string, bool> classAttributes = this.GetClassAttributes(ClassID);
    foreach (FieldValue fieldValue2 in ((PXGraph) this).Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (f => f.AttributeID != null)))
    {
      fieldValue2.Hidden = new bool?(!classAttributes.ContainsKey(fieldValue2.AttributeID));
      FieldValue fieldValue3 = fieldValue2;
      nullable1 = fieldValue2.Hidden;
      bool? nullable2 = new bool?(!nullable1.Value && classAttributes[fieldValue2.AttributeID]);
      fieldValue3.Required = nullable2;
    }
    ((PXSelectBase) this.Attributes).AllowSelect = !string.IsNullOrEmpty(ClassID);
  }

  protected override bool AskParameters()
  {
    // ISSUE: method pointer
    return ((PXSelectBase<FieldValue>) this.Fields).AskExt(new PXView.InitializePanel((object) this, __methodptr(FillPropertyValue))) == 1;
  }

  public override void ProccessItem(PXGraph graph, TPrimary item)
  {
    PXCache cach1 = graph.Caches[typeof (TPrimary)];
    TPrimary instance1 = (TPrimary) cach1.CreateInstance();
    PXCache<TPrimary>.RestoreCopy(instance1, item);
    string noteField = EntityHelper.GetNoteField(cach1.GetItemType());
    PXView view = graph.Views[graph.PrimaryView];
    object[] objArray = new object[view.Cache.BqlKeys.Count];
    string[] strArray = new string[view.Cache.BqlKeys.Count];
    for (int index = 0; index < cach1.BqlKeys.Count<System.Type>(); ++index)
    {
      strArray[index] = cach1.BqlKeys[index].Name;
      objArray[index] = cach1.GetValue((object) instance1, strArray[index]);
    }
    int num1 = 0;
    int num2 = 0;
    List<object> objectList1 = view.Select((object[]) null, (object[]) null, objArray, strArray, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2);
    TPrimary copy = (TPrimary) cach1.CreateCopy((object) PXResult.Unwrap<TPrimary>(objectList1[0]));
    foreach (FieldValue fieldValue1 in ((PXSelectBase) this.Fields).Cache.Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (o => o.AttributeID == null && o.Selected.GetValueOrDefault())))
    {
      FieldValue fieldValue = fieldValue1;
      PXFieldState stateExt = cach1.GetStateExt((object) copy, fieldValue.Name) as PXFieldState;
      PXIntState pxIntState = stateExt as PXIntState;
      PXStringState pxStringState = stateExt as PXStringState;
      if (pxIntState != null && pxIntState.AllowedValues != null && pxIntState.AllowedValues.Length != 0 && ((IEnumerable<int>) pxIntState.AllowedValues).All<int>((Func<int, bool>) (v => v != int.Parse(fieldValue.Value))) || pxStringState != null && pxStringState.AllowedValues != null && pxStringState.AllowedValues.Length != 0 && ((IEnumerable<string>) pxStringState.AllowedValues).All<string>((Func<string, bool>) (v => v != fieldValue.Value)))
        throw new PXSetPropertyException("The list value '{0}' is not allowed for the {1} field.", new object[2]
        {
          (object) fieldValue.Value,
          (object) fieldValue.Name
        });
      if (stateExt != null && !object.Equals(stateExt.Value, (object) fieldValue.Value))
      {
        cach1.SetValueExt((object) copy, fieldValue.Name, (object) fieldValue.Value);
        cach1.Update((object) copy);
      }
      List<object> objectList2 = view.Select((object[]) null, (object[]) null, objArray, strArray, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2);
      copy = (TPrimary) cach1.CreateCopy((object) PXResult.Unwrap<TPrimary>(objectList2[0]));
    }
    PXCache cach2 = cach1.Graph.Caches[typeof (CSAnswers)];
    foreach (FieldValue fieldValue in ((PXSelectBase) this.Attributes).Cache.Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (o => o.AttributeID != null && o.Selected.GetValueOrDefault())))
    {
      CSAnswers instance2 = (CSAnswers) cach2.CreateInstance();
      instance2.AttributeID = fieldValue.AttributeID;
      instance2.RefNoteID = cach1.GetValue((object) copy, noteField) as Guid?;
      instance2.Value = fieldValue.Value;
      cach2.Update((object) instance2);
    }
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable wizardNext(PXAdapter adapter) => adapter.Get();
}
