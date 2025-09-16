// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.QueueEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
[Serializable]
public class QueueEvent : IQueueEvent
{
  [JsonConstructor]
  [Obsolete("Serialization Only", true)]
  public QueueEvent()
  {
  }

  public QueueEvent(
    string tableName,
    IEnumerable<PXDataFieldParam> parameters,
    string screenId,
    PXDBOperation operation,
    int companyId)
  {
    this.Fields = parameters.Where<PXDataFieldParam>((Func<PXDataFieldParam, bool>) (p => p.Storage == StorageBehavior.Table)).Select<PXDataFieldParam, QueueEvent.Field>((Func<PXDataFieldParam, QueueEvent.Field>) (c => new QueueEvent.Field(c))).ToArray<QueueEvent.Field>();
    this.ScreenId = screenId;
    this.Operation = operation;
    this.TableName = tableName;
    this.CompanyId = companyId;
    this.GiSetToSkip = SuppressPushNotificationsScope.GetGiSetToSkip();
    this.EntityScreenSetToSkip = SuppressPushNotificationsScope.GetEntityScreenSetToSkip();
  }

  public Decimal? Identity { get; set; }

  public QueueEvent.Field[] Fields { get; set; }

  public string ScreenId { get; set; }

  [JsonConverter(typeof (StringEnumConverter))]
  public PXDBOperation Operation { get; set; }

  public string TableName { get; set; }

  public QueueEventType type => QueueEventType.Event;

  public int CompanyId { get; set; }

  public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();

  public List<Guid> GiSetToSkip { get; set; } = new List<Guid>();

  public List<string> EntityScreenSetToSkip { get; set; } = new List<string>();

  [DebuggerDisplay("{DebuggerDisplay,nq}")]
  public class Field
  {
    public Field(PXDataFieldParam param)
    {
      this.FieldName = param.Column.Name;
      if (param is PXDataFieldAssign assign)
      {
        this.IsChanged = assign.IsChanged;
        this.Behavior = assign.Behavior;
        this.NewValue = QueueEvent.Field.Calculate(assign);
        this.OldValue = assign.IsChanged ? assign.OldValue : assign.Value;
      }
      else
        this.NewValue = this.OldValue = param.Value;
    }

    private static object Calculate(PXDataFieldAssign assign)
    {
      IComparable comparable = assign.Value as IComparable;
      switch (assign.Behavior)
      {
        case PXDataFieldAssign.AssignBehavior.Summarize:
          return QueueEvent.Field.Sum(assign);
        case PXDataFieldAssign.AssignBehavior.Maximize:
          if (comparable == null)
            return assign.Value;
          return comparable.CompareTo(assign.OldValue) >= 0 ? (object) comparable : assign.OldValue;
        case PXDataFieldAssign.AssignBehavior.Minimize:
          if (comparable == null)
            return assign.Value;
          return comparable.CompareTo(assign.OldValue) <= 0 ? (object) comparable : assign.OldValue;
        case PXDataFieldAssign.AssignBehavior.Initialize:
          return assign.OldValue ?? assign.Value;
        default:
          return assign.Value;
      }
    }

    private static object Sum(PXDataFieldAssign assign)
    {
      if (assign.Value == null)
        return assign.OldValue;
      int? nullable1 = assign.Value as int?;
      if (nullable1.HasValue)
      {
        int? nullable2 = nullable1;
        int valueOrDefault = ((int?) assign.OldValue).GetValueOrDefault();
        return (object) (nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + valueOrDefault) : new int?());
      }
      byte? nullable3 = assign.Value as byte?;
      if (nullable3.HasValue)
      {
        byte? nullable4 = nullable3;
        int? nullable5 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
        nullable4 = (byte?) assign.OldValue;
        int valueOrDefault = (int) nullable4.GetValueOrDefault();
        return (object) (nullable5.HasValue ? new int?(nullable5.GetValueOrDefault() + valueOrDefault) : new int?());
      }
      short? nullable6 = assign.Value as short?;
      if (nullable6.HasValue)
      {
        short? nullable7 = nullable6;
        int? nullable8 = nullable7.HasValue ? new int?((int) nullable7.GetValueOrDefault()) : new int?();
        nullable7 = (short?) assign.OldValue;
        int valueOrDefault = (int) nullable7.GetValueOrDefault();
        return (object) (nullable8.HasValue ? new int?(nullable8.GetValueOrDefault() + valueOrDefault) : new int?());
      }
      long? nullable9 = assign.Value as long?;
      if (nullable9.HasValue)
      {
        long? nullable10 = nullable9;
        long valueOrDefault = ((long?) assign.OldValue).GetValueOrDefault();
        return (object) (nullable10.HasValue ? new long?(nullable10.GetValueOrDefault() + valueOrDefault) : new long?());
      }
      float? nullable11 = assign.Value as float?;
      if (nullable11.HasValue)
      {
        float? nullable12 = nullable11;
        float valueOrDefault = ((float?) assign.OldValue).GetValueOrDefault();
        return (object) (nullable12.HasValue ? new float?(nullable12.GetValueOrDefault() + valueOrDefault) : new float?());
      }
      double? nullable13 = assign.Value as double?;
      if (nullable13.HasValue)
      {
        double? nullable14 = nullable13;
        double valueOrDefault = ((double?) assign.OldValue).GetValueOrDefault();
        return (object) (nullable14.HasValue ? new double?(nullable14.GetValueOrDefault() + valueOrDefault) : new double?());
      }
      Decimal? nullable15 = assign.Value as Decimal?;
      if (!nullable15.HasValue)
        return assign.Value;
      Decimal? nullable16 = nullable15;
      Decimal valueOrDefault1 = ((Decimal?) assign.OldValue).GetValueOrDefault();
      return (object) (nullable16.HasValue ? new Decimal?(nullable16.GetValueOrDefault() + valueOrDefault1) : new Decimal?());
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
      get
      {
        return $"{this.FieldName ?? "null"}/{(this.OldValue ?? (object) "null")?.ToString()}->{(this.NewValue ?? (object) "null")?.ToString()}";
      }
    }

    [JsonConstructor]
    public Field()
    {
    }

    public string FieldName { get; set; }

    public object OldValue { get; set; }

    public object NewValue { get; set; }

    public bool IsChanged { get; set; }

    [JsonConverter(typeof (StringEnumConverter))]
    public PXDataFieldAssign.AssignBehavior Behavior { get; set; }
  }
}
