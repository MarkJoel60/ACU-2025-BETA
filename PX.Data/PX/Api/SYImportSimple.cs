// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportSimple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.ImportSimple;
using PX.Api.Models;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.Api.Export.MappingFieldNameTree;
using PX.Data.Description;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Api;

internal static class SYImportSimple
{
  public const string INCONTEXT_PARAM = "InContext";
  private const string FILE_SESSION_KEY = "FileForImport";

  public static void LoadFileFromSession(SYMappingSimpleProperty properties)
  {
    properties.File = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["FileForImport"];
  }

  public static bool ScenarioPropertiesAreValid(
    SYMappingSimpleProperty properties,
    out PXException exception)
  {
    exception = (PXException) null;
    if (properties != null)
    {
      if (!SYImportSimple.ScreenIDIsValid(properties.ScreenID))
        exception = new PXException("Screen Name is empty or the screen doesn't support the Copy and Paste actions.");
      else if (!SYImportSimple.ScenarioNameIsValid(properties.Name))
        exception = new PXException("Scenario Name is empty or a scenario or provider with this name already exists in the system.");
      else if (!SYImportSimple.ProviderTypeIsValid(properties.ProviderType))
        exception = new PXException("Provider Type is empty or its value is not permitted.");
    }
    return exception == null;
  }

  private static bool ScenarioNameIsValid(string scenarioName)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(scenarioName))
    {
      PXGraph graph = new PXGraph();
      if (PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.name, Equal<Required<SYMapping.name>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) scenarioName).Count == 0)
      {
        if (PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.name, Equal<Required<SYProvider.name>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) scenarioName).Count == 0)
          flag = true;
      }
    }
    return flag;
  }

  private static bool ProviderTypeIsValid(string providerType)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(providerType))
      flag = new PXSYProviderSelector().GetRecords().Cast<PXSYProviderSelector.ProviderRec>().Any<PXSYProviderSelector.ProviderRec>((Func<PXSYProviderSelector.ProviderRec, bool>) (provider => provider.TypeName == providerType));
    return flag;
  }

  private static System.Type GetGraphType(string screenID)
  {
    System.Type graphType = (System.Type) null;
    if (!string.IsNullOrEmpty(screenID))
    {
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
      if (screenIdUnsecure != null && !string.IsNullOrEmpty(screenIdUnsecure.GraphType))
        graphType = GraphHelper.GetType(screenIdUnsecure.GraphType);
    }
    return graphType;
  }

  private static SYProvider GetProvider(string providerName, string providerType)
  {
    return new SYProvider()
    {
      ProviderID = new Guid?(Guid.NewGuid()),
      Name = providerName,
      ProviderType = providerType,
      IsActive = new bool?(true),
      ParameterCntr = new short?((short) 0),
      ObjectCntr = new short?((short) 0)
    };
  }

  private static SYMappingActive GetMapping(
    string screenID,
    string mappingName,
    Guid? providerID,
    string providerObject)
  {
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenID);
    if (screenInfo == null)
      throw new PXException("This form cannot be automated.");
    SYMappingActive mapping = new SYMappingActive();
    mapping.MappingID = new Guid?(Guid.NewGuid());
    mapping.Name = mappingName;
    mapping.ScreenID = screenID;
    mapping.IsSimpleMapping = new bool?(true);
    mapping.IsActive = new bool?(true);
    mapping.GraphName = screenInfo.GraphName;
    mapping.ViewName = screenInfo.PrimaryView;
    mapping.ProviderID = providerID;
    mapping.ProviderObject = providerObject;
    mapping.MappingType = "I";
    mapping.FieldCntr = new short?((short) 0);
    return mapping;
  }

  private static IEnumerable<SYMappingField> GetSYMappingFields(Guid mappingID)
  {
    return PXSelectBase<SYMappingField, PXSelectReadonly<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMappingField.mappingID>>>, OrderBy<Asc<SYMappingField.orderNumber>>>.Config>.Select(new PXGraph(), (object) mappingID).AsEnumerable<PXResult<SYMappingField>>().Select<PXResult<SYMappingField>, SYMappingField>((Func<PXResult<SYMappingField>, SYMappingField>) (field => field[typeof (SYMappingField)] as SYMappingField));
  }

  private static SYProviderMaint InitializeProviderInfo(
    SYProvider provider,
    FileInfo file,
    bool refreshExistingProvider)
  {
    SYProviderMaint instance = PXGraph.CreateInstance<SYProviderMaint>();
    instance.provider = SYProviderMaint.TryToCreateProvider(provider.ProviderType);
    instance.Providers.Cache.Insert((object) provider);
    if (refreshExistingProvider)
      instance.Providers.Cache.SetStatus((object) provider, PXEntryStatus.Notchanged);
    instance.Parameters.Cache.Clear();
    instance.ReloadParameters.Press();
    SYProviderParameter providerParameter1 = new SYProviderParameter()
    {
      ProviderID = provider.ProviderID,
      Name = "InContext",
      Value = bool.TrueString
    };
    SYProviderParameter providerParameter2 = instance.Parameters.Cache.Insert((object) providerParameter1) as SYProviderParameter;
    PXContext.SetSlot<FileInfo>("InContext", file);
    instance.FillSchemaObjects.Press();
    instance.Objects.Current = (SYProviderObject) instance.Objects.Select()[0];
    instance.Objects.Current.IsActive = new bool?(true);
    instance.FillSchemaFields.Press();
    instance.Parameters.Cache.Delete((object) providerParameter2);
    return instance;
  }

  private static void InitializePropertyToRefresh(
    SYMappingSimpleProperty properties,
    SYMappingActive mappingToRefresh)
  {
    properties.Name = mappingToRefresh.Name;
    properties.ScreenID = mappingToRefresh.ScreenID;
    properties.ProviderType = ImportSimpleDefaulter.GetDefaultProviderType(properties.File.OriginalName);
    properties.Mapping = mappingToRefresh;
  }

  private static void InsertMappingInfo(SYMappingSimpleProperty properties)
  {
    properties.ProviderGraph = SYImportSimple.InitializeProviderInfo(SYImportSimple.GetProvider(properties.Name, properties.ProviderType), properties.File, false);
    Guid? providerId = properties.ProviderGraph.Providers.Current.ProviderID;
    string name = ((SYProviderObject) properties.ProviderGraph.Objects.Select()[0]).Name;
    properties.Mapping = SYImportSimple.GetMapping(properties.ScreenID, properties.Name, providerId, name);
  }

  private static void InsertMappingInfoRefresh(SYMappingSimpleProperty properties)
  {
    SYProvider provider = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYMapping.providerID>>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) properties.Mapping.ProviderID);
    properties.ProviderGraph = SYImportSimple.InitializeProviderInfo(provider, properties.File, true);
  }

  private static void InsertMappingFields(
    SYMappingSimpleProperty properties,
    PXSelectBase<SYMappingFieldSimple> mappingFields,
    bool refreshExistingMapping)
  {
    if (!refreshExistingMapping)
      SYImportSimple.DeleteOldMappingFields(mappingFields);
    List<SYImportSimple.NameDisplayNameModel> displayNameModelList = new List<SYImportSimple.NameDisplayNameModel>()
    {
      new SYImportSimple.NameDisplayNameModel()
      {
        Name = (string) null,
        DisplayName = string.Empty
      }
    };
    int num = 0;
    List<SYMappingFieldSimple> newFieldList = new List<SYMappingFieldSimple>();
    foreach (PXResult<SYProviderField> pxResult in properties.ProviderGraph.Fields.Select())
    {
      SYProviderField syProviderField = (SYProviderField) pxResult;
      displayNameModelList.Add(new SYImportSimple.NameDisplayNameModel()
      {
        Name = syProviderField.Name,
        DisplayName = syProviderField.DisplayName
      });
      SYMappingFieldSimple mappingFieldSimple = new SYMappingFieldSimple();
      mappingFieldSimple.MappingID = properties.Mapping.MappingID;
      mappingFieldSimple.IsActive = new bool?(true);
      mappingFieldSimple.Value = syProviderField.Name;
      mappingFieldSimple.IsKey = new bool?(false);
      mappingFieldSimple.OrderNumber = new int?(num);
      SYMappingFieldSimple newField = mappingFieldSimple;
      ++num;
      newFieldList.Add(newField);
      if (refreshExistingMapping)
        SYImportSimple.AdjustExistingMappingFields(mappingFields, newField);
      else
        mappingFields.Insert(newField);
    }
    if (refreshExistingMapping)
      SYImportSimple.DeleteObsoleteMappingFields(mappingFields, (IEnumerable<SYMappingFieldSimple>) newFieldList);
    properties.ValueFieldList = displayNameModelList;
  }

  private static void DeleteObsoleteMappingFields(
    PXSelectBase<SYMappingFieldSimple> mappingFields,
    IEnumerable<SYMappingFieldSimple> newFieldList)
  {
    foreach (SYMappingFieldSimple mappingFieldSimple in mappingFields.Select().AsEnumerable<PXResult<SYMappingFieldSimple>>().Select<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>((Func<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>) (result => (SYMappingFieldSimple) result)).Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => newFieldList.All<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (newField => newField.Value != field.Value)))))
      mappingFields.Delete(mappingFieldSimple);
  }

  private static void AdjustExistingMappingFields(
    PXSelectBase<SYMappingFieldSimple> mappingFields,
    SYMappingFieldSimple newField)
  {
    SYMappingFieldSimple[] array = mappingFields.Select().Select<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>((Expression<Func<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>>) (result => (SYMappingFieldSimple) result)).Where<SYMappingFieldSimple>((Expression<Func<SYMappingFieldSimple, bool>>) (field => field.Value == newField.Value)).ToArray<SYMappingFieldSimple>();
    if (array.Length != 0)
    {
      foreach (SYMappingFieldSimple mappingFieldSimple in ((IEnumerable<SYMappingFieldSimple>) array).Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field =>
      {
        int? orderNumber1 = field.OrderNumber;
        int? orderNumber2 = newField.OrderNumber;
        return !(orderNumber1.GetValueOrDefault() == orderNumber2.GetValueOrDefault() & orderNumber1.HasValue == orderNumber2.HasValue);
      })))
      {
        mappingFieldSimple.OrderNumber = newField.OrderNumber;
        mappingFields.Update(mappingFieldSimple);
      }
    }
    else
      mappingFields.Insert(newField);
  }

  private static List<SYImportSimple.NameDisplayNameModel> GetObjectNameList(
    PXViewDescription[] containers)
  {
    IList<string> nameKeysList = (IList<string>) new List<string>()
    {
      (string) null
    };
    IList<string> nameLabelsList = (IList<string>) new List<string>()
    {
      string.Empty
    };
    foreach (PXViewDescription container in containers)
    {
      nameKeysList.Add(container.ViewName);
      nameLabelsList.Add(container.DisplayName);
    }
    SYMappingMaint<SYMapping.mappingType.typeImport>.MakeObjectNameLabelsDistinct(nameKeysList, nameLabelsList);
    List<SYImportSimple.NameDisplayNameModel> objectNameList = new List<SYImportSimple.NameDisplayNameModel>();
    for (int index = 0; index < nameKeysList.Count; ++index)
      objectNameList.Add(new SYImportSimple.NameDisplayNameModel()
      {
        Name = nameKeysList[index],
        DisplayName = nameLabelsList[index]
      });
    objectNameList.Sort((Comparison<SYImportSimple.NameDisplayNameModel>) ((x, y) => string.CompareOrdinal(x.Name, y.Name)));
    return objectNameList;
  }

  private static void CreateObjectNameList(SYMappingSimpleProperty properties)
  {
    PXSiteMap.ScreenInfo info = ScreenUtils.ScreenInfo.TryGet(properties.ScreenID);
    if (info == null)
      return;
    System.Type graphType = SYImportSimple.GetGraphType(properties.ScreenID);
    if (graphType == (System.Type) null)
      return;
    PXGraph instance = PXGraph.CreateInstance(graphType);
    List<Container> containers;
    List<Command> cpCommands;
    PXCopyPasteData<PXGraph>.GetScript(instance, properties.ScreenID, true, out cpCommands, out containers);
    if (containers == null)
      return;
    PXViewDescription[] array = info.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (infoContainer => infoContainer.Key.OrdinalEquals(info.PrimaryView) || cpCommands.Any<Command>((Func<Command, bool>) (cpCmd => cpCmd.ObjectName.OrdinalEquals(infoContainer.Key))))).Select<KeyValuePair<string, PXViewDescription>, PXViewDescription>((Func<KeyValuePair<string, PXViewDescription>, PXViewDescription>) (infoContainer => infoContainer.Value)).ToArray<PXViewDescription>();
    properties.ObjectNameList = SYImportSimple.GetObjectNameList(array);
    SYImportSimple.CreateKeyObjectFieldDictionary(properties, (IEnumerable<PXViewDescription>) array);
    SYImportSimple.CreateObjectFieldDictionary(properties, (IEnumerable<PXViewDescription>) array, (IEnumerable<Command>) cpCommands);
    SYImportSimple.CreateKeyObjectNameList(properties, array, info.PrimaryView);
    foreach (string key in properties.ObjectFieldDictionary.Keys.ToArray<string>())
    {
      System.Type itemType = instance.Views[StringExtensions.FirstSegment(key, ':')].GetItemType();
      if (itemType.IsDefined(typeof (PXPossibleRowsListAttribute), true))
      {
        string idField;
        string valueField;
        List<string> possibleRows = (itemType.GetCustomAttributes(typeof (PXPossibleRowsListAttribute), true)[0] as PXPossibleRowsListAttribute).GetPossibleRows(instance, out idField, out valueField);
        properties.ObjectFieldDictionary[key] = ((IEnumerable<SYImportSimple.NameDisplayNameModel>) new SYImportSimple.NameDisplayNameModel[1]
        {
          new SYImportSimple.NameDisplayNameModel()
          {
            Name = (string) null,
            DisplayName = string.Empty
          }
        }).Union<SYImportSimple.NameDisplayNameModel>(possibleRows.Select<string, SYImportSimple.NameDisplayNameModel>((Func<string, SYImportSimple.NameDisplayNameModel>) (s => new SYImportSimple.NameDisplayNameModel()
        {
          Name = s,
          DisplayName = s
        }))).OrderBy<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (model => model.DisplayName)).ToArray<SYImportSimple.NameDisplayNameModel>();
        properties.AttributeLikeGrids[key] = new KeyValuePair<string, string>(idField, valueField);
        properties.KeyObjectNameList.Remove(key);
      }
    }
  }

  private static void CreateKeyObjectNameList(
    SYMappingSimpleProperty properties,
    PXViewDescription[] validContainers,
    string primaryView)
  {
    properties.KeyObjectNameList = new List<string>();
    if (((IEnumerable<PXViewDescription>) validContainers).Any<PXViewDescription>((Func<PXViewDescription, bool>) (container => container.ViewName == primaryView && ((IEnumerable<PX.Data.Description.FieldInfo>) container.Fields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => field.IsKey)))))
      properties.KeyObjectNameList.Add(primaryView);
    properties.KeyObjectNameList.AddRange(((IEnumerable<PXViewDescription>) validContainers).Where<PXViewDescription>((Func<PXViewDescription, bool>) (container => container.HasLineNumber && container.ViewName != primaryView)).Select<PXViewDescription, string>((Func<PXViewDescription, string>) (container => container.ViewName)));
  }

  private static void CreateKeyObjectFieldDictionary(
    SYMappingSimpleProperty properties,
    IEnumerable<PXViewDescription> viewsDescriptions)
  {
    properties.KeyObjectFieldDictionary = new Dictionary<string, SYImportSimple.KeyFieldModel[]>();
    foreach (PXViewDescription pxViewDescription in viewsDescriptions.Where<PXViewDescription>((Func<PXViewDescription, bool>) (description => ((IEnumerable<PX.Data.Description.FieldInfo>) description.Fields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => field.IsKey)))))
      properties.KeyObjectFieldDictionary[pxViewDescription.ViewName] = ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription.Fields).Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => field.IsKey)).Select<PX.Data.Description.FieldInfo, SYImportSimple.KeyFieldModel>((Func<PX.Data.Description.FieldInfo, SYImportSimple.KeyFieldModel>) (field => new SYImportSimple.KeyFieldModel()
      {
        Name = field.FieldName,
        Invisible = field.Invisible
      })).ToArray<SYImportSimple.KeyFieldModel>();
  }

  private static void CreateObjectFieldDictionary(
    SYMappingSimpleProperty properties,
    IEnumerable<PXViewDescription> viewsDescriptions,
    IEnumerable<Command> commands)
  {
    properties.ObjectFieldDictionary = viewsDescriptions.ToDictionary<PXViewDescription, string, SYImportSimple.NameDisplayNameModel[]>((Func<PXViewDescription, string>) (description => description.ViewName), (Func<PXViewDescription, SYImportSimple.NameDisplayNameModel[]>) (description => ((IEnumerable<SYImportSimple.NameDisplayNameModel>) new SYImportSimple.NameDisplayNameModel[1]
    {
      new SYImportSimple.NameDisplayNameModel()
      {
        Name = (string) null,
        DisplayName = string.Empty
      }
    }).Union<SYImportSimple.NameDisplayNameModel>(((IEnumerable<PX.Data.Description.FieldInfo>) description.Fields).Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => properties.KeyObjectFieldDictionary.ContainsKey(description.ViewName) && ((IEnumerable<SYImportSimple.KeyFieldModel>) properties.KeyObjectFieldDictionary[description.ViewName]).Any<SYImportSimple.KeyFieldModel>((Func<SYImportSimple.KeyFieldModel, bool>) (f => f.Name.Equals(field.FieldName, StringComparison.Ordinal))) || commands.Any<Command>((Func<Command, bool>) (cmd => cmd.FieldName == field.FieldName && cmd.ObjectName == description.ViewName)))).Select<PX.Data.Description.FieldInfo, SYImportSimple.NameDisplayNameModel>((Func<PX.Data.Description.FieldInfo, SYImportSimple.NameDisplayNameModel>) (field => new SYImportSimple.NameDisplayNameModel()
    {
      Name = field.FieldName,
      DisplayName = field.DisplayName
    }))).OrderBy<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (model => model.DisplayName)).ToArray<SYImportSimple.NameDisplayNameModel>()));
  }

  private static void SaveProviderInfo(SYProviderMaint providerGraph, FileInfo file)
  {
    SYImportSimple.AttachFileToProvider(providerGraph, file);
    providerGraph.Persist();
  }

  private static void AttachFileToProvider(SYProviderMaint providerGraph, FileInfo file)
  {
    PXGraph.CreateInstance<UploadFileMaintenance>().SaveFile(file, FileExistsAction.CreateVersion);
    if (!file.UID.HasValue)
      return;
    PXNoteAttribute.SetFileNotes(providerGraph.Providers.Cache, providerGraph.Providers.Cache.Current, file.UID.Value);
  }

  private static void SaveMappingInfo(
    ref SYMappingActive mapping,
    string screenID,
    Dictionary<string, KeyValuePair<string, string>> attributeLikeGrids,
    SYMappingFieldSimple[] mappingFields,
    Dictionary<string, SYImportSimple.KeyFieldModel[]> keyFieldDictionary,
    List<SYImportSimple.NameDisplayNameModel> valueFieldList,
    bool recreateMappingFields)
  {
    SYImportMaint instance1 = PXGraph.CreateInstance<SYImportMaint>();
    instance1.SelectTimeStamp();
    instance1.Mappings.Cache.Clear();
    instance1.FieldMappings.Cache.Clear();
    instance1.Mappings.Cache.Insert((object) mapping);
    if (recreateMappingFields)
    {
      instance1.Mappings.Cache.SetStatus((object) mapping, PXEntryStatus.Notchanged);
      instance1.Mappings.Cache.IsDirty = false;
      foreach (PXResult<SYMappingField> pxResult in instance1.FieldMappings.Select())
      {
        SYMappingField syMappingField = (SYMappingField) pxResult;
        instance1.FieldMappings.Cache.Delete((object) syMappingField);
      }
      mapping.FieldCntr = new short?((short) 0);
      instance1.Mappings.Update((SYMapping) mapping);
    }
    System.Type graphType = SYImportSimple.GetGraphType(screenID);
    if (!(graphType != (System.Type) null))
      return;
    PXGraph instance2 = PXGraph.CreateInstance(graphType);
    List<Command> script;
    PXCopyPasteData<PXGraph>.GetScript(instance2, screenID, true, out script, out List<Container> _);
    if (script == null)
      return;
    List<SYMappingFieldSimple> keyFieldsSimple = new List<SYMappingFieldSimple>();
    List<SYMappingFieldSimple> formulaKeyFieldsSimple = new List<SYMappingFieldSimple>();
    foreach (SYMappingFieldSimple mappingField in mappingFields)
    {
      SYMappingFieldSimple field = mappingField;
      if (!string.IsNullOrEmpty(field.ObjectName) && !string.IsNullOrEmpty(field.FieldName))
      {
        if (keyFieldDictionary.ContainsKey(field.ObjectName) && ((IEnumerable<SYImportSimple.KeyFieldModel>) keyFieldDictionary[field.ObjectName]).Any<SYImportSimple.KeyFieldModel>((Func<SYImportSimple.KeyFieldModel, bool>) (f => f.Name.Equals(field.FieldName, StringComparison.Ordinal))))
          keyFieldsSimple.Add(field);
        else if (valueFieldList != null && valueFieldList.All<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (value => value.Name != field.Value)))
        {
          bool? isKey = field.IsKey;
          bool flag = true;
          if (isKey.GetValueOrDefault() == flag & isKey.HasValue)
            formulaKeyFieldsSimple.Add(field);
        }
      }
    }
    foreach (SYMappingField field in new FieldsSimpleToFieldsConverter((IEnumerable<SYMappingFieldSimple>) mappingFields, keyFieldsSimple, formulaKeyFieldsSimple, (IEnumerable<Command>) script, mapping.MappingID, mapping.ScreenID, attributeLikeGrids, instance2).ConvertToFields())
      instance1.FieldMappings.Cache.Insert((object) field);
    instance1.Persist();
    mapping = (SYMappingActive) PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMapping.mappingID, Equal<Required<SYMapping.mappingID>>>>.Config>.Select(new PXGraph(), (object) mapping.MappingID);
  }

  private static void DeleteOldMappingFields(PXSelectBase<SYMappingFieldSimple> mappingFields)
  {
    foreach (PXResult<SYMappingFieldSimple> pxResult in mappingFields.Select())
    {
      SYMappingFieldSimple mappingFieldSimple = (SYMappingFieldSimple) pxResult;
      mappingFields.Delete(mappingFieldSimple);
    }
  }

  public static string GenerateObjectKeyWarning(
    SYMappingSimpleProperty properties,
    string objectName)
  {
    return $"You have not provided external keys for the target object {properties.ObjectNameList.Where<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (obj => obj.Name == objectName)).Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (obj => obj.DisplayName)).FirstOrDefault<string>() ?? objectName}. As a result, every prepared source record will be treated as a separate object. Do you want to correct the scenario before saving?";
  }

  public static void CreateMappingInMemory(
    SYMappingSimpleProperty properties,
    PXSelectBase<SYMappingFieldSimple> mappingFields,
    SYMappingActive mappingToRefresh)
  {
    bool? refreshExistingMapping1 = properties.RefreshExistingMapping;
    bool flag1 = true;
    if (refreshExistingMapping1.GetValueOrDefault() == flag1 & refreshExistingMapping1.HasValue)
    {
      SYImportSimple.InitializePropertyToRefresh(properties, mappingToRefresh);
      SYImportSimple.InsertMappingInfoRefresh(properties);
    }
    else
      SYImportSimple.InsertMappingInfo(properties);
    SYMappingSimpleProperty properties1 = properties;
    PXSelectBase<SYMappingFieldSimple> mappingFields1 = mappingFields;
    bool? refreshExistingMapping2 = properties.RefreshExistingMapping;
    bool flag2 = true;
    int num = refreshExistingMapping2.GetValueOrDefault() == flag2 & refreshExistingMapping2.HasValue ? 1 : 0;
    SYImportSimple.InsertMappingFields(properties1, mappingFields1, num != 0);
    SYImportSimple.CreateObjectNameList(properties);
  }

  public static void PopulateValueFieldList(
    SYMappingSimpleProperty properties,
    PXCache fieldCache,
    SYMappingFieldSimple mappingField)
  {
    if (properties != null && properties.ValueFieldList != null)
      PXStringListAttribute.SetList<SYMappingField.value>(fieldCache, (object) mappingField, properties.ValueFieldList.Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (value => value.Name)).ToArray<string>(), properties.ValueFieldList.Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (value => value.DisplayName)).ToArray<string>());
    else
      PXStringListAttribute.SetList<SYMappingField.value>(fieldCache, (object) mappingField, new string[1], new string[1]
      {
        ""
      });
  }

  public static void PopulateObjectNameList(
    SYMappingSimpleProperty properties,
    PXCache fieldCache,
    SYMappingFieldSimple mappingField)
  {
    if (properties != null && properties.ObjectNameList != null)
      PXStringListAttribute.SetList<SYMappingField.objectName>(fieldCache, (object) mappingField, properties.ObjectNameList.Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (value => value.Name)).ToArray<string>(), properties.ObjectNameList.Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (value => value.DisplayName)).ToArray<string>());
    else
      PXStringListAttribute.SetList<SYMappingField.objectName>(fieldCache, (object) mappingField, new string[1], new string[1]
      {
        ""
      });
  }

  public static void PopulateFieldNameList(
    SYMappingSimpleProperty properties,
    PXCache fieldCache,
    SYMappingFieldSimple mappingField,
    bool isNewUI,
    MappingFieldTreeNodeFactory nodeFactory,
    MappingFieldNodeKeyParser nodeKeyParser)
  {
    if (properties.ObjectFieldDictionary == null)
      PXStringListAttribute.SetList<SYMappingFieldSimple.fieldName>(fieldCache, (object) mappingField, Array.Empty<string>(), Array.Empty<string>());
    else if (isNewUI)
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      if (nodeFactory != null && nodeKeyParser != null && properties.ObjectNameList != null && properties.ObjectFieldDictionary != null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        IEnumerable<MappingFieldTreeNode> mappingFieldTreeNodes = properties.ObjectNameList.Where<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (n => !string.IsNullOrEmpty(n.Name))).Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (n => n.Name)).Distinct<string>().Select<string, string>(SYImportSimple.\u003C\u003EO.\u003C0\u003E__GetViewNodeKey ?? (SYImportSimple.\u003C\u003EO.\u003C0\u003E__GetViewNodeKey = new Func<string, string>(MappingFieldNodeTextGenerator.GetViewNodeKey))).SelectMany<string, MappingFieldTreeNode>(new Func<string, IEnumerable<MappingFieldTreeNode>>(nodeFactory.CreateChildNodes));
        List<SYImportSimple.NameDisplayNameModel> second = new List<SYImportSimple.NameDisplayNameModel>();
        foreach (MappingFieldTreeNode mappingFieldTreeNode in mappingFieldTreeNodes)
        {
          (string str1, string str2) = nodeKeyParser.ParseNodeKey(mappingFieldTreeNode.Key, false);
          SYImportSimple.NameDisplayNameModel[] source;
          if (properties.ObjectFieldDictionary.TryGetValue(str1, out source))
          {
            SYImportSimple.NameDisplayNameModel displayNameModel = ((IEnumerable<SYImportSimple.NameDisplayNameModel>) source).FirstOrDefault<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (f => string.Equals(f.Name, str2, StringComparison.Ordinal)));
            if (displayNameModel != null)
            {
              second.Add(displayNameModel);
              stringList1.Add(str2);
              stringList2.Add(mappingFieldTreeNode.Text);
            }
          }
        }
        foreach (SYImportSimple.NameDisplayNameModel displayNameModel in properties.ObjectFieldDictionary.SelectMany<KeyValuePair<string, SYImportSimple.NameDisplayNameModel[]>, SYImportSimple.NameDisplayNameModel>((Func<KeyValuePair<string, SYImportSimple.NameDisplayNameModel[]>, IEnumerable<SYImportSimple.NameDisplayNameModel>>) (d => (IEnumerable<SYImportSimple.NameDisplayNameModel>) d.Value)).Except<SYImportSimple.NameDisplayNameModel>((IEnumerable<SYImportSimple.NameDisplayNameModel>) second).Where<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (f => !string.IsNullOrEmpty(f.Name))))
        {
          stringList1.Add(displayNameModel.Name);
          stringList2.Add(displayNameModel.DisplayName);
        }
      }
      PXStringListAttribute.SetList<SYMappingFieldSimple.fieldName>(fieldCache, (object) mappingField, stringList1.ToArray(), stringList2.ToArray());
    }
    else
    {
      SYImportSimple.NameDisplayNameModel[] source;
      if (!string.IsNullOrEmpty(mappingField.ObjectName) && properties.ObjectFieldDictionary.TryGetValue(mappingField.ObjectName, out source))
        PXStringListAttribute.SetList<SYMappingFieldSimple.fieldName>(fieldCache, (object) mappingField, ((IEnumerable<SYImportSimple.NameDisplayNameModel>) source).Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (field => field.Name)).ToArray<string>(), ((IEnumerable<SYImportSimple.NameDisplayNameModel>) source).Select<SYImportSimple.NameDisplayNameModel, string>((Func<SYImportSimple.NameDisplayNameModel, string>) (field => field.DisplayName)).ToArray<string>());
      else
        PXStringListAttribute.SetList<SYMappingFieldSimple.fieldName>(fieldCache, (object) mappingField, Array.Empty<string>(), Array.Empty<string>());
    }
  }

  public static bool IsKeyField(
    SYMappingSimpleProperty properties,
    PXCache mappingCache,
    SYMappingFieldSimple mappingField)
  {
    bool flag = false;
    if (properties != null && !string.IsNullOrEmpty(mappingField.FieldName) && properties.KeyObjectFieldDictionary != null)
    {
      SYImportSimple.KeyFieldModel[] source;
      flag = properties.KeyObjectFieldDictionary.TryGetValue(mappingField.ObjectName, out source) && ((IEnumerable<SYImportSimple.KeyFieldModel>) source).Any<SYImportSimple.KeyFieldModel>((Func<SYImportSimple.KeyFieldModel, bool>) (f => f.Name.Equals(mappingField.FieldName, StringComparison.Ordinal)));
    }
    return flag;
  }

  public static void SetMappingKeyEnabling(
    SYMappingSimpleProperty properties,
    PXCache mappingCache,
    SYMappingFieldSimple mappingField)
  {
    if (properties == null || properties.KeyObjectNameList == null || string.IsNullOrEmpty(mappingField.ObjectName))
      return;
    bool isEnabled = properties.KeyObjectNameList.Contains(mappingField.ObjectName);
    PXUIFieldAttribute.SetEnabled<SYMappingFieldSimple.isKey>(mappingCache, (object) mappingField, isEnabled);
  }

  public static bool ScreenIDIsValid(string screenID)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(screenID))
    {
      PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
      if (screenIdUnsecure != null)
        flag = PXSiteMapFilterRuleStorage.HasCopyPaste(screenIdUnsecure);
    }
    return flag;
  }

  public static void Save(
    SYMappingSimpleProperty properties,
    SYMappingFieldSimple[] mappingFields,
    bool attachFileToProvider,
    bool recreateMappingFields)
  {
    if (properties.Mapping == null || properties.AttributeLikeGrids == null || string.IsNullOrEmpty(properties.ScreenID))
      return;
    if (attachFileToProvider)
      SYImportSimple.SaveProviderInfo(properties.ProviderGraph, properties.File);
    SYMappingActive mapping = properties.Mapping;
    SYImportSimple.SaveMappingInfo(ref mapping, properties.ScreenID, properties.AttributeLikeGrids, mappingFields, properties.KeyObjectFieldDictionary, properties.ValueFieldList, recreateMappingFields);
    properties.Mapping = mapping;
  }

  public static IEnumerable<string> GetSaveWarnings(
    SYMappingSimpleProperty properties,
    SYMappingFieldSimple[] mappingFields)
  {
    string[] array = ((IEnumerable<SYMappingFieldSimple>) mappingFields).Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => properties.KeyObjectNameList.Contains(field.ObjectName))).Select<SYMappingFieldSimple, string>((Func<SYMappingFieldSimple, string>) (field => field.ObjectName)).Distinct<string>().Where<string>((Func<string, bool>) (objectName => properties.KeyObjectFieldDictionary.ContainsKey(objectName) && ((IEnumerable<SYImportSimple.KeyFieldModel>) properties.KeyObjectFieldDictionary[objectName]).Any<SYImportSimple.KeyFieldModel>((Func<SYImportSimple.KeyFieldModel, bool>) (f => !f.Invisible)))).ToArray<string>();
    return array.Length != 0 ? ((IEnumerable<string>) array).Where<string>((Func<string, bool>) (name => !((IEnumerable<SYMappingFieldSimple>) mappingFields).Any<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field =>
    {
      if (!(field.ObjectName == name))
        return false;
      bool? isKey = field.IsKey;
      bool flag = true;
      return isKey.GetValueOrDefault() == flag & isKey.HasValue;
    })))).Select<string, string>((Func<string, string>) (obj => SYImportSimple.GenerateObjectKeyWarning(properties, obj))) : (IEnumerable<string>) new string[0];
  }

  public static void LoadSavedMapping(SYMappingSimpleProperty properties, PXCache fieldCache)
  {
    if (properties == null || fieldCache == null)
      return;
    fieldCache.Clear();
    SYImportSimple.CreateObjectNameList(properties);
    Guid? mappingId = properties.MappingID;
    if (!mappingId.HasValue)
      return;
    mappingId = properties.MappingID;
    IEnumerable<SYMappingField> syMappingFields = SYImportSimple.GetSYMappingFields(mappingId.Value);
    mappingId = properties.MappingID;
    Guid mappingID = mappingId.Value;
    Dictionary<string, KeyValuePair<string, string>> attributeLikeGrids = properties.AttributeLikeGrids;
    string screenId = properties.Mapping.ScreenID;
    FieldsToFieldsSimpleConverter fieldsSimpleConverter = new FieldsToFieldsSimpleConverter(syMappingFields, mappingID, attributeLikeGrids, screenId);
    bool isDirty = fieldCache.IsDirty;
    HashSet<string> stringSet = new HashSet<string>();
    int num = 0;
    foreach (SYMappingFieldSimple simpleField in fieldsSimpleConverter.ConvertToSimpleFields())
    {
      if (fieldCache.GetStatus((object) simpleField) == PXEntryStatus.Notchanged)
      {
        simpleField.OrderNumber = new int?(num);
        fieldCache.Insert((object) simpleField);
        fieldCache.SetStatus((object) simpleField, PXEntryStatus.Held);
      }
      else
        ((SYMappingField) fieldCache.Locate((object) simpleField)).OrderNumber = new int?(num);
      if (!string.IsNullOrWhiteSpace(simpleField.Value))
        stringSet.Add(simpleField.Value);
      ++num;
    }
    List<SYImportSimple.NameDisplayNameModel> displayNameModelList = new List<SYImportSimple.NameDisplayNameModel>()
    {
      new SYImportSimple.NameDisplayNameModel()
      {
        Name = (string) null,
        DisplayName = string.Empty
      }
    };
    foreach (PXResult<SYProviderField> pxResult in PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Required<SYProvider.providerID>>, And<SYProviderField.objectName, Equal<Required<SYProviderObject.name>>>>, OrderBy<Asc<SYProviderField.providerID, Asc<SYProviderField.objectName, Asc<SYProviderField.lineNbr>>>>>.Config>.Select(fieldCache.Graph, (object) properties.Mapping.ProviderID, (object) properties.Mapping.ProviderObject))
    {
      SYProviderField syProviderField = (SYProviderField) pxResult;
      if (!string.IsNullOrWhiteSpace(syProviderField.Name))
      {
        displayNameModelList.Add(new SYImportSimple.NameDisplayNameModel()
        {
          Name = syProviderField.Name,
          DisplayName = syProviderField.DisplayName
        });
        if (!stringSet.Contains(syProviderField.Name))
        {
          SYMappingFieldSimple mappingFieldSimple1 = new SYMappingFieldSimple();
          mappingFieldSimple1.MappingID = properties.Mapping.MappingID;
          mappingFieldSimple1.IsActive = new bool?(false);
          mappingFieldSimple1.Value = syProviderField.Name;
          mappingFieldSimple1.IsKey = new bool?(false);
          mappingFieldSimple1.OrderNumber = new int?(num);
          SYMappingFieldSimple mappingFieldSimple2 = mappingFieldSimple1;
          fieldCache.Insert((object) mappingFieldSimple2);
          ++num;
        }
      }
    }
    properties.ValueFieldList = displayNameModelList;
    fieldCache.IsDirty = isDirty;
  }

  public static void SetMappingSimpleProperty(
    SYMappingSimpleProperty properties,
    SYMappingActive mapping)
  {
    if (properties == null || mapping == null)
      return;
    properties.Mapping = mapping;
    properties.ScreenID = mapping.ScreenID;
  }

  public static void ClearMappingSimpleInfo(
    SYMappingSimpleProperty properties,
    PXCache mappingCache)
  {
    if (properties == null || mappingCache == null)
      return;
    properties.Mapping = (SYMappingActive) null;
    properties.ScreenID = (string) null;
  }

  public class NameDisplayNameModel
  {
    public string Name { get; set; }

    public string DisplayName { get; set; }
  }

  public class KeyFieldModel
  {
    public string Name { get; set; }

    public bool Invisible { get; set; }
  }
}
