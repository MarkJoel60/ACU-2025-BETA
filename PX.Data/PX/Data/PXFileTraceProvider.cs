// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFileTraceProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.TraceProviders;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace PX.Data;

internal sealed class PXFileTraceProvider : PXTraceProvider, IExposesMinimumLevel
{
  private string filename;

  public override void Initialize(string provname, NameValueCollection config)
  {
    if (config == null)
      throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    base.Initialize(provname, config);
    this.filename = config["file"];
    if (string.IsNullOrEmpty(this.filename))
      throw new ArgumentNullException("file");
  }

  public SourceLevels MinimumLevel => this.ConfiguredMinimumLevel;

  protected override void TraceEventInternal(PXTrace.Event item)
  {
    using (StreamWriter streamWriter = new StreamWriter(this.filename, true))
    {
      streamWriter.WriteLine($"=========={item.EventType.ToString()}=========={System.DateTime.Now}==========");
      streamWriter.WriteLine(item.ToString(true));
    }
  }
}
