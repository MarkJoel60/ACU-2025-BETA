// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Command
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace PX.Api.Models;

public class Command
{
  public string FieldName;
  public string ObjectName;
  public string Value;
  [DefaultValue(false)]
  public bool Commit;
  [DefaultValue(false)]
  public bool IgnoreError;
  public Command LinkedCommand;
  public string Name;
  public ElementDescriptor Descriptor;
  [JsonIgnore]
  [XmlIgnore]
  public bool UseCurrent;

  public string FieldNameBase => this.FieldName.Split('_')[0];

  public override string ToString()
  {
    return string.Format("{3}{4}{0}:{1}={2}", (object) this.ObjectName, (object) this.FieldName, (object) this.Value, (object) (this.Commit ? "[C]" : ""), (object) (this.LinkedCommand != null ? "[L]" : ""));
  }

  public Command CloneCommand()
  {
    Command command = (Command) this.MemberwiseClone();
    if (command.LinkedCommand != null)
      command.LinkedCommand = command.LinkedCommand.CloneCommand();
    return command;
  }

  internal string GetCleanObjectName() => SyMappingUtils.CleanViewName(this.ObjectName);

  public Command CleanObjectName()
  {
    if (!string.IsNullOrEmpty(this.ObjectName) && this.ObjectName.Contains(":"))
    {
      this.ObjectName = this.GetCleanObjectName();
      if (this.LinkedCommand != null)
        this.LinkedCommand.CleanObjectName();
    }
    return this;
  }

  public bool IsCancelAction() => this.Name.OrdinalEquals("Cancel");
}
