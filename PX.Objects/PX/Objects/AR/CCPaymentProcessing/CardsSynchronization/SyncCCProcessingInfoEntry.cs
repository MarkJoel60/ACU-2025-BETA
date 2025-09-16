// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CardsSynchronization.SyncCCProcessingInfoEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.CardsSynchronization;

public class SyncCCProcessingInfoEntry
{
  public Guid NoteId { get; set; }

  public PXProcessingMessage ProcessingMessage { get; set; }
}
