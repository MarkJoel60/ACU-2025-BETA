// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBEmailAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Mail;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

#nullable disable
namespace PX.Data;

/// <summary>Maps a <tt>string</tt> DAC field representing email addresses
/// to the database column of <tt>nvarchar</tt> type.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>The field value must be a Unicode string. The field value length
/// is limited by 255.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBEmail]
/// [PXUIField(DisplayName = "Email",
///            Visibility = PXUIVisibility.SelectorVisible)]
/// public virtual string Email { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class PXDBEmailAttribute : PXDBStringAttribute, IPXFieldVerifyingSubscriber
{
  private const string EMAIL_SEPARATOR = "; ";
  /// <exclude />
  public static Dictionary<System.Type, List<string>> FieldList = new Dictionary<System.Type, List<string>>();

  /// <summary>
  /// Initializes a new instance of the attribute. The maximum string length is set to 255. The string is marked as Unicode.
  /// </summary>
  public PXDBEmailAttribute()
    : base((int) byte.MaxValue)
  {
    this.IsUnicode = true;
    this.IsFixed = false;
  }

  /// <exclude />
  public new virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    e.NewValue = (object) PXDBEmailAttribute.FormatAddressesWithoutDisplayName(newValue);
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    lock (((ICollection) PXDBEmailAttribute.FieldList).SyncRoot)
    {
      List<string> stringList;
      if (!PXDBEmailAttribute.FieldList.TryGetValue(bqlTable, out stringList))
        PXDBEmailAttribute.FieldList[bqlTable] = stringList = new List<string>();
      if (stringList.Contains(this.FieldName))
        return;
      stringList.Add(this.FieldName);
    }
  }

  /// <exclude />
  public static List<string> GetEMailFields(System.Type table)
  {
    DacMetadata.InitializationCompleted.Wait();
    List<string> stringList;
    return PXDBEmailAttribute.FieldList.TryGetValue(table, out stringList) ? stringList : new List<string>();
  }

  /// <exclude />
  public static string FormatAddressesWithoutDisplayName(string addresses)
  {
    return string.Join("; ", PXDBEmailAttribute.ParseAddressesList(addresses).Select<MailAddress, string>((Func<MailAddress, string>) (_ => _.Address))).TrimEnd(' ');
  }

  /// <exclude />
  public static string FormatAddressesWithSingleDisplayName(string addresses, string displayName)
  {
    return string.Join<MailAddress>("; ", PXDBEmailAttribute.ParseAddressesList(addresses).Select<MailAddress, MailAddress>((Func<MailAddress, MailAddress>) (_ => new MailAddress(_.Address, displayName)))).TrimEnd(' ');
  }

  /// <exclude />
  public static string AppendAddresses(string addresses1, string addresses2)
  {
    return string.Join<MailAddress>("; ", PXDBEmailAttribute.ParseAddressesList(addresses1).Union<MailAddress>((IEnumerable<MailAddress>) PXDBEmailAttribute.ParseAddressesList(addresses2)));
  }

  /// <exclude />
  public static string ToString(IEnumerable<MailAddress> addresses)
  {
    return string.Join<MailAddress>("; ", addresses);
  }

  /// <exclude />
  public static string ToRFC(string addresses)
  {
    return string.Join<MailAddress>("; ", (IEnumerable<MailAddress>) PXDBEmailAttribute.ParseAddressesList(addresses));
  }

  /// <exclude />
  public static string Distinct(string addresses)
  {
    List<MailAddress> addresses1 = new List<MailAddress>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (MailAddress address in EmailParser.ParseAddresses(addresses))
    {
      if (!stringSet.Contains(address.Address))
      {
        stringSet.Add(address.Address);
        addresses1.Add(address);
      }
    }
    return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) addresses1);
  }

  private static List<MailAddress> ParseAddressesList(string addresses)
  {
    if (string.IsNullOrWhiteSpace(addresses))
      return new List<MailAddress>();
    List<MailAddress> addresses1;
    try
    {
      addresses1 = EmailParser.ParseAddresses(addresses);
    }
    catch (ArgumentException ex)
    {
      throw new PXSetPropertyException("The email address {0} is invalid.", new object[1]
      {
        (object) addresses
      });
    }
    return addresses1 != null && addresses1.Any<MailAddress>() ? addresses1 : throw new PXSetPropertyException("The email address {0} is invalid.", new object[1]
    {
      (object) addresses
    });
  }

  /// <exclude />
  public interface IEmailValidator
  {
    bool Validate(string email);
  }

  /// <exclude />
  public class DotNetEmailValidator : PXDBEmailAttribute.IEmailValidator
  {
    public bool Validate(string email)
    {
      try
      {
        MailAddress mailAddress = new MailAddress(email);
        return true;
      }
      catch (FormatException ex)
      {
        return false;
      }
    }
  }
}
