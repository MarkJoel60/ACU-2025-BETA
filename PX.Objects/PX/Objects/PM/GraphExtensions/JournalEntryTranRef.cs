// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.JournalEntryTranRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class JournalEntryTranRef : PXGraph<JournalEntryTranRef>
{
  public virtual string GetDocType(PX.Objects.AP.APInvoice apDoc, PX.Objects.AR.ARInvoice arDoc, PX.Objects.GL.GLTran glTran)
  {
    if (apDoc != null)
    {
      switch (apDoc.DocType)
      {
        case "INV":
          return "BL";
        case "ACR":
          return "CA";
        case "ADR":
          return "DA";
        default:
          return (string) null;
      }
    }
    else
    {
      if (arDoc == null)
        return (string) null;
      switch (arDoc.DocType)
      {
        case "INV":
          return "IN";
        case "CRM":
          return "CR";
        case "DRM":
          return "DM";
        default:
          return (string) null;
      }
    }
  }

  public virtual Guid? GetNoteID(PX.Objects.AP.APInvoice apDoc, PX.Objects.AR.ARInvoice arDoc, PX.Objects.GL.GLTran glTran)
  {
    if (apDoc != null)
      return apDoc.NoteID;
    return arDoc?.NoteID;
  }

  public virtual void AssignCustomerVendorEmployee(PX.Objects.GL.GLTran glTran, PMTran pmTran)
  {
    pmTran.BAccountID = glTran.ReferenceID;
  }

  public virtual void AssignAdditionalFields(PX.Objects.GL.GLTran glTran, PMTran pmTran)
  {
  }

  public virtual List<TranWithInfo> GetAdditionalProjectTrans(
    string module,
    string tranType,
    string refNbr)
  {
    return new List<TranWithInfo>();
  }
}
