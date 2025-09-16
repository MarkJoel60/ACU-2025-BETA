// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CalendarComponentSetupMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

public class CalendarComponentSetupMaint : PXGraph<CalendarComponentSetupMaint>
{
  public PXSave<FSSetup> Save;
  public PXCancel<FSSetup> Cancel;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXSelect<FSAppointmentStatusColor> StatusColorRecords;
  public PXSelect<FSAppointmentStatusColor, Where<FSAppointmentStatusColor.statusID, Equal<Current<FSAppointmentStatusColor.statusID>>>> StatusColorSelected;
  public CalendarComponentSetupMaint.AppointmentBoxFields_View AppointmentBoxFields;
  public CalendarComponentSetupMaint.ServiceOrderComponentFields_View ServiceOrderFields;
  public CalendarComponentSetupMaint.UnassignedAppComponentFields_View UnassignedAppointmentFields;

  public virtual void ComponentFieldRowSelect(
    PXCache cache,
    FSCalendarComponentField row,
    Type objectName,
    Type field,
    bool isServiceOrder)
  {
    if (row != null && !string.IsNullOrEmpty(row.ObjectName))
    {
      List<string> strlist = new List<string>();
      List<string> strDispNames = new List<string>();
      if (!string.IsNullOrEmpty(row.ObjectName))
      {
        this.AddTableFields(row.ObjectName, false, strlist, strDispNames);
        if (!string.IsNullOrEmpty(row.FieldName))
        {
          Type tableType = this.GetTableType(row.ObjectName, objectName.Name, cache, (object) row);
          if (tableType == (Type) null)
            return;
          PXCache cach = ((PXGraph) this).Caches[tableType];
          PXCache pxCache = cache;
          FSCalendarComponentField calendarComponentField = row;
          string fieldName = row.FieldName;
          PXSetPropertyException propertyException;
          if (cach.Fields.Contains(row.FieldName))
            propertyException = (PXSetPropertyException) null;
          else
            propertyException = new PXSetPropertyException("A field with the name {0} cannot be found.", (PXErrorLevel) 2, new object[1]
            {
              (object) row.FieldName
            });
          pxCache.RaiseExceptionHandling<FSCalendarComponentField.fieldName>((object) calendarComponentField, (object) fieldName, (Exception) propertyException);
        }
        PXStringListAttribute.SetList(cache, (object) row, field.Name, strlist.ToArray(), strDispNames.ToArray());
      }
    }
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList1.Add(typeof (FSServiceOrder).FullName);
    stringList2.Add(typeof (FSServiceOrder).Name);
    if (!isServiceOrder)
    {
      stringList1.Add(typeof (FSAppointment).FullName);
      stringList2.Add(typeof (FSAppointment).Name);
    }
    stringList1.Add(typeof (FSContact).FullName);
    stringList2.Add(typeof (FSContact).Name);
    stringList1.Add(typeof (FSAddress).FullName);
    stringList2.Add(typeof (FSAddress).Name);
    PXStringListAttribute.SetList(cache, (object) row, objectName.Name, stringList1.ToArray(), stringList2.ToArray());
  }

  private Type GetTableType(
    string tableName,
    string fieldName,
    PXCache cache,
    object row,
    string warningMessage = null)
  {
    Type type = PXBuildManager.GetType(tableName, false);
    if (type == (Type) null)
    {
      if (string.IsNullOrEmpty(warningMessage))
        warningMessage = "A table with the alias {0} cannot be found.";
      cache.RaiseExceptionHandling(fieldName, row, (object) tableName, (Exception) new PXSetPropertyException(warningMessage, (PXErrorLevel) 2, new object[1]
      {
        (object) tableName
      }));
    }
    return type;
  }

  private void AddTableFields(
    string tableName,
    bool needTableName,
    List<string> strlist,
    List<string> strDispNames,
    Func<PXCache, string, bool> predicate = null)
  {
    if (((PXGraph) this).IsImport || string.IsNullOrEmpty(tableName))
      return;
    Type type = PXBuildManager.GetType(tableName, false);
    if (type == (Type) null)
      return;
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    List<KeyValuePair<string, string>> source = new List<KeyValuePair<string, string>>();
    PXCache cach = ((PXGraph) this).Caches[type];
    foreach (string field in (List<string>) cach.Fields)
    {
      Type bqlField = cach.GetBqlField(field);
      if ((!(bqlField != (Type) null) || BqlCommand.GetItemType(bqlField).IsAssignableFrom(type)) && (!(bqlField == (Type) null) || field.EndsWith("_Attributes") || field.EndsWith("Signed") || cach.IsKvExtAttribute(field)))
      {
        string str1 = bqlField != (Type) null ? bqlField.Name : field;
        if (!cach.GetAttributes(field).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute)) && !dictionary.ContainsKey(field))
        {
          dictionary[field] = field;
          string str2 = needTableName ? $"{tableName}.{field}" : field;
          PXFieldState stateExt = (PXFieldState) cach.GetStateExt((object) null, str2);
          if (stateExt != null && !string.IsNullOrEmpty(stateExt.DescriptionName) && (predicate == null || predicate(cach, str1 + "_description")))
          {
            string key = needTableName ? $"{tableName}.{str1}_description" : str1 + "_description";
            string str3 = str2 + "_Description";
            source.Add(new KeyValuePair<string, string>(key, str3));
          }
          if (predicate == null || predicate(cach, str1))
          {
            string key = needTableName ? $"{tableName}.{str1}" : str1;
            string str4 = str2;
            source.Add(new KeyValuePair<string, string>(key, str4));
          }
        }
      }
    }
    source.Sort((Comparison<KeyValuePair<string, string>>) ((f1, f2) => strDispNames != null ? string.Compare(f1.Value, f2.Value) : string.Compare(f1.Key, f2.Key)));
    strlist.AddRange(source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key)));
    if (strDispNames == null)
      return;
    strDispNames.AddRange(source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value)));
  }

  protected virtual void _(Events.RowSelected<AppointmentBoxComponentField> e)
  {
    this.ComponentFieldRowSelect(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AppointmentBoxComponentField>>) e).Cache, (FSCalendarComponentField) e.Row, typeof (AppointmentBoxComponentField.objectName), typeof (AppointmentBoxComponentField.fieldName), false);
  }

  protected virtual void _(Events.RowSelected<ServiceOrderComponentField> e)
  {
    this.ComponentFieldRowSelect(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ServiceOrderComponentField>>) e).Cache, (FSCalendarComponentField) e.Row, typeof (ServiceOrderComponentField.objectName), typeof (ServiceOrderComponentField.fieldName), true);
  }

  protected virtual void _(Events.RowSelected<UnassignedAppComponentField> e)
  {
    this.ComponentFieldRowSelect(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<UnassignedAppComponentField>>) e).Cache, (FSCalendarComponentField) e.Row, typeof (UnassignedAppComponentField.objectName), typeof (UnassignedAppComponentField.fieldName), false);
  }

  [PXDynamicButton(new string[] {"AppBoxFieldsPasteLineCmd", "AppBoxFieldsResetLineCmd"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class AppointmentBoxFields_View : 
    PXOrderedSelect<FSSetup, AppointmentBoxComponentField, Where<True, Equal<True>>, OrderBy<Asc<AppointmentBoxComponentField.sortOrder>>>
  {
    public const string AppBoxFieldsPasteLineCmd = "AppBoxFieldsPasteLineCmd";
    public const string AppBoxFieldsResetLineCmd = "AppBoxFieldsResetLineCmd";

    public AppointmentBoxFields_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentBoxFields_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      CalendarComponentSetupMaint.AppointmentBoxFields_View appointmentBoxFieldsView1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) appointmentBoxFieldsView1, __vmethodptr(appointmentBoxFieldsView1, PasteLine));
      ((PXOrderedSelectBase<FSSetup, AppointmentBoxComponentField>) this).AddAction(pxGraph1, "AppBoxFieldsPasteLineCmd", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      CalendarComponentSetupMaint.AppointmentBoxFields_View appointmentBoxFieldsView2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) appointmentBoxFieldsView2, __vmethodptr(appointmentBoxFieldsView2, ResetOrder));
      ((PXOrderedSelectBase<FSSetup, AppointmentBoxComponentField>) this).AddAction(pxGraph2, "AppBoxFieldsResetLineCmd", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }

  [PXDynamicButton(new string[] {"SOGridFieldsPasteLineCmd", "SOGridFieldsResetLineCmd"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class ServiceOrderComponentFields_View : 
    PXOrderedSelect<FSSetup, ServiceOrderComponentField, Where<True, Equal<True>>, OrderBy<Asc<ServiceOrderComponentField.sortOrder>>>
  {
    public const string SOGridFieldsPasteLineCmd = "SOGridFieldsPasteLineCmd";
    public const string SOGridFieldsResetLineCmd = "SOGridFieldsResetLineCmd";

    public ServiceOrderComponentFields_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderComponentFields_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      CalendarComponentSetupMaint.ServiceOrderComponentFields_View componentFieldsView1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) componentFieldsView1, __vmethodptr(componentFieldsView1, PasteLine));
      ((PXOrderedSelectBase<FSSetup, ServiceOrderComponentField>) this).AddAction(pxGraph1, "SOGridFieldsPasteLineCmd", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      CalendarComponentSetupMaint.ServiceOrderComponentFields_View componentFieldsView2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) componentFieldsView2, __vmethodptr(componentFieldsView2, ResetOrder));
      ((PXOrderedSelectBase<FSSetup, ServiceOrderComponentField>) this).AddAction(pxGraph2, "SOGridFieldsResetLineCmd", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }

  [PXDynamicButton(new string[] {"UAGridFieldsPasteLineCmd", "UAGridFieldsResetLineCmd"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class UnassignedAppComponentFields_View : 
    PXOrderedSelect<FSSetup, UnassignedAppComponentField, Where<True, Equal<True>>, OrderBy<Asc<ServiceOrderComponentField.sortOrder>>>
  {
    public const string UAGridFieldsPasteLineCmd = "UAGridFieldsPasteLineCmd";
    public const string UAGridFieldsResetLineCmd = "UAGridFieldsResetLineCmd";

    public UnassignedAppComponentFields_View(PXGraph graph)
      : base(graph)
    {
    }

    public UnassignedAppComponentFields_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      CalendarComponentSetupMaint.UnassignedAppComponentFields_View componentFieldsView1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) componentFieldsView1, __vmethodptr(componentFieldsView1, PasteLine));
      ((PXOrderedSelectBase<FSSetup, UnassignedAppComponentField>) this).AddAction(pxGraph1, "UAGridFieldsPasteLineCmd", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      CalendarComponentSetupMaint.UnassignedAppComponentFields_View componentFieldsView2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) componentFieldsView2, __vmethodptr(componentFieldsView2, ResetOrder));
      ((PXOrderedSelectBase<FSSetup, UnassignedAppComponentField>) this).AddAction(pxGraph2, "UAGridFieldsResetLineCmd", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }
}
