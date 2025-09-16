// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.Descriptor.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.Descriptor;

[PXLocalizable("PO Error")]
public static class Messages
{
  public const string InvalidAssignmentMap = "Invalid assignment map";
  public const string OnlyPurchaseOrdersAreAllowedMessage = "Only Purchase Orders are allowed.";
  public const string NoteFilesFieldName = "NoteFiles";
  private const string Prefix = "PO Error";

  public static class PoSetup
  {
    public const string SubcontractNumberingName = "SUBCONTR";
    public const string SubcontractNumberingId = "Subcontract Numbering Sequence";
    public const string RequireSubcontractControlTotal = "Validate Total on Entry";
    public const string SubcontractRequireApproval = "Require Approval";
  }
}
