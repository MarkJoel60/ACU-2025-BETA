// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CreateShipmentArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <exclude />
public class CreateShipmentArgs
{
  public SOOrderEntry Graph { get; set; }

  public bool MassProcess { get; set; }

  public SOOrder Order { get; set; }

  public int? OrderLineNbr { get; set; }

  public int? SiteID { get; set; }

  public DateTime? ShipDate { get; set; }

  public DateTime? EndDate { get; set; }

  public bool? UseOptimalShipDate { get; set; }

  public string Operation { get; set; }

  public DocumentList<SOShipment> ShipmentList { get; set; }

  public PXQuickProcess.ActionFlow QuickProcessFlow { get; set; }

  public string ShipmentType { get; set; } = "I";

  public PXNoteAttribute.IPXCopySettings CopyLineNotesAndFilesSettings { get; set; }

  public object FilesAndNotesSource { get; set; }

  public PXNoteAttribute.IPXCopySettings CopyNotesAndFilesSettings { get; set; }
}
