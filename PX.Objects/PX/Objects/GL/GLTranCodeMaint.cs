// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.GL;

[Serializable]
public class GLTranCodeMaint : PXGraph<GLTranCodeMaint>
{
  [PXImport(typeof (GLTranCode))]
  public PXSelect<GLTranCode> TranCodes;
  public PXSavePerRow<GLTranCode> Save;
  public PXCancel<GLTranCode> Cancel;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [CAAPARTranType.ListByModuleRestricted(typeof (GLTranCode.module))]
  [PXUIField]
  protected virtual void GLTranCode_TranType_CacheAttached(PXCache sender)
  {
  }

  public virtual void GLTranCode_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    GLTranCode row = (GLTranCode) e.Row;
    if (!GLTranCodeMaint.IsSupported(row))
    {
      PXErrorLevel pxErrorLevel = row.Active.GetValueOrDefault() ? (PXErrorLevel) 4 : (PXErrorLevel) 2;
      sender.RaiseExceptionHandling<GLTranCode.tranType>((object) row, (object) row.TranType, (Exception) new PXSetPropertyException("This Tran. type is not supported yet. It may not be set 'Active'", pxErrorLevel));
    }
    else
      sender.RaiseExceptionHandling<GLTranCode.tranType>((object) row, (object) row.TranType, (Exception) null);
  }

  public virtual void GLTranCode_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    GLTranCode row = (GLTranCode) e.Row;
    if (!row.Active.GetValueOrDefault() || GLTranCodeMaint.IsSupported(row))
      return;
    sender.RaiseExceptionHandling<GLTranCode.tranType>((object) row, (object) row.TranType, (Exception) new PXSetPropertyException("This Tran. type is not supported yet. It may not be set 'Active'", (PXErrorLevel) 4));
  }

  public virtual void GLTranCode_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row == null)
      return;
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (GLTranDoc.tranCode), (Type) null, (string) null);
  }

  protected static bool IsSupported(GLTranCode row)
  {
    bool flag = true;
    if (row.Module == "AP" && (row.TranType == "REF" || row.TranType == "VCK" || row.TranType == "VQC") || row.Module == "AR" && (row.TranType == "REF" || row.TranType == "FCH" || row.TranType == "SMB" || row.TranType == "SMC" || row.TranType == "UND" || row.TranType == "UND" || row.TranType == "RPM" || row.TranType == "RCS") || row.Module == "CA" && (row.TranType == "CAG" || row.TranType == "CDT" || row.TranType == "CVD" || row.TranType == "CTE" || row.TranType == "CTO" || row.TranType == "CTI"))
      flag = false;
    return flag;
  }
}
