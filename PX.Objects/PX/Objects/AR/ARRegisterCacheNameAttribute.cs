// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterCacheNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ARRegisterCacheNameAttribute(string name) : PXCacheNameAttribute(name)
{
  public virtual string GetName(object row)
  {
    if (!(row is ARRegister arRegister))
      return ((PXNameAttribute) this).GetName();
    string empty = string.Empty;
    string docType = arRegister.DocType;
    string str;
    if (docType != null && docType.Length == 3)
    {
      switch (docType[2])
      {
        case 'B':
          if (docType == "SMB")
          {
            str = "Balance WO";
            goto label_27;
          }
          break;
        case 'C':
          if (docType == "SMC")
          {
            str = "Credit WO";
            goto label_27;
          }
          break;
        case 'F':
          switch (docType)
          {
            case "REF":
              str = "Refund";
              goto label_27;
            case "VRF":
              str = "Voided Refund";
              goto label_27;
          }
          break;
        case 'H':
          if (docType == "FCH")
          {
            str = "Overdue Charge";
            goto label_27;
          }
          break;
        case 'L':
          if (docType == "CSL")
          {
            str = "Cash Sale";
            goto label_27;
          }
          break;
        case 'M':
          switch (docType)
          {
            case "CRM":
              str = "Credit Memo";
              goto label_27;
            case "DRM":
              str = "Debit Memo";
              goto label_27;
            case "PPM":
              str = "Prepayment";
              goto label_27;
            case "RPM":
              str = "Voided Payment";
              goto label_27;
          }
          break;
        case 'S':
          if (docType == "RCS")
          {
            str = "Cash Return";
            goto label_27;
          }
          break;
        case 'T':
          if (docType == "PMT")
          {
            str = "Payment";
            goto label_27;
          }
          break;
        case 'V':
          if (docType == "INV")
          {
            str = "Invoice";
            goto label_27;
          }
          break;
      }
    }
    str = "Register";
label_27:
    return PXMessages.LocalizeNoPrefix(str);
  }
}
