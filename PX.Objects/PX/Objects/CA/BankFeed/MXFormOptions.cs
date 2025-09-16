// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.MXFormOptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class MXFormOptions : PXPluginRedirectOptions
{
  public const string ConnectMode = "Connect";
  public const string UpdateMode = "Update";

  public virtual string Path => "plugins/bank-feed/qp-mx";

  public virtual string TagName => "qp-mx";

  public string Url { get; set; }

  public string Mode { get; set; }
}
