// Decompiled with JetBrains decompiler
// Type: PX.Objects.TaskTemplateMaintGraphExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.Automation;
using PX.Data.Description.GI;
using PX.Data.Maintenance.GI;
using PX.Data.Wiki.Parser.BlockParsers;
using PX.Objects.CR;
using PX.PushNotifications.SourceProcessors;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects;

public class TaskTemplateMaintGraphExtension : PXGraphExtension<TaskTemplateMaint>
{
  private const string EntityKey = "Entity";
  private const string OwnersKey = "Owners";
  private const char KeySeparator = '-';
  internal const char ValueSeparator = '|';
  private readonly PXGraph taskGraph = (PXGraph) PXGraph.CreateInstance<CRTaskMaint>();
  private readonly BqlCommand ownerSelect = new OwnerAttribute().GetSelect();
  private static readonly System.Type[] fieldsList = new System.Type[13]
  {
    typeof (CRActivity.startDate),
    typeof (CRActivity.endDate),
    typeof (CRActivity.priority),
    typeof (CRActivity.uistatus),
    typeof (CRActivity.categoryID),
    typeof (CRActivity.workgroupID),
    typeof (CRActivity.contactID),
    typeof (CRActivity.bAccountID),
    typeof (CRActivity.isPrivate),
    typeof (CRReminder.isReminderOn),
    typeof (CRReminder.reminderDate),
    typeof (PMTimeActivity.projectID),
    typeof (PMTimeActivity.projectTaskID)
  };

  [InjectDependency]
  private IWorkflowService _workflowService { get; set; }

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  [PXFieldNamesList(typeof (CRTaskMaint), new System.Type[] {typeof (CRActivity.startDate), typeof (CRActivity.endDate), typeof (CRActivity.priority), typeof (CRActivity.uistatus), typeof (CRActivity.categoryID), typeof (CRActivity.workgroupID), typeof (CRActivity.contactID), typeof (CRActivity.bAccountID), typeof (CRActivity.isPrivate), typeof (CRReminder.isReminderOn), typeof (CRReminder.reminderDate), typeof (PMTimeActivity.projectID), typeof (PMTimeActivity.projectTaskID)})]
  [PXMergeAttributes]
  public void TaskTemplateSetting_FieldName_CacheAttached(PXCache cache)
  {
  }

  [PXFieldValuesList(4000, typeof (CRTaskMaint), typeof (TaskTemplateSetting.fieldName), ExclusiveValues = false, IsActive = false)]
  [PXMergeAttributes]
  public void TaskTemplateSetting_Value_CacheAttached(PXCache cache)
  {
  }

  protected virtual void TaskTemplate_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaskTemplate row))
      return;
    PXCache cache1 = ((PXSelectBase) this.Base.TaskTemplateSettings).Cache;
    int? taskTemplateId = row.TaskTemplateID;
    int num = 0;
    if (!(taskTemplateId.GetValueOrDefault() < num & taskTemplateId.HasValue) || NonGenericIEnumerableExtensions.Any_(cache1.Inserted))
      return;
    foreach (System.Type fields in TaskTemplateMaintGraphExtension.fieldsList)
      ((TaskTemplateSetting) GraphHelper.NonDirtyInsert(cache1)).FieldName = PXFieldNamesListAttribute.MergeNames(fields.DeclaringType.Name, fields.Name);
  }

  protected virtual void TaskTemplateSetting_Value_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is TaskTemplateSetting row))
    {
      PXStringListAttribute.SetList(cache, (object) null, "value", new string[1]
      {
        ""
      }, new string[1]{ "" });
    }
    else
    {
      this.Base.UpdateValueFieldState(cache, row);
      this.InsertOrUpdateValueInCache(row);
    }
  }

  protected virtual void TaskTemplateSetting_Value_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaskTemplateSetting row))
      return;
    this.InsertOrUpdateValueInCache(row);
  }

  protected IEnumerable screenOwnerItems(string parent, bool KeyEqualsPath = false)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    if (maintGraphExtension.Base.CurrentSiteMapNode != null)
    {
      object[] searches = PXView.Searches;
      if ((searches != null ? ((IEnumerable<object>) searches).FirstOrDefault<object>() : (object) null) is string str)
        parent = StringExtensions.FirstSegment(str, '|');
      if (parent == null)
      {
        yield return (object) new CacheEntityItem()
        {
          Key = "Entity",
          Name = "Entity",
          Number = new int?(0)
        };
        yield return (object) new CacheEntityItem()
        {
          Key = "Owners",
          Name = "Owners",
          Number = new int?(1)
        };
      }
      else if (StringExtensions.OrdinalEquals(parent, "Owners"))
      {
        foreach (object lettersOfOwnerName in maintGraphExtension.GetFirstLettersOfOwnerNames(parent))
          yield return lettersOfOwnerName;
      }
      else if (parent.StartsWith("Owners"))
      {
        foreach (object allOwnerName in maintGraphExtension.GetAllOwnerNames(parent))
          yield return allOwnerName;
      }
      else if (StringExtensions.OrdinalEquals(parent, "Entity"))
      {
        if (maintGraphExtension.Base.CurrentScreenIsGI)
        {
          foreach (object obj in maintGraphExtension.GetAllOwnerFieldsForGI(parent, KeyEqualsPath))
            yield return obj;
        }
        else
        {
          foreach (object obj in maintGraphExtension.GetOwnerFieldsForEntry(parent, KeyEqualsPath))
            yield return obj;
        }
      }
    }
  }

  protected IEnumerable ScreenOwnerItemsWithPaths(string parent)
  {
    return this.screenOwnerItems(parent, true);
  }

  protected IEnumerable screenOwnerUserItems()
  {
    string parent = "Owners";
    object[] searches = PXView.Searches;
    if ((searches != null ? ((IEnumerable<object>) searches).FirstOrDefault<object>() : (object) null) is string str)
      parent = StringExtensions.FirstSegment(str, '|');
    if (StringExtensions.OrdinalEquals(parent, "Owners"))
    {
      foreach (CacheEntityItem lettersOfOwnerName in this.GetFirstLettersOfOwnerNames(parent))
      {
        if (lettersOfOwnerName.Key.StartsWith("Owners"))
        {
          foreach (object allOwnerName in this.GetAllOwnerNames(lettersOfOwnerName.Key))
            yield return allOwnerName;
        }
      }
    }
  }

  protected IEnumerable screenOwnerEntityItems(string parent)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    if (maintGraphExtension.Base.CurrentSiteMapNode != null)
    {
      object[] searches = PXView.Searches;
      if ((searches != null ? ((IEnumerable<object>) searches).FirstOrDefault<object>() : (object) null) is string str)
        parent = StringExtensions.FirstSegment(str, '|');
      if (parent == null)
        yield return (object) new CacheEntityItem()
        {
          Key = "Entity",
          Name = "Entity",
          Number = new int?(0)
        };
      else if (StringExtensions.OrdinalEquals(parent, "Entity"))
      {
        if (maintGraphExtension.Base.CurrentScreenIsGI)
        {
          foreach (object obj in maintGraphExtension.GetAllOwnerFieldsForGI(parent, true))
            yield return obj;
        }
        else
        {
          foreach (object obj in maintGraphExtension.GetOwnerFieldsForEntry(parent, true))
            yield return obj;
        }
      }
    }
  }

  [TaskTemplateMaintGraphExtension.OwnerNameSelector]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<TaskTemplate.ownerName> e)
  {
  }

  /// <summary>Inserts or updates data from the Value field of TaskTemplateSetting to the current record of an appropriate cache.</summary>
  /// <remarks>Procures work of connected selectors, such as on PMTimeActivity.ProjectID and PMTimeActivity.ProjectTaskID fields.</remarks>
  private void InsertOrUpdateValueInCache(TaskTemplateSetting row)
  {
    string str1;
    string str2;
    if (!PXFieldNamesListAttribute.SplitNames(row.FieldName, ref str1, ref str2))
      return;
    PXCache cach1 = this.taskGraph.Caches[str1];
    if (cach1 == null)
      return;
    PXCache cach2 = ((PXGraph) this.Base).Caches[cach1.GetItemType()];
    if (cach2.Current == null)
      cach2.Current = cach2.CreateInstance();
    try
    {
      cach2.SetValueExt(cach2.Current, str2, (object) row.Value);
    }
    catch
    {
    }
  }

  private IEnumerable<CacheEntityItem> GetFirstLettersOfOwnerNames(string parent)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    foreach (string str in new PXView((PXGraph) maintGraphExtension.Base, false, maintGraphExtension.ownerSelect).SelectMulti(Array.Empty<object>()).Cast<Contact>().Where<Contact>((Func<Contact, bool>) (c => !string.IsNullOrEmpty(c.DisplayName))).Select<Contact, string>((Func<Contact, string>) (c => c.DisplayName.Substring(0, 1).ToUpper())).OrderBy<string, string>((Func<string, string>) (c => c)).Distinct<string>())
      yield return new CacheEntityItem()
      {
        Name = str,
        Key = $"{parent}-{str}"
      };
  }

  private IEnumerable<CacheEntityItem> GetAllOwnerNames(string parent)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    string letter = StringExtensions.LastSegment(parent, '-');
    if (letter.IndexOf('|') <= 0)
    {
      foreach (Contact contact in new PXView((PXGraph) maintGraphExtension.Base, false, maintGraphExtension.ownerSelect).SelectMulti(Array.Empty<object>()).Cast<Contact>().Where<Contact>((Func<Contact, bool>) (c => StringExtensions.OrdinalStartsWith(c.DisplayName, new string[1]
      {
        letter
      }))))
        yield return new CacheEntityItem()
        {
          Name = contact.DisplayName + (string.IsNullOrEmpty(contact.Salutation) ? "" : $" ({contact.Salutation})"),
          Key = $"{parent}|{contact.ContactID.ToString()}",
          Path = $"{parent}|{contact.ContactID.ToString()}"
        };
    }
  }

  private IEnumerable<CacheEntityItem> GetOwnerFieldsForEntry(string parent, bool KeyEqualsPath = false)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
    Dictionary<string, string> prevResult = new Dictionary<string, string>();
    List<System.Type> tables = new List<System.Type>();
    PXGraph graph = PXGraph.CreateInstance(GraphHelper.GetType(maintGraphExtension.Base.CurrentSiteMapNode.GraphType));
    Dictionary<string, string> dictionary2 = ((IEnumerable<string>) graph.Views[graph.PrimaryView].Cache.Fields).ToDictionary<string, string, string>((Func<string, string>) (c => c), (Func<string, string>) (c => PushNotificationsProcessorHelper.FormFieldKey(graph.PrimaryView, c)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    IEnumerable source = EMailSourceHelper.TemplateEntity((PXGraph) maintGraphExtension.Base, (string) null, (string) null, maintGraphExtension.Base.CurrentSiteMapNode.GraphType, true, false, true, maintGraphExtension._workflowService);
    string primaryView = maintGraphExtension.PageIndexingService.GetPrimaryView(maintGraphExtension.Base.CurrentSiteMapNode.GraphType);
    foreach (string formName in source.OfType<CacheEntityItem>().Select<CacheEntityItem, string>((Func<CacheEntityItem, string>) (c => c.Key)))
    {
      PXView pxView;
      if (!graph.Views.TryGetOrCreateValue(formName, ref pxView))
        maintGraphExtension.EnumOwnerFormFields(formName, dictionary1, tables, graph, dictionary2);
      else if (formName.Equals(primaryView, StringComparison.OrdinalIgnoreCase))
      {
        Dictionary<string, HashSet<string>> allowedItems = maintGraphExtension.Base.GetAllowedItems();
        TaskTemplateMaintGraphExtension.EnumOwnerFieldsWithPrevios(graph, PXViewExtensionsForMobile.CacheType(pxView), tables, dictionary1, prevResult, dictionary2, primaryView, allowedItems, true);
      }
      else
        TaskTemplateMaintGraphExtension.EnumOwnerFields((string) null, (string) null, graph, PXViewExtensionsForMobile.CacheType(pxView), tables, dictionary1, dictionary2);
    }
    int num = 0;
    foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
      yield return new CacheEntityItem()
      {
        Key = KeyEqualsPath ? $"{parent}|(({keyValuePair.Key}))" : keyValuePair.Value,
        Name = keyValuePair.Value,
        Number = new int?(num++),
        Path = $"{parent}|(({keyValuePair.Key}))"
      };
    foreach (KeyValuePair<string, string> keyValuePair in prevResult)
      yield return new CacheEntityItem()
      {
        Key = KeyEqualsPath ? $"{parent}|{PreviousValueHelper.GetPrevFunctionInvocationText(keyValuePair.Key)}" : SMNotificationMaint.GetPrevKey(keyValuePair.Value),
        Name = SMNotificationMaint.GetPrevName(keyValuePair.Value),
        Number = new int?(num++),
        Path = $"{parent}|{PreviousValueHelper.GetPrevFunctionInvocationText(keyValuePair.Key)}"
      };
  }

  private IEnumerable<CacheEntityItem> GetAllOwnerFieldsForGI(string parent, bool KeyEqualsPath = false)
  {
    TaskTemplateMaintGraphExtension maintGraphExtension = this;
    Dictionary<string, string> result = new Dictionary<string, string>();
    Dictionary<string, string> prevResult = new Dictionary<string, string>();
    List<System.Type> tables = new List<System.Type>();
    PXGenericInqGrph instance = PXGenericInqGrph.CreateInstance(maintGraphExtension.Base.CurrentSiteMapNode.ScreenID);
    Dictionary<string, PXTable> usedTables = instance.BaseQueryDescription.UsedTables;
    Dictionary<string, HashSet<string>> allowedItems = maintGraphExtension.Base.GetAllowedItems();
    foreach (IGrouping<string, GIResult> source in instance.ResultColumns.GroupBy<GIResult, string>((Func<GIResult, string>) (c => c.ObjectName)))
    {
      PXTable pxTable;
      if (usedTables.TryGetValue(source.Key, out pxTable))
      {
        Dictionary<string, string> dictionary = source.ToDictionary<GIResult, string, string>((Func<GIResult, string>) (c => c.Field), (Func<GIResult, string>) (c => c.FieldName), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        string name = pxTable.BqlTable.Name;
        TaskTemplateMaintGraphExtension.EnumOwnerFieldsWithPrevios((PXGraph) instance, pxTable.CacheType, tables, result, prevResult, dictionary, name, allowedItems);
      }
    }
    int num = 0;
    foreach (KeyValuePair<string, string> keyValuePair in result)
      yield return new CacheEntityItem()
      {
        Key = KeyEqualsPath ? $"{parent}|(({keyValuePair.Key}))" : keyValuePair.Value,
        Name = keyValuePair.Value,
        Number = new int?(num++),
        Path = $"{parent}|(({keyValuePair.Key}))"
      };
    foreach (KeyValuePair<string, string> keyValuePair in prevResult)
      yield return new CacheEntityItem()
      {
        Key = KeyEqualsPath ? $"{parent}|{PreviousValueHelper.GetPrevFunctionInvocationText(keyValuePair.Key)}" : SMNotificationMaint.GetPrevKey(keyValuePair.Value),
        Name = SMNotificationMaint.GetPrevName(keyValuePair.Value),
        Number = new int?(num++),
        Path = $"{parent}|{PreviousValueHelper.GetPrevFunctionInvocationText(keyValuePair.Key)}"
      };
  }

  private static void EnumOwnerFieldsWithPrevios(
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> result,
    Dictionary<string, string> prevResult,
    Dictionary<string, string> fields,
    string tableName,
    Dictionary<string, HashSet<string>> allowedItems,
    bool enumEntryScreenSelectors = false)
  {
    Dictionary<string, string> names = new Dictionary<string, string>();
    TaskTemplateMaintGraphExtension.EnumOwnerFields((string) null, (string) null, graph, table, tables, names, fields);
    foreach (KeyValuePair<string, string> keyValuePair in names)
    {
      if (!result.ContainsKey(keyValuePair.Key))
      {
        result.Add(keyValuePair.Key, keyValuePair.Value);
        int length = keyValuePair.Key.IndexOf("!");
        string str = length == -1 ? keyValuePair.Key : keyValuePair.Key.Substring(0, length);
        int num1 = str.IndexOf(".");
        if (num1 != -1)
        {
          str = str.Substring(num1 + 1);
        }
        else
        {
          int num2 = str.IndexOf('_');
          if (num2 != -1)
            str = str.Substring(num2 + 1);
        }
        HashSet<string> stringSet;
        if ((allowedItems == null ? 1 : (!allowedItems.TryGetValue(tableName, out stringSet) ? 0 : (stringSet.Contains(str) ? 1 : 0))) != 0)
          prevResult.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
  }

  private static void EnumOwnerFields(
    string internalPath,
    string displayPath,
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields)
  {
    Func<System.Type, List<string>> func = new Func<System.Type, List<string>>(OwnerAttribute.GetFields);
    TemplateGraphHelper.EnumAssigneeFields(internalPath, displayPath, graph, table, tables, names, fields, (Func<System.Type, IEnumerable<string>>) func);
  }

  private void EnumOwnerFormFields(
    string formName,
    Dictionary<string, string> names,
    List<System.Type> tables,
    PXGraph graph,
    Dictionary<string, string> fields)
  {
    TemplateGraphHelper.EnumAssigneeFormFields(this.Base.CurrentSiteMapNode.ScreenID, formName, graph, tables, names, fields, new Func<System.Type, IEnumerable<string>>(OwnerAttribute.GetFields));
  }

  public class OwnerNameSelectorAttribute : PXCustomSelectorAttribute
  {
    public OwnerNameSelectorAttribute()
      : base(typeof (CacheEntityItem.path))
    {
      ((PXSelectorAttribute) this).DescriptionField = typeof (CacheEntityItem.name);
      ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) 16 /*0x10*/;
    }

    public virtual IEnumerable GetRecords()
    {
      TaskTemplateMaintGraphExtension graph = this._Graph.GetExtension<TaskTemplateMaintGraphExtension>();
      foreach (object screenOwnerUserItem in graph.screenOwnerUserItems())
        yield return screenOwnerUserItem;
      foreach (object screenOwnerEntityItem in graph.screenOwnerEntityItems("Entity"))
        yield return screenOwnerEntityItem;
    }
  }
}
