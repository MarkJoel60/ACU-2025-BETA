// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>
/// Saves messages declared in derived class into internal dictionary and performs search of specific message
/// when it is required. Also calls localization method (from PXLocalizer class) if the message is found
/// which retrieves localized string from dictionary. Each message is stored by the PXMessages class
/// as a triplet in which human-readable message is a key, fully qualified name of message and message
/// number (generated automatically) are value.
/// For example, {No idfield found: {CountryIdFieldNotFound, 24}}. Here,
/// "No idfield found" is a culture-independent human-readable message (key),
/// "PX_Data_ErrorMessages_CountryIdFieldNotFound" is a fully qualified name of message in PXMessages-derived
/// class (which will be used as a key for string in resource-file), "24" is a mesage number
/// (in order of declaration inside of derived class).
/// </summary>
public class PXMessages
{
  private static Dictionary<string, List<PXMessages.DictVal>> MsgDicts = new Dictionary<string, List<PXMessages.DictVal>>();
  private static Dictionary<string, string> MessagePrefixes = new Dictionary<string, string>();
  private static int _isCollected = 0;

  /// <summary>
  /// Passes through all loaded assemblies, looks for PXMessages-derived classes and adds messages from them to internal dictionaries.
  /// Messages from each PXMessages-derived class are stored in their own separate dictionary.
  /// </summary>
  internal static void CollectMessages(ILogger logger)
  {
    logger = logger.ForContext<PXMessages>();
    if (Interlocked.Exchange(ref PXMessages._isCollected, 1) == 1)
    {
      logger.WithStack().Warning("Collection of PXMessages called while messages have been already collected ");
    }
    else
    {
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (CollectMessages)))
      {
        if (enumTypesInAssembly != (System.Type) null && enumTypesInAssembly.IsDefined(typeof (PXLocalizableAttribute), false))
        {
          PXMessages.AddMessagesToDictionary(enumTypesInAssembly);
          PXSubstManager.AddTypeToNamedList(nameof (CollectMessages), enumTypesInAssembly);
        }
      }
      PXSubstManager.SaveTypeCache(nameof (CollectMessages));
    }
  }

  /// <summary>
  /// Adds messages from specific PXMessages-derived class to their own dictionary
  /// </summary>
  /// <param name="messagesType">Type of PXMessages-derived class to add messages from</param>
  private static void AddMessagesToDictionary(System.Type messagesType)
  {
    uint msgNumber = 1;
    foreach (System.Reflection.FieldInfo field in messagesType.GetFields(BindingFlags.Static | BindingFlags.Public))
    {
      string key = (string) field.GetValue((object) null);
      List<PXMessages.DictVal> dictValList = (List<PXMessages.DictVal>) null;
      if (!PXMessages.MsgDicts.TryGetValue(key, out dictValList))
      {
        dictValList = new List<PXMessages.DictVal>();
        PXMessages.MsgDicts[key] = dictValList;
      }
      dictValList.Add(new PXMessages.DictVal(messagesType, field.Name, msgNumber));
      ++msgNumber;
    }
  }

  private static string Localize(PXMessages.SingleMessage msg)
  {
    string messageName = msg.MessageName;
    string str = msg.GlobalMessage;
    for (int index = 0; index < msg.MessageClassTypeNames.Length; ++index)
    {
      str = PXLocalizer.Localize(msg.GlobalMessage, msg.MessageClassTypeNames[index]);
      if (str != msg.GlobalMessage)
        break;
    }
    return str;
  }

  internal static void ClearMessagePrefixes() => PXMessages.SingleMessage.ClearMessagePrefixes();

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <returns>A string representing localized message with prefix or strMessage if local resource can't be found</returns>
  public static string Localize(string strMessage)
  {
    PXMessages.SingleMessage msg = new PXMessages.SingleMessage(strMessage);
    if (msg.GlobalMessage == null)
      return strMessage;
    string str = PXMessages.Localize(msg);
    return msg.MessagePrefix == null || msg.MessagePrefix == string.Empty ? str : $"{msg.MessagePrefix}: {str}";
  }

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessageNumber">Will contain message number after method finishes</param>
  /// <returns>A string representing localized message with prefix or strMessage if local resource can't be found</returns>
  [Obsolete]
  public static string Localize(string strMessage, out uint MessageNumber)
  {
    MessageNumber = 0U;
    return PXMessages.Localize(strMessage);
  }

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessagePrefix">Will contain message prefix after method finishes</param>
  /// <returns>A string representing localized message or strMessage if local resource can't be found</returns>
  public static string Localize(string strMessage, out string MessagePrefix)
  {
    PXMessages.SingleMessage msg = new PXMessages.SingleMessage(strMessage);
    MessagePrefix = msg.MessagePrefix;
    return msg.GlobalMessage == null ? strMessage : PXMessages.Localize(msg);
  }

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessageNumber">Will contain message number after method finishes</param>
  /// <param name="MessagePrefix">Will contain message prefix after method finishes</param>
  /// <returns>A string representing localized message or strMessage if local resource can't be found</returns>
  [Obsolete("Use \"PX.Data.PXMessages.Localize(string strMessage, out string MessagePrefix)\" instead")]
  public static string Localize(
    string strMessage,
    out uint MessageNumber,
    out string MessagePrefix)
  {
    MessageNumber = 0U;
    return PXMessages.Localize(strMessage, out MessagePrefix);
  }

  /// <exclude />
  public static string LocalizeNoPrefix(string strMessage)
  {
    PXMessages.SingleMessage msg = new PXMessages.SingleMessage(strMessage);
    return msg.GlobalMessage == null ? PXLocalizer.EscapeString(strMessage) : PXMessages.Localize(msg);
  }

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <returns>A string representing message number and localized message with prefix or strMessage if local resource can't be found</returns>
  [Obsolete]
  public static string LocalizeAddNumber(string strMessage) => PXMessages.Localize(strMessage);

  /// <summary>Localizes a message</summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessagePrefix">Will contain message prefix after method finishes</param>
  /// <returns>A string representing message number and localized message or strMessage if local resource can't be found</returns>
  [Obsolete("Use \"PX.Data.PXMessages.Localize(string strMessage, out string MessagePrefix)\" instead")]
  public static string LocalizeAddNumber(string strMessage, out string MessagePrefix)
  {
    return PXMessages.Localize(strMessage, out MessagePrefix);
  }

  /// <summary>
  /// Localizes a message and formats it like String.Format does
  /// </summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessageNumber">Will contain message number after method finishes</param>
  /// <param name="args">An Object array containing zero or more objects to format</param>
  /// <returns>A formatted string representing localized message or strMessage if local resource can't be found</returns>
  [Obsolete]
  public static string LocalizeFormat(
    string strMessage,
    out uint MessageNumber,
    params object[] args)
  {
    MessageNumber = 0U;
    return PXMessages.LocalizeFormat(strMessage, args);
  }

  /// <summary>
  /// Localizes a message and formats it like String.Format does
  /// </summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="args">An Object array containing zero or more objects to format</param>
  /// <returns>A formatted string representing localized message or strMessage if local resource can't be found</returns>
  public static string LocalizeFormat(string strMessage, params object[] args)
  {
    if (args == null)
      return PXMessages.Localize(strMessage);
    object[] objArray = new object[args.Length];
    args.CopyTo((Array) objArray, 0);
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is string)
        objArray[index] = (object) PXMessages.LocalizeNoPrefix((string) objArray[index]);
    }
    string format = PXMessages.Localize(strMessage);
    if (format != null)
      format = string.Format(format, objArray);
    return format;
  }

  /// <summary>
  /// Localizes a message and formats it like String.Format does
  /// </summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessagePrefix">Will contain message prefix after method finishes</param>
  /// <param name="args">An Object array containing zero or more objects to format</param>
  /// <returns>A formatted string representing localized message or strMessage if local resource can't be found</returns>
  public static string LocalizeFormat(
    string strMessage,
    out string MessagePrefix,
    params object[] args)
  {
    object[] objArray = new object[args.Length];
    args.CopyTo((Array) objArray, 0);
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is string)
        objArray[index] = (object) PXMessages.LocalizeNoPrefix((string) objArray[index]);
    }
    string format = PXMessages.Localize(strMessage, out MessagePrefix);
    if (format != null)
      format = string.Format(format, objArray);
    return format;
  }

  /// <summary>
  /// Localizes a message and formats it like String.Format does
  /// </summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="MessageNumber">Will contain message number after method finishes</param>
  /// <param name="MessagePrefix">Will contain message prefix after method finishes</param>
  /// <param name="args">An Object array containing zero or more objects to format</param>
  /// <returns>A formatted string representing localized message or strMessage if local resource can't be found</returns>
  [Obsolete("Use \"PX.Data.PXMessages.LocalizeFormat(string strMessage, out string MessagePrefix, params object[] args)\" instead")]
  public static string LocalizeFormat(
    string strMessage,
    out uint MessageNumber,
    out string MessagePrefix,
    params object[] args)
  {
    MessageNumber = 0U;
    return PXMessages.LocalizeFormat(strMessage, out MessagePrefix, args);
  }

  /// <exclude />
  public static string LocalizeFormatNoPrefix(string strMessage, params object[] args)
  {
    object[] objArray = new object[args.Length];
    args.CopyTo((Array) objArray, 0);
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is string)
        objArray[index] = (object) PXMessages.LocalizeNoPrefix((string) objArray[index]);
    }
    return string.Format(PXMessages.LocalizeNoPrefix(strMessage), objArray);
  }

  /// <exclude />
  public static string LocalizeFormatNoPrefixNLA(string strMessage, params object[] args)
  {
    return string.Format(PXMessages.LocalizeNoPrefix(strMessage), args);
  }

  /// <summary>
  /// Localizes a message and formats it like String.Format does
  /// </summary>
  /// <param name="strMessage">A culture-independent human-readable message to localize</param>
  /// <param name="args">An Object array containing zero or more objects to format</param>
  /// <returns>A formatted string representing message number and localized message or strMessage if local resource can't be found</returns>
  [Obsolete]
  public static string LocalizeFormatAddNumber(string strMessage, params object[] args)
  {
    if (args == null)
      return PXMessages.LocalizeAddNumber(strMessage);
    object[] objArray = new object[args.Length];
    args.CopyTo((Array) objArray, 0);
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is string)
        objArray[index] = (object) PXMessages.LocalizeNoPrefix((string) objArray[index]);
    }
    return string.Format(PXMessages.LocalizeAddNumber(strMessage), objArray);
  }

  [Obsolete("Use \"PX.Data.PXMessages.LocalizeFormat(string strMessage, out string MessagePrefix, params object[] args)\" instead")]
  public static string LocalizeFormatAddNumber(
    string strMessage,
    out string MessagePrefix,
    params object[] args)
  {
    return PXMessages.LocalizeFormat(strMessage, out MessagePrefix, args);
  }

  /// <exclude />
  private class DictVal
  {
    public string messageClassTypeName;
    public string messagePrefix;
    public string messageName;
    public uint messageNumber;

    public DictVal()
    {
    }

    public DictVal(System.Type msgClassType, string msgName, uint msgNumber)
    {
      this.messageClassTypeName = msgClassType.FullName;
      this.messagePrefix = ((PXLocalizableAttribute) msgClassType.GetCustomAttributes(typeof (PXLocalizableAttribute), false).GetValue(0)).MessagePrefix;
      this.messageName = msgName;
      this.messageNumber = msgNumber;
    }
  }

  /// <exclude />
  private class SingleMessage
  {
    private string globalMessage;
    private string[] messageClassTypeNames;
    private string messageName;
    private uint messageNumber;
    private string messagePrefix;
    private static ReaderWriterLock rwLock = new ReaderWriterLock();

    /// <summary>
    /// Looks for specific culture-independent message in interanl dictionaries
    /// </summary>
    /// <param name="strMessage">Message to look for</param>
    /// <param name="msgpair">A pair representing fully-qulified message name and its number if message is found</param>
    /// <returns>true if message is found, otherwise returns false</returns>
    private bool SeekMessageInDicts(string strMessage, out List<PXMessages.DictVal> msginfo)
    {
      return PXMessages.MsgDicts.TryGetValue(strMessage, out msginfo);
    }

    private string[] GetClassNames(List<PXMessages.DictVal> msginfo)
    {
      string[] classNames = new string[msginfo.Count];
      for (int index = 0; index < msginfo.Count; ++index)
        classNames[index] = msginfo[index].messageClassTypeName;
      return classNames;
    }

    internal static void ClearMessagePrefixes()
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXMessages.SingleMessage.rwLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
        PXMessages.MessagePrefixes.Clear();
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }

    /// <summary>
    /// Looks for specific message in internal dictionaries and creates a single message triplet for it if found
    /// </summary>
    /// <param name="strMessage">Message to look for</param>
    public SingleMessage(string strMessage)
    {
      int num = PXContext.GetSlot<List<PXDatabaseProviderBase.RequiredSubscriber>>() != null ? 1 : 0;
      List<PXMessages.DictVal> msginfo = (List<PXMessages.DictVal>) null;
      if (num != 0 || strMessage == null || !this.SeekMessageInDicts(strMessage, out msginfo) || PXInvariantCultureScope.IsSet())
        return;
      this.globalMessage = strMessage;
      this.messageClassTypeNames = this.GetClassNames(msginfo);
      this.messageName = msginfo[0].messageName;
      this.messageNumber = msginfo[0].messageNumber;
      string str = (string) null;
      string name = Thread.CurrentThread.CurrentCulture.Name;
      for (int index = 0; index < msginfo.Count; ++index)
      {
        PXReaderWriterScope readerWriterScope;
        // ISSUE: explicit constructor call
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXMessages.SingleMessage.rwLock);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          string key = this.messageClassTypeNames[index] + name;
          if (PXMessages.MessagePrefixes.ContainsKey(key))
          {
            this.messagePrefix = PXMessages.MessagePrefixes[key];
            if (string.IsNullOrEmpty(str))
              str = this.messagePrefix;
          }
          else
          {
            ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
            if (PXMessages.MessagePrefixes.ContainsKey(key))
            {
              this.messagePrefix = PXMessages.MessagePrefixes[key];
              if (string.IsNullOrEmpty(str))
                str = this.messagePrefix;
            }
            else
            {
              PXMessages.MessagePrefixes.Add(key, (string) null);
              this.messagePrefix = PXMessages.LocalizeNoPrefix(msginfo[index].messagePrefix);
              PXMessages.MessagePrefixes[key] = this.messagePrefix;
              if (string.IsNullOrEmpty(str))
                str = this.messagePrefix;
            }
          }
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
      this.messagePrefix = str;
    }

    /// <summary>
    /// Gets a human-readable culture-independent message (as it is declared in PXMessages-derived class)
    /// </summary>
    public string GlobalMessage => this.globalMessage;

    /// <summary>
    /// Gets fully-qualified names of PXMessages-derived class in which message is declared
    /// </summary>
    public string[] MessageClassTypeNames => this.messageClassTypeNames;

    /// <summary>
    /// Gets name of message variable as it is declared in PXMessages-derived class
    /// </summary>
    public string MessageName => this.messageName;

    /// <summary>Gets message number</summary>
    public uint MessageNumber => this.messageNumber;

    /// <summary>
    /// Gets or sets message prefix (a string that will be displayed before each message, defined by PXLocalizable)
    /// </summary>
    public string MessagePrefix
    {
      get => this.messagePrefix;
      set => this.messagePrefix = value;
    }
  }
}
