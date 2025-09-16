// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Container
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Description;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace PX.Api.Models;

public class Container
{
  public Field[] Fields;
  public string Name;
  public Command[] ServiceCommands;
  [XmlIgnore]
  internal PXViewDescription ViewDescription;
  public Container[] Children;
  public string DisplayName;
  [XmlIgnore]
  public string CheckboxField;

  public string ViewDisplayName() => this.ViewDescription?.DisplayName;

  public string ViewName() => this.ViewDescription?.ViewName;

  public bool IsEmptyDescription() => this.ViewDescription == null;

  public bool IsGrid()
  {
    PXViewDescription viewDescription = this.ViewDescription;
    return viewDescription != null && viewDescription.IsGrid;
  }

  public bool IsTree()
  {
    PXViewDescription viewDescription = this.ViewDescription;
    return viewDescription != null && viewDescription.IsTree;
  }

  public bool IsSmartPanel()
  {
    PXViewDescription viewDescription = this.ViewDescription;
    return viewDescription != null && viewDescription.IsInSmartPanel;
  }

  public IEnumerable<PX.Data.Description.FieldInfo> KeyFields()
  {
    return ((IEnumerable<PX.Data.Description.FieldInfo>) this.ViewDescription.AllFields).Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => field.IsKey));
  }

  public bool HasNoteID()
  {
    PXViewDescription viewDescription = this.ViewDescription;
    return viewDescription != null && viewDescription.HasNoteID;
  }

  public string CleanViewName() => SyMappingUtils.CleanViewName(this.ViewName());

  public bool HasKeyField(string field) => this.ViewDescription.IsKeyField(field);

  public Field SelectorKeyField(bool throwIfNotFound = true)
  {
    try
    {
      return throwIfNotFound ? ((IEnumerable<Field>) this.Fields).Single<Field>((Func<Field, bool>) (f => f.FieldName.OrdinalEquals(this.ViewDescription?.SelectorKeyField))) : ((IEnumerable<Field>) this.Fields).FirstOrDefault<Field>((Func<Field, bool>) (f => f.FieldName.OrdinalEquals(this.ViewDescription?.SelectorKeyField)));
    }
    catch (Exception ex)
    {
      string format = "Unable to get SelectorKeyField from container " + this.Name;
      object[] objArray = Array.Empty<object>();
      PXTrace.WriteError(ex, format, objArray);
      throw;
    }
  }

  public Field SelectorDescriptionField(bool throwIfNotFound = true)
  {
    try
    {
      return throwIfNotFound ? ((IEnumerable<Field>) this.Fields).Single<Field>((Func<Field, bool>) (f => f.FieldName.OrdinalEquals(this.ViewDescription?.SelectorDescriptionField))) : ((IEnumerable<Field>) this.Fields).FirstOrDefault<Field>((Func<Field, bool>) (f => f.FieldName.OrdinalEquals(this.ViewDescription?.SelectorDescriptionField)));
    }
    catch (Exception ex)
    {
      string format = "Unable to get SelectorDescriptionField from container " + this.Name;
      object[] objArray = Array.Empty<object>();
      PXTrace.WriteError(ex, format, objArray);
      throw;
    }
  }

  public bool HasSelectorKey() => !string.IsNullOrEmpty(this.ViewDescription?.SelectorKeyField);

  public bool HasSelectorDescription()
  {
    return !string.IsNullOrEmpty(this.ViewDescription?.SelectorDescriptionField);
  }

  public IEnumerable<Field> GetFieldsByFieldName(string fieldName)
  {
    return ((IEnumerable<Field>) this.Fields).Where<Field>((Func<Field, bool>) (f => f.FieldNameBase.OrdinalEquals(fieldName)));
  }

  public IEnumerable<Field> GetKeysFields()
  {
    return ((IEnumerable<Command>) this.ServiceCommands).Where<Command>((Func<Command, bool>) (c => c is Key)).Select<Command, Field>((Func<Command, Field>) (c => ((IEnumerable<Field>) this.Fields).First<Field>((Func<Field, bool>) (f => (c.FieldName ?? c.Value).OrdinalEquals(f.FieldName)))));
  }

  public bool HasKeys()
  {
    return ((IEnumerable<Command>) this.ServiceCommands).Any<Command>((Func<Command, bool>) (c => c is Key));
  }

  public IEnumerable<Key> GetKeys() => this.ServiceCommands.OfType<Key>();
}
