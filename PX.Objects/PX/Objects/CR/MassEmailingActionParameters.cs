// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MassEmailingActionParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class MassEmailingActionParameters : IMassEmailingAction
{
  public MassEmailingActionParameters()
  {
  }

  public MassEmailingActionParameters(PXAdapter adapter)
  {
    this.MassProcess = adapter.MassProcess;
    this.Arguments = adapter.Arguments;
    bool? nullable1;
    adapter.Arguments.TryGetTypedValue<string, object, bool?>(nameof (AggregateEmails), out nullable1);
    this.AggregateEmails = nullable1;
    bool? nullable2;
    adapter.Arguments.TryGetTypedValue<string, object, bool?>(nameof (AggregateAttachments), out nullable2);
    this.AggregateAttachments = nullable2;
    string str;
    adapter.Arguments.TryGetTypedValue<string, object, string>(nameof (AggregatedAttachmentFileName), out str);
    this.AggregatedAttachmentFileName = str;
  }

  public bool MassProcess { get; set; }

  public Dictionary<string, object> Arguments { get; set; }

  public bool? AggregateEmails { get; set; }

  public bool? AggregateAttachments { get; set; }

  public string AggregatedAttachmentFileName { get; set; }
}
