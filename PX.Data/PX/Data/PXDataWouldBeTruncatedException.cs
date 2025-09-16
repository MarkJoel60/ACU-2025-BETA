// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataWouldBeTruncatedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class PXDataWouldBeTruncatedException(string table, Exception inner) : PXDatabaseException(table, (object[]) null, PXDbExceptions.DataWouldBeTruncated, inner.Message, inner)
{
  public string Column { get; set; }

  public PXDBOperation? Operation { get; set; }

  public string CommandText { get; set; }

  public override string Message
  {
    get
    {
      PXDBOperation? operation = this.Operation;
      string local;
      if (operation.HasValue)
      {
        switch (operation.GetValueOrDefault())
        {
          case PXDBOperation.Insert:
            local = ErrorMessages.GetLocal("Inserting ");
            goto label_5;
          case PXDBOperation.Delete:
            local = ErrorMessages.GetLocal("Deleting ");
            goto label_5;
        }
      }
      local = ErrorMessages.GetLocal("Updating ");
label_5:
      if (string.IsNullOrEmpty(this.Column))
        this._Message = PXMessages.LocalizeFormat("{0} the '{1}' record failed because data in some field is too long.", out this._MessagePrefix, (object) local, (object) this._Table);
      else
        this._Message = PXMessages.LocalizeFormat("{0} the '{1}' record failed because data in the '{2}' field is too long.", out this._MessagePrefix, (object) local, (object) this._Table, (object) this.Column);
      return base.Message;
    }
  }
}
