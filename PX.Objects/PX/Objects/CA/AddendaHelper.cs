// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddendaHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Objects.CA;

public class AddendaHelper
{
  public const string fieldStart = "[";
  public const string fieldEnd = "]";
  public const char separator = '*';
  public const char terminator = '\\';
  public const string formatter = ":";
  public static Dictionary<string, Type> Mapping = new Dictionary<string, Type>()
  {
    {
      "Payment Ref",
      typeof (PX.Objects.AP.APPayment.refNbr)
    },
    {
      "Payment Descr",
      typeof (PX.Objects.AP.APPayment.docDesc)
    },
    {
      "Payment Period",
      typeof (PX.Objects.AP.APPayment.finPeriodID)
    },
    {
      "Payment Date",
      typeof (PX.Objects.AP.APPayment.docDate)
    },
    {
      "Payment Ext Ref",
      typeof (PX.Objects.AP.APPayment.extRefNbr)
    },
    {
      "Payment Amount",
      typeof (PX.Objects.AP.APPayment.curyOrigDocAmt)
    },
    {
      "Bill Ref",
      typeof (PX.Objects.AP.APInvoice.refNbr)
    },
    {
      "Bill Ext Ref",
      typeof (PX.Objects.AP.APInvoice.invoiceNbr)
    },
    {
      "Bill Amount",
      typeof (PX.Objects.AP.APInvoice.origDiscAmt)
    },
    {
      "Bill Descr",
      typeof (PX.Objects.AP.APInvoice.docDesc)
    },
    {
      "Bill Date",
      typeof (PX.Objects.AP.APInvoice.docDate)
    },
    {
      "Bill Period",
      typeof (PX.Objects.AP.APInvoice.finPeriodID)
    },
    {
      "Bill Tax Amount",
      typeof (PX.Objects.AP.APInvoice.curyTaxTotal)
    }
  };

  public static string ParseAddendaTemplate(string templateString, out AddendaItemInfo[] parameters)
  {
    List<AddendaItemInfo> addendaItemInfoList = new List<AddendaItemInfo>();
    StringBuilder stringBuilder1 = new StringBuilder();
    int num1 = 0;
    string str1 = templateString;
    char[] chArray = new char[1]{ '*' };
    foreach (string str2 in str1.Split(chArray))
    {
      char ch;
      if (str2.StartsWith("["))
      {
        if (!str2.EndsWith("]"))
        {
          string str3 = str2;
          ch = '\\';
          string str4 = ch.ToString();
          if (!str3.EndsWith(str4))
            throw new PXException("The format string is missing a closing bracket. Correct the template.");
        }
        int num2 = str2.IndexOf("[");
        int num3 = str2.IndexOf("]");
        if (str2.Contains(":"))
        {
          int num4 = str2.IndexOf(":");
          string key = str2.Substring(num2 + 1, num4 - 1);
          addendaItemInfoList.Add(new AddendaItemInfo()
          {
            FieldName = key,
            FieldFormat = str2.Substring(num4 + 1, num3 - num4 - 1),
            FieldType = AddendaHelper.Mapping[key]
          });
        }
        else
        {
          string key = str2.Substring(num2 + 1, num3 - 1);
          addendaItemInfoList.Add(new AddendaItemInfo()
          {
            FieldName = key,
            FieldType = AddendaHelper.Mapping[key]
          });
        }
        stringBuilder1.Append($"{{{num1.ToString()}}}");
        ++num1;
      }
      else
        stringBuilder1.Append(str2);
      StringBuilder stringBuilder2 = stringBuilder1;
      ch = '*';
      string str5 = ch.ToString();
      stringBuilder2.Append(str5);
    }
    stringBuilder1[stringBuilder1.Length - 1] = '\\';
    parameters = addendaItemInfoList.ToArray();
    return stringBuilder1.ToString();
  }

  public static string BuildAddenda(PXResultset<PX.Objects.AP.APPayment> records, string templateString)
  {
    StringBuilder stringBuilder = new StringBuilder();
    AddendaItemInfo[] parameters;
    string addendaTemplate = AddendaHelper.ParseAddendaTemplate(templateString, out parameters);
    foreach (PXResult<PX.Objects.AP.APPayment, APAdjust, PX.Objects.AP.APInvoice> record in records)
    {
      PX.Objects.AP.APPayment payment = PXResult<PX.Objects.AP.APPayment, APAdjust, PX.Objects.AP.APInvoice>.op_Implicit(record);
      APAdjust adjust = PXResult<PX.Objects.AP.APPayment, APAdjust, PX.Objects.AP.APInvoice>.op_Implicit(record);
      PX.Objects.AP.APInvoice invoice = PXResult<PX.Objects.AP.APPayment, APAdjust, PX.Objects.AP.APInvoice>.op_Implicit(record);
      stringBuilder.Append(string.Format(addendaTemplate, (object[]) AddendaHelper.GetParameters(parameters, payment, adjust, invoice)));
      if (stringBuilder.Length > 80 /*0x50*/)
      {
        stringBuilder = new StringBuilder(stringBuilder.ToString().Substring(0, 80 /*0x50*/));
        break;
      }
    }
    if (stringBuilder[stringBuilder.Length - 1] != '\\')
    {
      for (int index = stringBuilder.Length - 1; index >= 0; --index)
      {
        if (stringBuilder[index] == '*')
        {
          stringBuilder = new StringBuilder(stringBuilder.ToString().Substring(0, index));
          stringBuilder.Append('\\');
          break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public static string[] GetParameters(
    AddendaItemInfo[] parameters,
    PX.Objects.AP.APPayment payment,
    APAdjust adjust,
    PX.Objects.AP.APInvoice invoice)
  {
    List<string> stringList1 = new List<string>();
    foreach (AddendaItemInfo parameter in parameters)
    {
      string fieldName = parameter.FieldName;
      DateTime? docDate;
      DateTime dateTime;
      if (fieldName != null)
      {
        switch (fieldName.Length)
        {
          case 8:
            if (fieldName == "Bill Ref")
            {
              stringList1.Add(invoice.RefNbr);
              continue;
            }
            continue;
          case 9:
            if (fieldName == "Bill Date")
            {
              List<string> stringList2 = stringList1;
              string str;
              if (!string.IsNullOrEmpty(parameter.FieldFormat))
              {
                docDate = invoice.DocDate;
                dateTime = docDate.Value;
                str = dateTime.ToString(parameter.FieldFormat);
              }
              else
              {
                docDate = invoice.DocDate;
                str = docDate.ToString();
              }
              stringList2.Add(str);
              continue;
            }
            continue;
          case 10:
            if (fieldName == "Bill Descr")
            {
              stringList1.Add(invoice.DocDesc);
              continue;
            }
            continue;
          case 11:
            switch (fieldName[5])
            {
              case 'A':
                if (fieldName == "Bill Amount")
                {
                  stringList1.Add(invoice.OrigDocAmt.ToString());
                  continue;
                }
                continue;
              case 'P':
                if (fieldName == "Bill Period")
                {
                  stringList1.Add(invoice.FinPeriodID);
                  continue;
                }
                continue;
              case 'n':
                if (fieldName == "Payment Ref")
                {
                  stringList1.Add(payment.RefNbr);
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 12:
            switch (fieldName[0])
            {
              case 'B':
                if (fieldName == "Bill Ext Ref")
                {
                  stringList1.Add(invoice.InvoiceNbr);
                  continue;
                }
                continue;
              case 'P':
                if (fieldName == "Payment Date")
                {
                  List<string> stringList3 = stringList1;
                  string str;
                  if (!string.IsNullOrEmpty(parameter.FieldFormat))
                  {
                    docDate = payment.DocDate;
                    dateTime = docDate.Value;
                    str = dateTime.ToString(parameter.FieldFormat);
                  }
                  else
                  {
                    docDate = payment.DocDate;
                    str = docDate.ToString();
                  }
                  stringList3.Add(str);
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 13:
            if (fieldName == "Payment Descr")
            {
              stringList1.Add(payment.DocDesc);
              continue;
            }
            continue;
          case 14:
            switch (fieldName[8])
            {
              case 'A':
                if (fieldName == "Payment Amount")
                {
                  stringList1.Add(payment.CuryOrigDocAmt.ToString());
                  continue;
                }
                continue;
              case 'P':
                if (fieldName == "Payment Period")
                {
                  stringList1.Add(payment.FinPeriodID);
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 15:
            switch (fieldName[0])
            {
              case 'B':
                if (fieldName == "Bill Tax Amount")
                {
                  stringList1.Add(invoice.CuryTaxTotal.ToString());
                  continue;
                }
                continue;
              case 'P':
                if (fieldName == "Payment Ext Ref")
                {
                  stringList1.Add(payment.ExtRefNbr);
                  continue;
                }
                continue;
              default:
                continue;
            }
          default:
            continue;
        }
      }
    }
    return stringList1.ToArray();
  }
}
