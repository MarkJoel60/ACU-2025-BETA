// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.RowNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.Exceptions;

public class RowNotFoundException : PXException
{
  private const string KeysSeparator = " ";

  public Type RowType { get; protected set; }

  public object[] Keys { get; protected set; }

  public RowNotFoundException(PXCache cache, params object[] keys)
    : base("'{0}' '{1}' cannot be found in the system.", new object[2]
    {
      (object) cache.DisplayName,
      (object) string.Join(" ", keys)
    })
  {
    this.RowType = cache.GetItemType();
    this.Keys = keys;
  }

  public RowNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
