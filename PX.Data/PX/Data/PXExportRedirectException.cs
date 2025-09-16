// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExportRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class PXExportRedirectException : PXBaseRedirectException
{
  private readonly string _extension;
  private readonly string _key;
  private readonly object _data;

  public string Extension => this._extension;

  public string Key => this._key;

  public object ExportData => this._data;

  public virtual string Url
  {
    get
    {
      return string.Format(this.AppendDateToUrl ? "{0}_{1:yyyyMMdd}.{2}" : "{0}.{2}", (object) this.Message, (object) System.DateTime.Today, (object) this.Extension);
    }
  }

  protected virtual bool AppendDateToUrl => true;

  public PXExportRedirectException(string ext, string message, string key, object data)
    : base(string.IsNullOrEmpty(message) ? string.Empty : message.Replace(' ', '_'))
  {
    this._extension = ext.TrimStart('.');
    this._key = key;
    this._data = data;
  }

  public PXExportRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXExportRedirectException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXExportRedirectException>(this, info);
    base.GetObjectData(info, context);
  }
}
