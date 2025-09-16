// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseSlot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDatabaseSlot
{
  private System.DateTime createTime;
  private System.DateTime lastAccessTime;
  private Lazy<object> _prefetchMethod;

  public System.DateTime CreateTime => this.createTime;

  public System.DateTime LastAccessTime => this.lastAccessTime;

  public bool IsValueFaulted { get; set; }

  public PXDatabaseSlot(object value)
  {
    this.createTime = this.lastAccessTime = System.DateTime.Now;
    this._prefetchMethod = new Lazy<object>((Func<object>) (() => value));
  }

  public PXDatabaseSlot(Func<object> prefetchMethod)
  {
    this.createTime = this.lastAccessTime = System.DateTime.Now;
    this._prefetchMethod = new Lazy<object>(prefetchMethod);
  }

  public PXDatabaseSlot(Func<PXDatabaseSlot, object> prefetchMethod)
  {
    PXDatabaseSlot pxDatabaseSlot = this;
    this.createTime = this.lastAccessTime = System.DateTime.Now;
    this._prefetchMethod = new Lazy<object>((Func<object>) (() => prefetchMethod(pxDatabaseSlot)));
  }

  public object Value
  {
    get
    {
      this.lastAccessTime = System.DateTime.Now;
      return this._prefetchMethod.Value;
    }
  }
}
