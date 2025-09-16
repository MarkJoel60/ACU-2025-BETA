// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public abstract class PXBaseRedirectException : PXException
{
  private PXBaseRedirectException.WindowMode _mode = PXBaseRedirectException.WindowMode.Same;
  private bool _repaintControls;
  public readonly ICollection<PXBaseRedirectException.Filter> Filters = (ICollection<PXBaseRedirectException.Filter>) new List<PXBaseRedirectException.Filter>();

  public void SetMessage(string message) => this._Message = message;

  public override string Message => this._Message;

  public virtual bool RepaintControls
  {
    get => this._repaintControls;
    set => this._repaintControls = value;
  }

  protected PXBaseRedirectException(string message)
    : base(message)
  {
    this._Message = message;
  }

  protected PXBaseRedirectException(string message, bool repaintControls)
    : base(message)
  {
    this._Message = message;
    this._repaintControls = repaintControls;
  }

  protected PXBaseRedirectException(string message, params object[] args)
    : base(message, args)
  {
  }

  public PXBaseRedirectException.WindowMode Mode
  {
    get => this._mode;
    set => this._mode = value;
  }

  public PXBaseRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXBaseRedirectException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXBaseRedirectException>(this, info);
    base.GetObjectData(info, context);
  }

  protected static void populateGraphTimeStamp(PXGraph graph)
  {
    if (graph == null || graph.TimeStamp != null || !(graph.PrimaryItemType != (System.Type) null))
      return;
    PXCache cach = graph.Caches[graph.PrimaryItemType];
    if (cach.Current == null || !(cach.GetAttributesReadonly((string) null).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (_ => _ is PXDBTimestampAttribute)).FirstOrDefault<PXEventSubscriberAttribute>() is PXDBTimestampAttribute timestampAttribute) || !(cach.GetValue(cach.Current, timestampAttribute.FieldOrdinal) is byte[] numArray))
      return;
    graph._primaryRecordTimeStamp = numArray;
  }

  /// <summary>The modes of opening a page to which the exception redirects.</summary>
  public enum WindowMode
  {
    Base,
    Same,
    New,
    /// <summary>The new window.</summary>
    NewWindow,
    InlineWindow,
    Layer,
  }

  [Serializable]
  public struct Filter : IEquatable<PXBaseRedirectException.Filter>
  {
    private readonly string _dataMember;
    private readonly Guid? _id;
    private readonly string _name;
    private readonly PXFilterRow[] _filterRows;

    /// <summary>
    /// All records will be shown; if default filter exists, it won't be applied.
    /// </summary>
    /// <param name="dataMember"></param>
    public Filter(string dataMember)
    {
      this._dataMember = dataMember;
      this._id = new Guid?();
      this._name = (string) null;
      this._filterRows = (PXFilterRow[]) null;
    }

    public Filter(string dataMember, Guid id)
      : this(dataMember)
    {
      this._id = new Guid?(id);
    }

    public Filter(string dataMember, string name)
      : this(dataMember)
    {
      this._name = name;
    }

    public Filter(string dataMember, PXFilterRow[] filterRows)
      : this(dataMember)
    {
      this._filterRows = filterRows;
    }

    [JsonConstructor]
    public Filter(string dataMember, PXFilterRow[] filterRows, string name)
      : this(dataMember, filterRows)
    {
      this._name = name;
    }

    public string DataMember => this._dataMember;

    public Guid? ID => this._id;

    public string Name => this._name;

    public PXFilterRow[] FilterRows => this._filterRows;

    public bool Equals(PXBaseRedirectException.Filter other)
    {
      if (other.DataMember == this.DataMember)
      {
        Guid? id1 = other.ID;
        Guid? id2 = this.ID;
        if ((id1.HasValue == id2.HasValue ? (id1.HasValue ? (id1.GetValueOrDefault() == id2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
        {
          id2 = other.ID;
          if (!id2.HasValue)
          {
            id2 = this.ID;
            if (id2.HasValue)
              goto label_14;
          }
          else
            goto label_14;
        }
        if (other.Name == this.Name)
        {
          if (other.FilterRows == null && this.FilterRows == null)
            return true;
          int? length1 = other.FilterRows?.Length;
          int? length2 = this.FilterRows?.Length;
          if (length1.GetValueOrDefault() == length2.GetValueOrDefault() & length1.HasValue == length2.HasValue)
          {
            for (int index = 0; index < this.FilterRows.Length; ++index)
            {
              if (!other.FilterRows[index].Equals(this.FilterRows[index]))
                return false;
            }
            return true;
          }
        }
      }
label_14:
      return false;
    }
  }
}
