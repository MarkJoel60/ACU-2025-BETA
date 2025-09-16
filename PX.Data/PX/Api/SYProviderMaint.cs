// Decompiled with JetBrains decompiler
// Type: PX.Api.SYProviderMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

public class SYProviderMaint : PXGraph<SYProviderMaint, SYProvider>
{
  public PXSelect<SYProvider> Providers;
  public PXSelect<SYProviderParameter, Where<SYProviderParameter.providerID, Equal<Current<SYProvider.providerID>>>> Parameters;
  public PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Current<SYProvider.providerID>>>> Objects;
  public PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Current<SYProvider.providerID>>, And<SYProviderObject.lineNbr, Equal<Current<SYProviderObject.lineNbr>>>>> CurrentObject;
  public PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYProvider.providerID>>, And<SYProviderField.objectName, Equal<Optional<SYProviderObject.name>>>>, OrderBy<Asc<SYProviderField.providerID, Asc<SYProviderField.objectName, Asc<SYProviderField.lineNbr>>>>> Fields;
  public PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYProvider.providerID>>, And<SYProviderField.objectName, Equal<Optional<SYProviderObject.name>>, And<SYProviderField.lineNbr, Equal<Optional<SYProviderField.lineNbr>>>>>> CurrentField;
  public PXFilter<GetLinkFilterType> GetFileLinkFilter;
  public PXAction<SYProvider> ReloadParameters;
  public PXAction<SYProvider> FillSchemaObjects;
  public PXAction<SYProvider> FillSchemaFields;
  public PXAction<SYProvider> GetFileLink;
  public PXAction<SYProvider> ShowObjectCommand;
  public PXAction<SYProvider> ShowFieldCommand;
  internal IPXSYProvider provider;
  private bool keepFields;
  private Dictionary<string, string> collectedDataTypes = new Dictionary<string, string>();
  private bool autoFillSchemaObjectsIsOn;
  public PXAction<SYProvider> ToggleFieldsActivation;

  public SYProviderMaint()
  {
    System.Type[] typeArray = new System.Type[7]
    {
      typeof (string),
      typeof (System.DateTime),
      typeof (double),
      typeof (Decimal),
      typeof (bool),
      typeof (int),
      typeof (long)
    };
    foreach (System.Type type in typeArray)
      this.collectedDataTypes[type.FullName] = type.Name;
    PXUIFieldAttribute.SetDisplayName<GetLinkFilterType.wikiLink>(this.GetFileLinkFilter.Cache, "Link");
    string[] allowedValues = new string[this.collectedDataTypes.Count];
    string[] allowedLabels = new string[this.collectedDataTypes.Count];
    int index = 0;
    foreach (string key in this.collectedDataTypes.Keys)
    {
      allowedValues[index] = key;
      allowedLabels[index] = this.collectedDataTypes[key];
      ++index;
    }
    PXStringListAttribute.SetList<SYProviderField.dataType>(this.Fields.Cache, (object) null, allowedValues, allowedLabels);
  }

  public override bool CanClipboardCopyPaste() => false;

  [PXButton(Tooltip = "Load parameters for the selected provider.")]
  [PXUIField(DisplayName = "Reload Parameters", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable reloadParameters(PXAdapter adapter)
  {
    this.ReloadParametersInt();
    return adapter.Get();
  }

  private void ReloadParametersInt()
  {
    this.EnsureProvider(false);
    foreach (PXResult<SYProviderParameter> pxResult in this.Parameters.Select())
      this.Parameters.Delete((SYProviderParameter) pxResult);
    IEnumerable<string> source = (this.provider is IPXSYProviderWithEncryptedParameters provider ? provider.EncryptedParameters : (IEnumerable<string>) null) ?? Enumerable.Empty<string>();
    foreach (PXStringState pxStringState in this.provider.GetParametersDefenition())
    {
      SYProviderParameter providerParameter = new SYProviderParameter();
      providerParameter.Name = pxStringState.Name;
      providerParameter.DisplayName = pxStringState.DisplayName;
      if (source.Contains<string>(providerParameter.Name))
        providerParameter.IsEncrypted = new bool?(true);
      providerParameter.Value = pxStringState.Value == null ? (string) null : pxStringState.Value.ToString();
      this.Parameters.Insert(providerParameter);
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Toggle Activation")]
  protected virtual IEnumerable toggleFieldsActivation(PXAdapter adapter)
  {
    int num;
    if (this.Fields.Current != null)
    {
      bool? isActive = this.Fields.Current.IsActive;
      if (isActive.HasValue)
      {
        isActive = this.Fields.Current.IsActive;
        num = isActive.Value ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool flag = num != 0;
    PXResultset<SYProviderField> source = this.Fields.Select();
    Expression<Func<PXResult<SYProviderField>, SYProviderField>> selector = (Expression<Func<PXResult<SYProviderField>, SYProviderField>>) (r => (SYProviderField) r);
    foreach (SYProviderField syProviderField in (IEnumerable<SYProviderField>) source.Select<PXResult<SYProviderField>, SYProviderField>(selector))
    {
      syProviderField.IsActive = new bool?(!flag);
      this.Fields.Update(syProviderField);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Fill in the schema objects by using the selected provider.")]
  [PXUIField(DisplayName = "Fill Schema Objects", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable fillSchemaObjects(PXAdapter adapter)
  {
    this.EnsureProvider(true);
    try
    {
      List<SYProviderObject> syProviderObjectList = new List<SYProviderObject>();
      this.keepFields = true;
      foreach (PXResult<SYProviderObject> pxResult in this.Objects.Select())
      {
        SYProviderObject syProviderObject = (SYProviderObject) pxResult;
        syProviderObjectList.Add((SYProviderObject) this.Objects.Cache.CreateCopy((object) syProviderObject));
        this.Objects.Delete(syProviderObject);
      }
      this.Providers.Cache.SetValueExt<SYProvider.objectCntr>(this.Providers.Cache.Current, (object) (short) 0);
      this.keepFields = false;
      foreach (string schemaObject in this.provider.GetSchemaObjects())
      {
        SYProviderObject o = new SYProviderObject();
        o.Name = schemaObject;
        SYProviderObject syProviderObject = syProviderObjectList.Find((Predicate<SYProviderObject>) (existingField => existingField.Name == o.Name));
        if (syProviderObject != null)
        {
          syProviderObjectList.Remove(syProviderObject);
          o.IsActive = syProviderObject.IsActive;
          o.Command = syProviderObject.Command;
        }
        o.IsCustom = new bool?(false);
        this.Objects.Insert(o);
      }
      foreach (SYProviderObject syProviderObject in syProviderObjectList)
      {
        bool? isCustom = syProviderObject.IsCustom;
        bool flag = true;
        if (isCustom.GetValueOrDefault() == flag & isCustom.HasValue)
        {
          syProviderObject.LineNbr = new short?();
          this.Objects.Insert(syProviderObject);
        }
        else
        {
          foreach (PXResult<SYProviderField> pxResult in this.Fields.Select((object) syProviderObject.Name))
            this.Fields.Delete((SYProviderField) pxResult);
        }
      }
    }
    catch (TargetInvocationException ex)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Fill in the schema fields for the currently selected object by using the selected provider.")]
  [PXUIField(DisplayName = "Fill Schema Fields", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable fillSchemaFields(PXAdapter adapter)
  {
    if (this.Objects.Current == null)
      throw new PXException("A schema object is not selected.");
    this.EnsureProvider(true);
    try
    {
      List<SYProviderField> syProviderFieldList = new List<SYProviderField>();
      foreach (PXResult<SYProviderField> pxResult in this.Fields.Select())
      {
        SYProviderField syProviderField = (SYProviderField) pxResult;
        syProviderFieldList.Add((SYProviderField) this.Fields.Cache.CreateCopy((object) syProviderField));
        this.Fields.Delete(syProviderField);
      }
      this.Objects.Cache.SetValueExt<SYProviderObject.fieldCntr>(this.Objects.Cache.Current, (object) (short) 0);
      foreach (PXFieldState schemaField in this.provider.GetSchemaFields(this.Objects.Current.Command ?? this.Objects.Current.Name))
      {
        SYProviderField f = new SYProviderField();
        f.Name = schemaField.Name;
        f.IsKey = new bool?(schemaField.PrimaryKey);
        f.DisplayName = schemaField.DisplayName;
        System.Type underlyingType = Nullable.GetUnderlyingType(schemaField.DataType);
        f.DataType = underlyingType == (System.Type) null ? schemaField.DataType.FullName : underlyingType.FullName;
        f.DataLength = new int?(schemaField.Length);
        SYProviderField syProviderField = syProviderFieldList.Find((Predicate<SYProviderField>) (existingField => existingField.Name == f.Name));
        if (syProviderField != null)
        {
          syProviderFieldList.Remove(syProviderField);
          f.IsActive = syProviderField.IsActive;
          f.Command = syProviderField.Command;
        }
        f.IsCustom = new bool?(false);
        this.Fields.Insert(f);
      }
      foreach (SYProviderField syProviderField in syProviderFieldList)
      {
        bool? isCustom = syProviderField.IsCustom;
        bool flag = true;
        if (isCustom.GetValueOrDefault() == flag & isCustom.HasValue)
        {
          syProviderField.LineNbr = new short?();
          this.Fields.Insert(syProviderField);
        }
      }
    }
    catch (TargetInvocationException ex)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Get link to the attached file.", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Get File Link", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable getFileLink(PXAdapter adapter)
  {
    this.GetFileLinkFilter.Current.WikiLink = "";
    if (this.Providers.Current != null && this.Providers.Current.NoteID.HasValue)
    {
      UploadFile uploadFile = (UploadFile) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Current<SYProvider.noteID>>>>.Config>.Select((PXGraph) this);
      if (uploadFile != null && !string.IsNullOrEmpty(uploadFile.Name))
        this.GetFileLinkFilter.Current.WikiLink = uploadFile.Name;
    }
    int num = (int) this.GetFileLinkFilter.AskExt(true);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Edit the value in the Command column for the selected object.")]
  [PXUIField(DisplayName = "Edit Command", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable showObjectCommand(PXAdapter adapter)
  {
    if (this.Objects.Current == null)
      throw new PXException("A schema object is not selected.");
    int num = (int) this.CurrentObject.AskExt(true);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Edit the value in the Command column for the selected field.")]
  [PXUIField(DisplayName = "Edit Command", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable showFieldCommand(PXAdapter adapter)
  {
    if (this.Objects.Current == null)
      throw new PXException("A schema object is not selected.");
    if (this.Fields.Current == null)
      throw new PXException("A schema field is not selected.");
    int num = (int) this.CurrentField.AskExt(true);
    return adapter.Get();
  }

  protected virtual void SYProviderObject_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SYProviderObject row = (SYProviderObject) e.Row;
    if (row == null)
      return;
    PXCache cache = sender;
    SYProviderObject data = row;
    bool? isCustom = row.IsCustom;
    bool flag = true;
    int num = isCustom.GetValueOrDefault() == flag & isCustom.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SYProviderField.name>(cache, (object) data, num != 0);
  }

  protected virtual void SYProviderObject_IsActive_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYProviderObject row))
      return;
    bool? isActive = row.IsActive;
    bool flag = true;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    foreach (PXResult<SYProviderField> pxResult in this.Fields.Select())
    {
      SYProviderField syProviderField = (SYProviderField) pxResult;
      syProviderField.IsActive = new bool?(true);
      this.Fields.Update(syProviderField);
    }
  }

  protected virtual void SYProviderField_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row is SYProviderField && (this.Objects.Current == null || string.IsNullOrEmpty(this.Objects.Current.Name)))
      throw new PXException("A schema object is not selected.");
  }

  protected virtual void SYProviderField_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SYProviderField row = (SYProviderField) e.Row;
    if (row == null)
      return;
    PXCache cache = sender;
    SYProviderField data = row;
    bool? isCustom = row.IsCustom;
    bool flag = true;
    int num = isCustom.GetValueOrDefault() == flag & isCustom.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SYProviderField.name>(cache, (object) data, num != 0);
    if (string.IsNullOrEmpty(row.DataType))
      return;
    System.Type type = System.Type.GetType(row.DataType, false);
    if (!(type != (System.Type) null))
      return;
    this.collectedDataTypes[type.FullName] = type.Name;
  }

  protected virtual void SYProviderField_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    e.Cancel = this.keepFields;
  }

  protected virtual void SYProvider_ProviderID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void SYProvider_ProviderType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.ReloadParametersInt();
    foreach (PXResult<SYProviderObject> pxResult in this.Objects.Select())
      this.Objects.Delete((SYProviderObject) pxResult);
  }

  protected virtual void SYProvider_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool isEnabled = this.Providers.Current != null && !string.IsNullOrEmpty(this.Providers.Current.ProviderType);
    this.ReloadParameters.SetEnabled(isEnabled);
    this.FillSchemaObjects.SetEnabled(isEnabled);
    this.FillSchemaFields.SetEnabled(isEnabled);
    this.ShowObjectCommand.SetEnabled(isEnabled);
    this.ShowFieldCommand.SetEnabled(isEnabled);
    this.ToggleFieldsActivation.SetEnabled(isEnabled);
    if (this.autoFillSchemaObjectsIsOn)
      return;
    this.autoFillSchemaObjectsIsOn = true;
    this.AutoFillSchemaObjects(sender, e.Row as SYProvider);
  }

  private void AutoFillSchemaObjects(PXCache providerCache, SYProvider provider)
  {
    try
    {
      Guid[] fileNotes = PXNoteAttribute.GetFileNotes(providerCache, (object) provider);
      if (fileNotes == null || fileNotes.Length == 0 || this.Objects.Select().AsEnumerable<PXResult<SYProviderObject>>().Any<PXResult<SYProviderObject>>((Func<PXResult<SYProviderObject>, bool>) (o => o != null)))
        return;
      this.FillSchemaObjects.Press();
    }
    catch
    {
    }
  }

  protected virtual void SYProvider_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if ((SYMapping) PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) ((SYProvider) e.Row).ProviderID) != null)
      throw new PXException("A mapping that uses the current data source exists. The record cannot be deleted.");
  }

  private string GetAttachedFileName()
  {
    PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Current<SYProvider.noteID>>>, OrderBy<Desc<UploadFile.createdDateTime>>>.Config>.Clear((PXGraph) this);
    return ((UploadFile) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Current<SYProvider.noteID>>>, OrderBy<Desc<UploadFile.createdDateTime>>>.Config>.Select((PXGraph) this))?.Name;
  }

  protected virtual void SYProviderParameter_Value_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (((SYProviderParameter) e.Row).Name == "FileName" && e.NewValue == null)
      e.NewValue = (object) "<EmptyFileName>";
    else if (e.NewValue == null)
      throw new PXSetPropertyException("The parameter Value cannot be empty.");
  }

  protected virtual void SYProviderParameter_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SYProviderParameter row = (SYProviderParameter) e.Row;
    if (row == null)
      return;
    try
    {
      this.EnsureProvider(false);
      foreach (PXStringState pxStringState in this.provider.GetParametersDefenition())
      {
        if (pxStringState.Name == row.Name)
        {
          if ((string) e.ReturnValue == "<EmptyFileName>" && !string.IsNullOrEmpty(this.GetAttachedFileName()))
            pxStringState.Value = (object) this.GetAttachedFileName();
          else
            pxStringState.Value = e.ReturnValue;
          e.ReturnState = (object) pxStringState;
          break;
        }
      }
    }
    catch
    {
    }
  }

  internal static IPXSYProvider TryToCreateProvider(string providerType)
  {
    IPXSYProvider createProvider = (IPXSYProvider) null;
    if (!string.IsNullOrEmpty(providerType))
    {
      System.Type type1 = PXBuildManager.GetType(providerType, false);
      if ((object) type1 == null)
        type1 = System.Type.GetType(providerType, false);
      System.Type type2 = type1;
      if (type2 != (System.Type) null)
        createProvider = Activator.CreateInstance(type2) as IPXSYProvider;
    }
    return createProvider;
  }

  private void EnsureProvider(bool setParameters)
  {
    if (this.provider == null)
    {
      SYProvider current = this.Providers.Current;
      if (current != null || !string.IsNullOrEmpty(current.ProviderType))
        this.provider = SYProviderMaint.TryToCreateProvider(current.ProviderType);
      if (this.provider == null)
        throw new PXException(" IPXSYProvider cannot be instantiated from the {0} type.", new object[1]
        {
          (object) current.ProviderType
        });
      if (this.provider is IPXSYProviderWithID provider)
      {
        provider.ProviderID = current.ProviderID.Value;
        provider.Graph = (PXGraph) this;
      }
    }
    if (!setParameters)
      return;
    List<PXSYParameter> pxsyParameterList = new List<PXSYParameter>();
    foreach (PXResult<SYProviderParameter> pxResult in this.Parameters.Select())
    {
      SYProviderParameter providerParameter = (SYProviderParameter) pxResult;
      if (providerParameter.Value == "<EmptyFileName>" && !string.IsNullOrEmpty(this.GetAttachedFileName()))
        pxsyParameterList.Add(new PXSYParameter(providerParameter.Name, this.GetAttachedFileName()));
      else
        pxsyParameterList.Add(new PXSYParameter(providerParameter.Name, providerParameter.Value));
    }
    this.provider.SetParameters(pxsyParameterList.ToArray());
  }

  public static IPXSYProvider GetProvider(Guid providerID)
  {
    SYProviderMaint instance = PXGraph.CreateInstance<SYProviderMaint>();
    instance.Providers.Current = (SYProvider) instance.Providers.Search<SYProvider.providerID>((object) providerID);
    if (instance.Providers.Current == null)
      throw new PXArgumentException(nameof (providerID));
    instance.EnsureProvider(true);
    return instance.provider;
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    IEnumerable enumerable = base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    if (viewName == "Fields")
    {
      string[] allowedValues = new string[this.collectedDataTypes.Count];
      string[] allowedLabels = new string[this.collectedDataTypes.Count];
      int index = 0;
      foreach (string key in this.collectedDataTypes.Keys)
      {
        allowedValues[index] = key;
        allowedLabels[index] = this.collectedDataTypes[key];
        ++index;
      }
      PXStringListAttribute.SetList<SYProviderField.dataType>(this.Fields.Cache, (object) null, allowedValues, allowedLabels);
    }
    return enumerable;
  }

  public override void Persist()
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<SYProviderObject> pxResult in this.Objects.Select())
    {
      SYProviderObject syProviderObject = (SYProviderObject) pxResult;
      if (dictionary.ContainsKey(syProviderObject.Name))
        throw new PXException("There is a duplicate for object '{0}' in the schema. Schema object names should be unique.", new object[1]
        {
          (object) syProviderObject.Name
        });
      dictionary.Add(syProviderObject.Name, (object) syProviderObject);
    }
    dictionary.Clear();
    foreach (PXResult<SYProviderField> pxResult in this.Fields.Select())
    {
      SYProviderField data = (SYProviderField) pxResult;
      if (string.IsNullOrEmpty(data.Name))
      {
        string str = PXMessages.LocalizeFormatNoPrefixNLA("'{0}' cannot be empty.", (object) PXUIFieldAttribute.GetDisplayName<SYProviderField.name>(this.Fields.Cache));
        PXUIFieldAttribute.SetError<SYProviderField.name>(this.Fields.Cache, (object) data, str);
        throw new PXException(str);
      }
      if (dictionary.ContainsKey(data.Name))
        throw new PXException("There is a duplicate for field '{0}' in the schema. Schema field names should be unique.", new object[1]
        {
          (object) data.Name
        });
      dictionary.Add(data.Name, (object) data);
    }
    List<SYProviderField> syProviderFieldList = new List<SYProviderField>();
    foreach (SYProviderField syProviderField in this.Fields.Cache.Updated)
    {
      syProviderFieldList.Add((SYProviderField) this.Fields.Cache.CreateCopy((object) syProviderField));
      this.Fields.Cache.SetStatus((object) syProviderField, PXEntryStatus.Deleted);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (SYProviderField row in this.Fields.Cache.Deleted)
        this.Fields.Cache.PersistDeleted((object) row);
      foreach (SYProviderField row in syProviderFieldList)
      {
        this.Fields.Cache.SetStatus((object) row, PXEntryStatus.Inserted);
        this.Fields.Cache.PersistInserted((object) row);
      }
      this.Objects.Cache.Persist(PXDBOperation.Delete);
      this.Objects.Cache.Persist(PXDBOperation.Update);
      base.Persist();
      transactionScope.Complete((PXGraph) this);
    }
  }
}
