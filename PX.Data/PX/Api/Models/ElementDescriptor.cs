// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.ElementDescriptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PX.Data;
using System.ComponentModel;

#nullable disable
namespace PX.Api.Models;

public class ElementDescriptor
{
  public string DisplayName;
  [DefaultValue(false)]
  public bool IsDisabled;
  [DefaultValue(false)]
  public bool IsRequired;
  [DefaultValue(ElementTypes.String)]
  public ElementTypes ElementType;
  [DefaultValue(0)]
  public int LengthLimit;
  public string InputMask;
  public string DisplayRules;
  public string[] AllowedValues;
  public Container Container;
  public string ActionIcon;
  public int FieldPriority;
  [JsonConverter(typeof (StringEnumConverter))]
  public PXSpecialButtonType ButtonType;
  public string LinkCommand;
  public bool Visible;
  public bool IsTimeList;
  public bool? PreserveTime;
  public string DependsOn;
  public string StateColumn;
  public bool AutoPostback;
  public FieldTypes FieldType;
}
