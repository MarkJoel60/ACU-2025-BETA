// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseUnhandledRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public abstract class PXBaseUnhandledRedirectException : PXBaseRedirectException
{
  public string DataSourceID { get; set; }

  protected PXBaseUnhandledRedirectException(string message)
    : base(message)
  {
  }

  protected PXBaseUnhandledRedirectException(string message, bool repaintControls)
    : base(message, repaintControls)
  {
  }

  protected PXBaseUnhandledRedirectException(string message, params object[] args)
    : base(message, args)
  {
  }

  protected PXBaseUnhandledRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
