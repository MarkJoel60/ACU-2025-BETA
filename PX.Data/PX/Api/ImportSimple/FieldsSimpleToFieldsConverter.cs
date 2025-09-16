// Decompiled with JetBrains decompiler
// Type: PX.Api.ImportSimple.FieldsSimpleToFieldsConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Data;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.ImportSimple;

public class FieldsSimpleToFieldsConverter
{
  private readonly IEnumerable<SYMappingFieldSimple> fieldsSimple;
  private readonly List<SYMappingFieldSimple> keyFieldsSimple;
  private readonly List<SYMappingFieldSimple> formulaKeyFieldsSimple;
  private readonly IEnumerable<Command> cpCommands;
  private readonly Guid? mappingID;
  private readonly PXGraph graph;
  private readonly PXSiteMap.ScreenInfo screenInfo;
  private readonly Dictionary<string, KeyValuePair<string, string>> AttributeLikeGrids;

  public FieldsSimpleToFieldsConverter(
    IEnumerable<SYMappingFieldSimple> fieldsSimple,
    List<SYMappingFieldSimple> keyFieldsSimple,
    List<SYMappingFieldSimple> formulaKeyFieldsSimple,
    IEnumerable<Command> cpCommands,
    Guid? mappingID,
    string screenID,
    Dictionary<string, KeyValuePair<string, string>> attributeLikeGrids,
    PXGraph graph)
  {
    if (fieldsSimple != null && cpCommands != null && mappingID.HasValue && graph != null)
    {
      this.fieldsSimple = fieldsSimple;
      this.keyFieldsSimple = keyFieldsSimple;
      this.formulaKeyFieldsSimple = formulaKeyFieldsSimple;
      this.cpCommands = cpCommands;
      this.mappingID = mappingID;
      this.graph = graph;
      this.screenInfo = ScreenUtils.ScreenInfo.TryGet(screenID);
      this.AttributeLikeGrids = attributeLikeGrids;
    }
    else
      throw new PXException("{0} cannot be instantiated. The Ctr parameters are not valid.", new object[1]
      {
        (object) typeof (FieldsSimpleToFieldsConverter).Name
      });
  }

  public IEnumerable<SYMappingField> ConvertToFields()
  {
    return this.ConvertCommandsToMappings(this.GetProcessedCommands());
  }

  private void InsertCancelAction(
    List<SYMappingField> mappingFields,
    string value,
    string objectName,
    string fieldName,
    int position)
  {
    string actionName = this.GetActionName(PXSpecialButtonType.Cancel);
    if (string.IsNullOrEmpty(actionName))
      return;
    mappingFields.Insert(position, this.CreateMappingFieldFromAction(objectName, actionName, this.GetIsActiveFieldValue(value, objectName, fieldName)));
  }

  private void InsertSaveButton(List<SYMappingField> mappingFields, string objectName)
  {
    string actionName = this.GetActionName(PXSpecialButtonType.Save);
    if (string.IsNullOrEmpty(actionName))
      return;
    mappingFields.Add(this.CreateMappingFieldFromAction(objectName, actionName, new bool?(true)));
  }

  private void InsertKeyCommand(
    List<SYMappingField> mappingFields,
    string objectName,
    string fieldName,
    bool hasExtReference,
    string value,
    int position)
  {
    if (hasExtReference)
      mappingFields.Insert(position, this.CreateMappingFieldForExtRef(objectName, value));
    else
      mappingFields.Insert(position, this.CreateMappingFieldForKeyCommand(objectName, value, fieldName));
  }

  private string GetActionName(PXSpecialButtonType buttonType)
  {
    foreach (PXAction pxAction in (IEnumerable) this.graph.Actions.Values)
    {
      if (pxAction.GetState((object) null) is PXButtonState state && state.SpecialType == buttonType)
        return state.Name;
    }
    return (string) null;
  }

  private IEnumerable<SYMappingField> ConvertCommandsToMappings(IList<Command> commands)
  {
    List<SYMappingField> mappingFields = new List<SYMappingField>();
    List<KeyValuePair<string, string>> list1 = this.fieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field =>
    {
      bool? isKey = field.IsKey;
      bool flag = true;
      return isKey.GetValueOrDefault() == flag & isKey.HasValue;
    })).Select<SYMappingFieldSimple, KeyValuePair<string, string>>((Func<SYMappingFieldSimple, KeyValuePair<string, string>>) (field => new KeyValuePair<string, string>(field.ObjectName, field.FieldName))).ToList<KeyValuePair<string, string>>();
    List<string> list2 = this.fieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field =>
    {
      bool? isKey = field.IsKey;
      bool flag = true;
      return isKey.GetValueOrDefault() == flag & isKey.HasValue && string.IsNullOrEmpty(field.FieldName);
    })).Select<SYMappingFieldSimple, string>((Func<SYMappingFieldSimple, string>) (field => field.ObjectName)).ToList<string>();
    List<Command> list3 = this.fieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => field.ObjectName.OrdinalEquals(this.screenInfo.PrimaryView) && !string.IsNullOrEmpty(field.FieldName) && !commands.Any<Command>((Func<Command, bool>) (command => command.ObjectName.OrdinalEquals(field.ObjectName) && command.FieldName.OrdinalEquals(field.FieldName))))).Select<SYMappingFieldSimple, Command>((Func<SYMappingFieldSimple, Command>) (field =>
    {
      Command mappings = new Command();
      mappings.ObjectName = field.ObjectName;
      mappings.FieldName = field.FieldName;
      mappings.Value = field.Value;
      bool? needCommit = field.NeedCommit;
      bool flag = true;
      mappings.Commit = needCommit.GetValueOrDefault() == flag & needCommit.HasValue;
      return mappings;
    })).ToList<Command>();
    list3.AddRange((IEnumerable<Command>) commands);
    commands = (IList<Command>) list3;
    int position = -1;
    string a = (string) null;
    for (int index1 = 0; index1 < commands.Count; ++index1)
    {
      Command command = commands[index1];
      bool flag = list1.Contains(new KeyValuePair<string, string>(command.ObjectName, command.FieldName));
      bool hasExtReference = flag && string.IsNullOrEmpty(command.FieldName) && list2.Contains(command.ObjectName);
      if (!a.OrdinalEquals(command.ObjectName))
      {
        a = command.ObjectName;
        position = -1;
      }
      if (flag)
        this.InsertKeyCommand(mappingFields, command.ObjectName, command.FieldName, hasExtReference, command.Value, this.GetAndIncrementKeyPosition(ref position, mappingFields.Count));
      if (flag && command.ObjectName.OrdinalEquals(this.screenInfo.PrimaryView))
        this.InsertCancelAction(mappingFields, command.Value, this.screenInfo.PrimaryView, command.FieldName, this.GetAndIncrementKeyPosition(ref position, mappingFields.Count));
      if (!hasExtReference)
      {
        int index2 = flag ? this.GetAndIncrementKeyPosition(ref position, mappingFields.Count) : mappingFields.Count;
        mappingFields.Insert(index2, this.CreateMappingFieldFromCommand(command, false));
      }
      if (!flag && position == -1 && !this.IsNewLineCmd(command.FieldName, command.Value))
        position = index1;
    }
    if (commands.Count > 0)
      this.InsertSaveButton(mappingFields, this.screenInfo.PrimaryView);
    return (IEnumerable<SYMappingField>) mappingFields;
  }

  private int GetAndIncrementKeyPosition(ref int position, int count)
  {
    if (position == -1)
      return count;
    ++position;
    return position - 1;
  }

  private IEnumerable<SYMappingField> ConvertLinkedCommandsToMappings(Command cmd)
  {
    List<SYMappingField> mappings = new List<SYMappingField>();
    for (Command linkedCommand = cmd.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
      mappings.Add(this.CreateMappingFieldFromCommand(linkedCommand, true));
    mappings.Reverse();
    return (IEnumerable<SYMappingField>) mappings;
  }

  private bool IsNewLineCmd(string fieldName, string fieldValue)
  {
    return FieldNameAndValueGenerator.NewLineFieldName.Equals(fieldName) && FieldNameAndValueGenerator.NewLineFieldValue.Equals(fieldValue);
  }

  private bool IsAttributeServiceCmd(string objectName, string fieldName)
  {
    return this.AttributeLikeGrids.ContainsKey(objectName) && fieldName == FieldNameAndValueGenerator.GenerateKeyFieldName(this.AttributeLikeGrids[objectName].Key);
  }

  private bool IsAttributeValueCmd(string objectName, string fieldName)
  {
    return this.AttributeLikeGrids.ContainsKey(objectName) && this.AttributeLikeGrids[objectName].Value == fieldName;
  }

  private bool? GetAttributeServiceCmdIsActive(string objectName, string value)
  {
    return this.fieldsSimple.FirstOrDefault<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (f => objectName == f.ObjectName && value == FieldNameAndValueGenerator.GenerateAttributeFieldValue(f.FieldName)))?.IsActive;
  }

  private bool? GetAttributeValueCmdIsActive(string objectName, string value)
  {
    return this.fieldsSimple.FirstOrDefault<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (f => f.ObjectName == objectName && f.Value == value))?.IsActive;
  }

  private bool? GetOrdinaryCmdIsActive(string value, string objectName, string fieldName)
  {
    return this.fieldsSimple.FirstOrDefault<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (f => f.FieldName == fieldName && f.ObjectName == objectName && f.Value == value))?.IsActive;
  }

  private bool? GetIsActiveFieldValue(string value, string objectName, string fieldName)
  {
    if (this.IsAttributeServiceCmd(objectName, fieldName))
      return this.GetAttributeServiceCmdIsActive(objectName, value);
    return this.IsAttributeValueCmd(objectName, fieldName) ? this.GetAttributeValueCmdIsActive(objectName, value) : this.GetOrdinaryCmdIsActive(value, objectName, fieldName);
  }

  private SYMappingField CreateMappingFieldFromCommand(Command cmd, bool linkedCommand)
  {
    PXViewDescription pxViewDescription = this.screenInfo.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => string.Equals(c.Value.ViewName, cmd.ObjectName, StringComparison.OrdinalIgnoreCase))).Select<KeyValuePair<string, PXViewDescription>, PXViewDescription>((Func<KeyValuePair<string, PXViewDescription>, PXViewDescription>) (c => c.Value)).FirstOrDefault<PXViewDescription>();
    PX.Data.Description.FieldInfo fieldInfo = (PX.Data.Description.FieldInfo) null;
    if (pxViewDescription != null)
      fieldInfo = ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription.Fields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => string.Equals(f.FieldName, cmd.FieldName, StringComparison.OrdinalIgnoreCase)));
    string str = cmd.FieldName;
    if (fieldInfo != null && !string.IsNullOrWhiteSpace(fieldInfo.TextField))
      str = !char.IsLower(fieldInfo.TextField[0]) ? $"{str}!{fieldInfo.TextField}" : $"{str}!{char.ToUpper(fieldInfo.TextField[0]).ToString()}{fieldInfo.TextField.Substring(1)}";
    return new SYMappingField()
    {
      MappingID = this.mappingID,
      ObjectName = linkedCommand ? FieldNameAndValueGenerator.GenerateObjectNameFromLinkedCommand(cmd.ObjectName) : cmd.ObjectName,
      FieldName = str,
      Value = cmd.Value,
      NeedCommit = new bool?(cmd.Commit),
      IgnoreError = new bool?(cmd.IgnoreError),
      IsActive = this.GetIsActiveFieldValue(cmd.Value, cmd.ObjectName, cmd.FieldName),
      IsVisible = new bool?(true)
    };
  }

  private SYMappingField CreateMappingFieldFromAction(
    string objectName,
    string actionName,
    bool? isActive)
  {
    return new SYMappingField()
    {
      MappingID = this.mappingID,
      ObjectName = objectName,
      FieldName = FieldNameAndValueGenerator.GenerateFieldNameFromAction(actionName),
      NeedCommit = new bool?(false),
      IgnoreError = new bool?(false),
      IsActive = isActive,
      IsVisible = new bool?(true)
    };
  }

  private SYMappingField CreateMappingFieldForExtRef(string objectName, string value)
  {
    return new SYMappingField()
    {
      MappingID = this.mappingID,
      FieldName = FieldNameAndValueGenerator.GenerateExtRefValue(),
      ObjectName = objectName,
      Value = value,
      NeedCommit = new bool?(false),
      IgnoreError = new bool?(false),
      IsActive = this.GetIsActiveFieldValue(value, objectName, (string) null),
      IsVisible = new bool?(true)
    };
  }

  private SYMappingField CreateMappingFieldForKeyCommand(
    string objectName,
    string value,
    string fieldName)
  {
    return new SYMappingField()
    {
      MappingID = this.mappingID,
      ObjectName = objectName,
      FieldName = FieldNameAndValueGenerator.GenerateKeyFieldName(fieldName),
      NeedCommit = new bool?(false),
      IgnoreError = new bool?(false),
      IsActive = this.GetIsActiveFieldValue(value, objectName, fieldName),
      IsVisible = new bool?(true),
      Value = FieldNameAndValueGenerator.GenerateKeyValue(objectName, fieldName)
    };
  }

  private IEnumerable<Command> CreateCommandsForAttribute(
    Command currentCommand,
    Dictionary<string, List<KeyValuePair<string, string>>> attributes)
  {
    foreach (KeyValuePair<string, string> keyValuePair in attributes[currentCommand.ObjectName])
    {
      KeyValuePair<string, string> at = keyValuePair;
      yield return new Command()
      {
        ObjectName = currentCommand.ObjectName,
        FieldName = FieldNameAndValueGenerator.GenerateKeyFieldName(this.AttributeLikeGrids[currentCommand.ObjectName].Key),
        Value = FieldNameAndValueGenerator.GenerateAttributeFieldValue(at.Key)
      };
      yield return new Command()
      {
        ObjectName = currentCommand.ObjectName,
        FieldName = this.AttributeLikeGrids[currentCommand.ObjectName].Value,
        Value = at.Value,
        Commit = true
      };
      at = new KeyValuePair<string, string>();
    }
  }

  private IEnumerable<Command> CreateCommandsForExternalKey(
    Command currentCommand,
    Dictionary<string, List<string>> externalKeys)
  {
    foreach (string str in externalKeys[currentCommand.ObjectName])
      yield return new Command()
      {
        ObjectName = currentCommand.ObjectName,
        Value = str
      };
  }

  private IEnumerable<Command> CreateCommandsForKeyFields(Command currentCommand)
  {
    SYMappingFieldSimple[] array = this.keyFieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => field.ObjectName == currentCommand.ObjectName)).ToArray<SYMappingFieldSimple>();
    foreach (SYMappingFieldSimple mappingFieldSimple in array)
      this.keyFieldsSimple.Remove(mappingFieldSimple);
    return ((IEnumerable<SYMappingFieldSimple>) array).Select<SYMappingFieldSimple, Command>((Func<SYMappingFieldSimple, Command>) (field => new Command()
    {
      ObjectName = field.ObjectName,
      FieldName = field.FieldName,
      Value = field.Value,
      Commit = true
    }));
  }

  private IEnumerable<Command> CreateCommandsOrdinary(
    Command currentCommand,
    Dictionary<string, HashSet<string>> objectFieldsDistinct,
    List<Command> existingCommands)
  {
    foreach (string str in this.GetFieldValue(currentCommand.ObjectName, currentCommand.FieldName))
    {
      string val = str;
      Command command = existingCommands.FirstOrDefault<Command>((Func<Command, bool>) (cmd => cmd.ObjectName == currentCommand.ObjectName && cmd.FieldName == currentCommand.FieldName && cmd.Value == val));
      if (command != null)
      {
        command.Commit = currentCommand.Commit;
        command.LinkedCommand = this.FilterUnusedLinkedCommands(objectFieldsDistinct, currentCommand);
      }
      else
        yield return new Command()
        {
          ObjectName = currentCommand.ObjectName,
          FieldName = currentCommand.FieldName,
          Value = val,
          Commit = currentCommand.Commit,
          LinkedCommand = this.FilterUnusedLinkedCommands(objectFieldsDistinct, currentCommand)
        };
    }
  }

  private Command CreateCommandForNewLine(Command currentCommand)
  {
    return new Command()
    {
      ObjectName = currentCommand.ObjectName,
      FieldName = FieldNameAndValueGenerator.NewLineFieldName,
      Value = FieldNameAndValueGenerator.NewLineFieldValue
    };
  }

  private IEnumerable<Command> CreateCommandsForFormulaKey(Command currentCommand)
  {
    SYMappingFieldSimple[] array = this.formulaKeyFieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => field.ObjectName == currentCommand.ObjectName)).ToArray<SYMappingFieldSimple>();
    foreach (SYMappingFieldSimple mappingFieldSimple in array)
      this.formulaKeyFieldsSimple.Remove(mappingFieldSimple);
    return ((IEnumerable<SYMappingFieldSimple>) array).Select<SYMappingFieldSimple, Command>((Func<SYMappingFieldSimple, Command>) (field => new Command()
    {
      ObjectName = field.ObjectName,
      FieldName = field.FieldName,
      Value = field.Value,
      Commit = true
    }));
  }

  private IList<Command> GetProcessedCommands()
  {
    List<Command> existingCommands = new List<Command>();
    Dictionary<string, List<string>> externalKeys;
    Dictionary<string, List<KeyValuePair<string, string>>> attributes;
    Dictionary<string, HashSet<string>> objectsAndFields = this.GetDistinctObjectsAndFields(out externalKeys, out attributes);
    HashSet<string> objectsWithNewLineCommand = new HashSet<string>();
    foreach (Command cpCommand in this.cpCommands)
    {
      Command currentCommand = cpCommand;
      if (attributes.ContainsKey(currentCommand.ObjectName))
      {
        existingCommands.AddRange(this.CreateCommandsForAttribute(currentCommand, attributes));
        attributes.Remove(currentCommand.ObjectName);
      }
      else
      {
        if (externalKeys.ContainsKey(currentCommand.ObjectName))
        {
          existingCommands.AddRange(this.CreateCommandsForExternalKey(currentCommand, externalKeys));
          externalKeys.Remove(currentCommand.ObjectName);
        }
        if (this.keyFieldsSimple != null && this.keyFieldsSimple.Count > 0)
          existingCommands.AddRange(this.CreateCommandsForKeyFields(currentCommand));
        if (this.formulaKeyFieldsSimple != null && this.formulaKeyFieldsSimple.Count > 0)
          existingCommands.AddRange(this.CreateCommandsForFormulaKey(currentCommand));
        if (objectsAndFields.ContainsKey(currentCommand.ObjectName) && objectsAndFields[currentCommand.ObjectName].Contains(currentCommand.FieldName))
        {
          if (this.NeedToInsertNewLine(currentCommand, objectsWithNewLineCommand))
          {
            int index = existingCommands.FindIndex((Predicate<Command>) (c => c.ObjectName.Equals(currentCommand.ObjectName)));
            if (index >= 0)
              existingCommands.Insert(index, this.CreateCommandForNewLine(currentCommand));
            else
              existingCommands.Add(this.CreateCommandForNewLine(currentCommand));
          }
          existingCommands.AddRange(this.CreateCommandsOrdinary(currentCommand, objectsAndFields, existingCommands));
        }
      }
    }
    return (IList<Command>) existingCommands;
  }

  private bool NeedToInsertNewLine(Command command, HashSet<string> objectsWithNewLineCommand)
  {
    if (!this.screenInfo.Containers[command.ObjectName].HasLineNumber || this.screenInfo.Containers[command.ObjectName].HasSearchesByKey || objectsWithNewLineCommand.Contains(command.ObjectName))
      return false;
    objectsWithNewLineCommand.Add(command.ObjectName);
    return true;
  }

  private Command FilterUnusedLinkedCommands(
    Dictionary<string, HashSet<string>> objectFieldsDistinct,
    Command command)
  {
    List<Command> commandList = new List<Command>();
    for (Command linkedCommand = command.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
    {
      if (objectFieldsDistinct.ContainsKey(linkedCommand.ObjectName) && objectFieldsDistinct[linkedCommand.ObjectName].Contains(linkedCommand.FieldName))
        commandList.Add(linkedCommand);
    }
    for (int index = 0; index < commandList.Count - 2; ++index)
      commandList[index].LinkedCommand = commandList[index + 1];
    return commandList.Count != 0 ? commandList[0] : (Command) null;
  }

  private IEnumerable<string> GetFieldValue(string objectName, string fieldName)
  {
    return this.fieldsSimple.Where<SYMappingFieldSimple>((Func<SYMappingFieldSimple, bool>) (field => field.ObjectName == objectName && field.FieldName == fieldName)).Select<SYMappingFieldSimple, string>((Func<SYMappingFieldSimple, string>) (field => field.Value));
  }

  private Dictionary<string, HashSet<string>> GetDistinctObjectsAndFields(
    out Dictionary<string, List<string>> externalKeys,
    out Dictionary<string, List<KeyValuePair<string, string>>> attributes)
  {
    Dictionary<string, HashSet<string>> objectsAndFields = new Dictionary<string, HashSet<string>>();
    externalKeys = new Dictionary<string, List<string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    attributes = new Dictionary<string, List<KeyValuePair<string, string>>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (SYMappingFieldSimple mappingFieldSimple in this.fieldsSimple)
    {
      if (!string.IsNullOrEmpty(mappingFieldSimple.ObjectName))
      {
        if (this.AttributeLikeGrids.ContainsKey(mappingFieldSimple.ObjectName))
        {
          if (!string.IsNullOrEmpty(mappingFieldSimple.FieldName) && !string.IsNullOrEmpty(mappingFieldSimple.Value))
          {
            List<KeyValuePair<string, string>> keyValuePairList;
            if (!attributes.TryGetValue(mappingFieldSimple.ObjectName, out keyValuePairList))
              attributes[mappingFieldSimple.ObjectName] = keyValuePairList = new List<KeyValuePair<string, string>>();
            keyValuePairList.Add(new KeyValuePair<string, string>(mappingFieldSimple.FieldName, mappingFieldSimple.Value));
          }
        }
        else if (!string.IsNullOrEmpty(mappingFieldSimple.FieldName))
        {
          if (!objectsAndFields.ContainsKey(mappingFieldSimple.ObjectName))
            objectsAndFields[mappingFieldSimple.ObjectName] = new HashSet<string>();
          objectsAndFields[mappingFieldSimple.ObjectName].Add(mappingFieldSimple.FieldName);
        }
        else
        {
          bool? isKey = mappingFieldSimple.IsKey;
          bool flag = true;
          if (isKey.GetValueOrDefault() == flag & isKey.HasValue)
          {
            if (!externalKeys.ContainsKey(mappingFieldSimple.ObjectName))
              externalKeys[mappingFieldSimple.ObjectName] = new List<string>();
            externalKeys[mappingFieldSimple.ObjectName].Add(mappingFieldSimple.Value);
          }
        }
      }
    }
    return objectsAndFields;
  }
}
