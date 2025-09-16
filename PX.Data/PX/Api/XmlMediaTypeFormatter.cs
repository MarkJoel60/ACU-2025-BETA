// Decompiled with JetBrains decompiler
// Type: PX.Api.XmlMediaTypeFormatter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;

#nullable disable
namespace PX.Api;

public class XmlMediaTypeFormatter : XmlMediaTypeFormatter
{
  public virtual bool CanWriteType(Type type)
  {
    return (this.UseXmlSerializer || !typeof (ICollection<>).GenericIsAssignableFrom(type) || type.HasDefaultConstructor()) && base.CanWriteType(type);
  }
}
