// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXExchangeConversionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Mail;
using PX.Data;
using PX.Data.Update.ExchangeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.CS.Email;

public static class PXExchangeConversionHelper
{
  public static ExtendedPropertyType[] GetExtendedProperties(
    params Tuple<string, MapiPropertyTypeType, object>[] def)
  {
    List<ExtendedPropertyType> extendedPropertyTypeList = new List<ExtendedPropertyType>();
    for (int index = 0; index < def.Length; ++index)
    {
      Tuple<string, MapiPropertyTypeType, object> tuple = def[index];
      if (tuple.Item3 != null)
        extendedPropertyTypeList.Add(new ExtendedPropertyType()
        {
          ExtendedFieldURI = new PathToExtendedFieldType()
          {
            PropertyTag = tuple.Item1,
            PropertyType = tuple.Item2
          },
          Item = tuple.Item3
        });
    }
    return extendedPropertyTypeList.Count <= 0 ? (ExtendedPropertyType[]) null : extendedPropertyTypeList.ToArray();
  }

  public static PhysicalAddressDictionaryEntryType GetValueByType(
    PhysicalAddressDictionaryEntryType[] types,
    PhysicalAddressKeyType key,
    bool checkEmpty = false)
  {
    if (types != null)
    {
      foreach (PhysicalAddressDictionaryEntryType type in types)
      {
        if (type.Key == key)
          return type;
      }
    }
    if (checkEmpty)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("Email with type {0} has not found.", new object[1]
      {
        (object) key.ToString()
      }));
    return (PhysicalAddressDictionaryEntryType) null;
  }

  public static TaskStatusType ParceActivityStatus(string status)
  {
    if (status != null && status.Length == 2)
    {
      switch (status[0])
      {
        case 'A':
          if (status == "AP")
            return (TaskStatusType) 0;
          break;
        case 'C':
          switch (status)
          {
            case "CL":
              return (TaskStatusType) 2;
            case "CD":
              return (TaskStatusType) 2;
          }
          break;
        case 'D':
          if (status == "DR")
            return (TaskStatusType) 0;
          break;
        case 'I':
          if (status == "IP")
            return (TaskStatusType) 1;
          break;
        case 'O':
          if (status == "OP")
            return (TaskStatusType) 0;
          break;
        case 'P':
          if (status == "PA")
            return (TaskStatusType) 0;
          break;
        case 'R':
          switch (status)
          {
            case "RJ":
              return (TaskStatusType) 4;
            case "RL":
              return (TaskStatusType) 2;
          }
          break;
      }
    }
    return (TaskStatusType) 0;
  }

  public static string ParceActivityStatus(TaskStatusType status)
  {
    switch ((int) status)
    {
      case 0:
        return "OP";
      case 1:
        return "IP";
      case 2:
        return "CD";
      case 3:
        return "IP";
      case 4:
        return "RJ";
      default:
        return "DR";
    }
  }

  public static ImportanceChoicesType ParceActivityPriority(int? priority)
  {
    if (!priority.HasValue)
      return (ImportanceChoicesType) 1;
    if (priority.HasValue)
    {
      switch (priority.GetValueOrDefault())
      {
        case 0:
          return (ImportanceChoicesType) 0;
        case 1:
          return (ImportanceChoicesType) 1;
        case 2:
          return (ImportanceChoicesType) 2;
      }
    }
    return (ImportanceChoicesType) 1;
  }

  public static int ParceActivityPriority(ImportanceChoicesType priority)
  {
    switch ((int) priority)
    {
      case 0:
        return 0;
      case 1:
        return 1;
      case 2:
        return 2;
      default:
        return 1;
    }
  }

  public static string ParseReminderMinutesBeforeStart(string reminderMinutesBeforeStart)
  {
    if (reminderMinutesBeforeStart != null)
    {
      switch (reminderMinutesBeforeStart.Length)
      {
        case 1:
          switch (reminderMinutesBeforeStart[0])
          {
            case '0':
              return "ATEV";
            case '5':
              return "B05m";
          }
          break;
        case 2:
          switch (reminderMinutesBeforeStart[0])
          {
            case '1':
              if (reminderMinutesBeforeStart == "15")
                return "B15m";
              break;
            case '3':
              if (reminderMinutesBeforeStart == "30")
                return "B30m";
              break;
            case '6':
              if (reminderMinutesBeforeStart == "60")
                return "B01h";
              break;
          }
          break;
        case 3:
          if (reminderMinutesBeforeStart == "120")
            return "B02h";
          break;
        case 4:
          switch (reminderMinutesBeforeStart[0])
          {
            case '1':
              if (reminderMinutesBeforeStart == "1440")
                return "B01d";
              break;
            case '4':
              if (reminderMinutesBeforeStart == "4320")
                return "B03d";
              break;
          }
          break;
        case 5:
          if (reminderMinutesBeforeStart == "10080")
            return "B07d";
          break;
      }
    }
    return "EXCH";
  }

  public static ResponseTypeType ParceAttendeeStatus(int? status)
  {
    if (!status.HasValue)
      return (ResponseTypeType) 0;
    if (status.HasValue)
    {
      switch (status.GetValueOrDefault())
      {
        case 0:
          return (ResponseTypeType) 0;
        case 1:
          return (ResponseTypeType) 5;
        case 2:
          return (ResponseTypeType) 3;
        case 3:
          return (ResponseTypeType) 4;
        case 4:
          return (ResponseTypeType) 3;
        case 5:
          return (ResponseTypeType) 4;
      }
    }
    return (ResponseTypeType) 0;
  }

  public static int ParceAttendeeStatus(ResponseTypeType status)
  {
    switch ((int) status)
    {
      case 0:
        return 0;
      case 1:
        return 2;
      case 2:
        return 2;
      case 3:
        return 2;
      case 4:
        return 3;
      case 5:
        return 1;
      default:
        return 0;
    }
  }

  public static LegacyFreeBusyType ParceShowAs(int? status)
  {
    if (!status.HasValue)
      return (LegacyFreeBusyType) 5;
    if (status.HasValue)
    {
      switch (status.GetValueOrDefault())
      {
        case 1:
          return (LegacyFreeBusyType) 0;
        case 2:
          return (LegacyFreeBusyType) 2;
        case 3:
          return (LegacyFreeBusyType) 3;
      }
    }
    return (LegacyFreeBusyType) 5;
  }

  public static int ParceShowAs(LegacyFreeBusyType status)
  {
    switch ((int) status)
    {
      case 0:
        return 1;
      case 2:
        return 2;
      case 3:
        return 3;
      default:
        return 1;
    }
  }

  public static object ParceAcGender(string status)
  {
    switch (status)
    {
      case null:
        return (object) "0";
      case "M":
        return (object) "2";
      case "F":
        return (object) "1";
      default:
        return (object) "0";
    }
  }

  public static string ParceExGender(object status)
  {
    if (status == null)
      return (string) null;
    switch (status.ToString())
    {
      case "2":
        return "M";
      case "1":
        return "F";
      case "0":
        return (string) null;
      default:
        return (string) null;
    }
  }

  public static PhoneNumberDictionaryEntryType ParcePhone(string type, string value)
  {
    PhoneNumberKeyType phoneNumberKeyType = (PhoneNumberKeyType) 6;
    if (type != null)
    {
      switch (type.Length)
      {
        case 1:
          if (type == "C")
          {
            phoneNumberKeyType = (PhoneNumberKeyType) 11;
            break;
          }
          break;
        case 2:
          switch (type[1])
          {
            case '1':
              switch (type)
              {
                case "B1":
                  phoneNumberKeyType = (PhoneNumberKeyType) 2;
                  break;
                case "H1":
                  phoneNumberKeyType = (PhoneNumberKeyType) 8;
                  break;
              }
              break;
            case '2':
              if (type == "B2")
              {
                phoneNumberKeyType = (PhoneNumberKeyType) 3;
                break;
              }
              break;
            case '3':
              if (type == "B3")
              {
                phoneNumberKeyType = (PhoneNumberKeyType) 13;
                break;
              }
              break;
            case 'F':
              switch (type)
              {
                case "BF":
                  phoneNumberKeyType = (PhoneNumberKeyType) 1;
                  break;
                case "HF":
                  phoneNumberKeyType = (PhoneNumberKeyType) 7;
                  break;
              }
              break;
          }
          break;
        case 3:
          if (type == "BA1")
          {
            phoneNumberKeyType = (PhoneNumberKeyType) 0;
            break;
          }
          break;
      }
    }
    return new PhoneNumberDictionaryEntryType()
    {
      Key = phoneNumberKeyType,
      Value = value
    };
  }

  public static bool ParcePhone(
    PhoneNumberDictionaryEntryType phone,
    out string type,
    out string value)
  {
    value = phone.Value;
    type = "C";
    switch ((int) phone.Key)
    {
      case 0:
        type = "BA1";
        break;
      case 1:
        type = "BF";
        break;
      case 2:
      case 15:
        type = "B1";
        break;
      case 3:
        type = "B2";
        break;
      case 4:
      case 5:
      case 6:
      case 9:
      case 10:
      case 12:
      case 14:
      case 16 /*0x10*/:
      case 17:
        return false;
      case 7:
        type = "HF";
        break;
      case 8:
        type = "H1";
        break;
      case 11:
        type = "C";
        break;
      case 13:
        type = "B3";
        break;
    }
    return true;
  }

  public static IEnumerable<PhoneNumberDictionaryEntryType> PurgePhones(
    params PhoneNumberDictionaryEntryType[] existingTypes)
  {
    // ISSUE: unable to decompile the method.
  }

  public static EmailAddressType ParceAddress(string address)
  {
    if (string.IsNullOrEmpty(address))
      return (EmailAddressType) null;
    using (List<MailAddress>.Enumerator enumerator = EmailParser.ParseAddresses(address).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        MailAddress current = enumerator.Current;
        return new EmailAddressType()
        {
          EmailAddress = current.Address,
          Name = current.DisplayName
        };
      }
    }
    return (EmailAddressType) null;
  }

  public static EmailAddressType[] ParceAddresses(string address)
  {
    if (string.IsNullOrEmpty(address))
      return (EmailAddressType[]) null;
    List<EmailAddressType> emailAddressTypeList = new List<EmailAddressType>();
    foreach (MailAddress address1 in EmailParser.ParseAddresses(address))
      emailAddressTypeList.Add(new EmailAddressType()
      {
        EmailAddress = address1.Address,
        Name = address1.DisplayName
      });
    return emailAddressTypeList.ToArray();
  }

  public static string ParceAddresses(EmailAddressType[] addresses)
  {
    return addresses == null || addresses.Length == 0 ? (string) null : PXDBEmailAttribute.ToString(((IEnumerable<EmailAddressType>) addresses).Select<EmailAddressType, MailAddress>((Func<EmailAddressType, MailAddress>) (_ => new MailAddress(_.EmailAddress, _.Name))));
  }
}
