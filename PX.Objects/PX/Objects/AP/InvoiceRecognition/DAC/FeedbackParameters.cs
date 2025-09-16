// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.FeedbackParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXHidden]
public class FeedbackParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal DocumentFeedbackBuilder FeedbackBuilder { get; set; }

  internal Dictionary<string, Uri> Links { get; set; }
}
